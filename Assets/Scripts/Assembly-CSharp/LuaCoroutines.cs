using LuaInterface;
using UnityEngine;

public class LuaCoroutines : MonoBehaviour
{
	private string script = "                                   \n            function fib(n)\n                local a, b = 0, 1\n                while n > 0 do\n                    a, b = b, a + b\n                    n = n - 1\n                end\n\n                return a\n            end\n\n            function CoFunc()\n                print('Coroutine started')\n                local i = 0\n                for i = 0, 10, 1 do\n                    print(fib(i))                    \n                    coroutine.wait(1)\n                end\n                print('Coroutine ended')\n            end\n\n            function myFunc()\n                coroutine.start(CoFunc)\n            end\n        ";

	private LuaScriptMgr lua;

	private void Awake()
	{
		lua = new LuaScriptMgr();
		lua.Start();
		lua.DoString(script);
		LuaFunction luaFunction = lua.GetLuaFunction("myFunc");
		luaFunction.Call();
		luaFunction.Release();
	}

	private void Update()
	{
		lua.Update();
	}

	private void LateUpdate()
	{
		lua.LateUpate();
	}

	private void FixedUpdate()
	{
		lua.FixedUpdate();
	}
}
