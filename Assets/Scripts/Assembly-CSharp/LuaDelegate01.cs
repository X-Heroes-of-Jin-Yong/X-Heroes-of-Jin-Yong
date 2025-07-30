using LuaInterface;
using UnityEngine;

public class LuaDelegate01 : MonoBehaviour
{
	private const string script = "\n        local func1 = function() print('测试委托1'); end\n        local func2 = function(gameObj) print('测试委托2:>'..gameObj.name); end        \n        \n        function testDelegate(go) \n            local ev = go:AddComponent(TestDelegateListener.GetClassType());\n        \n            ---直接赋值模式---\n            ev.onClick = func1;\n\n            ---C#的加减模式---\n            local delegate = DelegateFactory.TestLuaDelegate_VoidDelegate(func2);\n            ev.onEvClick = ev.onEvClick + delegate;\n            --ev.onEvClick = ev.onEvClick - delegate;\n        end\n    ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.DoString("\n        local func1 = function() print('测试委托1'); end\n        local func2 = function(gameObj) print('测试委托2:>'..gameObj.name); end        \n        \n        function testDelegate(go) \n            local ev = go:AddComponent(TestDelegateListener.GetClassType());\n        \n            ---直接赋值模式---\n            ev.onClick = func1;\n\n            ---C#的加减模式---\n            local delegate = DelegateFactory.TestLuaDelegate_VoidDelegate(func2);\n            ev.onEvClick = ev.onEvClick + delegate;\n            --ev.onEvClick = ev.onEvClick - delegate;\n        end\n    ");
		LuaFunction luaFunction = luaScriptMgr.GetLuaFunction("testDelegate");
		luaFunction.Call(base.gameObject);
	}
}
