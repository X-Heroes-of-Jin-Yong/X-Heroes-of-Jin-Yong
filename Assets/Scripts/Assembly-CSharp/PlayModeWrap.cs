using System;
using LuaInterface;
using UnityEngine;

public class PlayModeWrap
{
	private static LuaMethod[] enums = new LuaMethod[3]
	{
		new LuaMethod("StopSameLayer", GetStopSameLayer),
		new LuaMethod("StopAll", GetStopAll),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.PlayMode", typeof(PlayMode), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetStopSameLayer(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayMode.StopSameLayer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetStopAll(IntPtr L)
	{
		LuaScriptMgr.Push(L, PlayMode.StopAll);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		PlayMode playMode = (PlayMode)num;
		LuaScriptMgr.Push(L, playMode);
		return 1;
	}
}
