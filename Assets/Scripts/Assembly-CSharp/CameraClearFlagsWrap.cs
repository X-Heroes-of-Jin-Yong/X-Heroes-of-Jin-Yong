using System;
using LuaInterface;
using UnityEngine;

public class CameraClearFlagsWrap
{
	private static LuaMethod[] enums = new LuaMethod[6]
	{
		new LuaMethod("Skybox", GetSkybox),
		new LuaMethod("Color", GetColor),
		new LuaMethod("SolidColor", GetSolidColor),
		new LuaMethod("Depth", GetDepth),
		new LuaMethod("Nothing", GetNothing),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.CameraClearFlags", typeof(CameraClearFlags), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSkybox(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Skybox);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Color);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSolidColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Color);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetDepth(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Depth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetNothing(IntPtr L)
	{
		LuaScriptMgr.Push(L, CameraClearFlags.Nothing);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		CameraClearFlags cameraClearFlags = (CameraClearFlags)num;
		LuaScriptMgr.Push(L, cameraClearFlags);
		return 1;
	}
}
