using System;
using LuaInterface;
using UnityEngine;

public class MeshColliderWrap
{
	private static Type classType = typeof(MeshCollider);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[3]
		{
			new LuaMethod("New", _CreateMeshCollider),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[2]
		{
			new LuaField("sharedMesh", get_sharedMesh, set_sharedMesh),
			new LuaField("convex", get_convex, set_convex)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.MeshCollider", typeof(MeshCollider), regs, fields, typeof(Collider));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateMeshCollider(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			MeshCollider obj = new MeshCollider();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: MeshCollider.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sharedMesh(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MeshCollider meshCollider = (MeshCollider)luaObject;
		if (meshCollider == null)
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
		LuaScriptMgr.Push(L, meshCollider.sharedMesh);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_convex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MeshCollider meshCollider = (MeshCollider)luaObject;
		if (meshCollider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name convex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index convex on a nil value");
			}
		}
		LuaScriptMgr.Push(L, meshCollider.convex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sharedMesh(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MeshCollider meshCollider = (MeshCollider)luaObject;
		if (meshCollider == null)
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
		meshCollider.sharedMesh = (Mesh)LuaScriptMgr.GetUnityObject(L, 3, typeof(Mesh));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_convex(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MeshCollider meshCollider = (MeshCollider)luaObject;
		if (meshCollider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name convex");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index convex on a nil value");
			}
		}
		meshCollider.convex = LuaScriptMgr.GetBoolean(L, 3);
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
