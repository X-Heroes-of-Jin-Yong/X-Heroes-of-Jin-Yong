using System;
using LuaInterface;
using UnityEngine;

public class AnimationBlendModeWrap
{
	private static LuaMethod[] enums = new LuaMethod[3]
	{
		new LuaMethod("Blend", GetBlend),
		new LuaMethod("Additive", GetAdditive),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AnimationBlendMode", typeof(AnimationBlendMode), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetBlend(IntPtr L)
	{
		LuaScriptMgr.Push(L, AnimationBlendMode.Blend);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAdditive(IntPtr L)
	{
		LuaScriptMgr.Push(L, AnimationBlendMode.Additive);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		AnimationBlendMode animationBlendMode = (AnimationBlendMode)num;
		LuaScriptMgr.Push(L, animationBlendMode);
		return 1;
	}
}
