using LuaInterface;
using UnityEngine;

public class A6_LuaCall : MonoBehaviour
{
	private const string script = "\n        A6_LuaCall = luanet.import_type('A6_LuaCall')  \n\n        LuaClass = {}\n        LuaClass.__index = LuaClass\n\n        function LuaClass:New() \n            local self = {};   \n            setmetatable(self, LuaClass); \n            return self;    \n        end\n\n        function LuaClass:test() \n            A6_LuaCall.OnSharpCall(self, self.callback);\n        end\n\n        function LuaClass:callback()\n            print('test--->>>');\n        end\n\n        LuaClass:New():test();\n    ";

	private void Start()
	{
		LuaState luaState = new LuaState();
		luaState.DoString("\n        A6_LuaCall = luanet.import_type('A6_LuaCall')  \n\n        LuaClass = {}\n        LuaClass.__index = LuaClass\n\n        function LuaClass:New() \n            local self = {};   \n            setmetatable(self, LuaClass); \n            return self;    \n        end\n\n        function LuaClass:test() \n            A6_LuaCall.OnSharpCall(self, self.callback);\n        end\n\n        function LuaClass:callback()\n            print('test--->>>');\n        end\n\n        LuaClass:New():test();\n    ");
	}

	public static void OnSharpCall(LuaTable self, LuaFunction func)
	{
		func.Call(self);
	}
}
