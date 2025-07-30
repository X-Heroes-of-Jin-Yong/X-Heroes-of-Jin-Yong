using System;
using LuaInterface;
using UnityEngine;

public class ScreenWrap
{
	private static Type classType = typeof(Screen);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[3]
		{
			new LuaMethod("SetResolution", SetResolution),
			new LuaMethod("New", _CreateScreen),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaField[] fields = new LuaField[12]
		{
			new LuaField("resolutions", get_resolutions, null),
			new LuaField("currentResolution", get_currentResolution, null),
			new LuaField("width", get_width, null),
			new LuaField("height", get_height, null),
			new LuaField("dpi", get_dpi, null),
			new LuaField("fullScreen", get_fullScreen, set_fullScreen),
			new LuaField("autorotateToPortrait", get_autorotateToPortrait, set_autorotateToPortrait),
			new LuaField("autorotateToPortraitUpsideDown", get_autorotateToPortraitUpsideDown, set_autorotateToPortraitUpsideDown),
			new LuaField("autorotateToLandscapeLeft", get_autorotateToLandscapeLeft, set_autorotateToLandscapeLeft),
			new LuaField("autorotateToLandscapeRight", get_autorotateToLandscapeRight, set_autorotateToLandscapeRight),
			new LuaField("orientation", get_orientation, set_orientation),
			new LuaField("sleepTimeout", get_sleepTimeout, set_sleepTimeout)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Screen", typeof(Screen), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateScreen(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Screen o = new Screen();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Screen.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_resolutions(IntPtr L)
	{
		LuaScriptMgr.PushArray(L, Screen.resolutions);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_currentResolution(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, Screen.currentResolution);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_width(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.width);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_height(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.height);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_dpi(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.dpi);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_fullScreen(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.fullScreen);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_autorotateToPortrait(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.autorotateToPortrait);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_autorotateToPortraitUpsideDown(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.autorotateToPortraitUpsideDown);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_autorotateToLandscapeLeft(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.autorotateToLandscapeLeft);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_autorotateToLandscapeRight(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.autorotateToLandscapeRight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_orientation(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.orientation);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sleepTimeout(IntPtr L)
	{
		LuaScriptMgr.Push(L, Screen.sleepTimeout);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_fullScreen(IntPtr L)
	{
		Screen.fullScreen = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_autorotateToPortrait(IntPtr L)
	{
		Screen.autorotateToPortrait = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_autorotateToPortraitUpsideDown(IntPtr L)
	{
		Screen.autorotateToPortraitUpsideDown = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_autorotateToLandscapeLeft(IntPtr L)
	{
		Screen.autorotateToLandscapeLeft = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_autorotateToLandscapeRight(IntPtr L)
	{
		Screen.autorotateToLandscapeRight = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_orientation(IntPtr L)
	{
		Screen.orientation = (ScreenOrientation)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(ScreenOrientation));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sleepTimeout(IntPtr L)
	{
		Screen.sleepTimeout = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetResolution(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 3:
		{
			int width2 = (int)LuaScriptMgr.GetNumber(L, 1);
			int height2 = (int)LuaScriptMgr.GetNumber(L, 2);
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 3);
			Screen.SetResolution(width2, height2, boolean2);
			return 0;
		}
		case 4:
		{
			int width = (int)LuaScriptMgr.GetNumber(L, 1);
			int height = (int)LuaScriptMgr.GetNumber(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			int preferredRefreshRate = (int)LuaScriptMgr.GetNumber(L, 4);
			Screen.SetResolution(width, height, boolean, preferredRefreshRate);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Screen.SetResolution");
			return 0;
		}
	}
}
