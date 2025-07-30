using System;
using LuaInterface;
using UnityEngine;

public class MeshRendererWrap
{
	private static Type classType = typeof(MeshRenderer);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[3]
		{
			new LuaMethod("New", _CreateMeshRenderer),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[1]
		{
			new LuaField("additionalVertexStreams", get_additionalVertexStreams, set_additionalVertexStreams)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.MeshRenderer", typeof(MeshRenderer), regs, fields, typeof(Renderer));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateMeshRenderer(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			MeshRenderer obj = new MeshRenderer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: MeshRenderer.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_additionalVertexStreams(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MeshRenderer meshRenderer = (MeshRenderer)luaObject;
		if (meshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name additionalVertexStreams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index additionalVertexStreams on a nil value");
			}
		}
		LuaScriptMgr.Push(L, meshRenderer.additionalVertexStreams);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_additionalVertexStreams(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MeshRenderer meshRenderer = (MeshRenderer)luaObject;
		if (meshRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name additionalVertexStreams");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index additionalVertexStreams on a nil value");
			}
		}
		meshRenderer.additionalVertexStreams = (Mesh)LuaScriptMgr.GetUnityObject(L, 3, typeof(Mesh));
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
