using System;
using LuaInterface;

public class TestOverride_SpaceWrap
{
	private static LuaMethod[] enums = new LuaMethod[2]
	{
		new LuaMethod("World", GetWorld),
		new LuaMethod("IntToEnum", IntToEnum)
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "TestOverride.Space", typeof(TestOverride.Space), enums);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetWorld(IntPtr L)
	{
		LuaScriptMgr.Push(L, TestOverride.Space.World);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		TestOverride.Space space = (TestOverride.Space)num;
		LuaScriptMgr.Push(L, space);
		return 1;
	}
}
