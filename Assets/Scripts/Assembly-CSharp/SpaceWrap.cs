using System;
using LuaInterface;
using UnityEngine;

public class SpaceWrap
{
	private static LuaMethod[] enums = new LuaMethod[3]
	{
		new LuaMethod("World", GetWorld),
		new LuaMethod("Self", GetSelf),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Space", typeof(Space), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetWorld(IntPtr L)
	{
		LuaScriptMgr.Push(L, Space.World);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSelf(IntPtr L)
	{
		LuaScriptMgr.Push(L, Space.Self);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		Space space = (Space)num;
		LuaScriptMgr.Push(L, space);
		return 1;
	}
}
