using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Model.Base
{
    public static class DataManager
    {
        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
        };

#if UNITY_EDITOR
        public static string ModsRoot => Path.Combine(Application.dataPath, "Mods");
#else
        public static string ModsRoot => Path.Combine(Application.persistentDataPath, "Mods");
#endif

        public static List<string> ListMods()
        {
            if (!Directory.Exists(ModsRoot)) return new List<string>();
            return Directory.GetDirectories(ModsRoot)
                .Select(Path.GetFileName).OrderBy(n => n).ToList();
        }

        public static List<T> Load<T>(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning($"[Data] 文件不存在: {path}");
                return new List<T>();
            }
            try
            {
                var json = File.ReadAllText(path);
                var container = JsonConvert.DeserializeObject<DataContainer<T>>(json, JsonSettings);
                var items = container?.items ?? new List<T>();
                Debug.Log($"[Data] 加载 {items.Count} 条: {Path.GetFileName(path)}");
                return items;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[Data] 加载失败: {path}\n{ex}");
                return new List<T>();
            }
        }

        public static void Save<T>(List<T> items, string path)
        {
            try
            {
                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var container = new DataContainer<T>
                {
                    items = items,
                    _metadata = new DataMetadata
                    {
                        count = items.Count,
                        savedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    }
                };
                File.WriteAllText(path, JsonConvert.SerializeObject(container, JsonSettings));
                Debug.Log($"[Data] 保存 {items.Count} 条 → {Path.GetFileName(path)}");
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
#endif
            }
            catch (Exception ex)
            {
                Debug.LogError($"[Data] 保存失败: {path}\n{ex}");
            }
        }

        [Serializable] private class DataContainer<T> { public List<T> items; public DataMetadata _metadata; }
        [Serializable] private class DataMetadata { public int count; public string savedAt; }
    }
}
