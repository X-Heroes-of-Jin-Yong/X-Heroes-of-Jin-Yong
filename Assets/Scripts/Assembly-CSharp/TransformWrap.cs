using System;
using System.Collections;
using LuaInterface;
using UnityEngine;

public class TransformWrap
{
	private static Type classType = typeof(Transform);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[24]
		{
			new LuaMethod("SetParent", SetParent),
			new LuaMethod("Translate", Translate),
			new LuaMethod("Rotate", Rotate),
			new LuaMethod("RotateAround", RotateAround),
			new LuaMethod("LookAt", LookAt),
			new LuaMethod("TransformDirection", TransformDirection),
			new LuaMethod("InverseTransformDirection", InverseTransformDirection),
			new LuaMethod("TransformVector", TransformVector),
			new LuaMethod("InverseTransformVector", InverseTransformVector),
			new LuaMethod("TransformPoint", TransformPoint),
			new LuaMethod("InverseTransformPoint", InverseTransformPoint),
			new LuaMethod("DetachChildren", DetachChildren),
			new LuaMethod("SetAsFirstSibling", SetAsFirstSibling),
			new LuaMethod("SetAsLastSibling", SetAsLastSibling),
			new LuaMethod("SetSiblingIndex", SetSiblingIndex),
			new LuaMethod("GetSiblingIndex", GetSiblingIndex),
			new LuaMethod("Find", Find),
			new LuaMethod("IsChildOf", IsChildOf),
			new LuaMethod("FindChild", FindChild),
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetChild", GetChild),
			new LuaMethod("New", _CreateTransform),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[17]
		{
			new LuaField("position", get_position, set_position),
			new LuaField("localPosition", get_localPosition, set_localPosition),
			new LuaField("eulerAngles", get_eulerAngles, set_eulerAngles),
			new LuaField("localEulerAngles", get_localEulerAngles, set_localEulerAngles),
			new LuaField("right", get_right, set_right),
			new LuaField("up", get_up, set_up),
			new LuaField("forward", get_forward, set_forward),
			new LuaField("rotation", get_rotation, set_rotation),
			new LuaField("localRotation", get_localRotation, set_localRotation),
			new LuaField("localScale", get_localScale, set_localScale),
			new LuaField("parent", get_parent, set_parent),
			new LuaField("worldToLocalMatrix", get_worldToLocalMatrix, null),
			new LuaField("localToWorldMatrix", get_localToWorldMatrix, null),
			new LuaField("root", get_root, null),
			new LuaField("childCount", get_childCount, null),
			new LuaField("lossyScale", get_lossyScale, null),
			new LuaField("hasChanged", get_hasChanged, set_hasChanged)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Transform", typeof(Transform), regs, fields, typeof(Component));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateTransform(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Transform class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_position(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.position);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localPosition(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localPosition on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.localPosition);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_eulerAngles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eulerAngles on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.eulerAngles);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localEulerAngles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localEulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localEulerAngles on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.localEulerAngles);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_right(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name right");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index right on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.right);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_up(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name up");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index up on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.up);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_forward(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forward");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forward on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.forward);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rotation(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotation on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.rotation);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localRotation(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localRotation on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.localRotation);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.localScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_parent(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parent on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.parent);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldToLocalMatrix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
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
		LuaScriptMgr.PushValue(L, transform.worldToLocalMatrix);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localToWorldMatrix(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
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
		LuaScriptMgr.PushValue(L, transform.localToWorldMatrix);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_root(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name root");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index root on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.root);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_childCount(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name childCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index childCount on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.childCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lossyScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lossyScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lossyScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.lossyScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hasChanged(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasChanged on a nil value");
			}
		}
		LuaScriptMgr.Push(L, transform.hasChanged);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_position(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}
		transform.position = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localPosition(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localPosition on a nil value");
			}
		}
		transform.localPosition = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_eulerAngles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eulerAngles on a nil value");
			}
		}
		transform.eulerAngles = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localEulerAngles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localEulerAngles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localEulerAngles on a nil value");
			}
		}
		transform.localEulerAngles = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_right(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name right");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index right on a nil value");
			}
		}
		transform.right = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_up(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name up");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index up on a nil value");
			}
		}
		transform.up = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_forward(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name forward");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index forward on a nil value");
			}
		}
		transform.forward = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rotation(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rotation on a nil value");
			}
		}
		transform.rotation = LuaScriptMgr.GetQuaternion(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localRotation(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localRotation on a nil value");
			}
		}
		transform.localRotation = LuaScriptMgr.GetQuaternion(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localScale on a nil value");
			}
		}
		transform.localScale = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_parent(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parent");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parent on a nil value");
			}
		}
		transform.parent = (Transform)LuaScriptMgr.GetUnityObject(L, 3, typeof(Transform));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_hasChanged(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Transform transform = (Transform)luaObject;
		if (transform == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasChanged on a nil value");
			}
		}
		transform.hasChanged = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetParent(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform parent2 = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			transform2.SetParent(parent2);
			return 0;
		}
		case 3:
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform parent = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			transform.SetParent(parent, boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.SetParent");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Translate(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			transform2.Translate(vector2);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(Transform)))
			{
				Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
				Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
				Transform relativeTo = (Transform)LuaScriptMgr.GetLuaObject(L, 3);
				transform.Translate(vector, relativeTo);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(Space)))
		{
			Transform transform3 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 2);
			Space relativeTo2 = (Space)(int)LuaScriptMgr.GetLuaObject(L, 3);
			transform3.Translate(vector3, relativeTo2);
			return 0;
		}
		switch (num)
		{
		case 4:
		{
			Transform transform5 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x2 = (float)LuaScriptMgr.GetNumber(L, 2);
			float y2 = (float)LuaScriptMgr.GetNumber(L, 3);
			float z2 = (float)LuaScriptMgr.GetNumber(L, 4);
			transform5.Translate(x2, y2, z2);
			return 0;
		}
		case 5:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float), typeof(Transform)))
			{
				Transform transform4 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
				float x = (float)LuaDLL.lua_tonumber(L, 2);
				float y = (float)LuaDLL.lua_tonumber(L, 3);
				float z = (float)LuaDLL.lua_tonumber(L, 4);
				Transform relativeTo3 = (Transform)LuaScriptMgr.GetLuaObject(L, 5);
				transform4.Translate(x, y, z, relativeTo3);
				return 0;
			}
			break;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float), typeof(Space)))
		{
			Transform transform6 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x3 = (float)LuaDLL.lua_tonumber(L, 2);
			float y3 = (float)LuaDLL.lua_tonumber(L, 3);
			float z3 = (float)LuaDLL.lua_tonumber(L, 4);
			Space relativeTo4 = (Space)(int)LuaScriptMgr.GetLuaObject(L, 5);
			transform6.Translate(x3, y3, z3, relativeTo4);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Transform.Translate");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Rotate(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			transform2.Rotate(vector2);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(float)))
			{
				Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
				Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
				float angle = (float)LuaDLL.lua_tonumber(L, 3);
				transform.Rotate(vector, angle);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(Space)))
		{
			Transform transform3 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 2);
			Space relativeTo = (Space)(int)LuaScriptMgr.GetLuaObject(L, 3);
			transform3.Rotate(vector3, relativeTo);
			return 0;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(float), typeof(Space)))
		{
			Transform transform4 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			float angle2 = (float)LuaDLL.lua_tonumber(L, 3);
			Space relativeTo2 = (Space)(int)LuaScriptMgr.GetLuaObject(L, 4);
			transform4.Rotate(vector4, angle2, relativeTo2);
			return 0;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(float), typeof(float), typeof(float)))
		{
			Transform transform5 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float xAngle = (float)LuaDLL.lua_tonumber(L, 2);
			float yAngle = (float)LuaDLL.lua_tonumber(L, 3);
			float zAngle = (float)LuaDLL.lua_tonumber(L, 4);
			transform5.Rotate(xAngle, yAngle, zAngle);
			return 0;
		}
		if (num == 5)
		{
			Transform transform6 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float xAngle2 = (float)LuaScriptMgr.GetNumber(L, 2);
			float yAngle2 = (float)LuaScriptMgr.GetNumber(L, 3);
			float zAngle2 = (float)LuaScriptMgr.GetNumber(L, 4);
			Space relativeTo3 = (Space)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(Space));
			transform6.Rotate(xAngle2, yAngle2, zAngle2, relativeTo3);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Transform.Rotate");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RotateAround(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
		Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
		float angle = (float)LuaScriptMgr.GetNumber(L, 4);
		transform.RotateAround(vector, vector2, angle);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LookAt(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable)))
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			transform.LookAt(vector);
			return 0;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(Transform)))
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform target = (Transform)LuaScriptMgr.GetLuaObject(L, 2);
			transform2.LookAt(target);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(LuaTable), typeof(LuaTable)))
		{
			Transform transform3 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 3);
			transform3.LookAt(vector2, vector3);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Transform), typeof(Transform), typeof(LuaTable)))
		{
			Transform transform4 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Transform target2 = (Transform)LuaScriptMgr.GetLuaObject(L, 2);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 3);
			transform4.LookAt(target2, vector4);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Transform.LookAt");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TransformDirection(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Vector3 v2 = transform2.TransformDirection(vector);
			LuaScriptMgr.Push(L, v2);
			return 1;
		}
		case 4:
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x = (float)LuaScriptMgr.GetNumber(L, 2);
			float y = (float)LuaScriptMgr.GetNumber(L, 3);
			float z = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 v = transform.TransformDirection(x, y, z);
			LuaScriptMgr.Push(L, v);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.TransformDirection");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InverseTransformDirection(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Vector3 v2 = transform2.InverseTransformDirection(vector);
			LuaScriptMgr.Push(L, v2);
			return 1;
		}
		case 4:
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x = (float)LuaScriptMgr.GetNumber(L, 2);
			float y = (float)LuaScriptMgr.GetNumber(L, 3);
			float z = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 v = transform.InverseTransformDirection(x, y, z);
			LuaScriptMgr.Push(L, v);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.InverseTransformDirection");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TransformVector(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Vector3 v2 = transform2.TransformVector(vector);
			LuaScriptMgr.Push(L, v2);
			return 1;
		}
		case 4:
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x = (float)LuaScriptMgr.GetNumber(L, 2);
			float y = (float)LuaScriptMgr.GetNumber(L, 3);
			float z = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 v = transform.TransformVector(x, y, z);
			LuaScriptMgr.Push(L, v);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.TransformVector");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InverseTransformVector(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Vector3 v2 = transform2.InverseTransformVector(vector);
			LuaScriptMgr.Push(L, v2);
			return 1;
		}
		case 4:
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x = (float)LuaScriptMgr.GetNumber(L, 2);
			float y = (float)LuaScriptMgr.GetNumber(L, 3);
			float z = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 v = transform.InverseTransformVector(x, y, z);
			LuaScriptMgr.Push(L, v);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.InverseTransformVector");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TransformPoint(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Vector3 v2 = transform2.TransformPoint(vector);
			LuaScriptMgr.Push(L, v2);
			return 1;
		}
		case 4:
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x = (float)LuaScriptMgr.GetNumber(L, 2);
			float y = (float)LuaScriptMgr.GetNumber(L, 3);
			float z = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 v = transform.TransformPoint(x, y, z);
			LuaScriptMgr.Push(L, v);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.TransformPoint");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InverseTransformPoint(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Transform transform2 = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Vector3 v2 = transform2.InverseTransformPoint(vector);
			LuaScriptMgr.Push(L, v2);
			return 1;
		}
		case 4:
		{
			Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
			float x = (float)LuaScriptMgr.GetNumber(L, 2);
			float y = (float)LuaScriptMgr.GetNumber(L, 3);
			float z = (float)LuaScriptMgr.GetNumber(L, 4);
			Vector3 v = transform.InverseTransformPoint(x, y, z);
			LuaScriptMgr.Push(L, v);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Transform.InverseTransformPoint");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DetachChildren(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		transform.DetachChildren();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetAsFirstSibling(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		transform.SetAsFirstSibling();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetAsLastSibling(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		transform.SetAsLastSibling();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetSiblingIndex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		int siblingIndex = (int)LuaScriptMgr.GetNumber(L, 2);
		transform.SetSiblingIndex(siblingIndex);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetSiblingIndex(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		int siblingIndex = transform.GetSiblingIndex();
		LuaScriptMgr.Push(L, siblingIndex);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Find(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Transform obj = transform.Find(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsChildOf(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		Transform parent = (Transform)LuaScriptMgr.GetUnityObject(L, 2, typeof(Transform));
		bool b = transform.IsChildOf(parent);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Transform obj = transform.Find(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		IEnumerator enumerator = transform.GetEnumerator();
		LuaScriptMgr.Push(L, enumerator);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetChild(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Transform transform = (Transform)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Transform");
		int index = (int)LuaScriptMgr.GetNumber(L, 2);
		Transform child = transform.GetChild(index);
		LuaScriptMgr.Push(L, child);
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
