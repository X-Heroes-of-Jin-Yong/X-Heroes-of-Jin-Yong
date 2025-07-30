using LuaInterface;
using UnityEngine;

public class AccessingLuaVariables01 : MonoBehaviour
{
	private string script = "\n            luanet.load_assembly('UnityEngine')\n            GameObject = luanet.import_type('UnityEngine.GameObject')\n            ParticleSystem = luanet.import_type('UnityEngine.ParticleSystem')\n\n            particles = {}\n\n            for i = 1, Objs2Spawn, 1 do\n                local newGameObj = GameObject('NewObj' .. tostring(i))\n                local ps = newGameObj:AddComponent(luanet.ctype(ParticleSystem))\n                --local ps = newGameObj:AddComponent('ParticleSystem') PS:Unity5.x已经废弃这种方式，Unity4.x可用--\n                ps:Stop()\n\n                table.insert(particles, ps)\n            end\n\n            var2read = 42\n        ";

	private void Start()
	{
		LuaState luaState = new LuaState();
		luaState["Objs2Spawn"] = 5;
		luaState.DoString(script);
		MonoBehaviour.print("Read from lua: " + luaState["var2read"].ToString());
		LuaTable luaTable = (LuaTable)luaState["particles"];
		foreach (ParticleSystem value in luaTable.Values)
		{
			value.Play();
		}
	}

	private void Update()
	{
	}
}
