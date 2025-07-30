using System;
using LuaInterface;
using UnityEngine;

public class SkinnedMeshRendererWrap
{
	private static Type classType = typeof(SkinnedMeshRenderer);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[6]
		{
			new LuaMethod("BakeMesh", BakeMesh),
			new LuaMethod("GetBlendShapeWeight", GetBlendShapeWeight),
			new LuaMethod("SetBlendShapeWeight", SetBlendShapeWeight),
			new LuaMethod("New", _CreateSkinnedMeshRenderer),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[6]
		{
			new LuaField("bones", get_bones, set_bones),
			new LuaField("rootBone", get_rootBone, set_rootBone),
			new LuaField("quality", get_quality, set_quality),
			new LuaField("sharedMesh", get_sharedMesh, set_sharedMesh),
			new LuaField("updateWhenOffscreen", get_updateWhenOffscreen, set_updateWhenOffscreen),
			new LuaField("localBounds", get_localBounds, set_localBounds)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.SkinnedMeshRenderer", typeof(SkinnedMeshRenderer), regs, fields, typeof(Renderer));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSkinnedMeshRenderer(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			SkinnedMeshRenderer obj = new SkinnedMeshRenderer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SkinnedMeshRenderer.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bones(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bones");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bones on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, skinnedMeshRenderer.bones);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rootBone(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootBone");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootBone on a nil value");
			}
		}
		LuaScriptMgr.Push(L, skinnedMeshRenderer.rootBone);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_quality(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}
		LuaScriptMgr.Push(L, skinnedMeshRenderer.quality);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sharedMesh(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMesh");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMesh on a nil value");
			}
		}
		LuaScriptMgr.Push(L, skinnedMeshRenderer.sharedMesh);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_updateWhenOffscreen(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name updateWhenOffscreen");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index updateWhenOffscreen on a nil value");
			}
		}
		LuaScriptMgr.Push(L, skinnedMeshRenderer.updateWhenOffscreen);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localBounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
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
		LuaScriptMgr.Push(L, skinnedMeshRenderer.localBounds);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bones(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bones");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bones on a nil value");
			}
		}
		skinnedMeshRenderer.bones = LuaScriptMgr.GetArrayObject<Transform>(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rootBone(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootBone");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootBone on a nil value");
			}
		}
		skinnedMeshRenderer.rootBone = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_quality(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name quality");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index quality on a nil value");
			}
		}
		skinnedMeshRenderer.quality = (SkinQuality)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(SkinQuality));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sharedMesh(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sharedMesh");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sharedMesh on a nil value");
			}
		}
		skinnedMeshRenderer.sharedMesh = (Mesh)LuaScriptMgr.GetUnityObject(L, 3, typeof(Mesh));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_updateWhenOffscreen(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name updateWhenOffscreen");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index updateWhenOffscreen on a nil value");
			}
		}
		skinnedMeshRenderer.updateWhenOffscreen = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localBounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)luaObject;
		if (skinnedMeshRenderer == null)
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
		skinnedMeshRenderer.localBounds = LuaScriptMgr.GetBounds(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BakeMesh(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SkinnedMeshRenderer");
		Mesh mesh = (Mesh)LuaScriptMgr.GetUnityObject(L, 2, typeof(Mesh));
		skinnedMeshRenderer.BakeMesh(mesh);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetBlendShapeWeight(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SkinnedMeshRenderer");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		float blendShapeWeight = skinnedMeshRenderer.GetBlendShapeWeight(index);
		LuaScriptMgr.Push(L, blendShapeWeight);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetBlendShapeWeight(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)LuaScriptMgr.GetUnityObjectSelf(L, 1, "SkinnedMeshRenderer");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		float value = (float)LuaScriptMgr.GetNumber(L, 3);
		skinnedMeshRenderer.SetBlendShapeWeight(index, value);
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
