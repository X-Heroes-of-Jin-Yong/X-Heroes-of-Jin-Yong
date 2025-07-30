using System;
using LuaInterface;
using UnityEngine;

public class SphereColliderWrap
{
	private static Type classType = typeof(SphereCollider);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[3]
		{
			new LuaMethod("New", _CreateSphereCollider),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[2]
		{
			new LuaField("center", get_center, set_center),
			new LuaField("radius", get_radius, set_radius)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.SphereCollider", typeof(SphereCollider), regs, fields, typeof(Collider));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateSphereCollider(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			SphereCollider obj = new SphereCollider();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: SphereCollider.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_center(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SphereCollider sphereCollider = (SphereCollider)luaObject;
		if (sphereCollider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name center");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index center on a nil value");
			}
		}
		LuaScriptMgr.Push(L, sphereCollider.center);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_radius(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SphereCollider sphereCollider = (SphereCollider)luaObject;
		if (sphereCollider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name radius");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index radius on a nil value");
			}
		}
		LuaScriptMgr.Push(L, sphereCollider.radius);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_center(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SphereCollider sphereCollider = (SphereCollider)luaObject;
		if (sphereCollider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name center");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index center on a nil value");
			}
		}
		sphereCollider.center = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_radius(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		SphereCollider sphereCollider = (SphereCollider)luaObject;
		if (sphereCollider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name radius");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index radius on a nil value");
			}
		}
		sphereCollider.radius = (float)LuaScriptMgr.GetNumber(L, 3);
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
