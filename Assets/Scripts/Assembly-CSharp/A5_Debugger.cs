using UnityEngine;

public class A5_Debugger : MonoBehaviour
{
	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.DoFile("debugger");
	}

	private void Update()
	{
	}
}
