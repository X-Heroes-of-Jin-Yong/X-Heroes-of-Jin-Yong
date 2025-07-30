using System;
using LuaInterface;
using UnityEngine;

public class ParticleRendererWrap
{
	private static Type classType = typeof(ParticleRenderer);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[3]
		{
			new LuaMethod("New", _CreateParticleRenderer),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[10]
		{
			new LuaField("particleRenderMode", get_particleRenderMode, set_particleRenderMode),
			new LuaField("lengthScale", get_lengthScale, set_lengthScale),
			new LuaField("velocityScale", get_velocityScale, set_velocityScale),
			new LuaField("cameraVelocityScale", get_cameraVelocityScale, set_cameraVelocityScale),
			new LuaField("maxParticleSize", get_maxParticleSize, set_maxParticleSize),
			new LuaField("uvAnimationXTile", get_uvAnimationXTile, set_uvAnimationXTile),
			new LuaField("uvAnimationYTile", get_uvAnimationYTile, set_uvAnimationYTile),
			new LuaField("uvAnimationCycles", get_uvAnimationCycles, set_uvAnimationCycles),
			new LuaField("maxPartileSize", get_maxPartileSize, set_maxPartileSize),
			new LuaField("uvTiles", get_uvTiles, set_uvTiles)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.ParticleRenderer", typeof(ParticleRenderer), regs, fields, typeof(Renderer));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateParticleRenderer(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			ParticleRenderer obj = new ParticleRenderer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: ParticleRenderer.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_particleRenderMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particleRenderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particleRenderMode on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.particleRenderMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_lengthScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lengthScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lengthScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.lengthScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_velocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.velocityScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cameraVelocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cameraVelocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cameraVelocityScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.cameraVelocityScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxParticleSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxParticleSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxParticleSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.maxParticleSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationXTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationXTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationXTile on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.uvAnimationXTile);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationYTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationYTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationYTile on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.uvAnimationYTile);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvAnimationCycles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationCycles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationCycles on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.uvAnimationCycles);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_maxPartileSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxPartileSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxPartileSize on a nil value");
			}
		}
		LuaScriptMgr.Push(L, particleRenderer.maxPartileSize);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uvTiles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvTiles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvTiles on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, particleRenderer.uvTiles);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_particleRenderMode(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name particleRenderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index particleRenderMode on a nil value");
			}
		}
		particleRenderer.particleRenderMode = (ParticleRenderMode)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(ParticleRenderMode));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_lengthScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lengthScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lengthScale on a nil value");
			}
		}
		particleRenderer.lengthScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_velocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocityScale on a nil value");
			}
		}
		particleRenderer.velocityScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_cameraVelocityScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cameraVelocityScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cameraVelocityScale on a nil value");
			}
		}
		particleRenderer.cameraVelocityScale = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxParticleSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxParticleSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxParticleSize on a nil value");
			}
		}
		particleRenderer.maxParticleSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationXTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationXTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationXTile on a nil value");
			}
		}
		particleRenderer.uvAnimationXTile = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationYTile(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationYTile");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationYTile on a nil value");
			}
		}
		particleRenderer.uvAnimationYTile = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvAnimationCycles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvAnimationCycles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvAnimationCycles on a nil value");
			}
		}
		particleRenderer.uvAnimationCycles = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_maxPartileSize(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxPartileSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxPartileSize on a nil value");
			}
		}
		particleRenderer.maxPartileSize = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_uvTiles(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		ParticleRenderer particleRenderer = (ParticleRenderer)luaObject;
		if (particleRenderer == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvTiles");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvTiles on a nil value");
			}
		}
		particleRenderer.uvTiles = LuaScriptMgr.GetArrayObject<Rect>(L, 3);
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
