using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LuaInterface;
using UnityEngine;

namespace JyGame
{
	public static class LuaManager
	{
		private static string[] files = new string[2] { "main.lua", "test.lua" };

		private static bool _inited = false;

		public static LuaScriptMgr _lua;

		private static Dictionary<string, object> _luaConfig;

		public static byte[] JyGameLuaLoader(string path)
		{
			if (CommonSettings.MOD_MODE)
			{
				if (path.StartsWith("jygame/"))
				{
					string text = ModManager.ModBaseUrlPath + "lua/" + path.Replace("jygame/", string.Empty);
					Debug.Log("loading lua file : " + text);
					using (StreamReader streamReader = new StreamReader(text))
					{
						string s = ((!GlobalData.CurrentMod.enc) ? streamReader.ReadToEnd() : SaveManager.crcm(streamReader.ReadToEnd()));
						return new UTF8Encoding(true).GetBytes(s);
					}
				}
				string text2 = "TextAssets/lua/" + path;
				Debug.Log("loading lua file : " + text2);
				return Resource.GetBytes("TextAssets/lua/" + path, false);
			}
			string text3 = "TextAssets/lua/" + path;
			Debug.Log("loading lua file : " + text3);
			return Resource.GetBytes("TextAssets/lua/" + path, false);
		}

		public static void Reload()
		{
			_luaConfig = null;
			Init(true);
		}

		public static void Init(bool forceReset = false)
		{
			if (forceReset)
			{
				_inited = false;
				if (_lua != null)
				{
					_lua.Destroy();
				}
			}
			if (_inited)
			{
				return;
			}
			_lua = new LuaScriptMgr();
			_lua.Start();
			try
			{
				string[] array = files;
				foreach (string text in array)
				{
					_lua.DoFile("jygame/" + text);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
				FileLogger.instance.LogError("============LUA语法错误！===========");
				FileLogger.instance.LogError(ex.ToString());
			}
			_inited = true;
			LuaTable luaTable = Call<LuaTable>("ROOT_getLuaFiles", new object[0]);
			try
			{
				foreach (string value in luaTable.Values)
				{
					_lua.DoFile("jygame/" + value);
				}
			}
			catch (Exception ex2)
			{
				Debug.LogError(ex2.ToString());
				FileLogger.instance.LogError("============LUA语法错误！===========");
				FileLogger.instance.LogError(ex2.ToString());
			}
		}

		public static object[] Call(string functionName, params object[] paras)
		{
			if (!_inited)
			{
				Init();
			}
			LuaFunction luaFunction = _lua.GetLuaFunction(functionName);
			if (luaFunction == null)
			{
				Debug.LogError("调用了未定义的lua 函数:" + functionName);
				return null;
			}
			return luaFunction.Call(paras);
		}

		public static T Call<T>(string functionName, params object[] paras)
		{
			if (!_inited)
			{
				Init();
			}
			LuaFunction luaFunction = _lua.GetLuaFunction(functionName);
			if (luaFunction == null)
			{
				Debug.LogError("调用了未定义的lua 函数:" + functionName);
				return default(T);
			}
			object[] array = luaFunction.Call(paras);
			if (array.Length == 0 || (array[0] is bool && !(bool)array[0]))
			{
				return default(T);
			}
			return (T)array[0];
		}

		public static int CallWithIntReturn(string functionName, params object[] paras)
		{
			if (!_inited)
			{
				Init();
			}
			LuaFunction luaFunction = _lua.GetLuaFunction(functionName);
			if (luaFunction == null)
			{
				Debug.LogError("调用了未定义的lua 函数:" + functionName);
				return -1;
			}
			object[] array = luaFunction.Call(paras);
			return Convert.ToInt32(array[0]);
		}

		public static T GetConfig<T>(string key)
		{
			if (_luaConfig == null)
			{
				LuaTable luaTable = Call<LuaTable>("ROOT_getConfigList", new object[0]);
				_luaConfig = new Dictionary<string, object>();
				foreach (DictionaryEntry item in luaTable)
				{
					_luaConfig.Add(item.Key.ToString(), item.Value);
				}
			}
			object obj = _luaConfig[key];
			return (T)obj;
		}

		public static string GetConfig(string key)
		{
			return GetConfig<string>(key);
		}

		public static int GetConfigInt(string key)
		{
			return Convert.ToInt32(GetConfig<object>(key));
		}

		public static double GetConfigDouble(string key)
		{
			return Convert.ToDouble(GetConfig<object>(key));
		}
	}
}
