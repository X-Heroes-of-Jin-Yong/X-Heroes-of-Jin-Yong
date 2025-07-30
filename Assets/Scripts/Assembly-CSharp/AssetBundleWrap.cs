using System;
using LuaInterface;
using UnityEngine;

public class AssetBundleWrap
{
	private static Type classType = typeof(AssetBundle);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[16]
		{
			new LuaMethod("CreateFromMemory", CreateFromMemory),
			new LuaMethod("CreateFromMemoryImmediate", CreateFromMemoryImmediate),
			new LuaMethod("CreateFromFile", CreateFromFile),
			new LuaMethod("Contains", Contains),
			new LuaMethod("LoadAsset", LoadAsset),
			new LuaMethod("LoadAssetAsync", LoadAssetAsync),
			new LuaMethod("LoadAssetWithSubAssets", LoadAssetWithSubAssets),
			new LuaMethod("LoadAssetWithSubAssetsAsync", LoadAssetWithSubAssetsAsync),
			new LuaMethod("LoadAllAssets", LoadAllAssets),
			new LuaMethod("LoadAllAssetsAsync", LoadAllAssetsAsync),
			new LuaMethod("Unload", Unload),
			new LuaMethod("GetAllAssetNames", GetAllAssetNames),
			new LuaMethod("GetAllScenePaths", GetAllScenePaths),
			new LuaMethod("New", _CreateAssetBundle),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[1]
		{
			new LuaField("mainAsset", get_mainAsset, null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AssetBundle", typeof(AssetBundle), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateAssetBundle(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			AssetBundle obj = new AssetBundle();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainAsset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		AssetBundle assetBundle = (AssetBundle)luaObject;
		if (assetBundle == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainAsset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainAsset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, assetBundle.mainAsset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreateFromMemory(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 1);
		AssetBundleCreateRequest o = AssetBundle.CreateFromMemory(arrayNumber);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreateFromMemoryImmediate(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 1);
		AssetBundle obj = AssetBundle.CreateFromMemoryImmediate(arrayNumber);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CreateFromFile(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		AssetBundle obj = AssetBundle.CreateFromFile(luaString);
		LuaScriptMgr.Push(L, obj);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Contains(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool b = assetBundle.Contains(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAsset(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			UnityEngine.Object obj2 = assetBundle2.LoadAsset(luaString2);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		case 3:
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			UnityEngine.Object obj = assetBundle.LoadAsset(luaString, typeObject);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAsset");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetAsync(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			AssetBundleRequest o2 = assetBundle2.LoadAssetAsync(luaString2);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		case 3:
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			AssetBundleRequest o = assetBundle.LoadAssetAsync(luaString, typeObject);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAssetAsync");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetWithSubAssets(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			UnityEngine.Object[] o2 = assetBundle2.LoadAssetWithSubAssets(luaString2);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		case 3:
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			UnityEngine.Object[] o = assetBundle.LoadAssetWithSubAssets(luaString, typeObject);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAssetWithSubAssets");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAssetWithSubAssetsAsync(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			AssetBundleRequest o2 = assetBundle2.LoadAssetWithSubAssetsAsync(luaString2);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		case 3:
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 3);
			AssetBundleRequest o = assetBundle.LoadAssetWithSubAssetsAsync(luaString, typeObject);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAssetWithSubAssetsAsync");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAllAssets(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			UnityEngine.Object[] o2 = assetBundle2.LoadAllAssets();
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		case 2:
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			UnityEngine.Object[] o = assetBundle.LoadAllAssets(typeObject);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAllAssets");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadAllAssetsAsync(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			AssetBundle assetBundle2 = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			AssetBundleRequest o2 = assetBundle2.LoadAllAssetsAsync();
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		case 2:
		{
			AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
			Type typeObject = LuaScriptMgr.GetTypeObject(L, 2);
			AssetBundleRequest o = assetBundle.LoadAllAssetsAsync(typeObject);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: AssetBundle.LoadAllAssetsAsync");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Unload(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		bool boolean = LuaScriptMgr.GetBoolean(L, 2);
		assetBundle.Unload(boolean);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAllAssetNames(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		string[] allAssetNames = assetBundle.GetAllAssetNames();
		LuaScriptMgr.PushArray(L, allAssetNames);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAllScenePaths(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		AssetBundle assetBundle = (AssetBundle)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AssetBundle");
		string[] allScenePaths = assetBundle.GetAllScenePaths();
		LuaScriptMgr.PushArray(L, allScenePaths);
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
