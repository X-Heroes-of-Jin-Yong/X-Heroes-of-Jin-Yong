using LuaInterface;
using UnityEngine;

public class LuaArray : MonoBehaviour
{
	private string source = "\n        function luaFunc(objs, len)\n            for i = 0, len - 1 do\n                print(objs[i])\n            end\n            local table1 = {'111', '222', '333'}\n            return unpack(table1)\n        end\n    ";

	private string[] objs = new string[3] { "aaa", "bbb", "ccc" };

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		LuaState lua = luaScriptMgr.lua;
		lua.DoString(source);
		LuaFunction function = lua.GetFunction("luaFunc");
		object[] array = function.Call(objs, objs.Length);
		function.Release();
		object[] array2 = array;
		foreach (object obj in array2)
		{
			Debug.Log(obj.ToString());
		}
	}
}
