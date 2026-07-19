using System.Collections.Generic;

namespace JyGame
{
    /// <summary>
    /// 脚本引擎抽象接口
    /// 用于统一调用热更新程序集中的方法
    /// </summary>
    public interface IScriptEngine
    {
        void Init(bool forceReset = false);
        void Reload();
        void Call(string functionName, params object[] args);
        T Call<T>(string functionName, params object[] args);
        int CallWithIntReturn(string functionName, params object[] args);
        T GetConfig<T>(string key);
        bool IsInited { get; }
    }

    /// <summary>
    /// 全局脚本引擎管理器（HybridCLR）
    /// </summary>
    public static class ScriptEngineManager
    {
        public static IScriptEngine Current { get; private set; }

        /// <summary>初始化脚本引擎</summary>
        public static void Initialize(System.Reflection.Assembly hotUpdateAssembly)
        {
            if (Current != null) return;

            if (hotUpdateAssembly != null)
            {
                Current = new HybridClrScriptEngine(hotUpdateAssembly);
                UnityEngine.Debug.Log("[ScriptEngineManager] Initialized with HybridCLR engine.");
            }
            else
            {
                UnityEngine.Debug.LogWarning("[ScriptEngineManager] No hot update assembly provided.");
            }
        }
    }
}
