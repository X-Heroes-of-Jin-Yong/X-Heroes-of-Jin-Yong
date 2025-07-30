using System;
using LuaInterface;
using UnityEngine;

public class ParticleEmitterWrap
{
	private static Type classType = typeof(ParticleEmitter);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[6]
		{
			new LuaMethod("ClearParticles", ClearParticles),
			new LuaMethod("Emit", Emit),
			new LuaMethod("Simulate", Simulate),
			new LuaMethod("New", _CreateParticleEmitter),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[18]
		{
			new LuaField("emit", get_emit, set_emit),
			new LuaField("minSize", get_minSize, set_minSize),
			new LuaField("maxSize", get_maxSize, set_maxSize),
			new LuaField("minEnergy", get_minEnergy, set_minEnergy),
			new LuaField("maxEnergy", get_maxEnergy, set_maxEnergy),
			new LuaField("minEmission", get_minEmission, set_minEmission),
			new LuaField("maxEmission", get_maxEmission, set_maxEmission),
			new LuaField("emitterVelocityScale", get_emitterVelocityScale, set_emitterVelocityScale),
			new LuaField("worldVelocity", get_worldVelocity, set_worldVelocity),
			new LuaField("localVelocity", get_localVelocity, set_localVelocity),
			new LuaField("rndVelocity", get_rndVelocity, set_rndVelocity),
			new LuaField("useWorldSpace", get_useWorldSpace, set_useWorldSpace),
			new LuaField("rndRotation", get_rndRotation, set_rndRotation),
			new LuaField("angularVelocity", get_angularVelocity, set_angularVelocity),
			new LuaField("rndAngularVelocity", get_rndAngularVelocity, set_rndAngularVelocity),
			new LuaField("particles", get_particles, set_particles),
			new LuaField("particleCount", get_particleCount, null),
			new LuaField("enabled", get_enabled, set_enabled)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.ParticleEmitter", typeof(ParticleEmitter), regs, fields, typeof(Component));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateParticleEmitter(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ParticleEmitter class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_emit(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name emit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index emit on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.emit);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.minSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.maxSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minEnergy(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minEnergy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minEnergy on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.minEnergy);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxEnergy(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxEnergy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxEnergy on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.maxEnergy);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_minEmission(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minEmission");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minEmission on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.minEmission);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxEmission(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxEmission");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxEmission on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.maxEmission);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_emitterVelocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name emitterVelocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index emitterVelocityScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.emitterVelocityScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_worldVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldVelocity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.worldVelocity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_localVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localVelocity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.localVelocity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rndVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rndVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rndVelocity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.rndVelocity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useWorldSpace(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useWorldSpace");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useWorldSpace on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.useWorldSpace);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rndRotation(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rndRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rndRotation on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.rndRotation);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_angularVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name angularVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index angularVelocity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.angularVelocity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_rndAngularVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rndAngularVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rndAngularVelocity on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.rndAngularVelocity);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particles on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, particleEmitter.particles);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particleCount(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particleCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particleCount on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.particleCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleEmitter.enabled);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_emit(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name emit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index emit on a nil value");
			}
		}
		particleEmitter.emit = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minSize on a nil value");
			}
		}
		particleEmitter.minSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxSize on a nil value");
			}
		}
		particleEmitter.maxSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minEnergy(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minEnergy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minEnergy on a nil value");
			}
		}
		particleEmitter.minEnergy = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxEnergy(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxEnergy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxEnergy on a nil value");
			}
		}
		particleEmitter.maxEnergy = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_minEmission(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minEmission");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minEmission on a nil value");
			}
		}
		particleEmitter.minEmission = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxEmission(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxEmission");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxEmission on a nil value");
			}
		}
		particleEmitter.maxEmission = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_emitterVelocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name emitterVelocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index emitterVelocityScale on a nil value");
			}
		}
		particleEmitter.emitterVelocityScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_worldVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldVelocity on a nil value");
			}
		}
		particleEmitter.worldVelocity = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_localVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localVelocity on a nil value");
			}
		}
		particleEmitter.localVelocity = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rndVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rndVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rndVelocity on a nil value");
			}
		}
		particleEmitter.rndVelocity = LuaScriptMgr.GetVector3(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useWorldSpace(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useWorldSpace");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useWorldSpace on a nil value");
			}
		}
		particleEmitter.useWorldSpace = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rndRotation(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rndRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rndRotation on a nil value");
			}
		}
		particleEmitter.rndRotation = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_angularVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name angularVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index angularVelocity on a nil value");
			}
		}
		particleEmitter.angularVelocity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_rndAngularVelocity(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rndAngularVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rndAngularVelocity on a nil value");
			}
		}
		particleEmitter.rndAngularVelocity = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_particles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particles on a nil value");
			}
		}
		particleEmitter.particles = LuaScriptMgr.GetArrayObject<Particle>(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enabled(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)luaObject;
		if (particleEmitter == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}
		particleEmitter.enabled = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ClearParticles(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		ParticleEmitter particleEmitter = (ParticleEmitter)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleEmitter");
		particleEmitter.ClearParticles();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Emit(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			ParticleEmitter particleEmitter4 = (ParticleEmitter)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleEmitter");
			particleEmitter4.Emit();
			return 0;
		}
		case 2:
		{
			ParticleEmitter particleEmitter3 = (ParticleEmitter)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleEmitter");
			int count = (int)LuaScriptMgr.GetNumber(L, 2);
			particleEmitter3.Emit(count);
			return 0;
		}
		case 6:
		{
			ParticleEmitter particleEmitter2 = (ParticleEmitter)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleEmitter");
			Vector3 vector3 = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector4 = LuaScriptMgr.GetVector3(L, 3);
			float size2 = (float)LuaScriptMgr.GetNumber(L, 4);
			float energy2 = (float)LuaScriptMgr.GetNumber(L, 5);
			Color color2 = LuaScriptMgr.GetColor(L, 6);
			particleEmitter2.Emit(vector3, vector4, size2, energy2, color2);
			return 0;
		}
		case 8:
		{
			ParticleEmitter particleEmitter = (ParticleEmitter)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleEmitter");
			Vector3 vector = LuaScriptMgr.GetVector3(L, 2);
			Vector3 vector2 = LuaScriptMgr.GetVector3(L, 3);
			float size = (float)LuaScriptMgr.GetNumber(L, 4);
			float energy = (float)LuaScriptMgr.GetNumber(L, 5);
			Color color = LuaScriptMgr.GetColor(L, 6);
			float rotation = (float)LuaScriptMgr.GetNumber(L, 7);
			float angularVelocity = (float)LuaScriptMgr.GetNumber(L, 8);
			particleEmitter.Emit(vector, vector2, size, energy, color, rotation, angularVelocity);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: ParticleEmitter.Emit");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Simulate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		ParticleEmitter particleEmitter = (ParticleEmitter)LuaScriptMgr.GetUnityObjectSelf(L, 1, "ParticleEmitter");
		float deltaTime = (float)LuaScriptMgr.GetNumber(L, 2);
		particleEmitter.Simulate(deltaTime);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object obj = LuaScriptMgr.GetLuaObject(L, 1) as UnityEngine.Object;
		UnityEngine.Object obj2 = LuaScriptMgr.GetLuaObject(L, 2) as UnityEngine.Object;
		bool b = obj == obj2;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
