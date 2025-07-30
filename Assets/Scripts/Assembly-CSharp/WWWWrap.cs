using System;
using System.Collections.Generic;
using System.Text;
using LuaInterface;
using UnityEngine;

public class WWWWrap
{
	private static Type classType = typeof(WWW);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[10]
		{
			new LuaMethod("Dispose", Dispose),
			new LuaMethod("InitWWW", InitWWW),
			new LuaMethod("EscapeURL", EscapeURL),
			new LuaMethod("UnEscapeURL", UnEscapeURL),
			new LuaMethod("GetAudioClip", GetAudioClip),
			new LuaMethod("GetAudioClipCompressed", GetAudioClipCompressed),
			new LuaMethod("LoadImageIntoTexture", LoadImageIntoTexture),
			new LuaMethod("LoadFromCacheOrDownload", LoadFromCacheOrDownload),
			new LuaMethod("New", _CreateWWW),
			new LuaMethod("GetClassType", GetClassType)
		};
		LuaField[] fields = new LuaField[15]
		{
			new LuaField("responseHeaders", get_responseHeaders, null),
			new LuaField("text", get_text, null),
			new LuaField("bytes", get_bytes, null),
			new LuaField("size", get_size, null),
			new LuaField("error", get_error, null),
			new LuaField("texture", get_texture, null),
			new LuaField("textureNonReadable", get_textureNonReadable, null),
			new LuaField("audioClip", get_audioClip, null),
			new LuaField("isDone", get_isDone, null),
			new LuaField("progress", get_progress, null),
			new LuaField("uploadProgress", get_uploadProgress, null),
			new LuaField("bytesDownloaded", get_bytesDownloaded, null),
			new LuaField("url", get_url, null),
			new LuaField("assetBundle", get_assetBundle, null),
			new LuaField("threadPriority", get_threadPriority, set_threadPriority)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.WWW", typeof(WWW), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateWWW(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 1:
		{
			string url2 = LuaScriptMgr.GetString(L, 1);
			WWW o2 = new WWW(url2);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		case 2:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(byte[])))
			{
				string url = LuaScriptMgr.GetString(L, 1);
				byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 2);
				WWW o = new WWW(url, arrayNumber);
				LuaScriptMgr.PushObject(L, o);
				return 1;
			}
			break;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(WWWForm)))
		{
			string url3 = LuaScriptMgr.GetString(L, 1);
			WWWForm form = (WWWForm)LuaScriptMgr.GetNetObject(L, 2, typeof(WWWForm));
			WWW o3 = new WWW(url3, form);
			LuaScriptMgr.PushObject(L, o3);
			return 1;
		}
		if (num == 3)
		{
			string url4 = LuaScriptMgr.GetString(L, 1);
			byte[] arrayNumber2 = LuaScriptMgr.GetArrayNumber<byte>(L, 2);
			Dictionary<string, string> headers = (Dictionary<string, string>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string, string>));
			WWW o4 = new WWW(url4, arrayNumber2, headers);
			LuaScriptMgr.PushObject(L, o4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: WWW.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_responseHeaders(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name responseHeaders");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index responseHeaders on a nil value");
			}
		}
		LuaScriptMgr.PushObject(L, wWW.responseHeaders);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_text(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name text");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index text on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.text);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bytes(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bytes");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bytes on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, wWW.bytes);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_size(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name size");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index size on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.size);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_error(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name error");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index error on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.error);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_texture(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name texture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index texture on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.texture);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_textureNonReadable(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name textureNonReadable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index textureNonReadable on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.textureNonReadable);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_audioClip(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name audioClip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index audioClip on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.audioClip);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_isDone(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isDone");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isDone on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.isDone);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_progress(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name progress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index progress on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.progress);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_uploadProgress(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uploadProgress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uploadProgress on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.uploadProgress);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_bytesDownloaded(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bytesDownloaded");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bytesDownloaded on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.bytesDownloaded);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_url(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name url");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index url on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.url);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_assetBundle(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name assetBundle");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index assetBundle on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.assetBundle);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_threadPriority(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name threadPriority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index threadPriority on a nil value");
			}
		}
		LuaScriptMgr.Push(L, wWW.threadPriority);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_threadPriority(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		WWW wWW = (WWW)luaObject;
		if (wWW == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name threadPriority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index threadPriority on a nil value");
			}
		}
		wWW.threadPriority = (ThreadPriority)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(ThreadPriority));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Dispose(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		WWW wWW = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
		wWW.Dispose();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int InitWWW(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		WWW wWW = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		byte[] arrayNumber = LuaScriptMgr.GetArrayNumber<byte>(L, 3);
		string[] arrayString = LuaScriptMgr.GetArrayString(L, 4);
		wWW.InitWWW(luaString, arrayNumber, arrayString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EscapeURL(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			string str2 = WWW.EscapeURL(luaString2);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			Encoding e = (Encoding)LuaScriptMgr.GetNetObject(L, 2, typeof(Encoding));
			string str = WWW.EscapeURL(luaString, e);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: WWW.EscapeURL");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int UnEscapeURL(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			string str2 = WWW.UnEscapeURL(luaString2);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			Encoding e = (Encoding)LuaScriptMgr.GetNetObject(L, 2, typeof(Encoding));
			string str = WWW.UnEscapeURL(luaString, e);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: WWW.UnEscapeURL");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAudioClip(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			WWW wWW3 = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
			bool boolean5 = LuaScriptMgr.GetBoolean(L, 2);
			AudioClip audioClip3 = wWW3.GetAudioClip(boolean5);
			LuaScriptMgr.Push(L, audioClip3);
			return 1;
		}
		case 3:
		{
			WWW wWW2 = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
			bool boolean3 = LuaScriptMgr.GetBoolean(L, 2);
			bool boolean4 = LuaScriptMgr.GetBoolean(L, 3);
			AudioClip audioClip2 = wWW2.GetAudioClip(boolean3, boolean4);
			LuaScriptMgr.Push(L, audioClip2);
			return 1;
		}
		case 4:
		{
			WWW wWW = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
			bool boolean = LuaScriptMgr.GetBoolean(L, 2);
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 3);
			AudioType audioType = (AudioType)(int)LuaScriptMgr.GetNetObject(L, 4, typeof(AudioType));
			AudioClip audioClip = wWW.GetAudioClip(boolean, boolean2, audioType);
			LuaScriptMgr.Push(L, audioClip);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: WWW.GetAudioClip");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetAudioClipCompressed(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			WWW wWW3 = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
			AudioClip audioClipCompressed3 = wWW3.GetAudioClipCompressed();
			LuaScriptMgr.Push(L, audioClipCompressed3);
			return 1;
		}
		case 2:
		{
			WWW wWW2 = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 2);
			AudioClip audioClipCompressed2 = wWW2.GetAudioClipCompressed(boolean2);
			LuaScriptMgr.Push(L, audioClipCompressed2);
			return 1;
		}
		case 3:
		{
			WWW wWW = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
			bool boolean = LuaScriptMgr.GetBoolean(L, 2);
			AudioType audioType = (AudioType)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(AudioType));
			AudioClip audioClipCompressed = wWW.GetAudioClipCompressed(boolean, audioType);
			LuaScriptMgr.Push(L, audioClipCompressed);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: WWW.GetAudioClipCompressed");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadImageIntoTexture(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		WWW wWW = (WWW)LuaScriptMgr.GetNetObjectSelf(L, 1, "WWW");
		Texture2D tex = (Texture2D)LuaScriptMgr.GetUnityObject(L, 2, typeof(Texture2D));
		wWW.LoadImageIntoTexture(tex);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LoadFromCacheOrDownload(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(Hash128)))
		{
			string url = LuaScriptMgr.GetString(L, 1);
			Hash128 hash = (Hash128)LuaScriptMgr.GetLuaObject(L, 2);
			WWW o = WWW.LoadFromCacheOrDownload(url, hash);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(int)))
		{
			string url2 = LuaScriptMgr.GetString(L, 1);
			int version = (int)LuaDLL.lua_tonumber(L, 2);
			WWW o2 = WWW.LoadFromCacheOrDownload(url2, version);
			LuaScriptMgr.PushObject(L, o2);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(Hash128), typeof(uint)))
		{
			string url3 = LuaScriptMgr.GetString(L, 1);
			Hash128 hash2 = (Hash128)LuaScriptMgr.GetLuaObject(L, 2);
			uint crc = (uint)LuaDLL.lua_tonumber(L, 3);
			WWW o3 = WWW.LoadFromCacheOrDownload(url3, hash2, crc);
			LuaScriptMgr.PushObject(L, o3);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(int), typeof(uint)))
		{
			string url4 = LuaScriptMgr.GetString(L, 1);
			int version2 = (int)LuaDLL.lua_tonumber(L, 2);
			uint crc2 = (uint)LuaDLL.lua_tonumber(L, 3);
			WWW o4 = WWW.LoadFromCacheOrDownload(url4, version2, crc2);
			LuaScriptMgr.PushObject(L, o4);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: WWW.LoadFromCacheOrDownload");
		return 0;
	}
}
