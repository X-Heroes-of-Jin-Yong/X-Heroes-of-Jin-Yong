using System;
using LuaInterface;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceWrap
{
	private static Type classType = typeof(AudioSource);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[19]
		{
			new LuaMethod("Play", Play),
			new LuaMethod("PlayDelayed", PlayDelayed),
			new LuaMethod("PlayScheduled", PlayScheduled),
			new LuaMethod("SetScheduledStartTime", SetScheduledStartTime),
			new LuaMethod("SetScheduledEndTime", SetScheduledEndTime),
			new LuaMethod("Stop", Stop),
			new LuaMethod("Pause", Pause),
			new LuaMethod("UnPause", UnPause),
			new LuaMethod("PlayOneShot", PlayOneShot),
			new LuaMethod("PlayClipAtPoint", PlayClipAtPoint),
			new LuaMethod("SetCustomCurve", SetCustomCurve),
			new LuaMethod("GetCustomCurve", GetCustomCurve),
			new LuaMethod("GetOutputData", GetOutputData),
			new LuaMethod("GetSpectrumData", GetSpectrumData),
			new LuaMethod("SetSpatializerFloat", SetSpatializerFloat),
			new LuaMethod("GetSpatializerFloat", GetSpatializerFloat),
			new LuaMethod("New", _CreateAudioSource),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[26]
		{
			new LuaField("volume", get_volume, set_volume),
			new LuaField("pitch", get_pitch, set_pitch),
			new LuaField("time", get_time, set_time),
			new LuaField("timeSamples", get_timeSamples, set_timeSamples),
			new LuaField("clip", get_clip, set_clip),
			new LuaField("outputAudioMixerGroup", get_outputAudioMixerGroup, set_outputAudioMixerGroup),
			new LuaField("isPlaying", get_isPlaying, null),
			new LuaField("loop", get_loop, set_loop),
			new LuaField("ignoreListenerVolume", get_ignoreListenerVolume, set_ignoreListenerVolume),
			new LuaField("playOnAwake", get_playOnAwake, set_playOnAwake),
			new LuaField("ignoreListenerPause", get_ignoreListenerPause, set_ignoreListenerPause),
			new LuaField("velocityUpdateMode", get_velocityUpdateMode, set_velocityUpdateMode),
			new LuaField("panStereo", get_panStereo, set_panStereo),
			new LuaField("spatialBlend", get_spatialBlend, set_spatialBlend),
			new LuaField("spatialize", get_spatialize, set_spatialize),
			new LuaField("reverbZoneMix", get_reverbZoneMix, set_reverbZoneMix),
			new LuaField("bypassEffects", get_bypassEffects, set_bypassEffects),
			new LuaField("bypassListenerEffects", get_bypassListenerEffects, set_bypassListenerEffects),
			new LuaField("bypassReverbZones", get_bypassReverbZones, set_bypassReverbZones),
			new LuaField("dopplerLevel", get_dopplerLevel, set_dopplerLevel),
			new LuaField("spread", get_spread, set_spread),
			new LuaField("priority", get_priority, set_priority),
			new LuaField("mute", get_mute, set_mute),
			new LuaField("minDistance", get_minDistance, set_minDistance),
			new LuaField("maxDistance", get_maxDistance, set_maxDistance),
			new LuaField("rolloffMode", get_rolloffMode, set_rolloffMode)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AudioSource", typeof(AudioSource), regs, fields, typeof(Behaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAudioSource(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			AudioSource obj = new AudioSource();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_volume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volume on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.volume);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pitch(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pitch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pitch on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.pitch);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_time(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.time);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_timeSamples(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeSamples");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeSamples on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.timeSamples);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_clip(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
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
		LuaScriptMgr.Push(L, audioSource.clip);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_outputAudioMixerGroup(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name outputAudioMixerGroup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index outputAudioMixerGroup on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.outputAudioMixerGroup);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPlaying(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
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
		LuaScriptMgr.Push(L, audioSource.isPlaying);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loop(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loop on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.loop);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ignoreListenerVolume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerVolume on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.ignoreListenerVolume);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_playOnAwake(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playOnAwake");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playOnAwake on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.playOnAwake);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ignoreListenerPause(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerPause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerPause on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.ignoreListenerPause);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocityUpdateMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityUpdateMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityUpdateMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.velocityUpdateMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_panStereo(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name panStereo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index panStereo on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.panStereo);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_spatialBlend(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spatialBlend");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spatialBlend on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.spatialBlend);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_spatialize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spatialize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spatialize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.spatialize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reverbZoneMix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reverbZoneMix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reverbZoneMix on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.reverbZoneMix);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bypassEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassEffects on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.bypassEffects);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bypassListenerEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassListenerEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassListenerEffects on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.bypassListenerEffects);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bypassReverbZones(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassReverbZones");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassReverbZones on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.bypassReverbZones);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_dopplerLevel(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dopplerLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dopplerLevel on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.dopplerLevel);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_spread(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spread");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spread on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.spread);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_priority(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name priority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index priority on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.priority);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mute(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mute on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.mute);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minDistance on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.minDistance);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxDistance on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.maxDistance);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rolloffMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rolloffMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rolloffMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, audioSource.rolloffMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_volume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volume on a nil value");
			}
		}
		audioSource.volume = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pitch(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pitch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pitch on a nil value");
			}
		}
		audioSource.pitch = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_time(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name time");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index time on a nil value");
			}
		}
		audioSource.time = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_timeSamples(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name timeSamples");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index timeSamples on a nil value");
			}
		}
		audioSource.timeSamples = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_clip(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
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
		audioSource.clip = (AudioClip)LuaScriptMgr.GetUnityObject(L, 3, typeof(AudioClip));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_outputAudioMixerGroup(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name outputAudioMixerGroup");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index outputAudioMixerGroup on a nil value");
			}
		}
		audioSource.outputAudioMixerGroup = (AudioMixerGroup)LuaScriptMgr.GetUnityObject(L, 3, typeof(AudioMixerGroup));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_loop(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name loop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index loop on a nil value");
			}
		}
		audioSource.loop = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ignoreListenerVolume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerVolume on a nil value");
			}
		}
		audioSource.ignoreListenerVolume = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_playOnAwake(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playOnAwake");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playOnAwake on a nil value");
			}
		}
		audioSource.playOnAwake = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_ignoreListenerPause(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreListenerPause");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreListenerPause on a nil value");
			}
		}
		audioSource.ignoreListenerPause = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_velocityUpdateMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityUpdateMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityUpdateMode on a nil value");
			}
		}
		audioSource.velocityUpdateMode = (AudioVelocityUpdateMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(AudioVelocityUpdateMode));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_panStereo(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name panStereo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index panStereo on a nil value");
			}
		}
		audioSource.panStereo = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_spatialBlend(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spatialBlend");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spatialBlend on a nil value");
			}
		}
		audioSource.spatialBlend = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_spatialize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spatialize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spatialize on a nil value");
			}
		}
		audioSource.spatialize = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reverbZoneMix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reverbZoneMix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reverbZoneMix on a nil value");
			}
		}
		audioSource.reverbZoneMix = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bypassEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassEffects on a nil value");
			}
		}
		audioSource.bypassEffects = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bypassListenerEffects(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassListenerEffects");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassListenerEffects on a nil value");
			}
		}
		audioSource.bypassListenerEffects = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bypassReverbZones(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bypassReverbZones");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bypassReverbZones on a nil value");
			}
		}
		audioSource.bypassReverbZones = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_dopplerLevel(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dopplerLevel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dopplerLevel on a nil value");
			}
		}
		audioSource.dopplerLevel = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_spread(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spread");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spread on a nil value");
			}
		}
		audioSource.spread = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_priority(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name priority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index priority on a nil value");
			}
		}
		audioSource.priority = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mute(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mute");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mute on a nil value");
			}
		}
		audioSource.mute = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minDistance on a nil value");
			}
		}
		audioSource.minDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxDistance(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxDistance on a nil value");
			}
		}
		audioSource.maxDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rolloffMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AudioSource audioSource = (AudioSource)luaObject;
		if (audioSource == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rolloffMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rolloffMode on a nil value");
			}
		}
		audioSource.rolloffMode = (AudioRolloffMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(AudioRolloffMode));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Play(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			AudioSource audioSource2 = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			audioSource2.Play();
			return 0;
		}
		case 2:
		{
			AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			ulong delay = (ulong)LuaScriptMgr.GetNumber(L, 2);
			audioSource.Play(delay);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.Play");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayDelayed(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		float delay = (float)LuaScriptMgr.GetNumber(L, 2);
		audioSource.PlayDelayed(delay);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayScheduled(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		double time = LuaScriptMgr.GetNumber(L, 2);
		audioSource.PlayScheduled(time);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetScheduledStartTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		double scheduledStartTime = LuaScriptMgr.GetNumber(L, 2);
		audioSource.SetScheduledStartTime(scheduledStartTime);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetScheduledEndTime(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		double scheduledEndTime = LuaScriptMgr.GetNumber(L, 2);
		audioSource.SetScheduledEndTime(scheduledEndTime);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Stop(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		audioSource.Stop();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Pause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		audioSource.Pause();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnPause(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		audioSource.UnPause();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayOneShot(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			AudioSource audioSource2 = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			AudioClip clip2 = (AudioClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AudioClip));
			audioSource2.PlayOneShot(clip2);
			return 0;
		}
		case 3:
		{
			AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
			AudioClip clip = (AudioClip)LuaScriptMgr.GetUnityObject(L, 2, typeof(AudioClip));
			float volumeScale = (float)LuaScriptMgr.GetNumber(L, 3);
			audioSource.PlayOneShot(clip, volumeScale);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.PlayOneShot");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PlayClipAtPoint(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			AudioClip clip2 = (AudioClip)LuaScriptMgr.GetUnityObject(L, 1, typeof(AudioClip));
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			AudioSource.PlayClipAtPoint(clip2, vector2);
			return 0;
		}
		case 3:
		{
			AudioClip clip = (AudioClip)LuaScriptMgr.GetUnityObject(L, 1, typeof(AudioClip));
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			float volume = (float)LuaScriptMgr.GetNumber(L, 3);
			AudioSource.PlayClipAtPoint(clip, vector, volume);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AudioSource.PlayClipAtPoint");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetCustomCurve(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		AudioSourceCurveType type = (AudioSourceCurveType)(int)LuaScriptMgr.GetNetObject(L, 2, typeof(AudioSourceCurveType));
		AnimationCurve curve = (AnimationCurve)LuaScriptMgr.GetNetObject(L, 3, typeof(AnimationCurve));
		audioSource.SetCustomCurve(type, curve);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetCustomCurve(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		AudioSourceCurveType type = (AudioSourceCurveType)(int)LuaScriptMgr.GetNetObject(L, 2, typeof(AudioSourceCurveType));
		AnimationCurve customCurve = audioSource.GetCustomCurve(type);
		LuaScriptMgr.PushObject(L, customCurve);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetOutputData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int channel = (int)LuaScriptMgr.GetNumber(L, 3);
		audioSource.GetOutputData(arrayNumber, channel);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSpectrumData(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		float[] arrayNumber = LuaScriptMgr.GetArrayNumber<float>(L, 2);
		int channel = (int)LuaScriptMgr.GetNumber(L, 3);
		FFTWindow window = (FFTWindow)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(FFTWindow));
		audioSource.GetSpectrumData(arrayNumber, channel, window);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetSpatializerFloat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		float value = (float)LuaScriptMgr.GetNumber(L, 3);
		bool b = audioSource.SetSpatializerFloat(index, value);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSpatializerFloat(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		AudioSource audioSource = (AudioSource)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioSource");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		float value;
		bool spatializerFloat = audioSource.GetSpatializerFloat(index, out value);
		LuaScriptMgr.Push(L, spatializerFloat);
		LuaScriptMgr.Push(L, value);
		return 2;
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
