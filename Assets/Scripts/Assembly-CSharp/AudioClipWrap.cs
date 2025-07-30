using System;
using LuaInterface;
using UnityEngine;

public class AudioClipWrap
{
	private static Type classType = typeof(AudioClip);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[8]
		{
			new LuaMethod("LoadAudioData", LoadAudioData),
			new LuaMethod("UnloadAudioData", UnloadAudioData),
			new LuaMethod("GetData", GetData),
			new LuaMethod("SetData", SetData),
			new LuaMethod("Create", Create),
			new LuaMethod("New", _CreateAudioClip),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[8]
		{
			new LuaField("length", get_length, null),
			new LuaField("samples", get_samples, null),
			new LuaField("channels", get_channels, null),
			new LuaField("frequency", get_frequency, null),
			new LuaField("loadType", get_loadType, null),
			new LuaField("preloadAudioData", get_preloadAudioData, null),
			new LuaField("loadState", get_loadState, null),
			new LuaField("loadInBackground", get_loadInBackground, null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AudioClip", typeof(AudioClip), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAudioClip(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			AudioClip obj = new AudioClip();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioClip.New");
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
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
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
		LuaScriptMgr.Push(L, audioClip.length);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_samples(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name samples");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index samples on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.samples);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_channels(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name channels");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index channels on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.channels);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_frequency(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name frequency");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index frequency on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.frequency);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadType(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadType on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.loadType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_preloadAudioData(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preloadAudioData");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preloadAudioData on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.preloadAudioData);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadState(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadState on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.loadState);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadInBackground(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioClip audioClip = (AudioClip)luaObject;
		if (audioClip == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loadInBackground");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loadInBackground on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioClip.loadInBackground);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAudioData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		bool b = audioClip.LoadAudioData();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnloadAudioData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		bool b = audioClip.UnloadAudioData();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int offsetSamples = (int)LuaScriptMgr.GetNumber(L, 3);
		bool data = audioClip.GetData(arrayNumber, offsetSamples);
		LuaScriptMgr.Push(L, data);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioClip audioClip = (AudioClip)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioClip");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int offsetSamples = (int)LuaScriptMgr.GetNumber(L, 3);
		bool b = audioClip.SetData(arrayNumber, offsetSamples);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Create(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 5:
		{
			string luaString3 = LuaScriptMgr.GetLuaString(L, 1);
			int lengthSamples3 = (int)LuaScriptMgr.GetNumber(L, 2);
			int channels3 = (int)LuaScriptMgr.GetNumber(L, 3);
			int frequency3 = (int)LuaScriptMgr.GetNumber(L, 4);
			bool boolean3 = LuaScriptMgr.GetBoolean(L, 5);
			AudioClip obj3 = AudioClip.Create(luaString3, lengthSamples3, channels3, frequency3, boolean3);
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		case 6:
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			int lengthSamples2 = (int)LuaScriptMgr.GetNumber(L, 2);
			int channels2 = (int)LuaScriptMgr.GetNumber(L, 3);
			int frequency2 = (int)LuaScriptMgr.GetNumber(L, 4);
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 5);
			AudioClip.PCMReaderCallback pCMReaderCallback2 = null;
			LuaTypes luaTypes3 = LuaDLL.lua_type(L, 6);
			if (luaTypes3 != LuaTypes.LUA_TFUNCTION)
			{
				pCMReaderCallback2 = (AudioClip.PCMReaderCallback)LuaScriptMgr.GetNetObject(L, 6, typeof(AudioClip.PCMReaderCallback));
			}
			else
			{
				LuaFunction func3 = LuaScriptMgr.GetLuaFunction(L, 6);
				pCMReaderCallback2 = delegate(float[] param0)
				{
					int oldTop = func3.BeginPCall();
					LuaScriptMgr.PushArray(L, param0);
					func3.PCall(oldTop, 1);
					func3.EndPCall(oldTop);
				};
			}
			AudioClip obj2 = AudioClip.Create(luaString2, lengthSamples2, channels2, frequency2, boolean2, pCMReaderCallback2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		case 7:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			int lengthSamples = (int)LuaScriptMgr.GetNumber(L, 2);
			int channels = (int)LuaScriptMgr.GetNumber(L, 3);
			int frequency = (int)LuaScriptMgr.GetNumber(L, 4);
			bool boolean = LuaScriptMgr.GetBoolean(L, 5);
			AudioClip.PCMReaderCallback pCMReaderCallback = null;
			LuaTypes luaTypes = LuaDLL.lua_type(L, 6);
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				pCMReaderCallback = (AudioClip.PCMReaderCallback)LuaScriptMgr.GetNetObject(L, 6, typeof(AudioClip.PCMReaderCallback));
			}
			else
			{
				LuaFunction func = LuaScriptMgr.GetLuaFunction(L, 6);
				pCMReaderCallback = delegate(float[] param0)
				{
					int oldTop = func.BeginPCall();
					LuaScriptMgr.PushArray(L, param0);
					func.PCall(oldTop, 1);
					func.EndPCall(oldTop);
				};
			}
			AudioClip.PCMSetPositionCallback pCMSetPositionCallback = null;
			LuaTypes luaTypes2 = LuaDLL.lua_type(L, 7);
			if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
			{
				pCMSetPositionCallback = (AudioClip.PCMSetPositionCallback)LuaScriptMgr.GetNetObject(L, 7, typeof(AudioClip.PCMSetPositionCallback));
			}
			else
			{
				LuaFunction func2 = LuaScriptMgr.GetLuaFunction(L, 7);
				pCMSetPositionCallback = delegate(int param0)
				{
					int oldTop = func2.BeginPCall();
					LuaScriptMgr.Push(L, param0);
					func2.PCall(oldTop, 1);
					func2.EndPCall(oldTop);
				};
			}
			AudioClip obj = AudioClip.Create(luaString, lengthSamples, channels, frequency, boolean, pCMReaderCallback, pCMSetPositionCallback);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AudioClip.Create");
			return 0;
		}
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
