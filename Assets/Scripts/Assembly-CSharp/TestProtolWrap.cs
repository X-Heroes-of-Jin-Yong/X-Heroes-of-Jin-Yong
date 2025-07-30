using System;
using LuaInterface;

public class TestProtolWrap
{
	private static Type classType = typeof(TestProtol);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[2]
		{
			new LuaMethod("New", _CreateTestProtol),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaField[] fields = new LuaField[1]
		{
			new LuaField("data", get_data, set_data)
		};
		LuaScriptMgr.RegisterLib(L, "TestProtol", typeof(TestProtol), regs, fields, null);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateTestProtol(IntPtr L)
	{
		LuaDLL.luaL_error(L, "TestProtol class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_data(IntPtr L)
	{
		LuaScriptMgr.Push(L, TestProtol.data);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_data(IntPtr L)
	{
		TestProtol.data = LuaScriptMgr.GetStringBuffer(L, 3);
		return 0;
	}
}
