using System;
using LuaInterface;
using UnityEngine;

public class ColliderWrap
{
	private static Type classType = typeof(Collider);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[5]
		{
			new LuaMethod("ClosestPointOnBounds", ClosestPointOnBounds),
			new LuaMethod("Raycast", Raycast),
			new LuaMethod("New", _CreateCollider),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[7]
		{
			new LuaField("enabled", get_enabled, set_enabled),
			new LuaField("attachedRigidbody", get_attachedRigidbody, null),
			new LuaField("isTrigger", get_isTrigger, set_isTrigger),
			new LuaField("contactOffset", get_contactOffset, set_contactOffset),
			new LuaField("material", get_material, set_material),
			new LuaField("sharedMaterial", get_sharedMaterial, set_sharedMaterial),
			new LuaField("bounds", get_bounds, null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Collider", typeof(Collider), regs, fields, typeof(Component));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateCollider(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Collider obj = new Collider();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Collider.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
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
		LuaScriptMgr.Push(L, collider.enabled);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_attachedRigidbody(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name attachedRigidbody");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index attachedRigidbody on a nil value");
			}
		}
		LuaScriptMgr.Push(L, collider.attachedRigidbody);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isTrigger(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isTrigger");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isTrigger on a nil value");
			}
		}
		LuaScriptMgr.Push(L, collider.isTrigger);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_contactOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name contactOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index contactOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, collider.contactOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_material(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
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
		LuaScriptMgr.Push(L, collider.material);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sharedMaterial(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
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
		LuaScriptMgr.Push(L, collider.sharedMaterial);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bounds(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
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
		LuaScriptMgr.Push(L, collider.bounds);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
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
		collider.enabled = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isTrigger(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isTrigger");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isTrigger on a nil value");
			}
		}
		collider.isTrigger = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_contactOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name contactOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index contactOffset on a nil value");
			}
		}
		collider.contactOffset = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_material(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
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
		collider.material = (PhysicMaterial)LuaScriptMgr.GetUnityObject(L, 3, typeof(PhysicMaterial));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sharedMaterial(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Collider collider = (Collider)luaObject;
		if (collider == null)
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
		collider.sharedMaterial = (PhysicMaterial)LuaScriptMgr.GetUnityObject(L, 3, typeof(PhysicMaterial));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClosestPointOnBounds(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Collider collider = (Collider)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Collider");
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		Vector3 v = collider.ClosestPointOnBounds(vector);
		LuaScriptMgr.Push(L, v);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Raycast(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Collider collider = (Collider)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Collider");
		Ray ray = LuaScriptMgr.GetRay(L, 2);
		float maxDistance = (float)LuaScriptMgr.GetNumber(L, 4);
		RaycastHit hitInfo;
		bool b = collider.Raycast(ray, out hitInfo, maxDistance);
		LuaScriptMgr.Push(L, b);
		LuaScriptMgr.Push(L, hitInfo);
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
