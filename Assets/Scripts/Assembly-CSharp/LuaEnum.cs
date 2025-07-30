using UnityEngine;

public class LuaEnum : MonoBehaviour
{
	private const string source = "\n        local type = LuaEnumType.IntToEnum(1);\n        print(type == LuaEnumType.AAA);\n    ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.lua.DoString("\n        local type = LuaEnumType.IntToEnum(1);\n        print(type == LuaEnumType.AAA);\n    ");
	}
}
