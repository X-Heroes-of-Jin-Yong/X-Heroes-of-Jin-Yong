using System;
using LuaInterface;

public class DebuggerWrap
{
	private static Type classType = typeof(Debugger);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[5]
		{
			new LuaMethod("Log", Log),
			new LuaMethod("LogWarning", LogWarning),
			new LuaMethod("LogError", LogError),
			new LuaMethod("New", _CreateDebugger),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaScriptMgr.RegisterLib(L, "Debugger", regs);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateDebugger(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Debugger class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Log(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 2, num - 1);
		Debugger.Log(luaString, paramsObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LogWarning(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 2, num - 1);
		Debugger.LogWarning(luaString, paramsObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LogError(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 2, num - 1);
		Debugger.LogError(luaString, paramsObject);
		return 0;
	}
}
