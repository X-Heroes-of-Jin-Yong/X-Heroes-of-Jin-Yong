using System;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;

public class ComponentWrap
{
	private static Type classType = typeof(Component);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[13]
		{
			new LuaMethod("GetComponent", GetComponent),
			new LuaMethod("GetComponentInChildren", GetComponentInChildren),
			new LuaMethod("GetComponentsInChildren", GetComponentsInChildren),
			new LuaMethod("GetComponentInParent", GetComponentInParent),
			new LuaMethod("GetComponentsInParent", GetComponentsInParent),
			new LuaMethod("GetComponents", GetComponents),
			new LuaMethod("CompareTag", CompareTag),
			new LuaMethod("SendMessageUpwards", SendMessageUpwards),
			new LuaMethod("SendMessage", SendMessage),
			new LuaMethod("BroadcastMessage", BroadcastMessage),
			new LuaMethod("New", _CreateComponent),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[3]
		{
			new LuaField("transform", get_transform, null),
			new LuaField("gameObject", get_gameObject, null),
			new LuaField("tag", get_tag, set_tag)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Component", typeof(Component), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateComponent(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Component obj = new Component();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Component.New");
		return 0;
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
		Component component = (Component)luaObject;
		if (component == null)
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
		LuaScriptMgr.Push(L, component.transform);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gameObject(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Component component = (Component)luaObject;
		if (component == null)
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
		LuaScriptMgr.Push(L, component.gameObject);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_tag(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Component component = (Component)luaObject;
		if (component == null)
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
		LuaScriptMgr.Push(L, component.tag);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_tag(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Component component = (Component)luaObject;
		if (component == null)
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
		component.tag = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponent(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(string)))
		{
			Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string type = LuaScriptMgr.GetString(L, 2);
			Component component2 = component.GetComponent(type);
			LuaScriptMgr.Push(L, component2);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(Type)))
		{
			Component component3 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			Component component4 = component3.GetComponent(typeObject);
			LuaScriptMgr.Push(L, component4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponent");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentInChildren(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
		Component componentInChildren = component.GetComponentInChildren(typeObject);
		LuaScriptMgr.Push(L, componentInChildren);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentsInChildren(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Component component2 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 2);
			Component[] componentsInChildren2 = component2.GetComponentsInChildren(typeObject2);
			LuaScriptMgr.PushArray(L, componentsInChildren2);
			return 1;
		}
		case 3:
		{
			Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Component[] componentsInChildren = component.GetComponentsInChildren(typeObject, boolean);
			LuaScriptMgr.PushArray(L, componentsInChildren);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponentsInChildren");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentInParent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
		Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
		Component componentInParent = component.GetComponentInParent(typeObject);
		LuaScriptMgr.Push(L, componentInParent);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponentsInParent(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Component component2 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 2);
			Component[] componentsInParent2 = component2.GetComponentsInParent(typeObject2);
			LuaScriptMgr.PushArray(L, componentsInParent2);
			return 1;
		}
		case 3:
		{
			Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Component[] componentsInParent = component.GetComponentsInParent(typeObject, boolean);
			LuaScriptMgr.PushArray(L, componentsInParent);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponentsInParent");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetComponents(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Component component2 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			Type typeObject2 = LuaScriptMgr.GetTypeObject(L, 2);
			Component[] components = component2.GetComponents(typeObject2);
			LuaScriptMgr.PushArray(L, components);
			return 1;
		}
		case 3:
		{
			Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			List<Component> results = (List<Component>)LuaScriptMgr.GetNetObject(L, 3, typeof(List<Component>));
			component.GetComponents(typeObject, results);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponents");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareTag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool b = component.CompareTag(luaString);
		LuaScriptMgr.Push(L, b);
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
			Component component2 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			component2.SendMessageUpwards(luaString);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
			{
				Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
				string methodName = LuaScriptMgr.GetString(L, 2);
				SendMessageOptions options = (SendMessageOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
				component.SendMessageUpwards(methodName, options);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object)))
		{
			Component component3 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string methodName2 = LuaScriptMgr.GetString(L, 2);
			object varObject = LuaScriptMgr.GetVarObject(L, 3);
			component3.SendMessageUpwards(methodName2, varObject);
			return 0;
		}
		if (num == 4)
		{
			Component component4 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			object varObject2 = LuaScriptMgr.GetVarObject(L, 3);
			SendMessageOptions options2 = (SendMessageOptions)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(SendMessageOptions));
			component4.SendMessageUpwards(luaString2, varObject2, options2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Component.SendMessageUpwards");
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
			Component component2 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			component2.SendMessage(luaString);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
			{
				Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
				string methodName = LuaScriptMgr.GetString(L, 2);
				SendMessageOptions options = (SendMessageOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
				component.SendMessage(methodName, options);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object)))
		{
			Component component3 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string methodName2 = LuaScriptMgr.GetString(L, 2);
			object varObject = LuaScriptMgr.GetVarObject(L, 3);
			component3.SendMessage(methodName2, varObject);
			return 0;
		}
		if (num == 4)
		{
			Component component4 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			object varObject2 = LuaScriptMgr.GetVarObject(L, 3);
			SendMessageOptions options2 = (SendMessageOptions)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(SendMessageOptions));
			component4.SendMessage(luaString2, varObject2, options2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Component.SendMessage");
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
			Component component2 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			component2.BroadcastMessage(luaString);
			return 0;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
			{
				Component component = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
				string methodName = LuaScriptMgr.GetString(L, 2);
				SendMessageOptions options = (SendMessageOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
				component.BroadcastMessage(methodName, options);
				return 0;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Component), typeof(string), typeof(object)))
		{
			Component component3 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string methodName2 = LuaScriptMgr.GetString(L, 2);
			object varObject = LuaScriptMgr.GetVarObject(L, 3);
			component3.BroadcastMessage(methodName2, varObject);
			return 0;
		}
		if (num == 4)
		{
			Component component4 = (Component)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Component");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			object varObject2 = LuaScriptMgr.GetVarObject(L, 3);
			SendMessageOptions options2 = (SendMessageOptions)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(SendMessageOptions));
			component4.BroadcastMessage(luaString2, varObject2, options2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Component.BroadcastMessage");
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
