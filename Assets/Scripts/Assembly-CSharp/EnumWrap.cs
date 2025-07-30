using System;
using LuaInterface;

public class EnumWrap
{
	private static Type classType = typeof(Enum);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[17]
		{
			new LuaMethod("GetTypeCode", GetTypeCode),
			new LuaMethod("GetValues", GetValues),
			new LuaMethod("GetNames", GetNames),
			new LuaMethod("GetName", GetName),
			new LuaMethod("IsDefined", IsDefined),
			new LuaMethod("GetUnderlyingType", GetUnderlyingType),
			new LuaMethod("Parse", Parse),
			new LuaMethod("CompareTo", CompareTo),
			new LuaMethod("ToString", ToString),
			new LuaMethod("ToObject", ToObject),
			new LuaMethod("Format", Format),
			new LuaMethod("GetHashCode", GetHashCode),
			new LuaMethod("Equals", Equals),
			new LuaMethod("New", _CreateEnum),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__tostring", Lua_ToString),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "System.Enum", typeof(Enum), regs, fields, null);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateEnum(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Enum class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_ToString(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		if (luaObject != null)
		{
			LuaScriptMgr.Push(L, luaObject.ToString());
		}
		else
		{
			LuaScriptMgr.Push(L, "Table: System.Enum");
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTypeCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Enum obj = (Enum)LuaScriptMgr.GetNetObjectSelf(L, 1, "Enum");
		TypeCode typeCode = obj.GetTypeCode();
		LuaScriptMgr.Push(L, typeCode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetValues(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		Array values = Enum.GetValues(typeObject);
		LuaScriptMgr.PushObject(L, values);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetNames(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		string[] names = Enum.GetNames(typeObject);
		LuaScriptMgr.PushArray(L, names);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetName(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		string name = Enum.GetName(typeObject, varObject);
		LuaScriptMgr.Push(L, name);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsDefined(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		bool b = Enum.IsDefined(typeObject, varObject);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetUnderlyingType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		Type underlyingType = Enum.GetUnderlyingType(typeObject);
		LuaScriptMgr.Push(L, underlyingType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Parse(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 1);
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			object o2 = Enum.Parse(typeObject2, luaString2);
			LuaScriptMgr.PushVarObject(L, o2);
			return 1;
		}
		case 3:
		{
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			object o = Enum.Parse(typeObject, luaString, boolean);
			LuaScriptMgr.PushVarObject(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Enum.Parse");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareTo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Enum obj = (Enum)LuaScriptMgr.GetNetObjectSelf(L, 1, "Enum");
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		int d = obj.CompareTo(varObject);
		LuaScriptMgr.Push(L, d);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToString(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			Enum obj2 = (Enum)LuaScriptMgr.GetNetObjectSelf(L, 1, "Enum");
			string str2 = obj2.ToString();
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
		{
			Enum obj = (Enum)LuaScriptMgr.GetNetObjectSelf(L, 1, "Enum");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			string str = obj.ToString(luaString);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Enum.ToString");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToObject(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Type), typeof(long)))
		{
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
			long value = (long)LuaDLL.lua_tonumber(L, 2);
			object o = Enum.ToObject(typeObject, value);
			LuaScriptMgr.PushVarObject(L, o);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Type), typeof(object)))
		{
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 1);
			object varObject = LuaScriptMgr.GetVarObject(L, 2);
			object o2 = Enum.ToObject(typeObject2, varObject);
			LuaScriptMgr.PushVarObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Enum.ToObject");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Format(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		string luaString = LuaScriptMgr.GetLuaString(L, 3);
		string str = Enum.Format(typeObject, varObject, luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Enum obj = LuaScriptMgr.GetLuaObject(L, 1) as Enum;
		Enum obj2 = LuaScriptMgr.GetLuaObject(L, 2) as Enum;
		bool b = obj == obj2;
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetHashCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Enum obj = (Enum)LuaScriptMgr.GetNetObjectSelf(L, 1, "Enum");
		int hashCode = obj.GetHashCode();
		LuaScriptMgr.Push(L, hashCode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Equals(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Enum obj = LuaScriptMgr.GetVarObject(L, 1) as Enum;
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		bool b = ((obj == null) ? (varObject == null) : obj.Equals(varObject));
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
