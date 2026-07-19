using System.Collections.Generic;

namespace JyGame
{
	public class ModManager
	{
		public static List<ModInfo> mods;

		public static string ModBaseUrl
		{
			get
			{
				return "file:///" + CommonSettings.persistentDataPath + "/modcache/" + GlobalData.CurrentMod.key + "/";
			}
		}

		public static string ModBaseUrlPath
		{
			get
			{
				return CommonSettings.persistentDataPath + "/modcache/" + GlobalData.CurrentMod.key + "/";
			}
		}

		public static void SetCurrentMod(ModInfo mod)
		{
			ResourceManager.ResetInitFlag();
			AssetBundleManager.IsInited = false;
			GlobalData.CurrentMod = mod;
			ModData.Load();
			// HybridCLR: 加载 Mod 的 C# DLL（替代原 XLua 的 Lua 脚本加载）
			if (mod != null)
			{
				ModScriptManager.LoadMod(mod.key);
			}
			else
			{
				ModScriptManager.UnloadMod();
			}
		}
	}
}
