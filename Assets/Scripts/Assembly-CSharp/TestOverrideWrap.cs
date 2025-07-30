using System;
using LuaInterface;

public class TestOverrideWrap
{
	private static Type classType = typeof(TestOverride);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[3]
		{
			new LuaMethod("Test", Test),
			new LuaMethod("New", _CreateTestOverride),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaField[] fields = new LuaField[0];
		LuaScriptMgr.RegisterLib(L, "TestOverride", typeof(TestOverride), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateTestOverride(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			TestOverride o = new TestOverride();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: TestOverride.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Test(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TestOverride), typeof(string)))
		{
			TestOverride testOverride = (TestOverride)LuaScriptMgr.GetNetObjectSelf(L, 1, "TestOverride");
			string str = LuaScriptMgr.GetString(L, 2);
			int d = testOverride.Test(str);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string)))
		{
			string str2 = LuaScriptMgr.GetString(L, 1);
			string str3 = LuaScriptMgr.GetString(L, 2);
			int d2 = TestOverride.Test(str2, str3);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TestOverride), typeof(TestOverride.Space)))
		{
			TestOverride testOverride2 = (TestOverride)LuaScriptMgr.GetNetObjectSelf(L, 1, "TestOverride");
			TestOverride.Space e = (TestOverride.Space)(int)LuaScriptMgr.GetLuaObject(L, 2);
			int d3 = testOverride2.Test(e);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TestOverride), typeof(double)))
		{
			TestOverride testOverride3 = (TestOverride)LuaScriptMgr.GetNetObjectSelf(L, 1, "TestOverride");
			double d4 = LuaDLL.lua_tonumber(L, 2);
			int d5 = testOverride3.Test(d4);
			LuaScriptMgr.Push(L, d5);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(TestOverride), typeof(object)))
		{
			TestOverride testOverride4 = (TestOverride)LuaScriptMgr.GetNetObjectSelf(L, 1, "TestOverride");
			object varObject = LuaScriptMgr.GetVarObject(L, 2);
			int d6 = testOverride4.Test(varObject);
			LuaScriptMgr.Push(L, d6);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(TestOverride), typeof(int), typeof(int)))
		{
			TestOverride testOverride5 = (TestOverride)LuaScriptMgr.GetNetObjectSelf(L, 1, "TestOverride");
			int i = (int)LuaDLL.lua_tonumber(L, 2);
			int j = (int)LuaDLL.lua_tonumber(L, 3);
			int d7 = testOverride5.Test(i, j);
			LuaScriptMgr.Push(L, d7);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(TestOverride), typeof(object), typeof(string)))
		{
			TestOverride testOverride6 = (TestOverride)LuaScriptMgr.GetNetObjectSelf(L, 1, "TestOverride");
			object varObject2 = LuaScriptMgr.GetVarObject(L, 2);
			string str4 = LuaScriptMgr.GetString(L, 3);
			int d8 = testOverride6.Test(varObject2, str4);
			LuaScriptMgr.Push(L, d8);
			return 1;
		}
		if (LuaScriptMgr.CheckParamsType(L, typeof(object), 2, num - 1))
		{
			TestOverride testOverride7 = (TestOverride)LuaScriptMgr.GetNetObjectSelf(L, 1, "TestOverride");
			object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 2, num - 1);
			int d9 = testOverride7.Test(paramsObject);
			LuaScriptMgr.Push(L, d9);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: TestOverride.Test");
		return 0;
	}
}
