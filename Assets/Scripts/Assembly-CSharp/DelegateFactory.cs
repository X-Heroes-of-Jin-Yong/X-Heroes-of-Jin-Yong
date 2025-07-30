using System;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;
using UnityEngine;
using UnityEngine.Events;

public static class DelegateFactory
{
	private delegate Delegate DelegateValue(LuaFunction func);

	private static Dictionary<Type, DelegateValue> dict = new Dictionary<Type, DelegateValue>();

	[NoToLua]
	public static void Register(IntPtr L)
	{
		dict.Add(typeof(Action<GameObject>), Action_GameObject);
		dict.Add(typeof(Action), Action);
		dict.Add(typeof(UnityAction), UnityEngine_Events_UnityAction);
		dict.Add(typeof(MemberFilter), System_Reflection_MemberFilter);
		dict.Add(typeof(TypeFilter), System_Reflection_TypeFilter);
		dict.Add(typeof(TestLuaDelegate.VoidDelegate), TestLuaDelegate_VoidDelegate);
		dict.Add(typeof(Camera.CameraCallback), Camera_CameraCallback);
		dict.Add(typeof(AudioClip.PCMReaderCallback), AudioClip_PCMReaderCallback);
		dict.Add(typeof(AudioClip.PCMSetPositionCallback), AudioClip_PCMSetPositionCallback);
		dict.Add(typeof(Application.LogCallback), Application_LogCallback);
	}

	[NoToLua]
	public static Delegate CreateDelegate(Type t, LuaFunction func)
	{
		DelegateValue value = null;
		if (!dict.TryGetValue(t, out value))
		{
			Debugger.LogError("Delegate {0} not register", t.FullName);
			return null;
		}
		return value(func);
	}

	public static Delegate Action_GameObject(LuaFunction func)
	{
		return (Action<GameObject>)delegate(GameObject param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		};
	}

	public static Delegate Action(LuaFunction func)
	{
		return (Action)delegate
		{
			func.Call();
		};
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		return (UnityAction)delegate
		{
			func.Call();
		};
	}

	public static Delegate System_Reflection_MemberFilter(LuaFunction func)
	{
		return (MemberFilter)delegate(MemberInfo param0, object param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.PushObject(luaState, param0);
			LuaScriptMgr.PushVarObject(luaState, param1);
			func.PCall(oldTop, 2);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (bool)array[0];
		};
	}

	public static Delegate System_Reflection_TypeFilter(LuaFunction func)
	{
		return (TypeFilter)delegate(Type param0, object param1)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.PushVarObject(luaState, param1);
			func.PCall(oldTop, 2);
			object[] array = func.PopValues(oldTop);
			func.EndPCall(oldTop);
			return (bool)array[0];
		};
	}

	public static Delegate TestLuaDelegate_VoidDelegate(LuaFunction func)
	{
		return (TestLuaDelegate.VoidDelegate)delegate(GameObject param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		};
	}

	public static Delegate Camera_CameraCallback(LuaFunction func)
	{
		return (Camera.CameraCallback)delegate(Camera param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		};
	}

	public static Delegate AudioClip_PCMReaderCallback(LuaFunction func)
	{
		return (AudioClip.PCMReaderCallback)delegate(float[] param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.PushArray(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		};
	}

	public static Delegate AudioClip_PCMSetPositionCallback(LuaFunction func)
	{
		return (AudioClip.PCMSetPositionCallback)delegate(int param0)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			func.PCall(oldTop, 1);
			func.EndPCall(oldTop);
		};
	}

	public static Delegate Application_LogCallback(LuaFunction func)
	{
		return (Application.LogCallback)delegate(string param0, string param1, LogType param2)
		{
			int oldTop = func.BeginPCall();
			IntPtr luaState = func.GetLuaState();
			LuaScriptMgr.Push(luaState, param0);
			LuaScriptMgr.Push(luaState, param1);
			LuaScriptMgr.Push(luaState, param2);
			func.PCall(oldTop, 3);
			func.EndPCall(oldTop);
		};
	}

	public static void Clear()
	{
		dict.Clear();
	}
}
