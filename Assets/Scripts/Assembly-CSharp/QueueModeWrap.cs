using System;
using LuaInterface;
using UnityEngine;

public class QueueModeWrap
{
	private static LuaMethod[] enums = new LuaMethod[3]
	{
		new LuaMethod("CompleteOthers", GetCompleteOthers),
		new LuaMethod("PlayNow", GetPlayNow),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.QueueMode", typeof(QueueMode), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetCompleteOthers(IntPtr L)
	{
		LuaScriptMgr.Push(L, QueueMode.CompleteOthers);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPlayNow(IntPtr L)
	{
		LuaScriptMgr.Push(L, QueueMode.PlayNow);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		QueueMode queueMode = (QueueMode)num;
		LuaScriptMgr.Push(L, queueMode);
		return 1;
	}
}
