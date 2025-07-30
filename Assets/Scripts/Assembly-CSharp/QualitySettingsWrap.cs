using System;
using LuaInterface;
using UnityEngine;

public class QualitySettingsWrap
{
	private static Type classType = typeof(QualitySettings);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[7]
		{
			new LuaMethod("GetQualityLevel", GetQualityLevel),
			new LuaMethod("SetQualityLevel", SetQualityLevel),
			new LuaMethod("IncreaseLevel", IncreaseLevel),
			new LuaMethod("DecreaseLevel", DecreaseLevel),
			new LuaMethod("New", _CreateQualitySettings),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[22]
		{
			new LuaField("names", get_names, null),
			new LuaField("pixelLightCount", get_pixelLightCount, set_pixelLightCount),
			new LuaField("shadowProjection", get_shadowProjection, set_shadowProjection),
			new LuaField("shadowCascades", get_shadowCascades, set_shadowCascades),
			new LuaField("shadowDistance", get_shadowDistance, set_shadowDistance),
			new LuaField("shadowNearPlaneOffset", get_shadowNearPlaneOffset, set_shadowNearPlaneOffset),
			new LuaField("shadowCascade2Split", get_shadowCascade2Split, set_shadowCascade2Split),
			new LuaField("shadowCascade4Split", get_shadowCascade4Split, set_shadowCascade4Split),
			new LuaField("masterTextureLimit", get_masterTextureLimit, set_masterTextureLimit),
			new LuaField("anisotropicFiltering", get_anisotropicFiltering, set_anisotropicFiltering),
			new LuaField("lodBias", get_lodBias, set_lodBias),
			new LuaField("maximumLODLevel", get_maximumLODLevel, set_maximumLODLevel),
			new LuaField("particleRaycastBudget", get_particleRaycastBudget, set_particleRaycastBudget),
			new LuaField("softVegetation", get_softVegetation, set_softVegetation),
			new LuaField("realtimeReflectionProbes", get_realtimeReflectionProbes, set_realtimeReflectionProbes),
			new LuaField("billboardsFaceCameraPosition", get_billboardsFaceCameraPosition, set_billboardsFaceCameraPosition),
			new LuaField("maxQueuedFrames", get_maxQueuedFrames, set_maxQueuedFrames),
			new LuaField("vSyncCount", get_vSyncCount, set_vSyncCount),
			new LuaField("antiAliasing", get_antiAliasing, set_antiAliasing),
			new LuaField("desiredColorSpace", get_desiredColorSpace, null),
			new LuaField("activeColorSpace", get_activeColorSpace, null),
			new LuaField("blendWeights", get_blendWeights, set_blendWeights)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.QualitySettings", typeof(QualitySettings), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateQualitySettings(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			QualitySettings obj = new QualitySettings();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_names(IntPtr L)
	{
		LuaScriptMgr.PushArray(L, QualitySettings.names);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_pixelLightCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.pixelLightCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowProjection(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowProjection);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCascades(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowCascades);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowDistance(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowDistance);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowNearPlaneOffset(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowNearPlaneOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCascade2Split(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowCascade2Split);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCascade4Split(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.shadowCascade4Split);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_masterTextureLimit(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.masterTextureLimit);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_anisotropicFiltering(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.anisotropicFiltering);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lodBias(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.lodBias);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maximumLODLevel(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.maximumLODLevel);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particleRaycastBudget(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.particleRaycastBudget);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_softVegetation(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.softVegetation);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_realtimeReflectionProbes(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.realtimeReflectionProbes);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_billboardsFaceCameraPosition(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.billboardsFaceCameraPosition);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxQueuedFrames(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.maxQueuedFrames);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_vSyncCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.vSyncCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_antiAliasing(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.antiAliasing);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_desiredColorSpace(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.desiredColorSpace);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_activeColorSpace(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.activeColorSpace);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_blendWeights(IntPtr L)
	{
		LuaScriptMgr.Push(L, QualitySettings.blendWeights);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_pixelLightCount(IntPtr L)
	{
		QualitySettings.pixelLightCount = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowProjection(IntPtr L)
	{
		QualitySettings.shadowProjection = (ShadowProjection)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(ShadowProjection));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCascades(IntPtr L)
	{
		QualitySettings.shadowCascades = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowDistance(IntPtr L)
	{
		QualitySettings.shadowDistance = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowNearPlaneOffset(IntPtr L)
	{
		QualitySettings.shadowNearPlaneOffset = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCascade2Split(IntPtr L)
	{
		QualitySettings.shadowCascade2Split = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCascade4Split(IntPtr L)
	{
		QualitySettings.shadowCascade4Split = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_masterTextureLimit(IntPtr L)
	{
		QualitySettings.masterTextureLimit = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_anisotropicFiltering(IntPtr L)
	{
		QualitySettings.anisotropicFiltering = (AnisotropicFiltering)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(AnisotropicFiltering));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lodBias(IntPtr L)
	{
		QualitySettings.lodBias = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maximumLODLevel(IntPtr L)
	{
		QualitySettings.maximumLODLevel = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_particleRaycastBudget(IntPtr L)
	{
		QualitySettings.particleRaycastBudget = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_softVegetation(IntPtr L)
	{
		QualitySettings.softVegetation = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_realtimeReflectionProbes(IntPtr L)
	{
		QualitySettings.realtimeReflectionProbes = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_billboardsFaceCameraPosition(IntPtr L)
	{
		QualitySettings.billboardsFaceCameraPosition = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxQueuedFrames(IntPtr L)
	{
		QualitySettings.maxQueuedFrames = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_vSyncCount(IntPtr L)
	{
		QualitySettings.vSyncCount = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_antiAliasing(IntPtr L)
	{
		QualitySettings.antiAliasing = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_blendWeights(IntPtr L)
	{
		QualitySettings.blendWeights = (BlendWeights)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(BlendWeights));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetQualityLevel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		int qualityLevel = QualitySettings.GetQualityLevel();
		LuaScriptMgr.Push(L, qualityLevel);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetQualityLevel(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			int qualityLevel = (int)LuaScriptMgr.GetNumber(L, 1);
			QualitySettings.SetQualityLevel(qualityLevel);
			return 0;
		}
		case 2:
		{
			int index = (int)LuaScriptMgr.GetNumber(L, 1);
			bool boolean = LuaScriptMgr.GetBoolean(L, 2);
			QualitySettings.SetQualityLevel(index, boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.SetQualityLevel");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IncreaseLevel(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 0:
			QualitySettings.IncreaseLevel();
			return 0;
		case 1:
		{
			bool boolean = LuaScriptMgr.GetBoolean(L, 1);
			QualitySettings.IncreaseLevel(boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.IncreaseLevel");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DecreaseLevel(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 0:
			QualitySettings.DecreaseLevel();
			return 0;
		case 1:
		{
			bool boolean = LuaScriptMgr.GetBoolean(L, 1);
			QualitySettings.DecreaseLevel(boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: QualitySettings.DecreaseLevel");
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
