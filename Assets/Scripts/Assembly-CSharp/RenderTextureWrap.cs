using System;
using LuaInterface;
using UnityEngine;

public class RenderTextureWrap
{
	private static Type classType = typeof(RenderTexture);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[13]
		{
			new LuaMethod("GetTemporary", GetTemporary),
			new LuaMethod("ReleaseTemporary", ReleaseTemporary),
			new LuaMethod("Create", Create),
			new LuaMethod("Release", Release),
			new LuaMethod("IsCreated", IsCreated),
			new LuaMethod("DiscardContents", DiscardContents),
			new LuaMethod("MarkRestoreExpected", MarkRestoreExpected),
			new LuaMethod("SetGlobalShaderProperty", SetGlobalShaderProperty),
			new LuaMethod("GetTexelOffset", GetTexelOffset),
			new LuaMethod("SupportsStencil", SupportsStencil),
			new LuaMethod("New", _CreateRenderTexture),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[16]
		{
			new LuaField("width", get_width, set_width),
			new LuaField("height", get_height, set_height),
			new LuaField("depth", get_depth, set_depth),
			new LuaField("isPowerOfTwo", get_isPowerOfTwo, set_isPowerOfTwo),
			new LuaField("sRGB", get_sRGB, null),
			new LuaField("format", get_format, set_format),
			new LuaField("useMipMap", get_useMipMap, set_useMipMap),
			new LuaField("generateMips", get_generateMips, set_generateMips),
			new LuaField("isCubemap", get_isCubemap, set_isCubemap),
			new LuaField("isVolume", get_isVolume, set_isVolume),
			new LuaField("volumeDepth", get_volumeDepth, set_volumeDepth),
			new LuaField("antiAliasing", get_antiAliasing, set_antiAliasing),
			new LuaField("enableRandomWrite", get_enableRandomWrite, set_enableRandomWrite),
			new LuaField("colorBuffer", get_colorBuffer, null),
			new LuaField("depthBuffer", get_depthBuffer, null),
			new LuaField("active", get_active, set_active)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.RenderTexture", typeof(RenderTexture), regs, fields, typeof(Texture));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateRenderTexture(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 3:
		{
			int width3 = (int)LuaScriptMgr.GetNumber(L, 1);
			int height3 = (int)LuaScriptMgr.GetNumber(L, 2);
			int depth3 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTexture obj3 = new RenderTexture(width3, height3, depth3);
			LuaScriptMgr.Push(L, obj3);
			return 1;
		}
		case 4:
		{
			int width2 = (int)LuaScriptMgr.GetNumber(L, 1);
			int height2 = (int)LuaScriptMgr.GetNumber(L, 2);
			int depth2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat format2 = (RenderTextureFormat)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTexture obj2 = new RenderTexture(width2, height2, depth2, format2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		case 5:
		{
			int width = (int)LuaScriptMgr.GetNumber(L, 1);
			int height = (int)LuaScriptMgr.GetNumber(L, 2);
			int depth = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat format = (RenderTextureFormat)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite readWrite = (RenderTextureReadWrite)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			RenderTexture obj = new RenderTexture(width, height, depth, format, readWrite);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: RenderTexture.New");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_width(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index width on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.width);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_height(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.height);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_depth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depth on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.depth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPowerOfTwo(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPowerOfTwo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPowerOfTwo on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.isPowerOfTwo);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sRGB(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sRGB");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sRGB on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.sRGB);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_format(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name format");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index format on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.format);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_useMipMap(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useMipMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useMipMap on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.useMipMap);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_generateMips(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name generateMips");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index generateMips on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.autoGenerateMips);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isCubemap(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isCubemap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isCubemap on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.isCubemap);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isVolume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isVolume on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.isVolume);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_volumeDepth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volumeDepth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volumeDepth on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.volumeDepth);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_antiAliasing(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name antiAliasing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index antiAliasing on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.antiAliasing);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_enableRandomWrite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enableRandomWrite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enableRandomWrite on a nil value");
			}
		}
		LuaScriptMgr.Push(L, renderTexture.enableRandomWrite);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_colorBuffer(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colorBuffer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colorBuffer on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, renderTexture.colorBuffer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_depthBuffer(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depthBuffer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depthBuffer on a nil value");
			}
		}
		LuaScriptMgr.PushValue(L, renderTexture.depthBuffer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_active(IntPtr L)
	{
		LuaScriptMgr.Push(L, RenderTexture.active);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_width(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name width");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index width on a nil value");
			}
		}
		renderTexture.width = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_height(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name height");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index height on a nil value");
			}
		}
		renderTexture.height = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_depth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depth on a nil value");
			}
		}
		renderTexture.depth = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isPowerOfTwo(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPowerOfTwo");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPowerOfTwo on a nil value");
			}
		}
		renderTexture.isPowerOfTwo = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_format(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name format");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index format on a nil value");
			}
		}
		renderTexture.format = (RenderTextureFormat)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(RenderTextureFormat));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_useMipMap(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useMipMap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useMipMap on a nil value");
			}
		}
		renderTexture.useMipMap = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_generateMips(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name generateMips");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index generateMips on a nil value");
			}
		}
		renderTexture.autoGenerateMips = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isCubemap(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isCubemap");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isCubemap on a nil value");
			}
		}
		renderTexture.isCubemap = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_isVolume(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isVolume");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isVolume on a nil value");
			}
		}
		renderTexture.isVolume = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_volumeDepth(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name volumeDepth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index volumeDepth on a nil value");
			}
		}
		renderTexture.volumeDepth = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_antiAliasing(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name antiAliasing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index antiAliasing on a nil value");
			}
		}
		renderTexture.antiAliasing = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_enableRandomWrite(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		RenderTexture renderTexture = (RenderTexture)luaObject;
		if (renderTexture == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enableRandomWrite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enableRandomWrite on a nil value");
			}
		}
		renderTexture.enableRandomWrite = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_active(IntPtr L)
	{
		RenderTexture.active = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 3, typeof(RenderTexture));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTemporary(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			int width5 = (int)LuaScriptMgr.GetNumber(L, 1);
			int height5 = (int)LuaScriptMgr.GetNumber(L, 2);
			RenderTexture temporary5 = RenderTexture.GetTemporary(width5, height5);
			LuaScriptMgr.Push(L, temporary5);
			return 1;
		}
		case 3:
		{
			int width4 = (int)LuaScriptMgr.GetNumber(L, 1);
			int height4 = (int)LuaScriptMgr.GetNumber(L, 2);
			int depthBuffer4 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTexture temporary4 = RenderTexture.GetTemporary(width4, height4, depthBuffer4);
			LuaScriptMgr.Push(L, temporary4);
			return 1;
		}
		case 4:
		{
			int width3 = (int)LuaScriptMgr.GetNumber(L, 1);
			int height3 = (int)LuaScriptMgr.GetNumber(L, 2);
			int depthBuffer3 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat format3 = (RenderTextureFormat)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTexture temporary3 = RenderTexture.GetTemporary(width3, height3, depthBuffer3, format3);
			LuaScriptMgr.Push(L, temporary3);
			return 1;
		}
		case 5:
		{
			int width2 = (int)LuaScriptMgr.GetNumber(L, 1);
			int height2 = (int)LuaScriptMgr.GetNumber(L, 2);
			int depthBuffer2 = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat format2 = (RenderTextureFormat)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite readWrite2 = (RenderTextureReadWrite)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			RenderTexture temporary2 = RenderTexture.GetTemporary(width2, height2, depthBuffer2, format2, readWrite2);
			LuaScriptMgr.Push(L, temporary2);
			return 1;
		}
		case 6:
		{
			int width = (int)LuaScriptMgr.GetNumber(L, 1);
			int height = (int)LuaScriptMgr.GetNumber(L, 2);
			int depthBuffer = (int)LuaScriptMgr.GetNumber(L, 3);
			RenderTextureFormat format = (RenderTextureFormat)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(RenderTextureFormat));
			RenderTextureReadWrite readWrite = (RenderTextureReadWrite)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(RenderTextureReadWrite));
			int antiAliasing = (int)LuaScriptMgr.GetNumber(L, 6);
			RenderTexture temporary = RenderTexture.GetTemporary(width, height, depthBuffer, format, readWrite, antiAliasing);
			LuaScriptMgr.Push(L, temporary);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: RenderTexture.GetTemporary");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ReleaseTemporary(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture temp = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 1, typeof(RenderTexture));
		RenderTexture.ReleaseTemporary(temp);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Create(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture renderTexture = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		bool b = renderTexture.Create();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Release(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture renderTexture = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		renderTexture.Release();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsCreated(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture renderTexture = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		bool b = renderTexture.IsCreated();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DiscardContents(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			RenderTexture renderTexture2 = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
			renderTexture2.DiscardContents();
			return 0;
		}
		case 3:
		{
			RenderTexture renderTexture = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
			bool boolean = LuaScriptMgr.GetBoolean(L, 2);
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 3);
			renderTexture.DiscardContents(boolean, boolean2);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: RenderTexture.DiscardContents");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int MarkRestoreExpected(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture renderTexture = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		renderTexture.MarkRestoreExpected();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGlobalShaderProperty(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		RenderTexture renderTexture = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		renderTexture.SetGlobalShaderProperty(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTexelOffset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture renderTexture = (RenderTexture)LuaScriptMgr.GetUnityObjectSelf(L, 1, "RenderTexture");
		Vector2 texelOffset = renderTexture.GetTexelOffset();
		LuaScriptMgr.Push(L, texelOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SupportsStencil(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		RenderTexture rt = (RenderTexture)LuaScriptMgr.GetUnityObject(L, 1, typeof(RenderTexture));
		bool b = RenderTexture.SupportsStencil(rt);
		LuaScriptMgr.Push(L, b);
		return 1;
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
