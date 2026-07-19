using HybridCLR.Editor;
using HybridCLR.Editor.Commands;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// HybridCLR 构建辅助工具
/// 在打包前自动执行生成命令，并在打包后将热更新 DLL 复制到 StreamingAssets
/// </summary>
public class HybridCLRBuildHelper
{
    /// <summary>
    /// 一键执行所有 HybridCLR 生成命令（编译热更 DLL、生成桥接函数、AOT 引用等）
    /// 与菜单 HybridCLR/Generate/All 等效
    /// </summary>
    [MenuItem("HybridCLR/BuildHelper/Generate All", priority = 50)]
    public static void GenerateAll()
    {
        Debug.Log("[HybridCLRBuildHelper] Starting GenerateAll...");

        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
        Debug.Log($"[HybridCLRBuildHelper] Target platform: {target}");

        // 1. 编译热更新 DLL
        Debug.Log("[HybridCLRBuildHelper] Step 1/5: Compiling hot update DLLs...");
        CompileDllCommand.CompileDll(target);

        // 2. 生成 Il2CppDef
        Debug.Log("[HybridCLRBuildHelper] Step 2/5: Generating Il2CppDef...");
        Il2CppDefGeneratorCommand.GenerateIl2CppDef();

        // 3. 生成 link.xml
        Debug.Log("[HybridCLRBuildHelper] Step 3/5: Generating link.xml...");
        LinkGeneratorCommand.GenerateLinkXml(target);

        // 4. 生成裁剪后的 AOT DLL
        Debug.Log("[HybridCLRBuildHelper] Step 4/5: Stripping AOT DLLs...");
        StripAOTDllCommand.GenerateStripedAOTDlls(target);

        // 5. 生成 MethodBridge 和 AOT 泛型引用
        Debug.Log("[HybridCLRBuildHelper] Step 5/5: Generating MethodBridge & AOT reference...");
        MethodBridgeGeneratorCommand.GenerateMethodBridgeAndReversePInvokeWrapper(target);
        AOTReferenceGeneratorCommand.GenerateAOTGenericReference(target);

        Debug.Log("[HybridCLRBuildHelper] GenerateAll completed successfully!");

        // 复制热更新 DLL 到 StreamingAssets
        CopyHotUpdateDllToStreamingAssets(target);

        // 复制 AOT 元数据到 StreamingAssets
        CopyAOTMetadataToStreamingAssets(target);
    }

    /// <summary>
    /// 复制 HotUpdate.dll 到 StreamingAssets/HotUpdate.dll.bytes
    /// </summary>
    [MenuItem("HybridCLR/BuildHelper/Copy Dll to StreamingAssets", priority = 60)]
    public static void CopyDllToStreamingAssets()
    {
        BuildTarget target = EditorUserBuildSettings.activeBuildTarget;
        CopyHotUpdateDllToStreamingAssets(target);
        CopyAOTMetadataToStreamingAssets(target);
    }

    static void CopyHotUpdateDllToStreamingAssets(BuildTarget target)
    {
        string hotUpdateDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
        string sourcePath = Path.Combine(hotUpdateDir, "HotUpdate.dll");
        string targetDir = Path.Combine(Application.streamingAssetsPath);
        string targetPath = Path.Combine(targetDir, "HotUpdate.dll.bytes");

        if (!File.Exists(sourcePath))
        {
            Debug.LogError($"[HybridCLRBuildHelper] HotUpdate.dll not found at: {sourcePath}\nPlease run 'Generate All' first.");
            return;
        }

        if (!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }

        File.Copy(sourcePath, targetPath, overwrite: true);
        Debug.Log($"[HybridCLRBuildHelper] Copied HotUpdate.dll -> {targetPath}");

        // 也复制 .pdb 用于调试
        string pdbSource = Path.Combine(hotUpdateDir, "HotUpdate.pdb");
        string pdbTarget = Path.Combine(targetDir, "HotUpdate.pdb.bytes");
        if (File.Exists(pdbSource))
        {
            File.Copy(pdbSource, pdbTarget, overwrite: true);
            Debug.Log($"[HybridCLRBuildHelper] Copied HotUpdate.pdb -> {pdbTarget}");
        }
    }

    static void CopyAOTMetadataToStreamingAssets(BuildTarget target)
    {
        string aotSourceDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);
        string aotTargetDir = Path.Combine(Application.streamingAssetsPath, "AOTMetadata");

        if (!Directory.Exists(aotSourceDir))
        {
            Debug.LogWarning($"[HybridCLRBuildHelper] AOT metadata source directory not found: {aotSourceDir}");
            return;
        }

        if (!Directory.Exists(aotTargetDir))
        {
            Directory.CreateDirectory(aotTargetDir);
        }

        // 只复制补充元数据 DLL（名称中包含 .dll 的文件）
        var dllFiles = Directory.GetFiles(aotSourceDir, "*.dll");
        foreach (string dllFile in dllFiles)
        {
            string fileName = Path.GetFileName(dllFile);
            string targetPath = Path.Combine(aotTargetDir, fileName + ".bytes");
            File.Copy(dllFile, targetPath, overwrite: true);
        }

        Debug.Log($"[HybridCLRBuildHelper] Copied AOT metadata to {aotTargetDir} ({dllFiles.Length} files)");
    }

    /// <summary>
    /// 清理生成的临时文件
    /// </summary>
    [MenuItem("HybridCLR/BuildHelper/Clean Generated Files", priority = 70)]
    public static void CleanGeneratedFiles()
    {
        string[] dirsToClean = {
            "HybridCLRData",
            "HybridCLRGenerate",
            Path.Combine(Application.streamingAssetsPath, "HotUpdate.dll.bytes"),
            Path.Combine(Application.streamingAssetsPath, "HotUpdate.pdb.bytes"),
            Path.Combine(Application.streamingAssetsPath, "AOTMetadata"),
        };

        foreach (string dir in dirsToClean)
        {
            string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", dir));
            if (Directory.Exists(fullPath))
            {
                Directory.Delete(fullPath, recursive: true);
                Debug.Log($"[HybridCLRBuildHelper] Deleted: {fullPath}");
            }
            else if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                Debug.Log($"[HybridCLRBuildHelper] Deleted: {fullPath}");
            }
        }

        AssetDatabase.Refresh();
        Debug.Log("[HybridCLRBuildHelper] Clean completed.");
    }
}

/// <summary>
/// 打包前自动执行 HybridCLR 生成流程
/// </summary>
public class HybridCLRPrebuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder => 100;

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log($"[HybridCLRPrebuild] Pre-build processing for target: {report.summary.platform}");

        // 自动执行生成流程
        BuildTarget target = report.summary.platform;
        CompileDllCommand.CompileDll(target);
        Il2CppDefGeneratorCommand.GenerateIl2CppDef();
        LinkGeneratorCommand.GenerateLinkXml(target);
        StripAOTDllCommand.GenerateStripedAOTDlls(target);
        MethodBridgeGeneratorCommand.GenerateMethodBridgeAndReversePInvokeWrapper(target);
        AOTReferenceGeneratorCommand.GenerateAOTGenericReference(target);

        // 复制到 StreamingAssets
        string hotUpdateDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);
        string sourcePath = Path.Combine(hotUpdateDir, "HotUpdate.dll");
        string targetDir = Path.Combine(Application.streamingAssetsPath);
        string targetPath = Path.Combine(targetDir, "HotUpdate.dll.bytes");

        if (File.Exists(sourcePath))
        {
            if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);
            File.Copy(sourcePath, targetPath, overwrite: true);
            Debug.Log($"[HybridCLRPrebuild] Auto-copied HotUpdate.dll to StreamingAssets");
        }
    }
}
