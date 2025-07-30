using System;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class GameObjectWrap
{
	private static Type classType = typeof(GameObject);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[20]
		{
			new LuaMethod("CreatePrimitive", CreatePrimitive),
			new LuaMethod("GetComponent", GetComponent),
			new LuaMethod("GetComponentInChildren", GetComponentInChildren),
			new LuaMethod("GetComponentInParent", GetComponentInParent),
			new LuaMethod("GetComponents", GetComponents),
			new LuaMethod("GetComponentsInChildren", GetComponentsInChildren),
			new LuaMethod("GetComponentsInParent", GetComponentsInParent),
			new LuaMethod("SetActive", SetActive),
			new LuaMethod("CompareTag", CompareTag),
			new LuaMethod("FindGameObjectWithTag", FindGameObjectWithTag),
			new LuaMethod("FindWithTag", FindWithTag),
			new LuaMethod("FindGameObjectsWithTag", FindGameObjectsWithTag),
			new LuaMethod("SendMessageUpwards", SendMessageUpwards),
			new LuaMethod("SendMessage", SendMessage),
			new LuaMethod("BroadcastMessage", BroadcastMessage),
			new LuaMethod("AddComponent", AddComponent),
			new LuaMethod("Find", Find),
			new LuaMethod("New", _CreateGameObject),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[7]
		{
			new LuaField("transform", get_transform, null),
			new LuaField("layer", get_layer, set_layer),
			new LuaField("activeSelf", get_activeSelf, null),
			new LuaField("activeInHierarchy", get_activeInHierarchy, null),
			new LuaField("isStatic", get_isStatic, set_isStatic),
			new LuaField("tag", get_tag, set_tag),
			new LuaField("gameObject", get_gameObject, null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.GameObject", typeof(GameObject), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateGameObject(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 0:
		{
			GameObject obj3 = new GameObject();
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		case 1:
		{
			string name2 = LuaScriptMgr.GetString(L, 1);
			GameObject obj2 = new GameObject(name2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		default:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(string)) && LuaScriptMgr.CheckParamsType(L, typeof(Type), 2, num - 1))
			{
				string name = LuaScriptMgr.GetString(L, 1);
				Type[] paramsObject = LuaScriptMgr.GetParamsObject<Type>(L, 2, num - 1);
				GameObject obj = new GameObject(name, paramsObject);
				LuaScriptMgr.Push(L, obj);
				return 1;
			}
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.New");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_transform(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transform");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transform on a nil value");
			}
		}
		LuaScriptMgr.Push(L, gameObject.transform);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_layer(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layer on a nil value");
			}
		}
		LuaScriptMgr.Push(L, gameObject.layer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_activeSelf(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activeSelf");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activeSelf on a nil value");
			}
		}
		LuaScriptMgr.Push(L, gameObject.activeSelf);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_activeInHierarchy(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activeInHierarchy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activeInHierarchy on a nil value");
			}
		}
		LuaScriptMgr.Push(L, gameObject.activeInHierarchy);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isStatic(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isStatic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isStatic on a nil value");
			}
		}
		LuaScriptMgr.Push(L, gameObject.isStatic);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_tag(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tag on a nil value");
			}
		}
		LuaScriptMgr.Push(L, gameObject.tag);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gameObject(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameObject on a nil value");
			}
		}
		LuaScriptMgr.Push(L, gameObject.gameObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_layer(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layer on a nil value");
			}
		}
		gameObject.layer = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isStatic(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isStatic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isStatic on a nil value");
			}
		}
		gameObject.isStatic = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_tag(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		GameObject gameObject = (GameObject)luaObject;
		if (gameObject == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tag on a nil value");
			}
		}
		gameObject.tag = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreatePrimitive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		PrimitiveType type = (PrimitiveType)(int)LuaScriptMgr.GetNetObject(L, 1, typeof(PrimitiveType));
		GameObject obj = GameObject.CreatePrimitive(type);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponent(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string)))
		{
			GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string type = LuaScriptMgr.GetString(L, 2);
			Component component = gameObject.GetComponent(type);
			LuaScriptMgr.Push(L, component);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(Type)))
		{
			GameObject gameObject2 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			Component component2 = gameObject2.GetComponent(typeObject);
			LuaScriptMgr.Push(L, component2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponent");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentInChildren(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
		Component componentInChildren = gameObject.GetComponentInChildren(typeObject);
		LuaScriptMgr.Push(L, componentInChildren);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentInParent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
		Component componentInParent = gameObject.GetComponentInParent(typeObject);
		LuaScriptMgr.Push(L, componentInParent);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponents(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			GameObject gameObject2 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 2);
			Component[] components = gameObject2.GetComponents(typeObject2);
			LuaScriptMgr.PushArray(L, components);
			return 1;
		}
		case 3:
		{
			GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			List<Component> results = (List<Component>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Component>));
			gameObject.GetComponents(typeObject, results);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponents");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentsInChildren(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			GameObject gameObject2 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 2);
			Component[] componentsInChildren2 = gameObject2.GetComponentsInChildren(typeObject2);
			LuaScriptMgr.PushArray(L, componentsInChildren2);
			return 1;
		}
		case 3:
		{
			GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Component[] componentsInChildren = gameObject.GetComponentsInChildren(typeObject, boolean);
			LuaScriptMgr.PushArray(L, componentsInChildren);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponentsInChildren");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentsInParent(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			GameObject gameObject2 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 2);
			Component[] componentsInParent2 = gameObject2.GetComponentsInParent(typeObject2);
			LuaScriptMgr.PushArray(L, componentsInParent2);
			return 1;
		}
		case 3:
		{
			GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Component[] componentsInParent = gameObject.GetComponentsInParent(typeObject, boolean);
			LuaScriptMgr.PushArray(L, componentsInParent);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponentsInParent");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetActive(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
		bool boolean = LuaScriptMgr.GetBoolean(L, 2);
		gameObject.SetActive(boolean);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareTag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool b = gameObject.CompareTag(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindGameObjectWithTag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		GameObject obj = GameObject.FindGameObjectWithTag(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindWithTag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		GameObject obj = GameObject.FindWithTag(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int FindGameObjectsWithTag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		GameObject[] o = GameObject.FindGameObjectsWithTag(luaString);
		LuaScriptMgr.PushArray(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SendMessageUpwards(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			GameObject gameObject2 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			gameObject2.SendMessageUpwards(luaString);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
			{
				GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
				string methodName = LuaScriptMgr.GetString(L, 2);
				SendMessageOptions options = (SendMessageOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
				gameObject.SendMessageUpwards(methodName, options);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object)))
		{
			GameObject gameObject3 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string methodName2 = LuaScriptMgr.GetString(L, 2);
			object varObject = LuaScriptMgr.GetVarObject(L, 3);
			gameObject3.SendMessageUpwards(methodName2, varObject);
			return 0;
		}
		if (num == 4)
		{
			GameObject gameObject4 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			object varObject2 = LuaScriptMgr.GetVarObject(L, 3);
			SendMessageOptions options2 = (SendMessageOptions)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(SendMessageOptions));
			gameObject4.SendMessageUpwards(luaString2, varObject2, options2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.SendMessageUpwards");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SendMessage(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			GameObject gameObject2 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			gameObject2.SendMessage(luaString);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
			{
				GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
				string methodName = LuaScriptMgr.GetString(L, 2);
				SendMessageOptions options = (SendMessageOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
				gameObject.SendMessage(methodName, options);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object)))
		{
			GameObject gameObject3 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string methodName2 = LuaScriptMgr.GetString(L, 2);
			object varObject = LuaScriptMgr.GetVarObject(L, 3);
			gameObject3.SendMessage(methodName2, varObject);
			return 0;
		}
		if (num == 4)
		{
			GameObject gameObject4 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			object varObject2 = LuaScriptMgr.GetVarObject(L, 3);
			SendMessageOptions options2 = (SendMessageOptions)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(SendMessageOptions));
			gameObject4.SendMessage(luaString2, varObject2, options2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.SendMessage");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int BroadcastMessage(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			GameObject gameObject2 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			gameObject2.BroadcastMessage(luaString);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
			{
				GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
				string methodName = LuaScriptMgr.GetString(L, 2);
				SendMessageOptions options = (SendMessageOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
				gameObject.BroadcastMessage(methodName, options);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(GameObject), typeof(string), typeof(object)))
		{
			GameObject gameObject3 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string methodName2 = LuaScriptMgr.GetString(L, 2);
			object varObject = LuaScriptMgr.GetVarObject(L, 3);
			gameObject3.BroadcastMessage(methodName2, varObject);
			return 0;
		}
		if (num == 4)
		{
			GameObject gameObject4 = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			object varObject2 = LuaScriptMgr.GetVarObject(L, 3);
			SendMessageOptions options2 = (SendMessageOptions)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(SendMessageOptions));
			gameObject4.BroadcastMessage(luaString2, varObject2, options2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.BroadcastMessage");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AddComponent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		GameObject gameObject = (GameObject)LuaScriptMgr.GetUnityObjectSelf(L, 1, "GameObject");
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
		Component obj = gameObject.AddComponent(typeObject);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Find(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		GameObject obj = GameObject.Find(luaString);
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
}
