using System;
using LuaInterface;
using UnityEngine;

public class LightTypeWrap
{
	private static LuaMethod[] enums = new LuaMethod[5]
	{
		new LuaMethod("Spot", GetSpot),
		new LuaMethod("Directional", GetDirectional),
		new LuaMethod("Point", GetPoint),
		new LuaMethod("Area", GetArea),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.LightType", typeof(LightType), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSpot(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightType.Spot);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetDirectional(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightType.Directional);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPoint(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightType.Point);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetArea(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightType.Area);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		LightType lightType = (LightType)num;
		LuaScriptMgr.Push(L, lightType);
		return 1;
	}
}
