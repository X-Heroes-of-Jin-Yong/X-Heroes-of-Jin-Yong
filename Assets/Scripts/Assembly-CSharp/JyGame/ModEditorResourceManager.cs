using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace JyGame
{
	public class ModEditorResourceManager
	{
		public static Dictionary<string, string> sprites = new Dictionary<string, string>();

		public static Dictionary<string, string> audios = new Dictionary<string, string>();

		public static Dictionary<string, string> xmls = new Dictionary<string, string>();

		public static Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();

		public static Sprite GetSprite(string key)
		{
			if (!sprites.ContainsKey(key))
			{
				return null;
			}
			return LoadSprite(sprites[key]);
		}

		public static void GetAudioClip(string key, Action<AudioClip> callback)
		{
			if (!audios.ContainsKey(key))
			{
				callback(null);
			}
			else
			{
				LoadAudio(audios[key], callback);
			}
		}

		public static string GetXml(string path)
		{
			if (xmls.ContainsKey(path))
			{
				return xmls[path];
			}
			return null;
		}

		public static void Clear()
		{
			sprites.Clear();
			audios.Clear();
			xmls.Clear();
		}

		public static void AddSprite(string key, string path)
		{
			if (!sprites.ContainsKey(key))
			{
				sprites.Add(key, path);
			}
		}

		public static void AddAudio(string key, string path)
		{
			if (!audios.ContainsKey(key))
			{
				audios.Add(key, path);
			}
		}

		public static IEnumerator LoadXml(LoadingUI ui, string path, CommonSettings.VoidCallBack callback)
		{
			using (UnityWebRequest request = UnityWebRequest.Get(ModManager.ModBaseUrl + path))
			{
				yield return request.SendWebRequest();
				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError(request.error);
				}
				else
				{
					string key = path.Split('.')[0];
					if (!xmls.ContainsKey(key))
					{
						xmls.Add(key, request.downloadHandler.text);
					}
					callback();
				}
			}
		}

		public static Sprite LoadSprite(string path)
		{
			if (textureCache.ContainsKey(path))
			{
				Texture2D texture2D = textureCache[path];
				return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			}
			AssetBundleManager.ModResourceData[] data = AssetBundleManager.data.data;
			foreach (AssetBundleManager.ModResourceData modResourceData in data)
			{
				if (modResourceData.url.Equals(path))
				{
					Texture2D t = new Texture2D(modResourceData.w, modResourceData.h);
					t.SetPixel(0, 0, Color.white);
					Sprite result = Sprite.Create(t, new Rect(0f, 0f, t.width, t.height), new Vector2(0.5f, 0.5f));
					string url = ModManager.ModBaseUrl + path;
					AudioManager.Instance.StartCoroutine(Tools.DownloadIntoTexture(t, url, delegate
					{
						textureCache[path] = t;
					}));
					return result;
				}
			}
			return null;
		}

		public static void LoadAudio(string path, Action<AudioClip> callback)
		{
			if (!Application.isMobilePlatform)
			{
				path = path.Replace(".mp3", ".ogg");
			}
			AudioManager.Instance.StartCoroutine(LoadAudioAsync(ModManager.ModBaseUrl + path, callback));
		}

		private static IEnumerator LoadAudioAsync(string url, Action<AudioClip> callback)
		{
			AudioType audioType = url.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ? AudioType.MPEG : AudioType.OGGVORBIS;
			using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
			{
				yield return request.SendWebRequest();
				if (request.result == UnityWebRequest.Result.Success)
				{
					callback(DownloadHandlerAudioClip.GetContent(request));
				}
				else
				{
					Debug.LogError(request.error);
					callback(null);
				}
			}
		}
	}
}
