using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 热更新入口类 — HybridCLR 模式下的程序入口
/// 所有原 Lua 全局函数已迁移至 JyGame.*Ext 类中
/// Mod 开发请参考 ModEntry.cs
/// </summary>
public class ScriptEntry
{
    public static void Run()
    {
        Debug.Log("Hello, HybridCLR");
    }

    public static List<string> ROOT_getLuaFiles()
    {
        return new List<string>();
    }

    public static Dictionary<string, object> ROOT_getConfigList()
    {
        return new Dictionary<string, object>();
    }
}
