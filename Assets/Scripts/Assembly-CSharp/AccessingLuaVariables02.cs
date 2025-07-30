using LuaInterface;
using UnityEngine;

public class AccessingLuaVariables02 : MonoBehaviour
{
	private string var = "Objs2Spawn = 0";

	private string script = "      \n            ParticleSystem = UnityEngine.ParticleSystem\n            particles = {}\n\n            for i = 1, Objs2Spawn, 1 do\n                local newGameObj = GameObject('NewObj' .. tostring(i))\n                local ps = newGameObj:AddComponent(ParticleSystem.GetClassType())\n                ps:Stop()\n\n                table.insert(particles, ps)\n            end\n\n            var2read = 42\n        ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		LuaState lua = luaScriptMgr.lua;
		lua.DoString(var);
		lua["Objs2Spawn"] = 5;
		lua.DoString(script);
		MonoBehaviour.print("Read from lua: " + lua["var2read"].ToString());
		LuaTable luaTable = (LuaTable)lua["particles"];
		foreach (ParticleSystem value in luaTable.Values)
		{
			value.Play();
		}
	}

	private void Update()
	{
	}
}
