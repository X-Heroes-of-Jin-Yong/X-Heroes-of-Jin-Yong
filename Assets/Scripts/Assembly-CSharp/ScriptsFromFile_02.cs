using LuaInterface;
using UnityEngine;

public class ScriptsFromFile_02 : MonoBehaviour
{
	private void Start()
	{
		LuaState luaState = new LuaState();
		string fileName = Application.dataPath + "/uLua/Examples/04_ScriptsFromFile/ScriptsFromFile02.lua";
		luaState.DoFile(fileName);
	}

	private void Update()
	{
	}
}
