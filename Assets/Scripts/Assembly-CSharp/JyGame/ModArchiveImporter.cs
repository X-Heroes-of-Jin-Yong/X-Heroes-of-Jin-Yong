using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using Cysharp.Threading.Tasks;

namespace JyGame
{
	public static class ModArchiveImporter
	{
		private const int MaxEntries = 20000;
		private const long MaxExtractedBytes = 2L * 1024L * 1024L * 1024L;

		public static async UniTask<ModInfo> ImportAsync(string archivePath)
		{
			string modCache = Path.Combine(CommonSettings.persistentDataPath, "modcache");
			await UniTask.SwitchToThreadPool();
			try
			{
				return Import(archivePath, modCache);
			}
			finally
			{
				await UniTask.SwitchToMainThread();
				DeleteTemporarySafCopy(archivePath);
			}
		}

		private static ModInfo Import(string archivePath, string modCache)
		{
			if (!File.Exists(archivePath))
			{
				throw new FileNotFoundException("找不到选择的MOD压缩包", archivePath);
			}

			Directory.CreateDirectory(modCache);
			string extractionRoot = Path.Combine(modCache, ".import-" + Guid.NewGuid().ToString("N"));
			Directory.CreateDirectory(extractionRoot);

			try
			{
				ExtractSafely(archivePath, extractionRoot);
				string descriptorPath = FindDescriptor(extractionRoot);
				string descriptorXml = File.ReadAllText(descriptorPath);
				ModInfo mod = ParseDescriptor(descriptorXml);
				ValidateKey(mod.key);

				string destination = Path.Combine(modCache, mod.key);
				string destinationDescriptor = Path.Combine(modCache, mod.key + ".xml");
				if (Directory.Exists(destination) || File.Exists(destinationDescriptor))
				{
					throw new IOException($"MOD {mod.key} 已存在，请先在本地MOD列表中删除旧版本");
				}

				string contentRoot = Path.GetDirectoryName(descriptorPath);
				Directory.Move(contentRoot, destination);
				File.WriteAllText(destinationDescriptor, descriptorXml);
				return mod;
			}
			finally
			{
				if (Directory.Exists(extractionRoot))
				{
					Directory.Delete(extractionRoot, true);
				}
			}
		}

		private static void ExtractSafely(string archivePath, string extractionRoot)
		{
			string rootWithSeparator = Path.GetFullPath(extractionRoot) + Path.DirectorySeparatorChar;
			long totalBytes = 0;
			using (ZipArchive archive = ZipFile.OpenRead(archivePath))
			{
				if (archive.Entries.Count > MaxEntries)
				{
					throw new InvalidDataException("MOD压缩包文件数量过多");
				}

				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					totalBytes += entry.Length;
					if (totalBytes > MaxExtractedBytes)
					{
						throw new InvalidDataException("MOD解压后超过2GB限制");
					}

					string destinationPath = Path.GetFullPath(Path.Combine(extractionRoot, entry.FullName));
					if (!destinationPath.StartsWith(rootWithSeparator, StringComparison.OrdinalIgnoreCase))
					{
						throw new InvalidDataException("MOD压缩包包含非法路径");
					}

					if (string.IsNullOrEmpty(entry.Name))
					{
						Directory.CreateDirectory(destinationPath);
						continue;
					}

					Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
					entry.ExtractToFile(destinationPath, false);
				}
			}
		}

		private static string FindDescriptor(string extractionRoot)
		{
			string[] xmlFiles = Directory.GetFiles(extractionRoot, "*.xml", SearchOption.AllDirectories);
			string descriptor = xmlFiles
				.OrderByDescending(path => string.Equals(Path.GetFileName(path), "Mod.xml", StringComparison.OrdinalIgnoreCase))
				.FirstOrDefault(IsModDescriptor);
			if (descriptor == null)
			{
				throw new InvalidDataException("压缩包中未找到包含 root/mod 的MOD描述XML");
			}
			return descriptor;
		}

		private static bool IsModDescriptor(string path)
		{
			try
			{
				XmlDocument document = new XmlDocument();
				document.Load(path);
				return document.SelectSingleNode("root/mod") != null;
			}
			catch (XmlException)
			{
				return false;
			}
		}

		private static ModInfo ParseDescriptor(string xml)
		{
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);
			XmlNode node = document.SelectSingleNode("root/mod");
			if (node == null)
			{
				throw new InvalidDataException("MOD描述XML格式无效");
			}
			return BasePojo.Create<ModInfo>(node.OuterXml);
		}

		private static void ValidateKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key) || key.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || key.Contains(".."))
			{
				throw new InvalidDataException("MOD key 无效");
			}
		}

		private static void DeleteTemporarySafCopy(string archivePath)
		{
			string cacheRoot = Path.GetFullPath(UnityEngine.Application.temporaryCachePath) + Path.DirectorySeparatorChar;
			string fullPath = Path.GetFullPath(archivePath);
			if (fullPath.StartsWith(cacheRoot, StringComparison.OrdinalIgnoreCase) && File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}
		}
	}
}
