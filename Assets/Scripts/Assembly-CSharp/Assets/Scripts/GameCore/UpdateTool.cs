using System;
using System.Collections;
using System.IO;
using System.Net;
using JyGame;
using UnityEngine;

namespace Assets.Scripts.GameCore
{
	internal class UpdateTool
	{
		public static bool IsDone;

		public static double Progress { get; set; }

		public static IEnumerator CheckResouces(CommonSettings.VoidCallBack callback = null)
		{
			if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
			{
				yield return null;
			}
			else
			{
				Download("http://120.24.166.63:8080/version.txt", "newV");
				yield return 0;
				string newV = ReadText("newV/version.txt");
				string oldV = ReadText("curV/version.txt");
				int newVersion = 0;
				if (!string.IsNullOrEmpty(newV))
				{
					newVersion = Convert.ToInt32(newV);
				}
				int oldVersion = 1;
				if (!string.IsNullOrEmpty(oldV))
				{
					oldVersion = Convert.ToInt32(oldV);
				}
				if (oldVersion < newVersion)
				{
					Download("http://120.24.166.63:8080/versionList.txt", "newV");
					string verList = ReadText("newV/versionList.txt");
					string[] verInfo = verList.Split('\n');
					ArrayList updateList = new ArrayList();
					string[] array = verInfo;
					foreach (string item in array)
					{
						string[] info = item.Split(',');
						int version = Convert.ToInt32(info[1]);
						if (version > oldVersion)
						{
							updateList.Add(info[0]);
						}
					}
					for (int j = 0; j < updateList.Count; j++)
					{
						string url = updateList[j] as string;
						Download(url, "resource");
						Progress = ((double)j + 1.0) / (double)updateList.Count;
						yield return Progress;
					}
					MoveFile("newV/version.txt", "curV/version.txt");
				}
			}
			IsDone = true;
			if (callback != null)
			{
				callback();
				yield return null;
			}
		}

		public static string ReadResource(string file)
		{
			if (Application.platform == RuntimePlatform.WindowsWebPlayer)
			{
				return null;
			}
			return ReadText("resource" + Path.DirectorySeparatorChar + file);
		}

		private static void MoveFile(string src, string des)
		{
			FileInfo fileInfo = new FileInfo(CommonSettings.persistentDataPath + Path.DirectorySeparatorChar + des);
			string path = fileInfo.FullName.Substring(0, fileInfo.FullName.LastIndexOf(Path.DirectorySeparatorChar));
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			if (fileInfo.Exists)
			{
				File.Delete(fileInfo.FullName);
			}
			File.Copy(CommonSettings.persistentDataPath + Path.DirectorySeparatorChar + src, fileInfo.FullName);
		}

		private static string ReadText(string path)
		{
			try
			{
				using (TextReader textReader = File.OpenText(CommonSettings.persistentDataPath + Path.DirectorySeparatorChar + path))
				{
					return textReader.ReadToEnd();
				}
			}
			catch
			{
				return null;
			}
		}

		private static void Download(string url, string dir)
		{
			try
			{
				WebClient webClient = new WebClient();
				dir = CommonSettings.persistentDataPath + Path.DirectorySeparatorChar + dir;
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
				webClient.DownloadFile(url, dir + Path.DirectorySeparatorChar + Path.GetFileName(url));
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}
}
