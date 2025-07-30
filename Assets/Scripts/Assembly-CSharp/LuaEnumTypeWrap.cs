using System;
using LuaInterface;

public class LuaEnumTypeWrap
{
	private static LuaMethod[] enums = new LuaMethod[5]
	{
		new LuaMethod("AAA", GetAAA),
		new LuaMethod("BBB", GetBBB),
		new LuaMethod("CCC", GetCCC),
		new LuaMethod("DDD", GetDDD),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "LuaEnumType", typeof(LuaEnumType), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAAA(IntPtr L)
	{
		LuaScriptMgr.Push(L, LuaEnumType.AAA);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetBBB(IntPtr L)
	{
		LuaScriptMgr.Push(L, LuaEnumType.BBB);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetCCC(IntPtr L)
	{
		LuaScriptMgr.Push(L, LuaEnumType.CCC);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetDDD(IntPtr L)
	{
		LuaScriptMgr.Push(L, LuaEnumType.DDD);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		LuaEnumType luaEnumType = (LuaEnumType)num;
		LuaScriptMgr.Push(L, luaEnumType);
		return 1;
	}
}
