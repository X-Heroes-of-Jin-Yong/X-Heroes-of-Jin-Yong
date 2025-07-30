using UnityEngine;

public class CreateGameObject02 : MonoBehaviour
{
	private string script = "\n            luanet.load_assembly('UnityEngine')\n            GameObject = UnityEngine.GameObject\n            ParticleSystem = UnityEngine.ParticleSystem\n            local newGameObj = GameObject('NewObj')\n            newGameObj:AddComponent(ParticleSystem.GetClassType())\n        ";

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.Start();
		luaScriptMgr.DoString(script);
	}

	private void Update()
	{
	}
}
