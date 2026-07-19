using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace JyGame
{
    /// <summary>
    /// HybridCLR 脚本引擎实现
    /// 通过反射调用 HotUpdate.dll 中的 C# 方法，替代 Lua 函数调用
    /// </summary>
    public class HybridClrScriptEngine : IScriptEngine
    {
        private readonly Assembly _hotUpdateAssembly;
        private Type _scriptEntryType;
        private Dictionary<string, MethodInfo> _methodCache = new Dictionary<string, MethodInfo>();
        private Dictionary<string, object> _configCache;
        private bool _inited;

        public bool IsInited => _inited;

        public HybridClrScriptEngine(Assembly hotUpdateAssembly)
        {
            _hotUpdateAssembly = hotUpdateAssembly ?? throw new ArgumentNullException(nameof(hotUpdateAssembly));
        }

        public void Init(bool forceReset = false)
        {
            if (forceReset)
            {
                _inited = false;
                _methodCache.Clear();
                _configCache = null;
                _scriptEntryType = null;
            }
            if (_inited) return;

            // 查找 ScriptEntry 类型作为方法入口
            _scriptEntryType = _hotUpdateAssembly.GetType("ScriptEntry");
            if (_scriptEntryType == null)
            {
                Debug.LogWarning("[HybridClrScriptEngine] 'ScriptEntry' type not found in HotUpdate assembly. Some Lua functions may not have C# equivalents yet.");
            }

            // 调用初始化方法
            try
            {
                CallScriptMethod("ROOT_getLuaFiles", new object[0]);
            }
            catch
            {
                // 忽略：ScriptEntry 可能还没有对应的方法
            }

            _inited = true;
            Debug.Log("[HybridClrScriptEngine] Initialized.");
        }

        public void Reload()
        {
            _configCache = null;
            Init(true);
        }

        public void Call(string functionName, params object[] args)
        {
            CallScriptMethod(functionName, args);
        }

        public T Call<T>(string functionName, params object[] args)
        {
            object result = CallScriptMethod(functionName, args);
            if (result == null) return default(T);
            return (T)result;
        }

        public int CallWithIntReturn(string functionName, params object[] args)
        {
            object result = CallScriptMethod(functionName, args);
            if (result == null) return -1;
            return Convert.ToInt32(result);
        }

        public T GetConfig<T>(string key)
        {
            if (_configCache == null)
            {
                _configCache = new Dictionary<string, object>();
                // 尝试从 C# 配置获取
                try
                {
                    object configResult = CallScriptMethod("ROOT_getConfigList", new object[0]);
                    if (configResult is Dictionary<string, object> dict)
                    {
                        _configCache = dict;
                    }
                }
                catch
                {
                    Debug.LogWarning("[HybridClrScriptEngine] ROOT_getConfigList not implemented yet, falling back to GameConfig.");
                }
            }

            if (_configCache.TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return default(T);
        }

        /// <summary>
        /// 通过反射调用 HotUpdate 程序集中的静态方法
        /// </summary>
        private object CallScriptMethod(string functionName, object[] args)
        {
            if (_scriptEntryType == null)
            {
                Debug.LogWarning($"[HybridClrScriptEngine] ScriptEntry type not found, cannot call '{functionName}'.");
                return null;
            }

            // 从缓存获取或查找方法
            if (!_methodCache.TryGetValue(functionName, out MethodInfo method))
            {
                method = _scriptEntryType.GetMethod(functionName,
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                if (method == null)
                {
                    Debug.LogWarning($"[HybridClrScriptEngine] Method '{functionName}' not found in ScriptEntry. Not yet migrated to C#.");
                    return null;
                }
                _methodCache[functionName] = method;
            }

            try
            {
                return method.Invoke(null, args);
            }
            catch (TargetInvocationException ex)
            {
                Debug.LogError($"[HybridClrScriptEngine] Error calling '{functionName}': {ex.InnerException}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[HybridClrScriptEngine] Error calling '{functionName}': {ex}");
                throw;
            }
        }
    }
}
