using LuaInterface;
using UnityEngine;

public class CallLuaFunction_01 : MonoBehaviour
{
	private string script = "\n            function luaFunc(message)\n                print(message)\n                return 42\n            end\n        ";

	private void Start()
	{
		LuaState luaState = new LuaState();
		luaState.DoString(script);
		LuaFunction function = luaState.GetFunction("luaFunc");
		object[] array = function.Call("I called a lua function!");
		MonoBehaviour.print(array[0]);
	}

	private void Update()
	{
	}
}
