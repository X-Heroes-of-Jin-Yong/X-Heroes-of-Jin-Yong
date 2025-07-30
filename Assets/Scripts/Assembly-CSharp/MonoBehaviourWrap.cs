using System;
using System.Collections;
using LuaInterface;
using UnityEngine;

public class MonoBehaviourWrap
{
	private static Type classType = typeof(MonoBehaviour);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[12]
		{
			new LuaMethod("Invoke", Invoke),
			new LuaMethod("InvokeRepeating", InvokeRepeating),
			new LuaMethod("CancelInvoke", CancelInvoke),
			new LuaMethod("IsInvoking", IsInvoking),
			new LuaMethod("StartCoroutine", StartCoroutine),
			new LuaMethod("StartCoroutine_Auto", StartCoroutine_Auto),
			new LuaMethod("StopCoroutine", StopCoroutine),
			new LuaMethod("StopAllCoroutines", StopAllCoroutines),
			new LuaMethod("print", print),
			new LuaMethod("New", _CreateMonoBehaviour),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[1]
		{
			new LuaField("useGUILayout", get_useGUILayout, set_useGUILayout)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.MonoBehaviour", typeof(MonoBehaviour), regs, fields, typeof(Behaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateMonoBehaviour(IntPtr L)
	{
		LuaDLL.luaL_error(L, "MonoBehaviour class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useGUILayout(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MonoBehaviour monoBehaviour = (MonoBehaviour)luaObject;
		if (monoBehaviour == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useGUILayout");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useGUILayout on a nil value");
			}
		}
		LuaScriptMgr.Push(L, monoBehaviour.useGUILayout);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useGUILayout(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		MonoBehaviour monoBehaviour = (MonoBehaviour)luaObject;
		if (monoBehaviour == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useGUILayout");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useGUILayout on a nil value");
			}
		}
		monoBehaviour.useGUILayout = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Invoke(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		float time = (float)LuaScriptMgr.GetNumber(L, 3);
		monoBehaviour.Invoke(luaString, time);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InvokeRepeating(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		float time = (float)LuaScriptMgr.GetNumber(L, 3);
		float repeatRate = (float)LuaScriptMgr.GetNumber(L, 4);
		monoBehaviour.InvokeRepeating(luaString, time, repeatRate);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CancelInvoke(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			MonoBehaviour monoBehaviour2 = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			monoBehaviour2.CancelInvoke();
			return 0;
		}
		case 2:
		{
			MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			monoBehaviour.CancelInvoke(luaString);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.CancelInvoke");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsInvoking(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			MonoBehaviour monoBehaviour2 = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			bool b2 = monoBehaviour2.IsInvoking();
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 2:
		{
			MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			bool b = monoBehaviour.IsInvoking(luaString);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.IsInvoking");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartCoroutine(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(string)))
		{
			MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			string methodName = LuaScriptMgr.GetString(L, 2);
			Coroutine o = monoBehaviour.StartCoroutine(methodName);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(IEnumerator)))
		{
			MonoBehaviour monoBehaviour2 = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			IEnumerator routine = (IEnumerator)LuaScriptMgr.GetLuaObject(L, 2);
			Coroutine o2 = monoBehaviour2.StartCoroutine(routine);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		if (num == 3)
		{
			MonoBehaviour monoBehaviour3 = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			object varObject = LuaScriptMgr.GetVarObject(L, 3);
			Coroutine o3 = monoBehaviour3.StartCoroutine(luaString, varObject);
			LuaScriptMgr.PushObject(L, o3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.StartCoroutine");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartCoroutine_Auto(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
		IEnumerator routine = (IEnumerator)LuaScriptMgr.GetNetObject(L, 2, typeof(IEnumerator));
		Coroutine o = monoBehaviour.StartCoroutine_Auto(routine);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopCoroutine(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(Coroutine)))
		{
			MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			Coroutine routine = (Coroutine)LuaScriptMgr.GetLuaObject(L, 2);
			monoBehaviour.StopCoroutine(routine);
			return 0;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(IEnumerator)))
		{
			MonoBehaviour monoBehaviour2 = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			IEnumerator routine2 = (IEnumerator)LuaScriptMgr.GetLuaObject(L, 2);
			monoBehaviour2.StopCoroutine(routine2);
			return 0;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(MonoBehaviour), typeof(string)))
		{
			MonoBehaviour monoBehaviour3 = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
			string methodName = LuaScriptMgr.GetString(L, 2);
			monoBehaviour3.StopCoroutine(methodName);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.StopCoroutine");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StopAllCoroutines(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		MonoBehaviour monoBehaviour = (MonoBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "MonoBehaviour");
		monoBehaviour.StopAllCoroutines();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int print(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		object varObject = LuaScriptMgr.GetVarObject(L, 1);
		MonoBehaviour.print(varObject);
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
