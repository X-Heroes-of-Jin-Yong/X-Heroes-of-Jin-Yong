using System;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;
using UnityEngine.Rendering;

public class RendererWrap
{
	private static Type classType = typeof(Renderer);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[6]
		{
			new LuaMethod("SetPropertyBlock", SetPropertyBlock),
			new LuaMethod("GetPropertyBlock", GetPropertyBlock),
			new LuaMethod("GetClosestReflectionProbes", GetClosestReflectionProbes),
			new LuaMethod("New", _CreateRenderer),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[22]
		{
			new LuaField("isPartOfStaticBatch", get_isPartOfStaticBatch, null),
			new LuaField("worldToLocalMatrix", get_worldToLocalMatrix, null),
			new LuaField("localToWorldMatrix", get_localToWorldMatrix, null),
			new LuaField("enabled", get_enabled, set_enabled),
			new LuaField("shadowCastingMode", get_shadowCastingMode, set_shadowCastingMode),
			new LuaField("receiveShadows", get_receiveShadows, set_receiveShadows),
			new LuaField("material", get_material, set_material),
			new LuaField("sharedMaterial", get_sharedMaterial, set_sharedMaterial),
			new LuaField("materials", get_materials, set_materials),
			new LuaField("sharedMaterials", get_sharedMaterials, set_sharedMaterials),
			new LuaField("bounds", get_bounds, null),
			new LuaField("lightmapIndex", get_lightmapIndex, set_lightmapIndex),
			new LuaField("realtimeLightmapIndex", get_realtimeLightmapIndex, set_realtimeLightmapIndex),
			new LuaField("lightmapScaleOffset", get_lightmapScaleOffset, set_lightmapScaleOffset),
			new LuaField("realtimeLightmapScaleOffset", get_realtimeLightmapScaleOffset, set_realtimeLightmapScaleOffset),
			new LuaField("isVisible", get_isVisible, null),
			new LuaField("useLightProbes", get_useLightProbes, set_useLightProbes),
			new LuaField("probeAnchor", get_probeAnchor, set_probeAnchor),
			new LuaField("reflectionProbeUsage", get_reflectionProbeUsage, set_reflectionProbeUsage),
			new LuaField("sortingLayerName", get_sortingLayerName, set_sortingLayerName),
			new LuaField("sortingLayerID", get_sortingLayerID, set_sortingLayerID),
			new LuaField("sortingOrder", get_sortingOrder, set_sortingOrder)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Renderer", typeof(Renderer), regs, fields, typeof(Component));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateRenderer(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Renderer obj = new Renderer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Renderer.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPartOfStaticBatch(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPartOfStaticBatch");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPartOfStaticBatch on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.isPartOfStaticBatch);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldToLocalMatrix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldToLocalMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldToLocalMatrix on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, renderer.worldToLocalMatrix);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localToWorldMatrix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localToWorldMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localToWorldMatrix on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, renderer.localToWorldMatrix);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.enabled);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shadowCastingMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowCastingMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowCastingMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.shadowCastingMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_receiveShadows(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name receiveShadows");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index receiveShadows on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.receiveShadows);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_material(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name material");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index material on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.material);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sharedMaterial(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterial");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterial on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sharedMaterial);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_materials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name materials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index materials on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, renderer.materials);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sharedMaterials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterials on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, renderer.sharedMaterials);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bounds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bounds on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.bounds);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapIndex on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.lightmapIndex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_realtimeLightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapIndex on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.realtimeLightmapIndex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapScaleOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.lightmapScaleOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_realtimeLightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapScaleOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.realtimeLightmapScaleOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isVisible(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isVisible on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.isVisible);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useLightProbes(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useLightProbes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useLightProbes on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.useLightProbes);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_probeAnchor(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name probeAnchor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index probeAnchor on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.probeAnchor);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_reflectionProbeUsage(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reflectionProbeUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reflectionProbeUsage on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.reflectionProbeUsage);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sortingLayerName(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sortingLayerName);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sortingLayerID(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sortingLayerID);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sortingOrder(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderer.sortingOrder);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}
		renderer.enabled = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shadowCastingMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shadowCastingMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shadowCastingMode on a nil value");
			}
		}
		renderer.shadowCastingMode = (ShadowCastingMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(ShadowCastingMode));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_receiveShadows(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name receiveShadows");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index receiveShadows on a nil value");
			}
		}
		renderer.receiveShadows = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_material(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name material");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index material on a nil value");
			}
		}
		renderer.material = (Material)LuaScriptMgr.GetUnityObject(L, 3, typeof(Material));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sharedMaterial(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterial");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterial on a nil value");
			}
		}
		renderer.sharedMaterial = (Material)LuaScriptMgr.GetUnityObject(L, 3, typeof(Material));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_materials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name materials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index materials on a nil value");
			}
		}
		renderer.materials = LuaScriptMgr.GetArrayObject<Material>(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sharedMaterials(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMaterials");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMaterials on a nil value");
			}
		}
		renderer.sharedMaterials = LuaScriptMgr.GetArrayObject<Material>(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapIndex on a nil value");
			}
		}
		renderer.lightmapIndex = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_realtimeLightmapIndex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapIndex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapIndex on a nil value");
			}
		}
		renderer.realtimeLightmapIndex = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lightmapScaleOffset on a nil value");
			}
		}
		renderer.lightmapScaleOffset = LuaScriptMgr.GetVector4(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_realtimeLightmapScaleOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name realtimeLightmapScaleOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index realtimeLightmapScaleOffset on a nil value");
			}
		}
		renderer.realtimeLightmapScaleOffset = LuaScriptMgr.GetVector4(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useLightProbes(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useLightProbes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useLightProbes on a nil value");
			}
		}
		renderer.useLightProbes = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_probeAnchor(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name probeAnchor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index probeAnchor on a nil value");
			}
		}
		renderer.probeAnchor = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_reflectionProbeUsage(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name reflectionProbeUsage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index reflectionProbeUsage on a nil value");
			}
		}
		renderer.reflectionProbeUsage = (ReflectionProbeUsage)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(ReflectionProbeUsage));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sortingLayerName(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}
		renderer.sortingLayerName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sortingLayerID(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}
		renderer.sortingLayerID = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sortingOrder(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Renderer renderer = (Renderer)luaObject;
		if (renderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}
		renderer.sortingOrder = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPropertyBlock(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Renderer renderer = (Renderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Renderer");
		MaterialPropertyBlock propertyBlock = (MaterialPropertyBlock)LuaScriptMgr.GetNetObject(L, 2, typeof(MaterialPropertyBlock));
		renderer.SetPropertyBlock(propertyBlock);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetPropertyBlock(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Renderer renderer = (Renderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Renderer");
		MaterialPropertyBlock dest = (MaterialPropertyBlock)LuaScriptMgr.GetNetObject(L, 2, typeof(MaterialPropertyBlock));
		renderer.GetPropertyBlock(dest);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClosestReflectionProbes(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Renderer renderer = (Renderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Renderer");
		List<ReflectionProbeBlendInfo> result = (List<ReflectionProbeBlendInfo>)LuaScriptMgr.GetNetObject(L, 2, typeof(List<ReflectionProbeBlendInfo>));
		renderer.GetClosestReflectionProbes(result);
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
