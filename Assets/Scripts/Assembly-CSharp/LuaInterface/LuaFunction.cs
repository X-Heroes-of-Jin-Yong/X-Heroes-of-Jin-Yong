using System;

namespace LuaInterface
{
	public class LuaFunction : LuaBase
	{
		internal LuaCSFunction function;

		private IntPtr L;

		private int beginPos = -1;

		public LuaFunction(int reference, LuaState interpreter)
		{
			_Reference = reference;
			function = null;
			_Interpreter = interpreter;
			L = _Interpreter.L;
			translator = _Interpreter.translator;
		}

		public LuaFunction(LuaCSFunction function, LuaState interpreter)
		{
			_Reference = 0;
			this.function = function;
			_Interpreter = interpreter;
			L = _Interpreter.L;
			translator = _Interpreter.translator;
		}

		public LuaFunction(int reference, IntPtr l)
		{
			_Reference = reference;
			function = null;
			L = l;
			translator = ObjectTranslator.FromState(L);
			_Interpreter = translator.interpreter;
		}

		internal object[] call(object[] args, Type[] returnTypes)
		{
			int num = 0;
			LuaScriptMgr.PushTraceBack(L);
			int num2 = LuaDLL.lua_gettop(L);
			if (!LuaDLL.lua_checkstack(L, args.Length + 6))
			{
				LuaDLL.lua_pop(L, 1);
				throw new LuaException("Lua stack overflow");
			}
			push(L);
			if (args != null)
			{
				num = args.Length;
				for (int i = 0; i < args.Length; i++)
				{
					PushArgs(L, args[i]);
				}
			}
			if (LuaDLL.lua_pcall(L, num, -1, -num - 2) != 0)
			{
				string text = LuaDLL.lua_tostring(L, -1);
				LuaDLL.lua_settop(L, num2 - 1);
				if (text == null)
				{
					text = "Unknown Lua Error";
				}
				throw new LuaScriptException(text, string.Empty);
			}
			object[] result = ((returnTypes == null) ? translator.popValues(L, num2) : translator.popValues(L, num2, returnTypes));
			LuaDLL.lua_settop(L, num2 - 1);
			return result;
		}

		public object[] Call(params object[] args)
		{
			return call(args, null);
		}

		public object[] Call()
		{
			int num = BeginPCall();
			if (PCall(num, 0))
			{
				object[] result = PopValues(num);
				EndPCall(num);
				return result;
			}
			LuaDLL.lua_settop(L, num - 1);
			return null;
		}

		public object[] Call(double arg1)
		{
			int num = BeginPCall();
			LuaScriptMgr.Push(L, arg1);
			if (PCall(num, 1))
			{
				object[] result = PopValues(num);
				EndPCall(num);
				return result;
			}
			LuaDLL.lua_settop(L, num - 1);
			return null;
		}

		public int BeginPCall()
		{
			LuaScriptMgr.PushTraceBack(L);
			beginPos = LuaDLL.lua_gettop(L);
			push(L);
			return beginPos;
		}

		public bool PCall(int oldTop, int args)
		{
			if (LuaDLL.lua_pcall(L, args, -1, -args - 2) != 0)
			{
				string text = LuaDLL.lua_tostring(L, -1);
				LuaDLL.lua_settop(L, oldTop - 1);
				if (text == null)
				{
					text = "Unknown Lua Error";
				}
				throw new LuaScriptException(text, string.Empty);
			}
			return true;
		}

		public object[] PopValues(int oldTop)
		{
			return translator.popValues(L, oldTop);
		}

		public void EndPCall(int oldTop)
		{
			LuaDLL.lua_settop(L, oldTop - 1);
		}

		public IntPtr GetLuaState()
		{
			return L;
		}

		internal void push(IntPtr luaState)
		{
			if (_Reference != 0)
			{
				LuaDLL.lua_getref(luaState, _Reference);
			}
			else
			{
				_Interpreter.pushCSFunction(function);
			}
		}

		internal void push()
		{
			if (_Reference != 0)
			{
				LuaDLL.lua_getref(L, _Reference);
			}
			else
			{
				_Interpreter.pushCSFunction(function);
			}
		}

		public override string ToString()
		{
			return "function";
		}

		public override bool Equals(object o)
		{
			if (o is LuaFunction)
			{
				LuaFunction luaFunction = (LuaFunction)o;
				if (_Reference != 0 && luaFunction._Reference != 0)
				{
					return _Interpreter.compareRef(luaFunction._Reference, _Reference);
				}
				return function == luaFunction.function;
			}
			return false;
		}

		public override int GetHashCode()
		{
			if (_Reference != 0)
			{
				return _Reference;
			}
			return function.GetHashCode();
		}

		public int GetReference()
		{
			return _Reference;
		}
	}
}
