using System;
using System.Collections;
using LuaInterface;
using UnityEngine;

public class AnimationWrap
{
	private static Type classType = typeof(Animation);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[19]
		{
			new LuaMethod("Stop", Stop),
			new LuaMethod("Rewind", Rewind),
			new LuaMethod("Sample", Sample),
			new LuaMethod("IsPlaying", IsPlaying),
			new LuaMethod("get_Item", get_Item),
			new LuaMethod("Play", Play),
			new LuaMethod("CrossFade", CrossFade),
			new LuaMethod("Blend", Blend),
			new LuaMethod("CrossFadeQueued", CrossFadeQueued),
			new LuaMethod("PlayQueued", PlayQueued),
			new LuaMethod("AddClip", AddClip),
			new LuaMethod("RemoveClip", RemoveClip),
			new LuaMethod("GetClipCount", GetClipCount),
			new LuaMethod("SyncLayer", SyncLayer),
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetClip", GetClip),
			new LuaMethod("New", _CreateAnimation),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[7]
		{
			new LuaField("clip", get_clip, set_clip),
			new LuaField("playAutomatically", get_playAutomatically, set_playAutomatically),
			new LuaField("wrapMode", get_wrapMode, set_wrapMode),
			new LuaField("isPlaying", get_isPlaying, null),
			new LuaField("animatePhysics", get_animatePhysics, set_animatePhysics),
			new LuaField("cullingType", get_cullingType, set_cullingType),
			new LuaField("localBounds", get_localBounds, set_localBounds)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Animation", typeof(Animation), regs, fields, typeof(Behaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAnimation(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Animation obj = new Animation();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Animation.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clip(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clip on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animation.clip);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_playAutomatically(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playAutomatically");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playAutomatically on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animation.playAutomatically);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_wrapMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
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
		LuaScriptMgr.Push(L, animation.wrapMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPlaying(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPlaying");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPlaying on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animation.isPlaying);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_animatePhysics(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animatePhysics");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animatePhysics on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animation.animatePhysics);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cullingType(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingType on a nil value");
			}
		}
		LuaScriptMgr.Push(L, animation.cullingType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localBounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
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
		LuaScriptMgr.Push(L, animation.localBounds);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clip(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clip on a nil value");
			}
		}
		animation.clip = (AnimationClip)LuaScriptMgr.GetUnityObject(L, 3, typeof(AnimationClip));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_playAutomatically(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playAutomatically");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playAutomatically on a nil value");
			}
		}
		animation.playAutomatically = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_wrapMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
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
		animation.wrapMode = (WrapMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(WrapMode));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_animatePhysics(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animatePhysics");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animatePhysics on a nil value");
			}
		}
		animation.animatePhysics = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cullingType(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingType on a nil value");
			}
		}
		animation.cullingType = (AnimationCullingType)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(AnimationCullingType));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localBounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Animation animation = (Animation)luaObject;
		if (animation == null)
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
		animation.localBounds = LuaScriptMgr.GetBounds(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Stop(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			animation2.Stop();
			return 0;
		}
		case 2:
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			animation.Stop(luaString);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Stop");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Rewind(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			animation2.Rewind();
			return 0;
		}
		case 2:
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			animation.Rewind(luaString);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Rewind");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Sample(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
		animation.Sample();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsPlaying(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool b = animation.IsPlaying(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Item(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		AnimationState obj = animation[luaString];
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Play(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 1:
		{
			Animation animation3 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			bool b2 = animation3.Play();
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 2:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(Animation), typeof(string)))
			{
				Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
				string animation2 = LuaScriptMgr.GetString(L, 2);
				bool b = animation.Play(animation2);
				LuaScriptMgr.Push(L, b);
				return 1;
			}
			break;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Animation), typeof(PlayMode)))
		{
			Animation animation4 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			PlayMode mode = (PlayMode)(int)LuaScriptMgr.GetLuaObject(L, 2);
			bool b3 = animation4.Play(mode);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 3)
		{
			Animation animation5 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			PlayMode mode2 = (PlayMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(PlayMode));
			bool b4 = animation5.Play(luaString, mode2);
			LuaScriptMgr.Push(L, b4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Play");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CrossFade(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Animation animation3 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			animation3.CrossFade(luaString3);
			return 0;
		}
		case 3:
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			float fadeLength2 = (float)LuaScriptMgr.GetNumber(L, 3);
			animation2.CrossFade(luaString2, fadeLength2);
			return 0;
		}
		case 4:
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			float fadeLength = (float)LuaScriptMgr.GetNumber(L, 3);
			PlayMode mode = (PlayMode)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(PlayMode));
			animation.CrossFade(luaString, fadeLength, mode);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.CrossFade");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Blend(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Animation animation3 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			animation3.Blend(luaString3);
			return 0;
		}
		case 3:
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			float targetWeight2 = (float)LuaScriptMgr.GetNumber(L, 3);
			animation2.Blend(luaString2, targetWeight2);
			return 0;
		}
		case 4:
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			float targetWeight = (float)LuaScriptMgr.GetNumber(L, 3);
			float fadeLength = (float)LuaScriptMgr.GetNumber(L, 4);
			animation.Blend(luaString, targetWeight, fadeLength);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Blend");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CrossFadeQueued(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Animation animation4 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString4 = LuaScriptMgr.GetLuaString(L, 2);
			AnimationState obj4 = animation4.CrossFadeQueued(luaString4);
			LuaScriptMgr.Push(L, obj4);
			return 1;
		}
		case 3:
		{
			Animation animation3 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			float fadeLength3 = (float)LuaScriptMgr.GetNumber(L, 3);
			AnimationState obj3 = animation3.CrossFadeQueued(luaString3, fadeLength3);
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		case 4:
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			float fadeLength2 = (float)LuaScriptMgr.GetNumber(L, 3);
			QueueMode queue2 = (QueueMode)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(QueueMode));
			AnimationState obj2 = animation2.CrossFadeQueued(luaString2, fadeLength2, queue2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		case 5:
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			float fadeLength = (float)LuaScriptMgr.GetNumber(L, 3);
			QueueMode queue = (QueueMode)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(QueueMode));
			PlayMode mode = (PlayMode)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(PlayMode));
			AnimationState obj = animation.CrossFadeQueued(luaString, fadeLength, queue, mode);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.CrossFadeQueued");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayQueued(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Animation animation3 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			AnimationState obj3 = animation3.PlayQueued(luaString3);
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		case 3:
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			QueueMode queue2 = (QueueMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(QueueMode));
			AnimationState obj2 = animation2.PlayQueued(luaString2, queue2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		case 4:
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			QueueMode queue = (QueueMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(QueueMode));
			PlayMode mode = (PlayMode)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(PlayMode));
			AnimationState obj = animation.PlayQueued(luaString, queue, mode);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.PlayQueued");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddClip(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 3:
		{
			Animation animation3 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			AnimationClip clip3 = (AnimationClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AnimationClip));
			string luaString3 = LuaScriptMgr.GetLuaString(L, 3);
			animation3.AddClip(clip3, luaString3);
			return 0;
		}
		case 5:
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			AnimationClip clip2 = (AnimationClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AnimationClip));
			string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
			int firstFrame2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int lastFrame2 = (int)LuaScriptMgr.GetNumber(L, 5);
			animation2.AddClip(clip2, luaString2, firstFrame2, lastFrame2);
			return 0;
		}
		case 6:
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			AnimationClip clip = (AnimationClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AnimationClip));
			string luaString = LuaScriptMgr.GetLuaString(L, 3);
			int firstFrame = (int)LuaScriptMgr.GetNumber(L, 4);
			int lastFrame = (int)LuaScriptMgr.GetNumber(L, 5);
			bool boolean = LuaScriptMgr.GetBoolean(L, 6);
			animation.AddClip(clip, luaString, firstFrame, lastFrame, boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.AddClip");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RemoveClip(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Animation), typeof(string)))
		{
			Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			string clipName = LuaScriptMgr.GetString(L, 2);
			animation.RemoveClip(clipName);
			return 0;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Animation), typeof(AnimationClip)))
		{
			Animation animation2 = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
			AnimationClip clip = (AnimationClip)LuaScriptMgr.GetLuaObject(L, 2);
			animation2.RemoveClip(clip);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Animation.RemoveClip");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClipCount(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
		int clipCount = animation.GetClipCount();
		LuaScriptMgr.Push(L, clipCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SyncLayer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
		int layer = (int)LuaScriptMgr.GetNumber(L, 2);
		animation.SyncLayer(layer);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
		IEnumerator enumerator = animation.GetEnumerator();
		LuaScriptMgr.Push(L, enumerator);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClip(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Animation animation = (Animation)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Animation");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		AnimationClip clip = animation.GetClip(luaString);
		LuaScriptMgr.Push(L, clip);
		return 1;
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
