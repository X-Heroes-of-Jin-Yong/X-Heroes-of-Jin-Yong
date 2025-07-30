using System;
using LuaInterface;
using UnityEngine;

public class BlendWeightsWrap
{
	private static LuaMethod[] enums = new LuaMethod[4]
	{
		new LuaMethod("OneBone", GetOneBone),
		new LuaMethod("TwoBones", GetTwoBones),
		new LuaMethod("FourBones", GetFourBones),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.BlendWeights", typeof(BlendWeights), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetOneBone(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendWeights.OneBone);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTwoBones(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendWeights.TwoBones);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetFourBones(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendWeights.FourBones);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		BlendWeights blendWeights = (BlendWeights)num;
		LuaScriptMgr.Push(L, blendWeights);
		return 1;
	}
}
