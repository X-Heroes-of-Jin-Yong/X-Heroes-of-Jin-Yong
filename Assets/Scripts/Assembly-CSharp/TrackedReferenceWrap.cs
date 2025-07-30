using System;
using LuaInterface;
using UnityEngine;

public class TrackedReferenceWrap
{
	private static Type classType = typeof(TrackedReference);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[5]
		{
			new LuaMethod("Equals", Equals),
			new LuaMethod("GetHashCode", GetHashCode),
			new LuaMethod("New", _CreateTrackedReference),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "UnityEngine.TrackedReference", typeof(TrackedReference), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateTrackedReference(IntPtr L)
	{
		LuaDLL.luaL_error(L, "TrackedReference class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Equals(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TrackedReference trackedReference = LuaScriptMgr.GetVarObject(L, 1) as TrackedReference;
		object varObject = LuaScriptMgr.GetVarObject(L, 2);
		bool b = ((!(trackedReference != null)) ? (varObject == null) : trackedReference.Equals(varObject));
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetHashCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TrackedReference trackedReference = (TrackedReference)LuaScriptMgr.GetTrackedObjectSelf(L, 1, "TrackedReference");
		int hashCode = trackedReference.GetHashCode();
		LuaScriptMgr.Push(L, hashCode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TrackedReference trackedReference = LuaScriptMgr.GetLuaObject(L, 1) as TrackedReference;
		TrackedReference trackedReference2 = LuaScriptMgr.GetLuaObject(L, 2) as TrackedReference;
		bool b = trackedReference == trackedReference2;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
