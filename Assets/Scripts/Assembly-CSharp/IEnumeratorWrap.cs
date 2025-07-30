using System;
using System.Collections;
using LuaInterface;

public class IEnumeratorWrap
{
	private static Type classType = typeof(IEnumerator);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[4]
		{
			new LuaMethod("MoveNext", MoveNext),
			new LuaMethod("Reset", Reset),
			new LuaMethod("New", _CreateIEnumerator),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaField[] fields = new LuaField[1]
		{
			new LuaField("Current", get_Current, null)
		};
		LuaScriptMgr.RegisterLib(L, "System.Collections.IEnumerator", typeof(IEnumerator), regs, fields, null);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateIEnumerator(IntPtr L)
	{
		LuaDLL.luaL_error(L, "IEnumerator class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Current(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		IEnumerator enumerator = (IEnumerator)luaObject;
		if (enumerator == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Current");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Current on a nil value");
			}
		}
		LuaScriptMgr.PushVarObject(L, enumerator.Current);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int MoveNext(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IEnumerator enumerator = (IEnumerator)LuaScriptMgr.GetNetObjectSelf(L, 1, "IEnumerator");
		bool b = enumerator.MoveNext();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Reset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		IEnumerator enumerator = (IEnumerator)LuaScriptMgr.GetNetObjectSelf(L, 1, "IEnumerator");
		enumerator.Reset();
		return 0;
	}
}
