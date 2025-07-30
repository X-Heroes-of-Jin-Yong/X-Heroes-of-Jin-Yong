using System;
using LuaInterface;
using UnityEngine;

public class PhysicsWrap
{
	private static Type classType = typeof(Physics);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[15]
		{
			new LuaMethod("Raycast", Raycast),
			new LuaMethod("RaycastAll", RaycastAll),
			new LuaMethod("Linecast", Linecast),
			new LuaMethod("OverlapSphere", OverlapSphere),
			new LuaMethod("CapsuleCast", CapsuleCast),
			new LuaMethod("SphereCast", SphereCast),
			new LuaMethod("CapsuleCastAll", CapsuleCastAll),
			new LuaMethod("SphereCastAll", SphereCastAll),
			new LuaMethod("CheckSphere", CheckSphere),
			new LuaMethod("CheckCapsule", CheckCapsule),
			new LuaMethod("IgnoreCollision", IgnoreCollision),
			new LuaMethod("IgnoreLayerCollision", IgnoreLayerCollision),
			new LuaMethod("GetIgnoreLayerCollision", GetIgnoreLayerCollision),
			new LuaMethod("New", _CreatePhysics),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaField[] fields = new LuaField[9]
		{
			new LuaField("IgnoreRaycastLayer", get_IgnoreRaycastLayer, null),
			new LuaField("DefaultRaycastLayers", get_DefaultRaycastLayers, null),
			new LuaField("AllLayers", get_AllLayers, null),
			new LuaField("gravity", get_gravity, set_gravity),
			new LuaField("defaultContactOffset", get_defaultContactOffset, set_defaultContactOffset),
			new LuaField("bounceThreshold", get_bounceThreshold, set_bounceThreshold),
			new LuaField("solverIterationCount", get_solverIterationCount, set_solverIterationCount),
			new LuaField("sleepThreshold", get_sleepThreshold, set_sleepThreshold),
			new LuaField("queriesHitTriggers", get_queriesHitTriggers, set_queriesHitTriggers)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Physics", typeof(Physics), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreatePhysics(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Physics o = new Physics();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_IgnoreRaycastLayer(IntPtr L)
	{
		LuaScriptMgr.Push(L, 4);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_DefaultRaycastLayers(IntPtr L)
	{
		LuaScriptMgr.Push(L, -5);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_AllLayers(IntPtr L)
	{
		LuaScriptMgr.Push(L, -1);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_gravity(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.gravity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_defaultContactOffset(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultContactOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bounceThreshold(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.bounceThreshold);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_solverIterationCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.defaultSolverIterations);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sleepThreshold(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.sleepThreshold);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_queriesHitTriggers(IntPtr L)
	{
		LuaScriptMgr.Push(L, Physics.queriesHitTriggers);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_gravity(IntPtr L)
	{
		Physics.gravity = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_defaultContactOffset(IntPtr L)
	{
		Physics.defaultContactOffset = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_bounceThreshold(IntPtr L)
	{
		Physics.bounceThreshold = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_solverIterationCount(IntPtr L)
	{
		Physics.defaultSolverIterations = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_sleepThreshold(IntPtr L)
	{
		Physics.sleepThreshold = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_queriesHitTriggers(IntPtr L)
	{
		Physics.queriesHitTriggers = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Raycast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 1:
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			bool b2 = Physics.Raycast(ray2);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 2:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null))
			{
				Ray ray = LuaScriptMgr.GetRay(L, 1);
				RaycastHit hitInfo;
				bool b = Physics.Raycast(ray, out hitInfo);
				LuaScriptMgr.Push(L, b);
				LuaScriptMgr.Push(L, hitInfo);
				return 2;
			}
			break;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			bool b3 = Physics.Raycast(vector, vector2);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 2);
			bool b4 = Physics.Raycast(ray3, maxDistance);
			LuaScriptMgr.Push(L, b4);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 3);
			bool b5 = Physics.Raycast(ray4, maxDistance2, layerMask);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			RaycastHit hitInfo2;
			bool b6 = Physics.Raycast(vector3, vector4, out hitInfo2);
			LuaScriptMgr.Push(L, b6);
			LuaScriptMgr.Push(L, hitInfo2);
			return 2;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 3);
			bool b7 = Physics.Raycast(vector5, vector6, maxDistance3);
			LuaScriptMgr.Push(L, b7);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float)))
		{
			Ray ray5 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 3);
			RaycastHit hitInfo3;
			bool b8 = Physics.Raycast(ray5, out hitInfo3, maxDistance4);
			LuaScriptMgr.Push(L, b8);
			LuaScriptMgr.Push(L, hitInfo3);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray6 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 4);
			bool b9 = Physics.Raycast(ray6, maxDistance5, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b9);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance6 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 4);
			bool b10 = Physics.Raycast(vector7, vector8, maxDistance6, layerMask3);
			LuaScriptMgr.Push(L, b10);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance7 = (float)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hitInfo4;
			bool b11 = Physics.Raycast(vector9, vector10, out hitInfo4, maxDistance7);
			LuaScriptMgr.Push(L, b11);
			LuaScriptMgr.Push(L, hitInfo4);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Ray ray7 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance8 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask4 = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hitInfo5;
			bool b12 = Physics.Raycast(ray7, out hitInfo5, maxDistance8, layerMask4);
			LuaScriptMgr.Push(L, b12);
			LuaScriptMgr.Push(L, hitInfo5);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance9 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask5 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 5);
			bool b13 = Physics.Raycast(vector11, vector12, maxDistance9, layerMask5, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b13);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 vector13 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector14 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance10 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask6 = (int)LuaDLL.lua_tonumber(L, 5);
			RaycastHit hitInfo6;
			bool b14 = Physics.Raycast(vector13, vector14, out hitInfo6, maxDistance10, layerMask6);
			LuaScriptMgr.Push(L, b14);
			LuaScriptMgr.Push(L, hitInfo6);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), null, typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray8 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance11 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask7 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction3 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 5);
			RaycastHit hitInfo7;
			bool b15 = Physics.Raycast(ray8, out hitInfo7, maxDistance11, layerMask7, queryTriggerInteraction3);
			LuaScriptMgr.Push(L, b15);
			LuaScriptMgr.Push(L, hitInfo7);
			return 2;
		}
		if (num == 6)
		{
			Vector3 vector15 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector16 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance12 = (float)LuaScriptMgr.GetNumber(L, 4);
			int layerMask8 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction4 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction));
			RaycastHit hitInfo8;
			bool b16 = Physics.Raycast(vector15, vector16, out hitInfo8, maxDistance12, layerMask8, queryTriggerInteraction4);
			LuaScriptMgr.Push(L, b16);
			LuaScriptMgr.Push(L, hitInfo8);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.Raycast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RaycastAll(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 1:
		{
			Ray ray = LuaScriptMgr.GetRay(L, 1);
			RaycastHit[] o2 = Physics.RaycastAll(ray);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		case 2:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable)))
			{
				Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
				Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
				RaycastHit[] o = Physics.RaycastAll(vector, vector2);
				LuaScriptMgr.PushArray(L, o);
				return 1;
			}
			break;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float)))
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit[] o3 = Physics.RaycastAll(ray2, maxDistance);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o4 = Physics.RaycastAll(vector3, vector4, maxDistance2);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 3);
			RaycastHit[] o5 = Physics.RaycastAll(ray3, maxDistance3, layerMask);
			LuaScriptMgr.PushArray(L, o5);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 3);
			int layermask = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o6 = Physics.RaycastAll(vector5, vector6, maxDistance4, layermask);
			LuaScriptMgr.PushArray(L, o6);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 2);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 4);
			RaycastHit[] o7 = Physics.RaycastAll(ray4, maxDistance5, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o7);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 3);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			RaycastHit[] o8 = Physics.RaycastAll(vector7, vector8, maxDistance6, layermask2, queryTriggerInteraction2);
			LuaScriptMgr.PushArray(L, o8);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.RaycastAll");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Linecast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			bool b2 = Physics.Linecast(vector3, vector4);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null))
			{
				Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
				Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
				RaycastHit hitInfo;
				bool b = Physics.Linecast(vector, vector2, out hitInfo);
				LuaScriptMgr.Push(L, b);
				LuaScriptMgr.Push(L, hitInfo);
				return 2;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 3);
			bool b3 = Physics.Linecast(vector5, vector6, layerMask);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), null, typeof(int)))
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hitInfo2;
			bool b4 = Physics.Linecast(vector7, vector8, out hitInfo2, layerMask2);
			LuaScriptMgr.Push(L, b4);
			LuaScriptMgr.Push(L, hitInfo2);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 4);
			bool b5 = Physics.Linecast(vector9, vector10, layerMask3, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 5)
		{
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 2);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			RaycastHit hitInfo3;
			bool b6 = Physics.Linecast(vector11, vector12, out hitInfo3, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b6);
			LuaScriptMgr.Push(L, hitInfo3);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.Linecast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OverlapSphere(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 2);
			Collider[] o3 = Physics.OverlapSphere(vector3, radius3);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		case 3:
		{
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 1);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 3);
			Collider[] o2 = Physics.OverlapSphere(vector2, radius2, layerMask2);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		case 4:
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(QueryTriggerInteraction));
			Collider[] o = Physics.OverlapSphere(vector, radius, layerMask, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.OverlapSphere");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 4:
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 4);
			bool b2 = Physics.CapsuleCast(vector4, vector5, radius2, vector6);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 5:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null))
			{
				Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
				Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
				float radius = (float)LuaDLL.lua_tonumber(L, 3);
				Vector3 vector3 = LuaScriptMgr.GetVector3(L, 4);
				RaycastHit hitInfo;
				bool b = Physics.CapsuleCast(vector, vector2, radius, vector3, out hitInfo);
				LuaScriptMgr.Push(L, b);
				LuaScriptMgr.Push(L, hitInfo);
				return 2;
			}
			break;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance = (float)LuaDLL.lua_tonumber(L, 5);
			bool b3 = Physics.CapsuleCast(vector7, vector8, radius3, vector9, maxDistance);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			float radius4 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 6);
			RaycastHit hitInfo2;
			bool b4 = Physics.CapsuleCast(vector10, vector11, radius4, vector12, out hitInfo2, maxDistance2);
			LuaScriptMgr.Push(L, b4);
			LuaScriptMgr.Push(L, hitInfo2);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector13 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector14 = LuaScriptMgr.GetVector3(L, 2);
			float radius5 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector15 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 6);
			bool b5 = Physics.CapsuleCast(vector13, vector14, radius5, vector15, maxDistance3, layerMask);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Vector3 vector16 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector17 = LuaScriptMgr.GetVector3(L, 2);
			float radius6 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector18 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 7);
			bool b6 = Physics.CapsuleCast(vector16, vector17, radius6, vector18, maxDistance4, layerMask2, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b6);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 vector19 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector20 = LuaScriptMgr.GetVector3(L, 2);
			float radius7 = (float)LuaDLL.lua_tonumber(L, 3);
			Vector3 vector21 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 6);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 7);
			RaycastHit hitInfo3;
			bool b7 = Physics.CapsuleCast(vector19, vector20, radius7, vector21, out hitInfo3, maxDistance5, layerMask3);
			LuaScriptMgr.Push(L, b7);
			LuaScriptMgr.Push(L, hitInfo3);
			return 2;
		}
		if (num == 8)
		{
			Vector3 vector22 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector23 = LuaScriptMgr.GetVector3(L, 2);
			float radius8 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector24 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 6);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 7);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 8, typeof(QueryTriggerInteraction));
			RaycastHit hitInfo4;
			bool b8 = Physics.CapsuleCast(vector22, vector23, radius8, vector24, out hitInfo4, maxDistance6, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b8);
			LuaScriptMgr.Push(L, hitInfo4);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCast(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 2);
			bool b2 = Physics.SphereCast(ray2, radius2);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float)))
			{
				Ray ray = LuaScriptMgr.GetRay(L, 1);
				float radius = (float)LuaDLL.lua_tonumber(L, 2);
				float maxDistance = (float)LuaDLL.lua_tonumber(L, 3);
				bool b = Physics.SphereCast(ray, radius, maxDistance);
				LuaScriptMgr.Push(L, b);
				return 1;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float radius3 = (float)LuaDLL.lua_tonumber(L, 2);
			RaycastHit hitInfo;
			bool b3 = Physics.SphereCast(ray3, radius3, out hitInfo);
			LuaScriptMgr.Push(L, b3);
			LuaScriptMgr.Push(L, hitInfo);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius4 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit hitInfo2;
			bool b4 = Physics.SphereCast(vector, radius4, vector2, out hitInfo2);
			LuaScriptMgr.Push(L, b4);
			LuaScriptMgr.Push(L, hitInfo2);
			return 2;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float radius5 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 4);
			bool b5 = Physics.SphereCast(ray4, radius5, maxDistance2, layerMask);
			LuaScriptMgr.Push(L, b5);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float)))
		{
			Ray ray5 = LuaScriptMgr.GetRay(L, 1);
			float radius6 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 4);
			RaycastHit hitInfo3;
			bool b6 = Physics.SphereCast(ray5, radius6, out hitInfo3, maxDistance3);
			LuaScriptMgr.Push(L, b6);
			LuaScriptMgr.Push(L, hitInfo3);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float), typeof(int)))
		{
			Ray ray6 = LuaScriptMgr.GetRay(L, 1);
			float radius7 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 5);
			RaycastHit hitInfo4;
			bool b7 = Physics.SphereCast(ray6, radius7, out hitInfo4, maxDistance4, layerMask2);
			LuaScriptMgr.Push(L, b7);
			LuaScriptMgr.Push(L, hitInfo4);
			return 2;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray7 = LuaScriptMgr.GetRay(L, 1);
			float radius8 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 5);
			bool b8 = Physics.SphereCast(ray7, radius8, maxDistance5, layerMask3, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b8);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius9 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance6 = (float)LuaDLL.lua_tonumber(L, 5);
			RaycastHit hitInfo5;
			bool b9 = Physics.SphereCast(vector3, radius9, vector4, out hitInfo5, maxDistance6);
			LuaScriptMgr.Push(L, b9);
			LuaScriptMgr.Push(L, hitInfo5);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), null, typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			float radius10 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance7 = (float)LuaDLL.lua_tonumber(L, 5);
			int layerMask4 = (int)LuaDLL.lua_tonumber(L, 6);
			RaycastHit hitInfo6;
			bool b10 = Physics.SphereCast(vector5, radius10, vector6, out hitInfo6, maxDistance7, layerMask4);
			LuaScriptMgr.Push(L, b10);
			LuaScriptMgr.Push(L, hitInfo6);
			return 2;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), null, typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray8 = LuaScriptMgr.GetRay(L, 1);
			float radius11 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance8 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask5 = (int)LuaDLL.lua_tonumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 6);
			RaycastHit hitInfo7;
			bool b11 = Physics.SphereCast(ray8, radius11, out hitInfo7, maxDistance8, layerMask5, queryTriggerInteraction2);
			LuaScriptMgr.Push(L, b11);
			LuaScriptMgr.Push(L, hitInfo7);
			return 2;
		}
		if (num == 7)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			float radius12 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance9 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layerMask6 = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction3 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction));
			RaycastHit hitInfo8;
			bool b12 = Physics.SphereCast(vector7, radius12, vector8, out hitInfo8, maxDistance9, layerMask6, queryTriggerInteraction3);
			LuaScriptMgr.Push(L, b12);
			LuaScriptMgr.Push(L, hitInfo8);
			return 2;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCast");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CapsuleCastAll(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 4:
		{
			Vector3 vector10 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector11 = LuaScriptMgr.GetVector3(L, 2);
			float radius4 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector12 = LuaScriptMgr.GetVector3(L, 4);
			RaycastHit[] o4 = Physics.CapsuleCastAll(vector10, vector11, radius4, vector12);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		case 5:
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector9 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance3 = (float)LuaScriptMgr.GetNumber(L, 5);
			RaycastHit[] o3 = Physics.CapsuleCastAll(vector7, vector8, radius3, vector9, maxDistance3);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		case 6:
		{
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance2 = (float)LuaScriptMgr.GetNumber(L, 5);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 6);
			RaycastHit[] o2 = Physics.CapsuleCastAll(vector4, vector5, radius2, vector6, maxDistance2, layermask2);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		case 7:
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 4);
			float maxDistance = (float)LuaScriptMgr.GetNumber(L, 5);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 6);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 7, typeof(QueryTriggerInteraction));
			RaycastHit[] o = Physics.CapsuleCastAll(vector, vector2, radius, vector3, maxDistance, layermask, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CapsuleCastAll");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SphereCastAll(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			Ray ray2 = LuaScriptMgr.GetRay(L, 1);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 2);
			RaycastHit[] o2 = Physics.SphereCastAll(ray2, radius2);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float)))
			{
				Ray ray = LuaScriptMgr.GetRay(L, 1);
				float radius = (float)LuaDLL.lua_tonumber(L, 2);
				float maxDistance = (float)LuaDLL.lua_tonumber(L, 3);
				RaycastHit[] o = Physics.SphereCastAll(ray, radius, maxDistance);
				LuaScriptMgr.PushArray(L, o);
				return 1;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable)))
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
			RaycastHit[] o3 = Physics.SphereCastAll(vector, radius3, vector2);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int)))
		{
			Ray ray3 = LuaScriptMgr.GetRay(L, 1);
			float radius4 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance2 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask = (int)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o4 = Physics.SphereCastAll(ray3, radius4, maxDistance2, layerMask);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float)))
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius5 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance3 = (float)LuaDLL.lua_tonumber(L, 4);
			RaycastHit[] o5 = Physics.SphereCastAll(vector3, radius5, vector4, maxDistance3);
			LuaScriptMgr.PushArray(L, o5);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(LuaTable), typeof(float), typeof(int)))
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			float radius6 = (float)LuaDLL.lua_tonumber(L, 2);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance4 = (float)LuaDLL.lua_tonumber(L, 4);
			int layerMask2 = (int)LuaDLL.lua_tonumber(L, 5);
			RaycastHit[] o6 = Physics.SphereCastAll(vector5, radius6, vector6, maxDistance4, layerMask2);
			LuaScriptMgr.PushArray(L, o6);
			return 1;
		}
		if (num == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(LuaTable), typeof(float), typeof(float), typeof(int), typeof(QueryTriggerInteraction)))
		{
			Ray ray4 = LuaScriptMgr.GetRay(L, 1);
			float radius7 = (float)LuaDLL.lua_tonumber(L, 2);
			float maxDistance5 = (float)LuaDLL.lua_tonumber(L, 3);
			int layerMask3 = (int)LuaDLL.lua_tonumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetLuaObject(L, 5);
			RaycastHit[] o7 = Physics.SphereCastAll(ray4, radius7, maxDistance5, layerMask3, queryTriggerInteraction);
			LuaScriptMgr.PushArray(L, o7);
			return 1;
		}
		if (num == 6)
		{
			Vector3 vector7 = LuaScriptMgr.GetVector3(L, 1);
			float radius8 = (float)LuaScriptMgr.GetNumber(L, 2);
			Vector3 vector8 = LuaScriptMgr.GetVector3(L, 3);
			float maxDistance6 = (float)LuaScriptMgr.GetNumber(L, 4);
			int layerMask4 = (int)LuaScriptMgr.GetNumber(L, 5);
			QueryTriggerInteraction queryTriggerInteraction2 = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 6, typeof(QueryTriggerInteraction));
			RaycastHit[] o8 = Physics.SphereCastAll(vector7, radius8, vector8, maxDistance6, layerMask4, queryTriggerInteraction2);
			LuaScriptMgr.PushArray(L, o8);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Physics.SphereCastAll");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckSphere(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 2);
			bool b3 = Physics.CheckSphere(vector3, radius3);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		case 3:
		{
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 1);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask2 = (int)LuaScriptMgr.GetNumber(L, 3);
			bool b2 = Physics.CheckSphere(vector2, radius2, layerMask2);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 4:
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			float radius = (float)LuaScriptMgr.GetNumber(L, 2);
			int layerMask = (int)LuaScriptMgr.GetNumber(L, 3);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(QueryTriggerInteraction));
			bool b = Physics.CheckSphere(vector, radius, layerMask, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckSphere");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CheckCapsule(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 3:
		{
			Vector3 vector5 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector6 = LuaScriptMgr.GetVector3(L, 2);
			float radius3 = (float)LuaScriptMgr.GetNumber(L, 3);
			bool b3 = Physics.CheckCapsule(vector5, vector6, radius3);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		case 4:
		{
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 2);
			float radius2 = (float)LuaScriptMgr.GetNumber(L, 3);
			int layermask2 = (int)LuaScriptMgr.GetNumber(L, 4);
			bool b2 = Physics.CheckCapsule(vector3, vector4, radius2, layermask2);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 5:
		{
			Vector3 vector = LuaScriptMgr.GetVector3(L, 1);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 2);
			float radius = (float)LuaScriptMgr.GetNumber(L, 3);
			int layermask = (int)LuaScriptMgr.GetNumber(L, 4);
			QueryTriggerInteraction queryTriggerInteraction = (QueryTriggerInteraction)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(QueryTriggerInteraction));
			bool b = Physics.CheckCapsule(vector, vector2, radius, layermask, queryTriggerInteraction);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.CheckCapsule");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IgnoreCollision(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			Collider collider3 = (Collider)LuaScriptMgr.GetUnityObject(L, 1, typeof(Collider));
			Collider collider4 = (Collider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Collider));
			Physics.IgnoreCollision(collider3, collider4);
			return 0;
		}
		case 3:
		{
			Collider collider = (Collider)LuaScriptMgr.GetUnityObject(L, 1, typeof(Collider));
			Collider collider2 = (Collider)LuaScriptMgr.GetUnityObject(L, 2, typeof(Collider));
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Physics.IgnoreCollision(collider, collider2, boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.IgnoreCollision");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IgnoreLayerCollision(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			int layer3 = (int)LuaScriptMgr.GetNumber(L, 1);
			int layer4 = (int)LuaScriptMgr.GetNumber(L, 2);
			Physics.IgnoreLayerCollision(layer3, layer4);
			return 0;
		}
		case 3:
		{
			int layer = (int)LuaScriptMgr.GetNumber(L, 1);
			int layer2 = (int)LuaScriptMgr.GetNumber(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			Physics.IgnoreLayerCollision(layer, layer2, boolean);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Physics.IgnoreLayerCollision");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetIgnoreLayerCollision(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		int layer = (int)LuaScriptMgr.GetNumber(L, 1);
		int layer2 = (int)LuaScriptMgr.GetNumber(L, 2);
		bool ignoreLayerCollision = Physics.GetIgnoreLayerCollision(layer, layer2);
		LuaScriptMgr.Push(L, ignoreLayerCollision);
		return 1;
	}
}
