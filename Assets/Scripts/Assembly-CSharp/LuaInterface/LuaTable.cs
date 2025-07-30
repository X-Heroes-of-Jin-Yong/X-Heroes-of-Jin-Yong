using System;
using System.Collections;

namespace LuaInterface
{
	public class LuaTable : LuaBase
	{
		public object this[string field]
		{
			get
			{
				return _Interpreter.getObject(_Reference, field);
			}
			set
			{
				_Interpreter.setObject(_Reference, field, value);
			}
		}

		public object this[object field]
		{
			get
			{
				return _Interpreter.getObject(_Reference, field);
			}
			set
			{
				_Interpreter.setObject(_Reference, field, value);
			}
		}

		public int Count
		{
			get
			{
				return _Interpreter.GetTableDict(this).Count;
			}
		}

		public ICollection Keys
		{
			get
			{
				return _Interpreter.GetTableDict(this).Keys;
			}
		}

		public ICollection Values
		{
			get
			{
				return _Interpreter.GetTableDict(this).Values;
			}
		}

		public LuaTable(int reference, LuaState interpreter)
		{
			_Reference = reference;
			_Interpreter = interpreter;
			translator = interpreter.translator;
		}

		public LuaTable(int reference, IntPtr L)
		{
			_Reference = reference;
			translator = ObjectTranslator.FromState(L);
			_Interpreter = translator.interpreter;
		}

		public IDictionaryEnumerator GetEnumerator()
		{
			return _Interpreter.GetTableDict(this).GetEnumerator();
		}

		public void SetMetaTable(LuaTable metaTable)
		{
			push(_Interpreter.L);
			metaTable.push(_Interpreter.L);
			LuaDLL.lua_setmetatable(_Interpreter.L, -2);
			LuaDLL.lua_pop(_Interpreter.L, 1);
		}

		public T[] ToArray<T>()
		{
			IntPtr l = _Interpreter.L;
			push(l);
			return LuaScriptMgr.GetArrayObject<T>(l, -1);
		}

		public void Set(string key, object o)
		{
			IntPtr l = _Interpreter.L;
			push(l);
			LuaDLL.lua_pushstring(l, key);
			PushArgs(l, o);
			LuaDLL.lua_rawset(l, -3);
			LuaDLL.lua_settop(l, 0);
		}

		internal object rawget(string field)
		{
			return _Interpreter.rawGetObject(_Reference, field);
		}

		internal object rawgetFunction(string field)
		{
			object obj = _Interpreter.rawGetObject(_Reference, field);
			if (obj is LuaCSFunction)
			{
				return new LuaFunction((LuaCSFunction)obj, _Interpreter);
			}
			return obj;
		}

		public LuaFunction RawGetFunc(string field)
		{
			IntPtr l = _Interpreter.L;
			LuaTypes luaTypes = LuaTypes.LUA_TNONE;
			LuaFunction result = null;
			int newTop = LuaDLL.lua_gettop(l);
			LuaDLL.lua_getref(l, _Reference);
			LuaDLL.lua_pushstring(l, field);
			LuaDLL.lua_gettable(l, -2);
			luaTypes = LuaDLL.lua_type(l, -1);
			if (luaTypes == LuaTypes.LUA_TFUNCTION)
			{
				result = new LuaFunction(LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX), l);
			}
			LuaDLL.lua_settop(l, newTop);
			return result;
		}

		internal void push(IntPtr luaState)
		{
			LuaDLL.lua_getref(luaState, _Reference);
		}

		public override string ToString()
		{
			return "table";
		}
	}
}
