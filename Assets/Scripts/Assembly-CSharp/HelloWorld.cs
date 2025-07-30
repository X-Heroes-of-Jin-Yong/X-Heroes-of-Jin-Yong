using LuaInterface;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
	private void Start()
	{
		LuaState luaState = new LuaState();
		string chunk = "print('hello world 世界')";
		luaState.DoString(chunk);
	}

	private void Update()
	{
	}
}
