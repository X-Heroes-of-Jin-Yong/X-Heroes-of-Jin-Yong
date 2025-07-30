using LuaInterface;
using UnityEngine;

public class CreateGameObject01 : MonoBehaviour
{
	private string script = "\n            luanet.load_assembly('UnityEngine')\n            GameObject = luanet.import_type('UnityEngine.GameObject')        \n\t        ParticleSystem = luanet.import_type('UnityEngine.ParticleSystem')         \n   \n            local newGameObj = GameObject('NewObj')\n            newGameObj:AddComponent(luanet.ctype(ParticleSystem))\n        ";

	private void Start()
	{
		LuaState luaState = new LuaState();
		luaState.DoString(script);
	}

	private void Update()
	{
	}
}
