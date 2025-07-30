using System;
using LuaInterface;
using UnityEngine;

public class ObjectWrap
{
	private static Type classType = typeof(UnityEngine.Object);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[15]
		{
			new LuaMethod("FindObjectsOfType", FindObjectsOfType),
			new LuaMethod("DontDestroyOnLoad", DontDestroyOnLoad),
			new LuaMethod("ToString", ToString),
			new LuaMethod("Equals", Equals),
			new LuaMethod("GetHashCode", GetHashCode),
			new LuaMethod("GetInstanceID", GetInstanceID),
			new LuaMethod("Instantiate", Instantiate),
			new LuaMethod("FindObjectOfType", FindObjectOfType),
			new LuaMethod("DestroyObject", DestroyObject),
			new LuaMethod("DestroyImmediate", DestroyImmediate),
			new LuaMethod("Destroy", Destroy),
			new LuaMethod("New", _CreateObject),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__tostring", Lua_ToString),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[2]
		{
			new LuaField("name", get_name, set_name),
			new LuaField("hideFlags", get_hideFlags, set_hideFlags)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Object", typeof(UnityEngine.Object), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateObject(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			UnityEngine.Object obj = new UnityEngine.Object();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Object.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_name(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UnityEngine.Object obj = (UnityEngine.Object)luaObject;
		if (obj == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}
		LuaScriptMgr.Push(L, obj.name);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_hideFlags(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UnityEngine.Object obj = (UnityEngine.Object)luaObject;
		if (obj == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hideFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hideFlags on a nil value");
			}
		}
		LuaScriptMgr.Push(L, obj.hideFlags);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_name(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UnityEngine.Object obj = (UnityEngine.Object)luaObject;
		if (obj == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}
		obj.name = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_hideFlags(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		UnityEngine.Object obj = (UnityEngine.Object)luaObject;
		if (obj == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hideFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hideFlags on a nil value");
			}
		}
		obj.hideFlags = (HideFlags)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(HideFlags));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_ToString(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		if (luaObject != null)
		{
			LuaScriptMgr.Push(L, luaObject.ToString());
		}
		else
		{
			LuaScriptMgr.Push(L, "Table: UnityEngine.Object");
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindObjectsOfType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		UnityEngine.Object[] o = UnityEngine.Object.FindObjectsOfType(typeObject);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DontDestroyOnLoad(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEngine.Object unityObject = LuaScriptMgr.GetUnityObject(L, 1, typeof(UnityEngine.Object));
		UnityEngine.Object.DontDestroyOnLoad(unityObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToString(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEngine.Object obj = (UnityEngine.Object)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Object");
		string str = obj.ToString();
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Equals(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object obj = LuaScriptMgr.GetVarObject(L, 1) as UnityEngine.Object;
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		bool b = ((!(obj != null)) ? (varObject == null) : obj.Equals(varObject));
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetHashCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEngine.Object obj = (UnityEngine.Object)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Object");
		int hashCode = obj.GetHashCode();
		LuaScriptMgr.Push(L, hashCode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetInstanceID(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UnityEngine.Object obj = (UnityEngine.Object)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Object");
		int instanceID = obj.GetInstanceID();
		LuaScriptMgr.Push(L, instanceID);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Instantiate(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			UnityEngine.Object unityObject2 = LuaScriptMgr.GetUnityObject(L, 1, typeof(UnityEngine.Object));
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(unityObject2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		case 3:
		{
			UnityEngine.Object unityObject = LuaScriptMgr.GetUnityObject(L, 1, typeof(UnityEngine.Object));
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Quaternion quaternion = LuaScriptMgr.GetQuaternion(L, 3);
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(unityObject, vector, quaternion);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.Instantiate");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindObjectOfType(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 1);
		UnityEngine.Object obj = UnityEngine.Object.FindObjectOfType(typeObject);
		LuaScriptMgr.Push(L, obj);
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

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DestroyObject(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			UnityEngine.Object obj2 = (UnityEngine.Object)LuaScriptMgr.GetLuaObject(L, 1);
			LuaScriptMgr.__gc(L);
			UnityEngine.Object.DestroyObject(obj2);
			return 0;
		}
		case 2:
		{
			UnityEngine.Object obj = (UnityEngine.Object)LuaScriptMgr.GetLuaObject(L, 1);
			float t = (float)LuaScriptMgr.GetNumber(L, 2);
			UnityEngine.Object.DestroyObject(obj, t);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.DestroyObject");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DestroyImmediate(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			UnityEngine.Object obj2 = (UnityEngine.Object)LuaScriptMgr.GetLuaObject(L, 1);
			LuaScriptMgr.__gc(L);
			UnityEngine.Object.DestroyImmediate(obj2);
			return 0;
		}
		case 2:
		{
			UnityEngine.Object obj = (UnityEngine.Object)LuaScriptMgr.GetLuaObject(L, 1);
			bool boolean = LuaScriptMgr.GetBoolean(L, 2);
			UnityEngine.Object.DestroyImmediate(obj, boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.DestroyImmediate");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Destroy(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			UnityEngine.Object obj2 = (UnityEngine.Object)LuaScriptMgr.GetLuaObject(L, 1);
			LuaScriptMgr.__gc(L);
			UnityEngine.Object.Destroy(obj2);
			return 0;
		}
		case 2:
		{
			UnityEngine.Object obj = (UnityEngine.Object)LuaScriptMgr.GetLuaObject(L, 1);
			float t = (float)LuaScriptMgr.GetNumber(L, 2);
			UnityEngine.Object.Destroy(obj, t);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.Destroy");
			return 0;
		}
	}
}
