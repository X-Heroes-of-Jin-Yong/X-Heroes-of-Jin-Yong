using System;
using LuaInterface;
using UnityEngine;

public class TestDelegateListenerWrap
{
	private static Type classType = typeof(TestDelegateListener);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[3]
		{
			new LuaMethod("New", _CreateTestDelegateListener),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[2]
		{
			new LuaField("onClick", get_onClick, set_onClick),
			new LuaField("onEvClick", get_onEvClick, set_onEvClick)
		};
		LuaScriptMgr.RegisterLib(L, "TestDelegateListener", typeof(TestDelegateListener), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateTestDelegateListener(IntPtr L)
	{
		LuaDLL.luaL_error(L, "TestDelegateListener class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestDelegateListener testDelegateListener = (TestDelegateListener)luaObject;
		if (testDelegateListener == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}
		LuaScriptMgr.Push(L, testDelegateListener.onClick);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_onEvClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestDelegateListener testDelegateListener = (TestDelegateListener)luaObject;
		if (testDelegateListener == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEvClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEvClick on a nil value");
			}
		}
		LuaScriptMgr.Push(L, testDelegateListener.onEvClick);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestDelegateListener testDelegateListener = (TestDelegateListener)luaObject;
		if (testDelegateListener == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}
		LuaTypes luaTypes2 = LuaDLL.lua_type(L, 3);
		if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
		{
			testDelegateListener.onClick = (Action)LuaScriptMgr.GetNetObject(L, 3, typeof(Action));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			testDelegateListener.onClick = delegate
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_onEvClick(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		TestDelegateListener testDelegateListener = (TestDelegateListener)luaObject;
		if (testDelegateListener == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEvClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEvClick on a nil value");
			}
		}
		LuaTypes luaTypes2 = LuaDLL.lua_type(L, 3);
		if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
		{
			testDelegateListener.onEvClick = (TestLuaDelegate.VoidDelegate)LuaScriptMgr.GetNetObject(L, 3, typeof(TestLuaDelegate.VoidDelegate));
		}
		else
		{
			LuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);
			testDelegateListener.onEvClick = delegate(GameObject param0)
			{
				int oldTop = func.BeginPCall();
				LuaScriptMgr.Push(L, param0);
				func.PCall(oldTop, 1);
				func.EndPCall(oldTop);
			};
		}
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
