using LuaInterface;
using UnityEngine;

public class LuaProtoBuffer01 : MonoBehaviour
{
	private string script = "      \n        function decoder()  \n            local msg = person_pb.Person()\n            msg:ParseFromString(TestProtol.data)\n            print('id:'..msg.id..' name:'..msg.name..' email:'..msg.email)\n        end\n\n        function encoder()                           \n            local msg = person_pb.Person()\n            msg.id = 100\n            msg.name = 'foo'\n            msg.email = 'bar'\n            local pb_data = msg:SerializeToString()\n            TestProtol.data = pb_data\n        end\n        ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		TestProtolWrap.Register(luaScriptMgr.GetL());
		luaScriptMgr.DoFile("3rd/pblua/person_pb.lua");
		luaScriptMgr.DoString(script);
		LuaFunction luaFunction = luaScriptMgr.GetLuaFunction("encoder");
		luaFunction.Call();
		luaFunction.Release();
		luaFunction = luaScriptMgr.GetLuaFunction("decoder");
		luaFunction.Call();
		luaFunction.Release();
	}
}
