using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace JyGame
{
    /// <summary>
    /// Mod 脚本管理器 — HybridCLR 热更新 Mod 加载
    /// 替代原 XLua 的 Mod Lua 文件加载机制。
    ///
    /// Mod 开发者将 C# 代码编译为 DLL，放入 gamedata/modcache/{modKey}/Mod.dll
    /// DLL 中需包含 ModEntry 类提供扩展方法。
    ///
    /// 加载优先级：Mod DLL 方法 → 内置 C# 默认实现
    /// </summary>
    public static class ModScriptManager
    {
        /// <summary>当前加载的 Mod 程序集</summary>
        public static Assembly CurrentModAssembly { get; private set; }

        /// <summary>有 Mod 加载吗</summary>
        public static bool HasMod => CurrentModAssembly != null;

        /// <summary>Mod 入口类型</summary>
        private static Type _modEntryType;

        /// <summary>方法缓存，避免重复反射查找</summary>
        private static Dictionary<string, MethodInfo> _methodCache = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// 加载 Mod DLL
        /// </summary>
        /// <param name="modKey">Mod 标识符</param>
        /// <returns>是否加载成功</returns>
        public static bool LoadMod(string modKey)
        {
            UnloadMod();

            string dllPath = Path.Combine(CommonSettings.persistentDataPath, "modcache", modKey, "Mod.dll");
            if (!File.Exists(dllPath))
            {
                // 兼容 .bytes 后缀
                dllPath = Path.Combine(CommonSettings.persistentDataPath, "modcache", modKey, "Mod.dll.bytes");
            }

            if (!File.Exists(dllPath))
            {
                Debug.Log($"[ModScriptManager] No Mod.dll found for mod '{modKey}', using built-in defaults.");
                return false;
            }

            try
            {
                byte[] dllBytes = File.ReadAllBytes(dllPath);
                Debug.Log($"[ModScriptManager] Loading Mod.dll for '{modKey}' ({dllBytes.Length} bytes)...");

#if UNITY_EDITOR
                // Editor 下无法卸载已加载的程序集，使用 Assembly.Load 会导致重复加载错误
                // 尝试 LoadFile 或者直接用 Load(byte[]) 加载到新 domain
                CurrentModAssembly = Assembly.Load(dllBytes);
#else
                // IL2CPP 运行时直接加载
                CurrentModAssembly = Assembly.Load(dllBytes);
#endif

                if (CurrentModAssembly == null)
                {
                    Debug.LogError($"[ModScriptManager] Failed to load Mod.dll for '{modKey}'.");
                    return false;
                }

                _modEntryType = CurrentModAssembly.GetType("ModEntry");
                _methodCache.Clear();

                if (_modEntryType != null)
                {
                    Debug.Log($"[ModScriptManager] Found ModEntry type in {modKey} Mod.dll.");
                }
                else
                {
                    Debug.Log($"[ModScriptManager] No ModEntry type found in {modKey} Mod.dll. Using default implementations.");
                }

                Debug.Log($"[ModScriptManager] Mod '{modKey}' loaded successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModScriptManager] Error loading Mod.dll for '{modKey}': {ex}");
                CurrentModAssembly = null;
                _modEntryType = null;
                return false;
            }
        }

        /// <summary>
        /// 卸载当前 Mod
        /// </summary>
        public static void UnloadMod()
        {
            if (CurrentModAssembly != null)
            {
                Debug.Log("[ModScriptManager] Unloading current mod.");
            }
            CurrentModAssembly = null;
            _modEntryType = null;
            _methodCache.Clear();
        }

        /// <summary>
        /// 尝试调用 Mod DLL 中的方法
        /// 如果 Mod 提供了该方法则调用，否则返回 false
        /// </summary>
        public static bool TryCall(string methodName, out object result, params object[] args)
        {
            result = null;

            if (_modEntryType == null) return false;

            try
            {
                if (!_methodCache.TryGetValue(methodName, out MethodInfo method))
                {
                    method = _modEntryType.GetMethod(methodName,
                        BindingFlags.Public | BindingFlags.Static);
                    _methodCache[methodName] = method;
                }

                if (method == null) return false;

                result = method.Invoke(null, args);
                return true;
            }
            catch (TargetInvocationException ex)
            {
                Debug.LogError($"[ModScriptManager] Error in mod method '{methodName}': {ex.InnerException}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[ModScriptManager] Error calling mod method '{methodName}': {ex}");
                return false;
            }
        }

        /// <summary>
        /// 尝试调用 Mod DLL 中的方法并获取返回值
        /// </summary>
        public static bool TryCall<T>(string methodName, out T result, params object[] args)
        {
            if (TryCall(methodName, out object objResult, args))
            {
                if (objResult is T typedResult)
                {
                    result = typedResult;
                    return true;
                }
            }
            result = default(T);
            return false;
        }

        /// <summary>
        /// 尝试调用无返回值的 Mod 方法
        /// </summary>
        public static bool TryCallVoid(string methodName, params object[] args)
        {
            return TryCall(methodName, out _, args);
        }
    }
}
