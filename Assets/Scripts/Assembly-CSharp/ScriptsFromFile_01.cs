using LuaInterface;
using UnityEngine;

public class ScriptsFromFile_01 : MonoBehaviour
{
	public TextAsset scriptFile;

	private void Start()
	{
		LuaState luaState = new LuaState();
		luaState.DoString(scriptFile.text);
	}

	private void Update()
	{
	}
}
