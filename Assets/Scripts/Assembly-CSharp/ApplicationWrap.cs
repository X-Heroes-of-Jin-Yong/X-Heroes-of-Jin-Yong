using System;
using LuaInterface;
using UnityEngine;

public class ApplicationWrap
{
	private static Type classType = typeof(Application);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[17]
		{
			new LuaMethod("Quit", Quit),
			new LuaMethod("CancelQuit", CancelQuit),
			new LuaMethod("LoadLevel", LoadLevel),
			new LuaMethod("LoadLevelAsync", LoadLevelAsync),
			new LuaMethod("LoadLevelAdditiveAsync", LoadLevelAdditiveAsync),
			new LuaMethod("UnloadLevel", UnloadLevel),
			new LuaMethod("LoadLevelAdditive", LoadLevelAdditive),
			new LuaMethod("GetStreamProgressForLevel", GetStreamProgressForLevel),
			new LuaMethod("CanStreamedLevelBeLoaded", CanStreamedLevelBeLoaded),
			new LuaMethod("CaptureScreenshot", CaptureScreenshot),
			new LuaMethod("HasProLicense", HasProLicense),
			new LuaMethod("ExternalCall", ExternalCall),
			new LuaMethod("OpenURL", OpenURL),
			new LuaMethod("RequestUserAuthorization", RequestUserAuthorization),
			new LuaMethod("HasUserAuthorization", HasUserAuthorization),
			new LuaMethod("New", _CreateApplication),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaField[] fields = new LuaField[35]
		{
			new LuaField("loadedLevel", get_loadedLevel, null),
			new LuaField("loadedLevelName", get_loadedLevelName, null),
			new LuaField("levelCount", get_levelCount, null),
			new LuaField("streamedBytes", get_streamedBytes, null),
			new LuaField("isPlaying", get_isPlaying, null),
			new LuaField("isEditor", get_isEditor, null),
			new LuaField("isWebPlayer", get_isWebPlayer, null),
			new LuaField("platform", get_platform, null),
			new LuaField("isMobilePlatform", get_isMobilePlatform, null),
			new LuaField("isConsolePlatform", get_isConsolePlatform, null),
			new LuaField("runInBackground", get_runInBackground, set_runInBackground),
			new LuaField("dataPath", get_dataPath, null),
			new LuaField("streamingAssetsPath", get_streamingAssetsPath, null),
			new LuaField("persistentDataPath", get_persistentDataPath, null),
			new LuaField("temporaryCachePath", get_temporaryCachePath, null),
			new LuaField("srcValue", get_srcValue, null),
			new LuaField("absoluteURL", get_absoluteURL, null),
			new LuaField("unityVersion", get_unityVersion, null),
			new LuaField("version", get_version, null),
			new LuaField("bundleIdentifier", get_bundleIdentifier, null),
			new LuaField("installMode", get_installMode, null),
			new LuaField("sandboxType", get_sandboxType, null),
			new LuaField("productName", get_productName, null),
			new LuaField("companyName", get_companyName, null),
			new LuaField("cloudProjectId", get_cloudProjectId, null),
			new LuaField("webSecurityEnabled", get_webSecurityEnabled, null),
			new LuaField("webSecurityHostUrl", get_webSecurityHostUrl, null),
			new LuaField("targetFrameRate", get_targetFrameRate, set_targetFrameRate),
			new LuaField("systemLanguage", get_systemLanguage, null),
			new LuaField("stackTraceLogType", get_stackTraceLogType, set_stackTraceLogType),
			new LuaField("backgroundLoadingPriority", get_backgroundLoadingPriority, set_backgroundLoadingPriority),
			new LuaField("internetReachability", get_internetReachability, null),
			new LuaField("genuine", get_genuine, null),
			new LuaField("genuineCheckAvailable", get_genuineCheckAvailable, null),
			new LuaField("isShowingSplashScreen", get_isShowingSplashScreen, null)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Application", typeof(Application), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateApplication(IntPtr L)
	{
		if (LuaDLL.lua_gettop(L) == 0)
		{
			Application o = new Application();
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadedLevel(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.loadedLevel);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_loadedLevelName(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.loadedLevelName);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_levelCount(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.levelCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_streamedBytes(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.streamedBytes);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isPlaying(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.isPlaying);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isEditor(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.isEditor);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isWebPlayer(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.isWebPlayer);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_platform(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.platform);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isMobilePlatform(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.isMobilePlatform);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isConsolePlatform(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.isConsolePlatform);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_runInBackground(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.runInBackground);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_dataPath(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.dataPath);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_streamingAssetsPath(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.streamingAssetsPath);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_persistentDataPath(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.persistentDataPath);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_temporaryCachePath(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.temporaryCachePath);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_srcValue(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.srcValue);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_absoluteURL(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.absoluteURL);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_unityVersion(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.unityVersion);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_version(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.version);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bundleIdentifier(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.identifier);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_installMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.installMode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_sandboxType(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.sandboxType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_productName(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.productName);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_companyName(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.companyName);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_cloudProjectId(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.cloudProjectId);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_webSecurityEnabled(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.webSecurityEnabled);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_webSecurityHostUrl(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.webSecurityHostUrl);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_targetFrameRate(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.targetFrameRate);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_systemLanguage(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.systemLanguage);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_stackTraceLogType(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.stackTraceLogType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_backgroundLoadingPriority(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.backgroundLoadingPriority);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_internetReachability(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.internetReachability);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_genuine(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.genuine);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_genuineCheckAvailable(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.genuineCheckAvailable);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isShowingSplashScreen(IntPtr L)
	{
		LuaScriptMgr.Push(L, Application.isShowingSplashScreen);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_runInBackground(IntPtr L)
	{
		Application.runInBackground = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_targetFrameRate(IntPtr L)
	{
		Application.targetFrameRate = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_stackTraceLogType(IntPtr L)
	{
		Application.stackTraceLogType = (StackTraceLogType)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(StackTraceLogType));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_backgroundLoadingPriority(IntPtr L)
	{
		Application.backgroundLoadingPriority = (ThreadPriority)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(ThreadPriority));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Quit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Application.Quit();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CancelQuit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Application.CancelQuit();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadLevel(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string name = LuaScriptMgr.GetString(L, 1);
			Application.LoadLevel(name);
			return 0;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int index = (int)LuaDLL.lua_tonumber(L, 1);
			Application.LoadLevel(index);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.LoadLevel");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadLevelAsync(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string levelName = LuaScriptMgr.GetString(L, 1);
			AsyncOperation o = Application.LoadLevelAsync(levelName);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int index = (int)LuaDLL.lua_tonumber(L, 1);
			AsyncOperation o2 = Application.LoadLevelAsync(index);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.LoadLevelAsync");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadLevelAdditiveAsync(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string levelName = LuaScriptMgr.GetString(L, 1);
			AsyncOperation o = Application.LoadLevelAdditiveAsync(levelName);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int index = (int)LuaDLL.lua_tonumber(L, 1);
			AsyncOperation o2 = Application.LoadLevelAdditiveAsync(index);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.LoadLevelAdditiveAsync");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnloadLevel(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string scenePath = LuaScriptMgr.GetString(L, 1);
			bool b = Application.UnloadLevel(scenePath);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int index = (int)LuaDLL.lua_tonumber(L, 1);
			bool b2 = Application.UnloadLevel(index);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.UnloadLevel");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadLevelAdditive(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string name = LuaScriptMgr.GetString(L, 1);
			Application.LoadLevelAdditive(name);
			return 0;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int index = (int)LuaDLL.lua_tonumber(L, 1);
			Application.LoadLevelAdditive(index);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.LoadLevelAdditive");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetStreamProgressForLevel(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string levelName = LuaScriptMgr.GetString(L, 1);
			float streamProgressForLevel = Application.GetStreamProgressForLevel(levelName);
			LuaScriptMgr.Push(L, streamProgressForLevel);
			return 1;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int levelIndex = (int)LuaDLL.lua_tonumber(L, 1);
			float streamProgressForLevel2 = Application.GetStreamProgressForLevel(levelIndex);
			LuaScriptMgr.Push(L, streamProgressForLevel2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.GetStreamProgressForLevel");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CanStreamedLevelBeLoaded(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(string)))
		{
			string levelName = LuaScriptMgr.GetString(L, 1);
			bool b = Application.CanStreamedLevelBeLoaded(levelName);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(int)))
		{
			int levelIndex = (int)LuaDLL.lua_tonumber(L, 1);
			bool b2 = Application.CanStreamedLevelBeLoaded(levelIndex);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Application.CanStreamedLevelBeLoaded");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CaptureScreenshot(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			Application.CaptureScreenshot(luaString2);
			return 0;
		}
		case 2:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			int superSize = (int)LuaScriptMgr.GetNumber(L, 2);
			Application.CaptureScreenshot(luaString, superSize);
			return 0;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Application.CaptureScreenshot");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HasProLicense(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		bool b = Application.HasProLicense();
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ExternalCall(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 2, num - 1);
		Application.ExternalCall(luaString, paramsObject);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int OpenURL(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		Application.OpenURL(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int RequestUserAuthorization(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UserAuthorization mode = (UserAuthorization)(int)LuaScriptMgr.GetNetObject(L, 1, typeof(UserAuthorization));
		AsyncOperation o = Application.RequestUserAuthorization(mode);
		LuaScriptMgr.PushObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HasUserAuthorization(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UserAuthorization mode = (UserAuthorization)(int)LuaScriptMgr.GetNetObject(L, 1, typeof(UserAuthorization));
		bool b = Application.HasUserAuthorization(mode);
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
