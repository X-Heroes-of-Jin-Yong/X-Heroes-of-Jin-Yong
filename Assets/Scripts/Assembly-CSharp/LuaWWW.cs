using UnityEngine;

public class LuaWWW : MonoBehaviour
{
	private LuaScriptMgr lua;

	private string script = "      \n        local WWW = UnityEngine.WWW\n                             \n        function testFunc()\n            local www = WWW('http://bbs.ulua.org/readme.txt');\n            coroutine.www(www);\n            print(www.text);    \n        end\n        \n        coroutine.start(testFunc)\n    ";

	private void Start()
	{
		lua = new LuaScriptMgr();
		lua.Start();
		lua.DoString(script);
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
