using LuaInterface;
using UnityEngine;

public class TestOverride01 : MonoBehaviour
{
	private string script = "                  \n        function Test(to)\n            assert(to:Test(1) == 4)\n            assert(to:Test('hello') == 6)\n            assert(to:Test(object.New()) == 8)\n            assert(to:Test(123, 456) == 5)            \n            assert(to:Test('123', '456') == 1)\n            assert(to:Test(object.New(), '456') == 1)\n            assert(to:Test('123', 456) == 9)\n            assert(to:Test('123', object.New()) == 9)\n            assert(to:Test(1,2,3) == 9)            \n            assert(to:Test(TestOverride.Space.World) == 10)        \n        end\n    ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		TestOverrideWrap.Register(luaScriptMgr.GetL());
		TestOverride_SpaceWrap.Register(luaScriptMgr.GetL());
		luaScriptMgr.DoString(script);
		TestOverride testOverride = new TestOverride();
		LuaFunction luaFunction = luaScriptMgr.GetLuaFunction("Test");
		luaFunction.Call(testOverride);
	}
}
