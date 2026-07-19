using HybridCLR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// HybridCLR 热更新程序集加载器
/// 负责加载 AOT 补充元数据和 HotUpdate.dll 热更新程序集
/// </summary>
public class LoadDll : MonoBehaviour
{
    void Start()
    {
        LoadHotUpdateAssembly();
    }

    /// <summary>
    /// 加载热更新程序集
    /// </summary>
    void LoadHotUpdateAssembly()
    {
        Assembly hotUpdateAss = null;

#if !UNITY_EDITOR
        // ===== IL2CPP 真机环境 =====
        // 1. 加载 AOT 补充元数据
        LoadAOTMetadata();

        // 2. 加载热更新 DLL
        string dllPath = $"{Application.streamingAssetsPath}/HotUpdate.dll.bytes";
        if (!File.Exists(dllPath))
        {
            Debug.LogWarning($"[LoadDll] HotUpdate.dll.bytes not found at {dllPath}, skipping hot update.");
            return;
        }

        try
        {
            byte[] dllBytes = File.ReadAllBytes(dllPath);
            Debug.Log($"[LoadDll] Loading HotUpdate.dll ({dllBytes.Length} bytes)...");
            hotUpdateAss = Assembly.Load(dllBytes);
            Debug.Log($"[LoadDll] HotUpdate.dll loaded successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[LoadDll] Failed to load HotUpdate.dll: {ex}");
            return;
        }
#else
        // ===== Unity Editor 环境 =====
        // Editor 下 HotUpdate 程序集已被 Unity 自动加载，直接查找即可
        hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "HotUpdate");

        if (hotUpdateAss == null)
        {
            Debug.LogWarning("[LoadDll] HotUpdate assembly not found in Editor. Make sure the asmdef is configured.");
            return;
        }
#endif

        // 3. 调用热更新入口方法
        RunHotUpdateEntry(hotUpdateAss);
    }

    /// <summary>
    /// 加载 AOT 程序集的补充元数据
    /// 在 IL2CPP 裁剪后，AOT 程序集缺少解释器需要的元数据，
    /// 需要加载补充元数据 DLL 才能让热更新程序集中的代码正常运行。
    /// </summary>
    void LoadAOTMetadata()
    {
        // AOT 元数据应从 StreamingAssets 加载
        string aotMetaDir = $"{Application.streamingAssetsPath}/AOTMetadata";

        if (!Directory.Exists(aotMetaDir))
        {
            Debug.LogWarning($"[LoadDll] AOT metadata directory not found: {aotMetaDir}");
            return;
        }

        // 加载所有 AOT 元数据 DLL
        HomologousImageMode mode = HomologousImageMode.SuperSet;
        foreach (string dllPath in Directory.GetFiles(aotMetaDir, "*.dll.bytes"))
        {
            try
            {
                byte[] dllBytes = File.ReadAllBytes(dllPath);
                string fileName = Path.GetFileNameWithoutExtension(dllPath);
                // 去除 .dll 后缀 (Path.GetFileNameWithoutExtension 对 .dll.bytes 只去掉 .bytes)
                string aotAssemblyName = Path.GetFileNameWithoutExtension(fileName);

                LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);
                if (err != LoadImageErrorCode.OK)
                {
                    Debug.LogError($"[LoadDll] LoadMetadataForAOTAssembly failed for {aotAssemblyName}: {err}");
                }
                else
                {
                    Debug.Log($"[LoadDll] AOT metadata loaded: {aotAssemblyName}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[LoadDll] Failed to load AOT metadata from {dllPath}: {ex}");
            }
        }
    }

    /// <summary>
    /// 执行热更新程序集的入口方法
    /// 默认调用 HotUpdate.dll 中 Hello 类的 Run 方法
    /// </summary>
    void RunHotUpdateEntry(Assembly hotUpdateAss)
    {
        try
        {
            Type entryType = hotUpdateAss.GetType("ScriptEntry");
            if (entryType == null)
            {
                Debug.LogWarning("[LoadDll] 'ScriptEntry' type not found in HotUpdate assembly.");
                return;
            }

            MethodInfo runMethod = entryType.GetMethod("Run", BindingFlags.Public | BindingFlags.Static);
            if (runMethod == null)
            {
                Debug.LogWarning("[LoadDll] 'ScriptEntry.Run()' method not found.");
                return;
            }

            runMethod.Invoke(null, null);
            Debug.Log("[LoadDll] HotUpdate entry point 'ScriptEntry.Run()' executed successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[LoadDll] Failed to run HotUpdate entry: {ex}");
        }
    }
}
