using System;
using LuaInterface;
using UnityEngine;

public class AnimationClipWrap
{
	private static Type classType = typeof(AnimationClip);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[8]
		{
			new LuaMethod("SampleAnimation", SampleAnimation),
			new LuaMethod("SetCurve", SetCurve),
			new LuaMethod("EnsureQuaternionContinuity", EnsureQuaternionContinuity),
			new LuaMethod("ClearCurves", ClearCurves),
			new LuaMethod("AddEvent", AddEvent),
			new LuaMethod("New", _CreateAnimationClip),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[7]
		{
			new LuaField("length", get_length, null),
			new LuaField("frameRate", get_frameRate, set_frameRate),
			new LuaField("wrapMode", get_wrapMode, set_wrapMode),
			new LuaField("localBounds", get_localBounds, set_localBounds),
			new LuaField("legacy", get_legacy, set_legacy),
			new LuaField("humanMotion", get_humanMotion, null),
			new LuaField("events", get_events, set_events)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AnimationClip", typeof(AnimationClip), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAnimationClip(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			AnimationClip obj = new AnimationClip();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AnimationClip.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_length(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name length");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index length on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animationClip.length);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_frameRate(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name frameRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index frameRate on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animationClip.frameRate);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_wrapMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wrapMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wrapMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animationClip.wrapMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localBounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localBounds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localBounds on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animationClip.localBounds);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_legacy(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name legacy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index legacy on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animationClip.legacy);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_humanMotion(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name humanMotion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index humanMotion on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animationClip.humanMotion);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_events(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name events");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index events on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, animationClip.events);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_frameRate(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name frameRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index frameRate on a nil value");
			}
		}
		animationClip.frameRate = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_wrapMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wrapMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wrapMode on a nil value");
			}
		}
		animationClip.wrapMode = (WrapMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(WrapMode));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localBounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localBounds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localBounds on a nil value");
			}
		}
		animationClip.localBounds = LuaScriptMgr.GetBounds(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_legacy(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name legacy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index legacy on a nil value");
			}
		}
		animationClip.legacy = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_events(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AnimationClip animationClip = (AnimationClip)luaObject;
		if (animationClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name events");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index events on a nil value");
			}
		}
		animationClip.events = LuaScriptMgr.GetArrayObject<AnimationEvent>(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SampleAnimation(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AnimationClip animationClip = (AnimationClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationClip");
		GameObject go = (GameObject)LuaScriptMgr.GetUnityObject(L, 2, typeof(GameObject));
		float time = (float)LuaScriptMgr.GetNumber(L, 3);
		animationClip.SampleAnimation(go, time);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetCurve(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		AnimationClip animationClip = (AnimationClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationClip");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 4);
		AnimationCurve curve = (AnimationCurve)LuaScriptMgr.GetNetObject(L, 5, typeof(AnimationCurve));
		animationClip.SetCurve(luaString, typeObject, luaString2, curve);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EnsureQuaternionContinuity(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AnimationClip animationClip = (AnimationClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationClip");
		animationClip.EnsureQuaternionContinuity();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearCurves(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AnimationClip animationClip = (AnimationClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationClip");
		animationClip.ClearCurves();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AnimationClip animationClip = (AnimationClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AnimationClip");
		AnimationEvent evt = (AnimationEvent)LuaScriptMgr.GetNetObject(L, 2, typeof(AnimationEvent));
		animationClip.AddEvent(evt);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object obj = LuaScriptMgr.GetLuaObject(L, 1) as UnityEngine.Object;
		UnityEngine.Object obj2 = LuaScriptMgr.GetLuaObject(L, 2) as UnityEngine.Object;
		bool b = obj == obj2;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
