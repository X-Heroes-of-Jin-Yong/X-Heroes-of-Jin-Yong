using System;
using System.Collections.Generic;
using System.Reflection;

namespace LuaInterface
{
	internal class LuaMethodWrapper
	{
		private ObjectTranslator _Translator;

		private MethodBase _Method;

		private MethodCache _LastCalledMethod = default(MethodCache);

		private string _MethodName;

		private MemberInfo[] _Members;

		public IReflect _TargetType;

		private ExtractValue _ExtractTarget;

		private object _Target;

		private BindingFlags _BindingType;

		public LuaMethodWrapper(ObjectTranslator translator, object target, IReflect targetType, MethodBase method)
		{
			_Translator = translator;
			_Target = target;
			_TargetType = targetType;
			if (targetType != null)
			{
				_ExtractTarget = translator.typeChecker.getExtractor(targetType);
			}
			_Method = method;
			_MethodName = method.Name;
			if (method.IsStatic)
			{
				_BindingType = BindingFlags.Static;
			}
			else
			{
				_BindingType = BindingFlags.Instance;
			}
		}

		public LuaMethodWrapper(ObjectTranslator translator, IReflect targetType, string methodName, BindingFlags bindingType)
		{
			_Translator = translator;
			_MethodName = methodName;
			_TargetType = targetType;
			if (targetType != null)
			{
				_ExtractTarget = translator.typeChecker.getExtractor(targetType);
			}
			_BindingType = bindingType;
			_Members = targetType.UnderlyingSystemType.GetMember(methodName, MemberTypes.Method, bindingType | BindingFlags.Public | BindingFlags.IgnoreCase);
		}

		private int SetPendingException(Exception e)
		{
			return _Translator.interpreter.SetPendingException(e);
		}

		private static bool IsInteger(double x)
		{
			return Math.Ceiling(x) == x;
		}

		private void ClearCachedArgs()
		{
			if (_LastCalledMethod.args != null)
			{
				for (int i = 0; i < _LastCalledMethod.args.Length; i++)
				{
					_LastCalledMethod.args[i] = null;
				}
			}
		}

		public int call(IntPtr luaState)
		{
			MethodBase method = _Method;
			object obj = _Target;
			bool flag = true;
			int num = 0;
			if (!LuaDLL.lua_checkstack(luaState, 5))
			{
				throw new LuaException("Lua stack overflow");
			}
			bool flag2 = (_BindingType & BindingFlags.Static) == BindingFlags.Static;
			SetPendingException(null);
			if (method == null)
			{
				obj = ((!flag2) ? _ExtractTarget(luaState, 1) : null);
				if (_LastCalledMethod.cachedMethod != null)
				{
					int num2 = ((!flag2) ? 1 : 0);
					int num3 = LuaDLL.lua_gettop(luaState) - num2;
					MethodBase cachedMethod = _LastCalledMethod.cachedMethod;
					if (num3 == _LastCalledMethod.argTypes.Length)
					{
						if (!LuaDLL.lua_checkstack(luaState, _LastCalledMethod.outList.Length + 6))
						{
							throw new LuaException("Lua stack overflow");
						}
						object[] args = _LastCalledMethod.args;
						try
						{
							for (int i = 0; i < _LastCalledMethod.argTypes.Length; i++)
							{
								MethodArgs methodArgs = _LastCalledMethod.argTypes[i];
								object obj2 = methodArgs.extractValue(luaState, i + 1 + num2);
								if (_LastCalledMethod.argTypes[i].isParamsArray)
								{
									args[methodArgs.index] = _Translator.tableToArray(obj2, methodArgs.paramsArrayType);
								}
								else
								{
									args[methodArgs.index] = obj2;
								}
								if (args[methodArgs.index] == null && !LuaDLL.lua_isnil(luaState, i + 1 + num2))
								{
									throw new LuaException("argument number " + (i + 1) + " is invalid");
								}
							}
							if ((_BindingType & BindingFlags.Static) == BindingFlags.Static)
							{
								_Translator.push(luaState, cachedMethod.Invoke(null, args));
							}
							else if (_LastCalledMethod.cachedMethod.IsConstructor)
							{
								_Translator.push(luaState, ((ConstructorInfo)cachedMethod).Invoke(args));
							}
							else
							{
								_Translator.push(luaState, cachedMethod.Invoke(obj, args));
							}
							flag = false;
						}
						catch (TargetInvocationException ex)
						{
							return SetPendingException(ex.GetBaseException());
						}
						catch (Exception pendingException)
						{
							if (_Members.Length == 1)
							{
								return SetPendingException(pendingException);
							}
						}
					}
				}
				if (flag)
				{
					if (!flag2)
					{
						if (obj == null)
						{
							_Translator.throwError(luaState, string.Format("instance method '{0}' requires a non null target object", _MethodName));
							LuaDLL.lua_pushnil(luaState);
							return 1;
						}
						LuaDLL.lua_remove(luaState, 1);
					}
					bool flag3 = false;
					string text = null;
					MemberInfo[] members = _Members;
					foreach (MemberInfo memberInfo in members)
					{
						text = memberInfo.ReflectedType.Name + "." + memberInfo.Name;
						MethodBase method2 = (MethodInfo)memberInfo;
						if (_Translator.matchParameters(luaState, method2, ref _LastCalledMethod))
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						string message = ((text != null) ? ("invalid arguments to method: " + text) : "invalid arguments to method call");
						LuaDLL.luaL_error(luaState, message);
						LuaDLL.lua_pushnil(luaState);
						ClearCachedArgs();
						return 1;
					}
				}
			}
			else if (method.ContainsGenericParameters)
			{
				_Translator.matchParameters(luaState, method, ref _LastCalledMethod);
				if (method.IsGenericMethodDefinition)
				{
					List<Type> list = new List<Type>();
					object[] args2 = _LastCalledMethod.args;
					foreach (object obj3 in args2)
					{
						list.Add(obj3.GetType());
					}
					MethodInfo methodInfo = (method as MethodInfo).MakeGenericMethod(list.ToArray());
					_Translator.push(luaState, methodInfo.Invoke(obj, _LastCalledMethod.args));
					flag = false;
				}
				else if (method.ContainsGenericParameters)
				{
					LuaDLL.luaL_error(luaState, "unable to invoke method on generic class as the current method is an open generic method");
					LuaDLL.lua_pushnil(luaState);
					ClearCachedArgs();
					return 1;
				}
			}
			else
			{
				if (!method.IsStatic && !method.IsConstructor && obj == null)
				{
					obj = _ExtractTarget(luaState, 1);
					LuaDLL.lua_remove(luaState, 1);
				}
				if (!_Translator.matchParameters(luaState, method, ref _LastCalledMethod))
				{
					LuaDLL.luaL_error(luaState, "invalid arguments to method call");
					LuaDLL.lua_pushnil(luaState);
					ClearCachedArgs();
					return 1;
				}
			}
			if (flag)
			{
				if (!LuaDLL.lua_checkstack(luaState, _LastCalledMethod.outList.Length + 6))
				{
					ClearCachedArgs();
					throw new LuaException("Lua stack overflow");
				}
				try
				{
					if (flag2)
					{
						_Translator.push(luaState, _LastCalledMethod.cachedMethod.Invoke(null, _LastCalledMethod.args));
					}
					else if (_LastCalledMethod.cachedMethod.IsConstructor)
					{
						_Translator.push(luaState, ((ConstructorInfo)_LastCalledMethod.cachedMethod).Invoke(_LastCalledMethod.args));
					}
					else
					{
						_Translator.push(luaState, _LastCalledMethod.cachedMethod.Invoke(obj, _LastCalledMethod.args));
					}
				}
				catch (TargetInvocationException ex2)
				{
					ClearCachedArgs();
					return SetPendingException(ex2.GetBaseException());
				}
				catch (Exception pendingException2)
				{
					ClearCachedArgs();
					return SetPendingException(pendingException2);
				}
			}
			for (int l = 0; l < _LastCalledMethod.outList.Length; l++)
			{
				num++;
				_Translator.push(luaState, _LastCalledMethod.args[_LastCalledMethod.outList[l]]);
			}
			if (!_LastCalledMethod.IsReturnVoid && num > 0)
			{
				num++;
			}
			ClearCachedArgs();
			return (num < 1) ? 1 : num;
		}
	}
}
