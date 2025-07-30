using System;
using LuaInterface;
using UnityEngine;

public class TouchPhaseWrap
{
	private static LuaMethod[] enums = new LuaMethod[6]
	{
		new LuaMethod("Began", GetBegan),
		new LuaMethod("Moved", GetMoved),
		new LuaMethod("Stationary", GetStationary),
		new LuaMethod("Ended", GetEnded),
		new LuaMethod("Canceled", GetCanceled),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.TouchPhase", typeof(TouchPhase), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetBegan(IntPtr L)
	{
		LuaScriptMgr.Push(L, TouchPhase.Began);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMoved(IntPtr L)
	{
		LuaScriptMgr.Push(L, TouchPhase.Moved);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetStationary(IntPtr L)
	{
		LuaScriptMgr.Push(L, TouchPhase.Stationary);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetEnded(IntPtr L)
	{
		LuaScriptMgr.Push(L, TouchPhase.Ended);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetCanceled(IntPtr L)
	{
		LuaScriptMgr.Push(L, TouchPhase.Canceled);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		TouchPhase touchPhase = (TouchPhase)num;
		LuaScriptMgr.Push(L, touchPhase);
		return 1;
	}
}
