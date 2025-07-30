using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LuaInterface
{
	internal class CheckType
	{
		private ObjectTranslator translator;

		private ExtractValue extractNetObject;

		private Dictionary<long, ExtractValue> extractValues = new Dictionary<long, ExtractValue>();

		public CheckType(ObjectTranslator translator)
		{
			this.translator = translator;
			extractValues.Add(typeof(object).TypeHandle.Value.ToInt64(), getAsObject);
			extractValues.Add(typeof(sbyte).TypeHandle.Value.ToInt64(), getAsSbyte);
			extractValues.Add(typeof(byte).TypeHandle.Value.ToInt64(), getAsByte);
			extractValues.Add(typeof(short).TypeHandle.Value.ToInt64(), getAsShort);
			extractValues.Add(typeof(ushort).TypeHandle.Value.ToInt64(), getAsUshort);
			extractValues.Add(typeof(int).TypeHandle.Value.ToInt64(), getAsInt);
			extractValues.Add(typeof(uint).TypeHandle.Value.ToInt64(), getAsUint);
			extractValues.Add(typeof(long).TypeHandle.Value.ToInt64(), getAsLong);
			extractValues.Add(typeof(ulong).TypeHandle.Value.ToInt64(), getAsUlong);
			extractValues.Add(typeof(double).TypeHandle.Value.ToInt64(), getAsDouble);
			extractValues.Add(typeof(char).TypeHandle.Value.ToInt64(), getAsChar);
			extractValues.Add(typeof(float).TypeHandle.Value.ToInt64(), getAsFloat);
			extractValues.Add(typeof(decimal).TypeHandle.Value.ToInt64(), getAsDecimal);
			extractValues.Add(typeof(bool).TypeHandle.Value.ToInt64(), getAsBoolean);
			extractValues.Add(typeof(string).TypeHandle.Value.ToInt64(), getAsString);
			extractValues.Add(typeof(LuaFunction).TypeHandle.Value.ToInt64(), getAsFunction);
			extractValues.Add(typeof(LuaTable).TypeHandle.Value.ToInt64(), getAsTable);
			extractValues.Add(typeof(Vector3).TypeHandle.Value.ToInt64(), getAsVector3);
			extractValues.Add(typeof(Vector2).TypeHandle.Value.ToInt64(), getAsVector2);
			extractValues.Add(typeof(Quaternion).TypeHandle.Value.ToInt64(), getAsQuaternion);
			extractValues.Add(typeof(Color).TypeHandle.Value.ToInt64(), getAsColor);
			extractValues.Add(typeof(Vector4).TypeHandle.Value.ToInt64(), getAsVector4);
			extractValues.Add(typeof(Ray).TypeHandle.Value.ToInt64(), getAsRay);
			extractValues.Add(typeof(Bounds).TypeHandle.Value.ToInt64(), getAsBounds);
			extractNetObject = getAsNetObject;
		}

		internal ExtractValue getExtractor(IReflect paramType)
		{
			return getExtractor(paramType.UnderlyingSystemType);
		}

		internal ExtractValue getExtractor(Type paramType)
		{
			if (paramType.IsByRef)
			{
				paramType = paramType.GetElementType();
			}
			long key = paramType.TypeHandle.Value.ToInt64();
			if (extractValues.ContainsKey(key))
			{
				return extractValues[key];
			}
			return extractNetObject;
		}

		internal ExtractValue checkType(IntPtr luaState, int stackPos, Type paramType)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(luaState, stackPos);
			if (paramType.IsByRef)
			{
				paramType = paramType.GetElementType();
			}
			Type underlyingType = Nullable.GetUnderlyingType(paramType);
			if (underlyingType != null)
			{
				paramType = underlyingType;
			}
			long key = paramType.TypeHandle.Value.ToInt64();
			if (paramType.Equals(typeof(object)))
			{
				return extractValues[key];
			}
			if (paramType.IsGenericParameter)
			{
				switch (luaTypes)
				{
				case LuaTypes.LUA_TBOOLEAN:
					return extractValues[typeof(bool).TypeHandle.Value.ToInt64()];
				case LuaTypes.LUA_TSTRING:
					return extractValues[typeof(string).TypeHandle.Value.ToInt64()];
				case LuaTypes.LUA_TTABLE:
					return extractValues[typeof(LuaTable).TypeHandle.Value.ToInt64()];
				case LuaTypes.LUA_TUSERDATA:
					return extractValues[typeof(object).TypeHandle.Value.ToInt64()];
				case LuaTypes.LUA_TFUNCTION:
					return extractValues[typeof(LuaFunction).TypeHandle.Value.ToInt64()];
				case LuaTypes.LUA_TNUMBER:
					return extractValues[typeof(double).TypeHandle.Value.ToInt64()];
				}
			}
			if (paramType.IsValueType && luaTypes == LuaTypes.LUA_TTABLE)
			{
				int newTop = LuaDLL.lua_gettop(luaState);
				ExtractValue extractValue = null;
				LuaDLL.lua_pushvalue(luaState, stackPos);
				LuaDLL.lua_pushstring(luaState, "class");
				LuaDLL.lua_gettable(luaState, -2);
				if (!LuaDLL.lua_isnil(luaState, -1))
				{
					string text = LuaDLL.lua_tostring(luaState, -1);
					extractValue = ((text == "Vector3" && paramType == typeof(Vector3)) ? extractValues[typeof(Vector3).TypeHandle.Value.ToInt64()] : ((text == "Vector2" && paramType == typeof(Vector2)) ? extractValues[typeof(Vector2).TypeHandle.Value.ToInt64()] : ((text == "Quaternion" && paramType == typeof(Quaternion)) ? extractValues[typeof(Quaternion).TypeHandle.Value.ToInt64()] : ((text == "Color" && paramType == typeof(Color)) ? extractValues[typeof(Color).TypeHandle.Value.ToInt64()] : ((text == "Vector4" && paramType == typeof(Vector4)) ? extractValues[typeof(Vector4).TypeHandle.Value.ToInt64()] : ((!(text == "Ray") || paramType != typeof(Ray)) ? null : extractValues[typeof(Ray).TypeHandle.Value.ToInt64()]))))));
				}
				LuaDLL.lua_settop(luaState, newTop);
				if (extractValue != null)
				{
					return extractValue;
				}
			}
			if (LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return extractValues[key];
			}
			if (paramType == typeof(bool))
			{
				if (LuaDLL.lua_isboolean(luaState, stackPos))
				{
					return extractValues[key];
				}
			}
			else if (paramType == typeof(string))
			{
				if (LuaDLL.lua_isstring(luaState, stackPos))
				{
					return extractValues[key];
				}
				if (luaTypes == LuaTypes.LUA_TNIL)
				{
					return extractNetObject;
				}
			}
			else if (paramType == typeof(LuaTable))
			{
				if (luaTypes == LuaTypes.LUA_TTABLE)
				{
					return extractValues[key];
				}
			}
			else if (paramType == typeof(LuaFunction))
			{
				if (luaTypes == LuaTypes.LUA_TFUNCTION)
				{
					return extractValues[key];
				}
			}
			else if (typeof(Delegate).IsAssignableFrom(paramType) && luaTypes == LuaTypes.LUA_TFUNCTION)
			{
				translator.throwError(luaState, "Delegates not implemnented");
			}
			else if (paramType.IsInterface && luaTypes == LuaTypes.LUA_TTABLE)
			{
				translator.throwError(luaState, "Interfaces not implemnented");
			}
			else
			{
				if ((paramType.IsInterface || paramType.IsClass) && luaTypes == LuaTypes.LUA_TNIL)
				{
					return extractNetObject;
				}
				if (LuaDLL.lua_type(luaState, stackPos) == LuaTypes.LUA_TTABLE)
				{
					if (LuaDLL.luaL_getmetafield(luaState, stackPos, "__index") != LuaTypes.LUA_TNIL)
					{
						object netObject = translator.getNetObject(luaState, -1);
						LuaDLL.lua_settop(luaState, -2);
						if (netObject != null && paramType.IsAssignableFrom(netObject.GetType()))
						{
							return extractNetObject;
						}
					}
				}
				else
				{
					object rawNetObject = translator.getRawNetObject(luaState, stackPos);
					if (rawNetObject != null && paramType.IsAssignableFrom(rawNetObject.GetType()))
					{
						return extractNetObject;
					}
				}
			}
			return null;
		}

		private object getAsSbyte(IntPtr luaState, int stackPos)
		{
			sbyte b = (sbyte)LuaDLL.lua_tonumber(luaState, stackPos);
			if (b == 0 && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return b;
		}

		private object getAsByte(IntPtr luaState, int stackPos)
		{
			byte b = (byte)LuaDLL.lua_tonumber(luaState, stackPos);
			if (b == 0 && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return b;
		}

		private object getAsShort(IntPtr luaState, int stackPos)
		{
			short num = (short)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0 && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsUshort(IntPtr luaState, int stackPos)
		{
			ushort num = (ushort)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0 && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsInt(IntPtr luaState, int stackPos)
		{
			int num = (int)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0 && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsUint(IntPtr luaState, int stackPos)
		{
			uint num = (uint)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0 && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsLong(IntPtr luaState, int stackPos)
		{
			long num = (long)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0L && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsUlong(IntPtr luaState, int stackPos)
		{
			ulong num = (ulong)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0L && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsDouble(IntPtr luaState, int stackPos)
		{
			double num = LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0.0 && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsChar(IntPtr luaState, int stackPos)
		{
			char c = (char)LuaDLL.lua_tonumber(luaState, stackPos);
			if (c == '\0' && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return c;
		}

		private object getAsFloat(IntPtr luaState, int stackPos)
		{
			float num = (float)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0f && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsDecimal(IntPtr luaState, int stackPos)
		{
			decimal num = (decimal)LuaDLL.lua_tonumber(luaState, stackPos);
			if (num == 0m && !LuaDLL.lua_isnumber(luaState, stackPos))
			{
				return null;
			}
			return num;
		}

		private object getAsBoolean(IntPtr luaState, int stackPos)
		{
			return LuaDLL.lua_toboolean(luaState, stackPos);
		}

		private object getAsString(IntPtr luaState, int stackPos)
		{
			string text = LuaDLL.lua_tostring(luaState, stackPos);
			if (text == string.Empty && !LuaDLL.lua_isstring(luaState, stackPos))
			{
				return null;
			}
			return text;
		}

		private object getAsTable(IntPtr luaState, int stackPos)
		{
			return translator.getTable(luaState, stackPos);
		}

		private object getAsFunction(IntPtr luaState, int stackPos)
		{
			return translator.getFunction(luaState, stackPos);
		}

		public object getAsObject(IntPtr luaState, int stackPos)
		{
			if (LuaDLL.lua_type(luaState, stackPos) == LuaTypes.LUA_TTABLE && LuaDLL.luaL_getmetafield(luaState, stackPos, "__index") != LuaTypes.LUA_TNIL)
			{
				if (LuaDLL.luaL_checkmetatable(luaState, -1))
				{
					LuaDLL.lua_insert(luaState, stackPos);
					LuaDLL.lua_remove(luaState, stackPos + 1);
				}
				else
				{
					LuaDLL.lua_settop(luaState, -2);
				}
			}
			return translator.getObject(luaState, stackPos);
		}

		public object getAsNetObject(IntPtr luaState, int stackPos)
		{
			object obj = translator.getRawNetObject(luaState, stackPos);
			if (obj == null && LuaDLL.lua_type(luaState, stackPos) == LuaTypes.LUA_TTABLE && LuaDLL.luaL_getmetafield(luaState, stackPos, "__index") != LuaTypes.LUA_TNIL)
			{
				if (LuaDLL.luaL_checkmetatable(luaState, -1))
				{
					LuaDLL.lua_insert(luaState, stackPos);
					LuaDLL.lua_remove(luaState, stackPos + 1);
					obj = translator.getNetObject(luaState, stackPos);
				}
				else
				{
					LuaDLL.lua_settop(luaState, -2);
				}
			}
			return obj;
		}

		public object getAsVector3(IntPtr L, int stackPos)
		{
			return LuaScriptMgr.GetVector3(L, stackPos);
		}

		public object getAsVector2(IntPtr L, int stackPos)
		{
			return LuaScriptMgr.GetVector2(L, stackPos);
		}

		public object getAsQuaternion(IntPtr L, int stackPos)
		{
			return LuaScriptMgr.GetQuaternion(L, stackPos);
		}

		public object getAsColor(IntPtr L, int stackPos)
		{
			return LuaScriptMgr.GetColor(L, stackPos);
		}

		public object getAsVector4(IntPtr L, int stackPos)
		{
			return LuaScriptMgr.GetVector4(L, stackPos);
		}

		public object getAsRay(IntPtr L, int stackPos)
		{
			return LuaScriptMgr.GetRay(L, stackPos);
		}

		public object getAsBounds(IntPtr L, int stackPos)
		{
			return LuaScriptMgr.GetBounds(L, stackPos);
		}
	}
}
