using System;
using System.Globalization;
using System.Text;
using LuaInterface;

public class stringWrap
{
	private static Type classType = typeof(string);

	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[45]
		{
			new LuaMethod("Clone", Clone),
			new LuaMethod("GetTypeCode", GetTypeCode),
			new LuaMethod("CopyTo", CopyTo),
			new LuaMethod("ToCharArray", ToCharArray),
			new LuaMethod("Split", Split),
			new LuaMethod("Substring", Substring),
			new LuaMethod("Trim", Trim),
			new LuaMethod("TrimStart", TrimStart),
			new LuaMethod("TrimEnd", TrimEnd),
			new LuaMethod("Compare", Compare),
			new LuaMethod("CompareTo", CompareTo),
			new LuaMethod("CompareOrdinal", CompareOrdinal),
			new LuaMethod("EndsWith", EndsWith),
			new LuaMethod("IndexOfAny", IndexOfAny),
			new LuaMethod("IndexOf", IndexOf),
			new LuaMethod("LastIndexOf", LastIndexOf),
			new LuaMethod("LastIndexOfAny", LastIndexOfAny),
			new LuaMethod("Contains", Contains),
			new LuaMethod("IsNullOrEmpty", IsNullOrEmpty),
			new LuaMethod("Normalize", Normalize),
			new LuaMethod("IsNormalized", IsNormalized),
			new LuaMethod("Remove", Remove),
			new LuaMethod("PadLeft", PadLeft),
			new LuaMethod("PadRight", PadRight),
			new LuaMethod("StartsWith", StartsWith),
			new LuaMethod("Replace", Replace),
			new LuaMethod("ToLower", ToLower),
			new LuaMethod("ToLowerInvariant", ToLowerInvariant),
			new LuaMethod("ToUpper", ToUpper),
			new LuaMethod("ToUpperInvariant", ToUpperInvariant),
			new LuaMethod("ToString", ToString),
			new LuaMethod("Format", Format),
			new LuaMethod("Copy", Copy),
			new LuaMethod("Concat", Concat),
			new LuaMethod("Insert", Insert),
			new LuaMethod("Intern", Intern),
			new LuaMethod("IsInterned", IsInterned),
			new LuaMethod("Join", Join),
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetHashCode", GetHashCode),
			new LuaMethod("Equals", Equals),
			new LuaMethod("New", _Createstring),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__tostring", Lua_ToString),
			new LuaMethod("__eq", Lua_Eq)
		};
		LuaField[] fields = new LuaField[2]
		{
			new LuaField("Empty", get_Empty, null),
			new LuaField("Length", get_Length, null)
		};
		LuaScriptMgr.RegisterLib(L, "System.String", typeof(string), regs, fields, typeof(object));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int _Createstring(IntPtr L)
	{
		LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
		if (luaTypes == LuaTypes.LUA_TSTRING)
		{
			string o = LuaDLL.lua_tostring(L, 1);
			LuaScriptMgr.PushObject(L, o);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: String.New");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Empty(IntPtr L)
	{
		LuaScriptMgr.Push(L, string.Empty);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Length(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		string text = (string)luaObject;
		if (text == null)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, 1);
			if (luaTypes == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Length");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Length on a nil value");
			}
		}
		LuaScriptMgr.Push(L, text.Length);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_ToString(IntPtr L)
	{
		object luaObject = LuaScriptMgr.GetLuaObject(L, 1);
		if (luaObject != null)
		{
			LuaScriptMgr.Push(L, luaObject.ToString());
		}
		else
		{
			LuaScriptMgr.Push(L, "Table: System.String");
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Clone(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		object o = text.Clone();
		LuaScriptMgr.PushVarObject(L, o);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetTypeCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		TypeCode typeCode = text.GetTypeCode();
		LuaScriptMgr.Push(L, typeCode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CopyTo(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 5);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		int sourceIndex = (int)LuaScriptMgr.GetNumber(L, 2);
		char[] arrayNumber = LuaScriptMgr.GetArrayNumber<char>(L, 3);
		int destinationIndex = (int)LuaScriptMgr.GetNumber(L, 4);
		int count = (int)LuaScriptMgr.GetNumber(L, 5);
		text.CopyTo(sourceIndex, arrayNumber, destinationIndex, count);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToCharArray(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] o2 = text2.ToCharArray();
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		case 3:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int startIndex = (int)LuaScriptMgr.GetNumber(L, 2);
			int length = (int)LuaScriptMgr.GetNumber(L, 3);
			char[] o = text.ToCharArray(startIndex, length);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.ToCharArray");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Split(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(StringSplitOptions)))
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			StringSplitOptions options = (StringSplitOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
			string[] o = text.Split(arrayNumber, options);
			LuaScriptMgr.PushArray(L, o);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int)))
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber2 = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int count = (int)LuaDLL.lua_tonumber(L, 3);
			string[] o2 = text2.Split(arrayNumber2, count);
			LuaScriptMgr.PushArray(L, o2);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(StringSplitOptions)))
		{
			string text3 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string[] arrayString = LuaScriptMgr.GetArrayString(L, 2);
			StringSplitOptions options2 = (StringSplitOptions)(int)LuaScriptMgr.GetLuaObject(L, 3);
			string[] o3 = text3.Split(arrayString, options2);
			LuaScriptMgr.PushArray(L, o3);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string[]), typeof(int), typeof(StringSplitOptions)))
		{
			string text4 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string[] arrayString2 = LuaScriptMgr.GetArrayString(L, 2);
			int count2 = (int)LuaDLL.lua_tonumber(L, 3);
			StringSplitOptions options3 = (StringSplitOptions)(int)LuaScriptMgr.GetLuaObject(L, 4);
			string[] o4 = text4.Split(arrayString2, count2, options3);
			LuaScriptMgr.PushArray(L, o4);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char[]), typeof(int), typeof(StringSplitOptions)))
		{
			string text5 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber3 = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int count3 = (int)LuaDLL.lua_tonumber(L, 3);
			StringSplitOptions options4 = (StringSplitOptions)(int)LuaScriptMgr.GetLuaObject(L, 4);
			string[] o5 = text5.Split(arrayNumber3, count3, options4);
			LuaScriptMgr.PushArray(L, o5);
			return 1;
		}
		if (LuaScriptMgr.CheckParamsType(L, typeof(char), 2, num - 1))
		{
			string text6 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber4 = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			string[] o6 = text6.Split(arrayNumber4);
			LuaScriptMgr.PushArray(L, o6);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.Split");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Substring(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int startIndex2 = (int)LuaScriptMgr.GetNumber(L, 2);
			string str2 = text2.Substring(startIndex2);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 3:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int startIndex = (int)LuaScriptMgr.GetNumber(L, 2);
			int length = (int)LuaScriptMgr.GetNumber(L, 3);
			string str = text.Substring(startIndex, length);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.Substring");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Trim(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 1)
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string str = text.Trim();
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		if (LuaScriptMgr.CheckParamsType(L, typeof(char), 2, num - 1))
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			string str2 = text2.Trim(arrayNumber);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.Trim");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TrimStart(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		char[] arrayNumber = LuaScriptMgr.GetArrayNumber<char>(L, 2);
		string str = text.TrimStart(arrayNumber);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int TrimEnd(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		char[] arrayNumber = LuaScriptMgr.GetArrayNumber<char>(L, 2);
		string str = text.TrimEnd(arrayNumber);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Compare(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			int d2 = string.Compare(luaString, luaString2);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
			{
				string strA = LuaScriptMgr.GetString(L, 1);
				string strB = LuaScriptMgr.GetString(L, 2);
				StringComparison comparisonType = (StringComparison)(int)LuaScriptMgr.GetLuaObject(L, 3);
				int d = string.Compare(strA, strB, comparisonType);
				LuaScriptMgr.Push(L, d);
				return 1;
			}
			break;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool)))
		{
			string strA2 = LuaScriptMgr.GetString(L, 1);
			string strB2 = LuaScriptMgr.GetString(L, 2);
			bool ignoreCase = LuaDLL.lua_toboolean(L, 3);
			int d3 = string.Compare(strA2, strB2, ignoreCase);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(CultureInfo), typeof(CompareOptions)))
		{
			string strA3 = LuaScriptMgr.GetString(L, 1);
			string strB3 = LuaScriptMgr.GetString(L, 2);
			CultureInfo culture = (CultureInfo)LuaScriptMgr.GetLuaObject(L, 3);
			CompareOptions options = (CompareOptions)(int)LuaScriptMgr.GetLuaObject(L, 4);
			int d4 = string.Compare(strA3, strB3, culture, options);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(bool), typeof(CultureInfo)))
		{
			string strA4 = LuaScriptMgr.GetString(L, 1);
			string strB4 = LuaScriptMgr.GetString(L, 2);
			bool ignoreCase2 = LuaDLL.lua_toboolean(L, 3);
			CultureInfo culture2 = (CultureInfo)LuaScriptMgr.GetLuaObject(L, 4);
			int d5 = string.Compare(strA4, strB4, ignoreCase2, culture2);
			LuaScriptMgr.Push(L, d5);
			return 1;
		}
		switch (num)
		{
		case 5:
		{
			string luaString3 = LuaScriptMgr.GetLuaString(L, 1);
			int indexA2 = (int)LuaScriptMgr.GetNumber(L, 2);
			string luaString4 = LuaScriptMgr.GetLuaString(L, 3);
			int indexB2 = (int)LuaScriptMgr.GetNumber(L, 4);
			int length2 = (int)LuaScriptMgr.GetNumber(L, 5);
			int d7 = string.Compare(luaString3, indexA2, luaString4, indexB2, length2);
			LuaScriptMgr.Push(L, d7);
			return 1;
		}
		case 6:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(StringComparison)))
			{
				string strA5 = LuaScriptMgr.GetString(L, 1);
				int indexA = (int)LuaDLL.lua_tonumber(L, 2);
				string strB5 = LuaScriptMgr.GetString(L, 3);
				int indexB = (int)LuaDLL.lua_tonumber(L, 4);
				int length = (int)LuaDLL.lua_tonumber(L, 5);
				StringComparison comparisonType2 = (StringComparison)(int)LuaScriptMgr.GetLuaObject(L, 6);
				int d6 = string.Compare(strA5, indexA, strB5, indexB, length, comparisonType2);
				LuaScriptMgr.Push(L, d6);
				return 1;
			}
			break;
		}
		if (num == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool)))
		{
			string strA6 = LuaScriptMgr.GetString(L, 1);
			int indexA3 = (int)LuaDLL.lua_tonumber(L, 2);
			string strB6 = LuaScriptMgr.GetString(L, 3);
			int indexB3 = (int)LuaDLL.lua_tonumber(L, 4);
			int length3 = (int)LuaDLL.lua_tonumber(L, 5);
			bool ignoreCase3 = LuaDLL.lua_toboolean(L, 6);
			int d8 = string.Compare(strA6, indexA3, strB6, indexB3, length3, ignoreCase3);
			LuaScriptMgr.Push(L, d8);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(CultureInfo), typeof(CompareOptions)))
		{
			string strA7 = LuaScriptMgr.GetString(L, 1);
			int indexA4 = (int)LuaDLL.lua_tonumber(L, 2);
			string strB7 = LuaScriptMgr.GetString(L, 3);
			int indexB4 = (int)LuaDLL.lua_tonumber(L, 4);
			int length4 = (int)LuaDLL.lua_tonumber(L, 5);
			CultureInfo culture3 = (CultureInfo)LuaScriptMgr.GetLuaObject(L, 6);
			CompareOptions options2 = (CompareOptions)(int)LuaScriptMgr.GetLuaObject(L, 7);
			int d9 = string.Compare(strA7, indexA4, strB7, indexB4, length4, culture3, options2);
			LuaScriptMgr.Push(L, d9);
			return 1;
		}
		if (num == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(int), typeof(string), typeof(int), typeof(int), typeof(bool), typeof(CultureInfo)))
		{
			string strA8 = LuaScriptMgr.GetString(L, 1);
			int indexA5 = (int)LuaDLL.lua_tonumber(L, 2);
			string strB8 = LuaScriptMgr.GetString(L, 3);
			int indexB5 = (int)LuaDLL.lua_tonumber(L, 4);
			int length5 = (int)LuaDLL.lua_tonumber(L, 5);
			bool ignoreCase4 = LuaDLL.lua_toboolean(L, 6);
			CultureInfo culture4 = (CultureInfo)LuaScriptMgr.GetLuaObject(L, 7);
			int d10 = string.Compare(strA8, indexA5, strB8, indexB5, length5, ignoreCase4, culture4);
			LuaScriptMgr.Push(L, d10);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.Compare");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareTo(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string)))
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string strB = LuaScriptMgr.GetString(L, 2);
			int d = text.CompareTo(strB);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(object)))
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			object varObject = LuaScriptMgr.GetVarObject(L, 2);
			int d2 = text2.CompareTo(varObject);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.CompareTo");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int CompareOrdinal(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string luaString3 = LuaScriptMgr.GetLuaString(L, 1);
			string luaString4 = LuaScriptMgr.GetLuaString(L, 2);
			int d2 = string.CompareOrdinal(luaString3, luaString4);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		case 5:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			int indexA = (int)LuaScriptMgr.GetNumber(L, 2);
			string luaString2 = LuaScriptMgr.GetLuaString(L, 3);
			int indexB = (int)LuaScriptMgr.GetNumber(L, 4);
			int length = (int)LuaScriptMgr.GetNumber(L, 5);
			int d = string.CompareOrdinal(luaString, indexA, luaString2, indexB, length);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.CompareOrdinal");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int EndsWith(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text3 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			bool b3 = text3.EndsWith(luaString3);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		case 3:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			StringComparison comparisonType = (StringComparison)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(StringComparison));
			bool b2 = text2.EndsWith(luaString2, comparisonType);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 4:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			CultureInfo culture = (CultureInfo)LuaScriptMgr.GetNetObject(L, 4, typeof(CultureInfo));
			bool b = text.EndsWith(luaString, boolean, culture);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.EndsWith");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IndexOfAny(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text3 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber3 = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int d3 = text3.IndexOfAny(arrayNumber3);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		case 3:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber2 = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int startIndex2 = (int)LuaScriptMgr.GetNumber(L, 3);
			int d2 = text2.IndexOfAny(arrayNumber2, startIndex2);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		case 4:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int startIndex = (int)LuaScriptMgr.GetNumber(L, 3);
			int count = (int)LuaScriptMgr.GetNumber(L, 4);
			int d = text.IndexOfAny(arrayNumber, startIndex, count);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.IndexOfAny");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IndexOf(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char)))
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char value = (char)LuaDLL.lua_tonumber(L, 2);
			int d = text.IndexOf(value);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string)))
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value2 = LuaScriptMgr.GetString(L, 2);
			int d2 = text2.IndexOf(value2);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int)))
		{
			string text3 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value3 = LuaScriptMgr.GetString(L, 2);
			int startIndex = (int)LuaDLL.lua_tonumber(L, 3);
			int d3 = text3.IndexOf(value3, startIndex);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int)))
		{
			string text4 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char value4 = (char)LuaDLL.lua_tonumber(L, 2);
			int startIndex2 = (int)LuaDLL.lua_tonumber(L, 3);
			int d4 = text4.IndexOf(value4, startIndex2);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
		{
			string text5 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value5 = LuaScriptMgr.GetString(L, 2);
			StringComparison comparisonType = (StringComparison)(int)LuaScriptMgr.GetLuaObject(L, 3);
			int d5 = text5.IndexOf(value5, comparisonType);
			LuaScriptMgr.Push(L, d5);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int)))
		{
			string text6 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value6 = LuaScriptMgr.GetString(L, 2);
			int startIndex3 = (int)LuaDLL.lua_tonumber(L, 3);
			int count = (int)LuaDLL.lua_tonumber(L, 4);
			int d6 = text6.IndexOf(value6, startIndex3, count);
			LuaScriptMgr.Push(L, d6);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(StringComparison)))
		{
			string text7 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value7 = LuaScriptMgr.GetString(L, 2);
			int startIndex4 = (int)LuaDLL.lua_tonumber(L, 3);
			StringComparison comparisonType2 = (StringComparison)(int)LuaScriptMgr.GetLuaObject(L, 4);
			int d7 = text7.IndexOf(value7, startIndex4, comparisonType2);
			LuaScriptMgr.Push(L, d7);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int), typeof(int)))
		{
			string text8 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char value8 = (char)LuaDLL.lua_tonumber(L, 2);
			int startIndex5 = (int)LuaDLL.lua_tonumber(L, 3);
			int count2 = (int)LuaDLL.lua_tonumber(L, 4);
			int d8 = text8.IndexOf(value8, startIndex5, count2);
			LuaScriptMgr.Push(L, d8);
			return 1;
		}
		if (num == 5)
		{
			string text9 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			int startIndex6 = (int)LuaScriptMgr.GetNumber(L, 3);
			int count3 = (int)LuaScriptMgr.GetNumber(L, 4);
			StringComparison comparisonType3 = (StringComparison)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(StringComparison));
			int d9 = text9.IndexOf(luaString, startIndex6, count3, comparisonType3);
			LuaScriptMgr.Push(L, d9);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.IndexOf");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LastIndexOf(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char)))
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char value = (char)LuaDLL.lua_tonumber(L, 2);
			int d = text.LastIndexOf(value);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string)))
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value2 = LuaScriptMgr.GetString(L, 2);
			int d2 = text2.LastIndexOf(value2);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int)))
		{
			string text3 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value3 = LuaScriptMgr.GetString(L, 2);
			int startIndex = (int)LuaDLL.lua_tonumber(L, 3);
			int d3 = text3.LastIndexOf(value3, startIndex);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int)))
		{
			string text4 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char value4 = (char)LuaDLL.lua_tonumber(L, 2);
			int startIndex2 = (int)LuaDLL.lua_tonumber(L, 3);
			int d4 = text4.LastIndexOf(value4, startIndex2);
			LuaScriptMgr.Push(L, d4);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(StringComparison)))
		{
			string text5 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value5 = LuaScriptMgr.GetString(L, 2);
			StringComparison comparisonType = (StringComparison)(int)LuaScriptMgr.GetLuaObject(L, 3);
			int d5 = text5.LastIndexOf(value5, comparisonType);
			LuaScriptMgr.Push(L, d5);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(int)))
		{
			string text6 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value6 = LuaScriptMgr.GetString(L, 2);
			int startIndex3 = (int)LuaDLL.lua_tonumber(L, 3);
			int count = (int)LuaDLL.lua_tonumber(L, 4);
			int d6 = text6.LastIndexOf(value6, startIndex3, count);
			LuaScriptMgr.Push(L, d6);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(int), typeof(StringComparison)))
		{
			string text7 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string value7 = LuaScriptMgr.GetString(L, 2);
			int startIndex4 = (int)LuaDLL.lua_tonumber(L, 3);
			StringComparison comparisonType2 = (StringComparison)(int)LuaScriptMgr.GetLuaObject(L, 4);
			int d7 = text7.LastIndexOf(value7, startIndex4, comparisonType2);
			LuaScriptMgr.Push(L, d7);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char), typeof(int), typeof(int)))
		{
			string text8 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char value8 = (char)LuaDLL.lua_tonumber(L, 2);
			int startIndex5 = (int)LuaDLL.lua_tonumber(L, 3);
			int count2 = (int)LuaDLL.lua_tonumber(L, 4);
			int d8 = text8.LastIndexOf(value8, startIndex5, count2);
			LuaScriptMgr.Push(L, d8);
			return 1;
		}
		if (num == 5)
		{
			string text9 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			int startIndex6 = (int)LuaScriptMgr.GetNumber(L, 3);
			int count3 = (int)LuaScriptMgr.GetNumber(L, 4);
			StringComparison comparisonType3 = (StringComparison)(int)LuaScriptMgr.GetNetObject(L, 5, typeof(StringComparison));
			int d9 = text9.LastIndexOf(luaString, startIndex6, count3, comparisonType3);
			LuaScriptMgr.Push(L, d9);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.LastIndexOf");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int LastIndexOfAny(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text3 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber3 = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int d3 = text3.LastIndexOfAny(arrayNumber3);
			LuaScriptMgr.Push(L, d3);
			return 1;
		}
		case 3:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber2 = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int startIndex2 = (int)LuaScriptMgr.GetNumber(L, 3);
			int d2 = text2.LastIndexOfAny(arrayNumber2, startIndex2);
			LuaScriptMgr.Push(L, d2);
			return 1;
		}
		case 4:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char[] arrayNumber = LuaScriptMgr.GetArrayNumber<char>(L, 2);
			int startIndex = (int)LuaScriptMgr.GetNumber(L, 3);
			int count = (int)LuaScriptMgr.GetNumber(L, 4);
			int d = text.LastIndexOfAny(arrayNumber, startIndex, count);
			LuaScriptMgr.Push(L, d);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.LastIndexOfAny");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Contains(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		string luaString = LuaScriptMgr.GetLuaString(L, 2);
		bool b = text.Contains(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsNullOrEmpty(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		bool b = string.IsNullOrEmpty(luaString);
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Normalize(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string str2 = text2.Normalize();
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			NormalizationForm normalizationForm = (NormalizationForm)(int)LuaScriptMgr.GetNetObject(L, 2, typeof(NormalizationForm));
			string str = text.Normalize(normalizationForm);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.Normalize");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsNormalized(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			bool b2 = text2.IsNormalized();
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 2:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			NormalizationForm normalizationForm = (NormalizationForm)(int)LuaScriptMgr.GetNetObject(L, 2, typeof(NormalizationForm));
			bool b = text.IsNormalized(normalizationForm);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.IsNormalized");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Remove(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int startIndex2 = (int)LuaScriptMgr.GetNumber(L, 2);
			string str2 = text2.Remove(startIndex2);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 3:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int startIndex = (int)LuaScriptMgr.GetNumber(L, 2);
			int count = (int)LuaScriptMgr.GetNumber(L, 3);
			string str = text.Remove(startIndex, count);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.Remove");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PadLeft(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int totalWidth2 = (int)LuaScriptMgr.GetNumber(L, 2);
			string str2 = text2.PadLeft(totalWidth2);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 3:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int totalWidth = (int)LuaScriptMgr.GetNumber(L, 2);
			char paddingChar = (char)LuaScriptMgr.GetNumber(L, 3);
			string str = text.PadLeft(totalWidth, paddingChar);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.PadLeft");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int PadRight(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int totalWidth2 = (int)LuaScriptMgr.GetNumber(L, 2);
			string str2 = text2.PadRight(totalWidth2);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 3:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			int totalWidth = (int)LuaScriptMgr.GetNumber(L, 2);
			char paddingChar = (char)LuaScriptMgr.GetNumber(L, 3);
			string str = text.PadRight(totalWidth, paddingChar);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.PadRight");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int StartsWith(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string text3 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString3 = LuaScriptMgr.GetLuaString(L, 2);
			bool b3 = text3.StartsWith(luaString3);
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		case 3:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
			StringComparison comparisonType = (StringComparison)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(StringComparison));
			bool b2 = text2.StartsWith(luaString2, comparisonType);
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		case 4:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			bool boolean = LuaScriptMgr.GetBoolean(L, 3);
			CultureInfo culture = (CultureInfo)LuaScriptMgr.GetNetObject(L, 4, typeof(CultureInfo));
			bool b = text.StartsWith(luaString, boolean, culture);
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.StartsWith");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Replace(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string)))
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string oldValue = LuaScriptMgr.GetString(L, 2);
			string newValue = LuaScriptMgr.GetString(L, 3);
			string str = text.Replace(oldValue, newValue);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(char), typeof(char)))
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			char oldChar = (char)LuaDLL.lua_tonumber(L, 2);
			char newChar = (char)LuaDLL.lua_tonumber(L, 3);
			string str2 = text2.Replace(oldChar, newChar);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.Replace");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToLower(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string str2 = text2.ToLower();
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			CultureInfo culture = (CultureInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(CultureInfo));
			string str = text.ToLower(culture);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.ToLower");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToLowerInvariant(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		string str = text.ToLowerInvariant();
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToUpper(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string str2 = text2.ToUpper();
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			CultureInfo culture = (CultureInfo)LuaScriptMgr.GetNetObject(L, 2, typeof(CultureInfo));
			string str = text.ToUpper(culture);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.ToUpper");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToUpperInvariant(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		string str = text.ToUpperInvariant();
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int ToString(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 1:
		{
			string text2 = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			string str2 = text2.ToString();
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
		{
			string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
			IFormatProvider formatProvider = (IFormatProvider)LuaScriptMgr.GetNetObject(L, 2, typeof(IFormatProvider));
			string str = text.ToString(formatProvider);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.ToString");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Format(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 2:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			object varObject3 = LuaScriptMgr.GetVarObject(L, 2);
			string str2 = string.Format(luaString, varObject3);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 3:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(object), typeof(object)))
			{
				string format = LuaScriptMgr.GetString(L, 1);
				object varObject = LuaScriptMgr.GetVarObject(L, 2);
				object varObject2 = LuaScriptMgr.GetVarObject(L, 3);
				string str = string.Format(format, varObject, varObject2);
				LuaScriptMgr.Push(L, str);
				return 1;
			}
			break;
		}
		if (num == 4)
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			object varObject4 = LuaScriptMgr.GetVarObject(L, 2);
			object varObject5 = LuaScriptMgr.GetVarObject(L, 3);
			object varObject6 = LuaScriptMgr.GetVarObject(L, 4);
			string str3 = string.Format(luaString2, varObject4, varObject5, varObject6);
			LuaScriptMgr.Push(L, str3);
			return 1;
		}
		if (LuaScriptMgr.CheckTypes(L, 1, typeof(IFormatProvider), typeof(string)) && LuaScriptMgr.CheckParamsType(L, typeof(object), 3, num - 2))
		{
			IFormatProvider provider = (IFormatProvider)LuaScriptMgr.GetLuaObject(L, 1);
			string format2 = LuaScriptMgr.GetString(L, 2);
			object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 3, num - 2);
			string str4 = string.Format(provider, format2, paramsObject);
			LuaScriptMgr.Push(L, str4);
			return 1;
		}
		if (LuaScriptMgr.CheckTypes(L, 1, typeof(string)) && LuaScriptMgr.CheckParamsType(L, typeof(object), 2, num - 1))
		{
			string format3 = LuaScriptMgr.GetString(L, 1);
			object[] paramsObject2 = LuaScriptMgr.GetParamsObject(L, 2, num - 1);
			string str5 = string.Format(format3, paramsObject2);
			LuaScriptMgr.Push(L, str5);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.Format");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Copy(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = string.Copy(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Concat(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		switch (num)
		{
		case 1:
		{
			object varObject = LuaScriptMgr.GetVarObject(L, 1);
			string str2 = string.Concat(varObject);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 2:
			if (LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string text = LuaScriptMgr.GetString(L, 1);
				string text2 = LuaScriptMgr.GetString(L, 2);
				string str = text + text2;
				LuaScriptMgr.Push(L, str);
				return 1;
			}
			break;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(object), typeof(object)))
		{
			object varObject2 = LuaScriptMgr.GetVarObject(L, 1);
			object varObject3 = LuaScriptMgr.GetVarObject(L, 2);
			string str3 = string.Concat(varObject2, varObject3);
			LuaScriptMgr.Push(L, str3);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string)))
		{
			string text3 = LuaScriptMgr.GetString(L, 1);
			string text4 = LuaScriptMgr.GetString(L, 2);
			string text5 = LuaScriptMgr.GetString(L, 3);
			string str4 = text3 + text4 + text5;
			LuaScriptMgr.Push(L, str4);
			return 1;
		}
		if (num == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(object), typeof(object), typeof(object)))
		{
			object varObject4 = LuaScriptMgr.GetVarObject(L, 1);
			object varObject5 = LuaScriptMgr.GetVarObject(L, 2);
			object varObject6 = LuaScriptMgr.GetVarObject(L, 3);
			string str5 = string.Concat(varObject4, varObject5, varObject6);
			LuaScriptMgr.Push(L, str5);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(string), typeof(string), typeof(string), typeof(string)))
		{
			string text6 = LuaScriptMgr.GetString(L, 1);
			string text7 = LuaScriptMgr.GetString(L, 2);
			string text8 = LuaScriptMgr.GetString(L, 3);
			string text9 = LuaScriptMgr.GetString(L, 4);
			string str6 = text6 + text7 + text8 + text9;
			LuaScriptMgr.Push(L, str6);
			return 1;
		}
		if (num == 4 && LuaScriptMgr.CheckTypes(L, 1, typeof(object), typeof(object), typeof(object), typeof(object)))
		{
			object varObject7 = LuaScriptMgr.GetVarObject(L, 1);
			object varObject8 = LuaScriptMgr.GetVarObject(L, 2);
			object varObject9 = LuaScriptMgr.GetVarObject(L, 3);
			object varObject10 = LuaScriptMgr.GetVarObject(L, 4);
			string str7 = string.Concat(varObject7, varObject8, varObject9, varObject10);
			LuaScriptMgr.Push(L, str7);
			return 1;
		}
		if (LuaScriptMgr.CheckParamsType(L, typeof(string), 1, num))
		{
			string[] paramsString = LuaScriptMgr.GetParamsString(L, 1, num);
			string str8 = string.Concat(paramsString);
			LuaScriptMgr.Push(L, str8);
			return 1;
		}
		if (LuaScriptMgr.CheckParamsType(L, typeof(object), 1, num))
		{
			object[] paramsObject = LuaScriptMgr.GetParamsObject(L, 1, num);
			string str9 = string.Concat(paramsObject);
			LuaScriptMgr.Push(L, str9);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.Concat");
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Insert(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		int startIndex = (int)LuaScriptMgr.GetNumber(L, 2);
		string luaString = LuaScriptMgr.GetLuaString(L, 3);
		string str = text.Insert(startIndex, luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Intern(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = string.Intern(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IsInterned(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string str = string.IsInterned(luaString);
		LuaScriptMgr.Push(L, str);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Join(IntPtr L)
	{
		switch (LuaDLL.lua_gettop(L))
		{
		case 2:
		{
			string luaString2 = LuaScriptMgr.GetLuaString(L, 1);
			string[] arrayString2 = LuaScriptMgr.GetArrayString(L, 2);
			string str2 = string.Join(luaString2, arrayString2);
			LuaScriptMgr.Push(L, str2);
			return 1;
		}
		case 4:
		{
			string luaString = LuaScriptMgr.GetLuaString(L, 1);
			string[] arrayString = LuaScriptMgr.GetArrayString(L, 2);
			int startIndex = (int)LuaScriptMgr.GetNumber(L, 3);
			int count = (int)LuaScriptMgr.GetNumber(L, 4);
			string str = string.Join(luaString, arrayString, startIndex, count);
			LuaScriptMgr.Push(L, str);
			return 1;
		}
		default:
			LuaDLL.luaL_error(L, "invalid arguments to method: string.Join");
			return 0;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetEnumerator(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		CharEnumerator enumerator = text.GetEnumerator();
		LuaScriptMgr.Push(L, enumerator);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int GetHashCode(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string text = (string)LuaScriptMgr.GetNetObjectSelf(L, 1, "string");
		int hashCode = text.GetHashCode();
		LuaScriptMgr.Push(L, hashCode);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string luaString = LuaScriptMgr.GetLuaString(L, 1);
		string luaString2 = LuaScriptMgr.GetLuaString(L, 2);
		bool b = luaString == luaString2;
		LuaScriptMgr.Push(L, b);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int Equals(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 2, typeof(string)))
		{
			string text = LuaScriptMgr.GetVarObject(L, 1) as string;
			string text2 = LuaScriptMgr.GetString(L, 2);
			bool b = ((text == null) ? (text2 == null) : text.Equals(text2));
			LuaScriptMgr.Push(L, b);
			return 1;
		}
		if (num == 2 && LuaScriptMgr.CheckTypes(L, 2, typeof(object)))
		{
			string text3 = LuaScriptMgr.GetVarObject(L, 1) as string;
			object varObject = LuaScriptMgr.GetVarObject(L, 2);
			bool b2 = ((text3 == null) ? (varObject == null) : text3.Equals(varObject));
			LuaScriptMgr.Push(L, b2);
			return 1;
		}
		if (num == 3)
		{
			string text4 = LuaScriptMgr.GetVarObject(L, 1) as string;
			string luaString = LuaScriptMgr.GetLuaString(L, 2);
			StringComparison comparisonType = (StringComparison)(int)LuaScriptMgr.GetNetObject(L, 3, typeof(StringComparison));
			bool b3 = ((text4 == null) ? (luaString == null) : text4.Equals(luaString, comparisonType));
			LuaScriptMgr.Push(L, b3);
			return 1;
		}
		LuaDLL.luaL_error(L, "invalid arguments to method: string.Equals");
		return 0;
	}
}
