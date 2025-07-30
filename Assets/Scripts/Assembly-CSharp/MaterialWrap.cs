using System;
using LuaInterface;
using UnityEngine;

public class MaterialWrap
{
	private static Type classType = typeof(Material);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[29]
		{
			new LuaMethod("SetColor", SetColor),
			new LuaMethod("GetColor", GetColor),
			new LuaMethod("SetVector", SetVector),
			new LuaMethod("GetVector", GetVector),
			new LuaMethod("SetTexture", SetTexture),
			new LuaMethod("GetTexture", GetTexture),
			new LuaMethod("SetTextureOffset", SetTextureOffset),
			new LuaMethod("GetTextureOffset", GetTextureOffset),
			new LuaMethod("SetTextureScale", SetTextureScale),
			new LuaMethod("GetTextureScale", GetTextureScale),
			new LuaMethod("SetMatrix", SetMatrix),
			new LuaMethod("GetMatrix", GetMatrix),
			new LuaMethod("SetFloat", SetFloat),
			new LuaMethod("GetFloat", GetFloat),
			new LuaMethod("SetInt", SetInt),
			new LuaMethod("GetInt", GetInt),
			new LuaMethod("SetBuffer", SetBuffer),
			new LuaMethod("HasProperty", HasProperty),
			new LuaMethod("GetTag", GetTag),
			new LuaMethod("SetOverrideTag", SetOverrideTag),
			new LuaMethod("Lerp", Lerp),
			new LuaMethod("SetPass", SetPass),
			new LuaMethod("CopyPropertiesFromMaterial", CopyPropertiesFromMaterial),
			new LuaMethod("EnableKeyword", EnableKeyword),
			new LuaMethod("DisableKeyword", DisableKeyword),
			new LuaMethod("IsKeywordEnabled", IsKeywordEnabled),
			new LuaMethod("New", _CreateMaterial),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[9]
		{
			new LuaField("shader", get_shader, set_shader),
			new LuaField("color", get_color, set_color),
			new LuaField("mainTexture", get_mainTexture, set_mainTexture),
			new LuaField("mainTextureOffset", get_mainTextureOffset, set_mainTextureOffset),
			new LuaField("mainTextureScale", get_mainTextureScale, set_mainTextureScale),
			new LuaField("passCount", get_passCount, null),
			new LuaField("renderQueue", get_renderQueue, set_renderQueue),
			new LuaField("shaderKeywords", get_shaderKeywords, set_shaderKeywords),
			new LuaField("globalIlluminationFlags", get_globalIlluminationFlags, set_globalIlluminationFlags)
		};
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Material", typeof(Material), regs, fields, typeof(UnityEngine.Object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _CreateMaterial(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material)))
		{
			Material source = (Material)LuaScriptMgr.GetUnityObject(L, 1, typeof(Material));
			Material obj = new Material(source);
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		if (num == 1 && LuaScriptMgr.CheckTypes(L, 1, typeof(Shader)))
		{
			Shader shader = (Shader)LuaScriptMgr.GetUnityObject(L, 1, typeof(Shader));
			Material obj2 = new Material(shader);
			LuaScriptMgr.Push(L, obj2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shader(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shader");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shader on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.shader);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_color(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.color);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainTexture(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTexture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTexture on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.mainTexture);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainTextureOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTextureOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTextureOffset on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.mainTextureOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_mainTextureScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTextureScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTextureScale on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.mainTextureScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_passCount(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name passCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index passCount on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.passCount);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_renderQueue(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderQueue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderQueue on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.renderQueue);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_shaderKeywords(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shaderKeywords");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shaderKeywords on a nil value");
			}
		}
		LuaScriptMgr.PushArray(L, material.shaderKeywords);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_globalIlluminationFlags(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name globalIlluminationFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index globalIlluminationFlags on a nil value");
			}
		}
		LuaScriptMgr.Push(L, material.globalIlluminationFlags);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shader(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shader");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shader on a nil value");
			}
		}
		material.shader = (Shader)LuaScriptMgr.GetUnityObject(L, 3, typeof(Shader));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_color(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}
		material.color = LuaScriptMgr.GetColor(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mainTexture(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTexture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTexture on a nil value");
			}
		}
		material.mainTexture = (Texture)LuaScriptMgr.GetUnityObject(L, 3, typeof(Texture));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mainTextureOffset(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTextureOffset");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTextureOffset on a nil value");
			}
		}
		material.mainTextureOffset = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_mainTextureScale(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTextureScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTextureScale on a nil value");
			}
		}
		material.mainTextureScale = LuaScriptMgr.GetVector2(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_renderQueue(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderQueue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderQueue on a nil value");
			}
		}
		material.renderQueue = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_shaderKeywords(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name shaderKeywords");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index shaderKeywords on a nil value");
			}
		}
		material.shaderKeywords = LuaScriptMgr.GetArrayString(L, 3);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int set_globalIlluminationFlags(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		Material material = (Material)luaObject;
		if (material == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name globalIlluminationFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index globalIlluminationFlags on a nil value");
			}
		}
		material.globalIlluminationFlags = (MaterialGlobalIlluminationFlags)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(MaterialGlobalIlluminationFlags));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetColor(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(LuaTable)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Color color = LuaScriptMgr.GetColor(L, 3);
			material.SetColor(nameID, color);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(LuaTable)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Color color2 = LuaScriptMgr.GetColor(L, 3);
			material2.SetColor(propertyName, color2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.SetColor");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetColor(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Color color = material.GetColor(nameID);
			LuaScriptMgr.Push(L, color);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Color color2 = material2.GetColor(propertyName);
			LuaScriptMgr.Push(L, color2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.GetColor");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetVector(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(LuaTable)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Vector4 vector = LuaScriptMgr.GetVector4(L, 3);
			material.SetVector(nameID, vector);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(LuaTable)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Vector4 vector2 = LuaScriptMgr.GetVector4(L, 3);
			material2.SetVector(propertyName, vector2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.SetVector");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetVector(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Vector4 vector = material.GetVector(nameID);
			LuaScriptMgr.Push(L, vector);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Vector4 vector2 = material2.GetVector(propertyName);
			LuaScriptMgr.Push(L, vector2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.GetVector");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTexture(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(Texture)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Texture texture = (Texture)LuaScriptMgr.GetLuaObject(L, 3);
			material.SetTexture(nameID, texture);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(Texture)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Texture texture2 = (Texture)LuaScriptMgr.GetLuaObject(L, 3);
			material2.SetTexture(propertyName, texture2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.SetTexture");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTexture(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Texture texture = material.GetTexture(nameID);
			LuaScriptMgr.Push(L, texture);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Texture texture2 = material2.GetTexture(propertyName);
			LuaScriptMgr.Push(L, texture2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.GetTexture");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTextureOffset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Vector2 vector = LuaScriptMgr.GetVector2(L, 3);
		material.SetTextureOffset(luaString, vector);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTextureOffset(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Vector2 textureOffset = material.GetTextureOffset(luaString);
		LuaScriptMgr.Push(L, textureOffset);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTextureScale(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Vector2 vector = LuaScriptMgr.GetVector2(L, 3);
		material.SetTextureScale(luaString, vector);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTextureScale(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		Vector2 textureScale = material.GetTextureScale(luaString);
		LuaScriptMgr.Push(L, textureScale);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetMatrix(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(Matrix4x4)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Matrix4x4 matrix = (Matrix4x4)LuaScriptMgr.GetLuaObject(L, 3);
			material.SetMatrix(nameID, matrix);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(Matrix4x4)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Matrix4x4 matrix2 = (Matrix4x4)LuaScriptMgr.GetLuaObject(L, 3);
			material2.SetMatrix(propertyName, matrix2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.SetMatrix");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetMatrix(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			Matrix4x4 matrix = material.GetMatrix(nameID);
			LuaScriptMgr.PushValue(L, matrix);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			Matrix4x4 matrix2 = material2.GetMatrix(propertyName);
			LuaScriptMgr.PushValue(L, matrix2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.GetMatrix");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetFloat(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(float)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			float value = (float)LuaDLL.lua_tonumber(L, 3);
			material.SetFloat(nameID, value);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(float)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			float value2 = (float)LuaDLL.lua_tonumber(L, 3);
			material2.SetFloat(propertyName, value2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.SetFloat");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetFloat(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			float d = material.GetFloat(nameID);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			float d2 = material2.GetFloat(propertyName);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.GetFloat");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetInt(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			int value = (int)LuaDLL.lua_tonumber(L, 3);
			material.SetInt(nameID, value);
			return 0;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string), typeof(int)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			int value2 = (int)LuaDLL.lua_tonumber(L, 3);
			material2.SetInt(propertyName, value2);
			return 0;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.SetInt");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetInt(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			int d = material.GetInt(nameID);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			int d2 = material2.GetInt(propertyName);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.GetInt");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetBuffer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		ComputeBuffer buffer = (ComputeBuffer)LuaScriptMgr.GetNetObject(L, 3, typeof(ComputeBuffer));
		material.SetBuffer(luaString, buffer);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int HasProperty(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(int)))
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			int nameID = (int)LuaDLL.lua_tonumber(L, 2);
			bool b = material.HasProperty(nameID);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Material), typeof(string)))
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string propertyName = LuaScriptMgr.GetString(L, 2);
			bool b2 = material2.HasProperty(propertyName);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: Material.HasProperty");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTag(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 3:
		{
			Material material2 = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			bool boolean2 = LuaScriptMgr.GetBoolean(L, 3);
			string tag2 = material2.GetTag(luaString3, boolean2);
			LuaScriptMgr.Push(L, tag2);
			return 1;
		}
		case 4:
		{
			Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			string luaString2 = LuaScriptMgr.GetLuaString(L, 4);
			string tag = material.GetTag(luaString, boolean, luaString2);
			LuaScriptMgr.Push(L, tag);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: Material.GetTag");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetOverrideTag(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
		material.SetOverrideTag(luaString, luaString2);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lerp(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		Material start = (Material)LuaScriptMgr.GetUnityObject(L, 2, typeof(Material));
		Material end = (Material)LuaScriptMgr.GetUnityObject(L, 3, typeof(Material));
		float t = (float)LuaScriptMgr.GetNumber(L, 4);
		material.Lerp(start, end, t);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetPass(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		int pass = (int)LuaScriptMgr.GetNumber(L, 2);
		bool b = material.SetPass(pass);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CopyPropertiesFromMaterial(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		Material mat = (Material)LuaScriptMgr.GetUnityObject(L, 2, typeof(Material));
		material.CopyPropertiesFromMaterial(mat);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EnableKeyword(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		material.EnableKeyword(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DisableKeyword(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		material.DisableKeyword(luaString);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsKeywordEnabled(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Material material = (Material)LuaScriptMgr.GetUnityObjectSelf(L, 1, "Material");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool b = material.IsKeywordEnabled(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		UnityEngine.Object obj = LuaScriptMgr.GetLuaObject(L, 1) as UnityEngine.Object;
		UnityEngine.Object obj2 = LuaScriptMgr.GetLuaObject(L, 2) as UnityEngine.Object;
		bool b = obj == obj2;
		LuaScriptMgr.Push(L, b);
		return 1;
	}
}
