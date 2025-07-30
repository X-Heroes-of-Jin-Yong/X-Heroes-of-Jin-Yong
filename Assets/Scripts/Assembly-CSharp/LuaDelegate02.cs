using LuaInterface;
using UnityEngine;

public class LuaDelegate02 : MonoBehaviour
{
	private string script = "                  \n            function DoClick1(go)\n                print('click1 on ', go.name)\n            end\n\n            function DoClick2(go)\n                print('click2 on ', go.name)\n            end\n            \n            local click2 = nil\n\n            function AddDelegate(listener)                     \n                listener.OnClick = DoClick1\n                click2 = DelegateFactory.Action_GameObject(DoClick2)                \n                listener.OnClick = listener.OnClick + click2                                    \n            end\n\n            function RemoveDelegate(listener)                \n                listener.OnClick = listener.OnClick - click2       \n                return delegate         \n            end\n    ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.DoString(script);
		TestEventListener testEventListener = base.gameObject.AddComponent<TestEventListener>();
		LuaFunction luaFunction = luaScriptMgr.GetLuaFunction("AddDelegate");
		luaFunction.Call(testEventListener);
		testEventListener.OnClick(base.gameObject);
		luaFunction.Release();
		Debug.Log("---------------------------------------------------------------------");
		luaFunction = luaScriptMgr.GetLuaFunction("RemoveDelegate");
		luaFunction.Call(testEventListener);
		testEventListener.OnClick(base.gameObject);
		luaFunction.Release();
	}
}
