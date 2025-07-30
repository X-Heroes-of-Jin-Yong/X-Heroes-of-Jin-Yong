using System;
using LuaInterface;
using UnityEngine;

public class CallLuaFunction_02 : MonoBehaviour
{
	private string script = "\n            function luaFunc(num)                \n                return num\n            end\n        ";

	private LuaFunction func;

	private void Start()
	{
		LuaScriptMgr luaScriptMgr = new LuaScriptMgr();
		luaScriptMgr.DoString(script);
		func = luaScriptMgr.GetLuaFunction("luaFunc");
		object[] array = func.Call(123456.0);
		MonoBehaviour.print(array[0]);
		int num = CallFunc();
		MonoBehaviour.print(num);
	}

	private void OnDestroy()
	{
		if (func != null)
		{
			func.Release();
		}
	}

	private int CallFunc()
	{
		int oldTop = func.BeginPCall();
		IntPtr luaState = func.GetLuaState();
		LuaScriptMgr.Push(luaState, 123456);
		func.PCall(oldTop, 1);
		int result = (int)LuaScriptMgr.GetNumber(luaState, -1);
		func.EndPCall(oldTop);
		return result;
	}

	private void Update()
	{
	}
}
