using System;
using System.IO;
using JyGame;
using UnityEngine;

namespace LuaInterface
{
	public static class LuaStatic
	{
		public static ReadLuaFile Load;

		public static string init_luanet;

		static LuaStatic()
		{
			init_luanet = "local metatable = {}\n            local rawget = rawget\n            local debug = debug\n            local import_type = luanet.import_type\n            local load_assembly = luanet.load_assembly\n            luanet.error, luanet.type = error, type\n            -- Lookup a .NET identifier component.\n            function metatable:__index(key) -- key is e.g. 'Form'\n            -- Get the fully-qualified name, e.g. 'System.Windows.Forms.Form'\n            local fqn = rawget(self,'.fqn')\n            fqn = ((fqn and fqn .. '.') or '') .. key\n\n            -- Try to find either a luanet function or a CLR type\n            local obj = rawget(luanet,key) or import_type(fqn)\n\n            -- If key is neither a luanet function or a CLR type, then it is simply\n            -- an identifier component.\n            if obj == nil then\n                -- It might be an assembly, so we load it too.\n                    pcall(load_assembly,fqn)\n                    obj = { ['.fqn'] = fqn }\n            setmetatable(obj, metatable)\n            end\n\n            -- Cache this lookup\n            rawset(self, key, obj)\n            return obj\n            end\n\n            -- A non-type has been called; e.g. foo = System.Foo()\n            function metatable:__call(...)\n            error('No such type: ' .. rawget(self,'.fqn'), 2)\n            end\n\n            -- This is the root of the .NET namespace\n            luanet['.fqn'] = false\n            setmetatable(luanet, metatable)\n\n            -- Preload the mscorlib assembly\n            luanet.load_assembly('mscorlib')\n\n            function traceback(msg)                \n                return debug.traceback(msg, 1)                \n            end";
			Load = LuaManager.JyGameLuaLoader;
		}

		public static byte[] DefaultLoader(string path)
		{
			byte[] result = null;
			if (File.Exists(path))
			{
				result = File.ReadAllBytes(path);
			}
			return result;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int panic(IntPtr L)
		{
			string message = string.Format("unprotected error in call to Lua API ({0})", LuaDLL.lua_tostring(L, -1));
			LuaDLL.lua_pop(L, 1);
			throw new LuaException(message);
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int traceback(IntPtr L)
		{
			LuaDLL.lua_getglobal(L, "debug");
			LuaDLL.lua_getfield(L, -1, "traceback");
			LuaDLL.lua_pushvalue(L, 1);
			LuaDLL.lua_pushnumber(L, 2.0);
			LuaDLL.lua_call(L, 2, 1);
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int print(IntPtr L)
		{
			int num = LuaDLL.lua_gettop(L);
			string text = string.Empty;
			LuaDLL.lua_getglobal(L, "tostring");
			for (int i = 1; i <= num; i++)
			{
				LuaDLL.lua_pushvalue(L, -1);
				LuaDLL.lua_pushvalue(L, i);
				LuaDLL.lua_call(L, 1, 1);
				if (i > 1)
				{
					text += "\t";
				}
				text += LuaDLL.lua_tostring(L, -1);
				LuaDLL.lua_pop(L, 1);
			}
			Debug.Log("LUA: " + text);
			return 0;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int loader(IntPtr L)
		{
			string empty = string.Empty;
			empty = LuaDLL.lua_tostring(L, 1);
			string text = empty.ToLower();
			if (text.EndsWith(".lua"))
			{
				int length = empty.LastIndexOf('.');
				empty = empty.Substring(0, length);
			}
			empty = empty.Replace('.', '/') + ".lua";
			LuaScriptMgr mgrFromLuaState = LuaScriptMgr.GetMgrFromLuaState(L);
			if (mgrFromLuaState == null)
			{
				return 0;
			}
			LuaDLL.lua_pushstdcallcfunction(L, mgrFromLuaState.lua.tracebackFunction);
			int oldTop = LuaDLL.lua_gettop(L);
			byte[] array = Load(empty);
			if (array == null)
			{
				if (!empty.Contains("mobdebug"))
				{
					Debugger.LogError("Loader lua file failed: {0}", empty);
				}
				LuaDLL.lua_pop(L, 1);
				return 0;
			}
			if (LuaDLL.luaL_loadbuffer(L, array, array.Length, empty) != 0)
			{
				mgrFromLuaState.lua.ThrowExceptionFromError(oldTop);
				LuaDLL.lua_pop(L, 1);
			}
			return 1;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int dofile(IntPtr L)
		{
			string empty = string.Empty;
			empty = LuaDLL.lua_tostring(L, 1);
			string text = empty.ToLower();
			if (text.EndsWith(".lua"))
			{
				int length = empty.LastIndexOf('.');
				empty = empty.Substring(0, length);
			}
			empty = empty.Replace('.', '/') + ".lua";
			int num = LuaDLL.lua_gettop(L);
			byte[] array = Load(empty);
			if (array == null)
			{
				return LuaDLL.lua_gettop(L) - num;
			}
			if (LuaDLL.luaL_loadbuffer(L, array, array.Length, empty) == 0)
			{
				LuaDLL.lua_call(L, 0, LuaDLL.LUA_MULTRET);
			}
			return LuaDLL.lua_gettop(L) - num;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int loadfile(IntPtr L)
		{
			return loader(L);
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int importWrap(IntPtr L)
		{
			string empty = string.Empty;
			empty = LuaDLL.lua_tostring(L, 1);
			empty = empty.Replace('.', '_');
			if (!string.IsNullOrEmpty(empty))
			{
				LuaBinder.Bind(L, empty);
			}
			return 0;
		}
	}
}
