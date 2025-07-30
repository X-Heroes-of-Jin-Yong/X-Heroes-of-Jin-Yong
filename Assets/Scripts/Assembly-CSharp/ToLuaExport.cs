using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using LuaInterface;
using UnityEngine;

public static class ToLuaExport
{
	public static string className;

	public static Type type;

	public static string baseClassName;

	public static bool isStaticClass;

	private static HashSet<string> usingList;

	private static MetaOp op;

	private static StringBuilder sb;

	private static MethodInfo[] methods;

	private static Dictionary<string, int> nameCounter;

	private static FieldInfo[] fields;

	private static PropertyInfo[] props;

	private static List<PropertyInfo> propList;

	private static BindingFlags binding;

	private static ObjAmbig ambig;

	public static string wrapClassName;

	public static string libClassName;

	public static string extendName;

	public static Type extendType;

	public static HashSet<Type> eventSet;

	public static List<string> memberFilter;

	private static Dictionary<Type, int> typeSize;

	static ToLuaExport()
	{
		className = string.Empty;
		type = null;
		baseClassName = null;
		isStaticClass = true;
		usingList = new HashSet<string>();
		op = MetaOp.None;
		sb = null;
		methods = null;
		nameCounter = null;
		fields = null;
		props = null;
		propList = new List<PropertyInfo>();
		binding = BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public;
		ambig = ObjAmbig.NetObj;
		wrapClassName = string.Empty;
		libClassName = string.Empty;
		extendName = string.Empty;
		extendType = null;
		eventSet = new HashSet<Type>();
		memberFilter = new List<string>
		{
			"AnimationClip.averageDuration", "AnimationClip.averageAngularSpeed", "AnimationClip.averageSpeed", "AnimationClip.apparentSpeed", "AnimationClip.isLooping", "AnimationClip.isAnimatorMotion", "AnimationClip.isHumanMotion", "AnimatorOverrideController.PerformOverrideClipListCleanup", "Caching.SetNoBackupFlag", "Caching.ResetNoBackupFlag",
			"Light.areaSize", "Security.GetChainOfTrustValue", "Texture2D.alphaIsTransparency", "WWW.movie", "WebCamTexture.MarkNonReadable", "WebCamTexture.isReadable", "Graphic.OnRebuildRequested", "Text.OnRebuildRequested", "UIInput.ProcessEvent", "UIWidget.showHandlesWithMoveTool",
			"UIWidget.showHandles", "Application.ExternalEval", "Resources.LoadAssetAtPath", "Input.IsJoystickPreconfigured", "String.Chars"
		};
		typeSize = new Dictionary<Type, int>
		{
			{
				typeof(bool),
				1
			},
			{
				typeof(char),
				2
			},
			{
				typeof(byte),
				3
			},
			{
				typeof(sbyte),
				4
			},
			{
				typeof(ushort),
				5
			},
			{
				typeof(short),
				6
			},
			{
				typeof(uint),
				7
			},
			{
				typeof(int),
				8
			},
			{
				typeof(float),
				9
			},
			{
				typeof(ulong),
				10
			},
			{
				typeof(long),
				11
			},
			{
				typeof(double),
				12
			}
		};
	}

	public static bool IsMemberFilter(MemberInfo mi)
	{
		return memberFilter.Contains(type.Name + "." + mi.Name);
	}

	public static void Clear()
	{
		className = null;
		type = null;
		isStaticClass = false;
		baseClassName = null;
		usingList.Clear();
		op = MetaOp.None;
		sb = new StringBuilder();
		methods = null;
		fields = null;
		props = null;
		propList.Clear();
		ambig = ObjAmbig.NetObj;
		wrapClassName = string.Empty;
		libClassName = string.Empty;
	}

	private static MetaOp GetOp(string name)
	{
		switch (name)
		{
		case "op_Addition":
			return MetaOp.Add;
		case "op_Subtraction":
			return MetaOp.Sub;
		case "op_Equality":
			return MetaOp.Eq;
		case "op_Multiply":
			return MetaOp.Mul;
		case "op_Division":
			return MetaOp.Div;
		case "op_UnaryNegation":
			return MetaOp.Neg;
		default:
			return MetaOp.None;
		}
	}

	private static void GenBaseOpFunction(List<MethodInfo> list)
	{
		for (Type baseType = type.BaseType; baseType != null; baseType = baseType.BaseType)
		{
			MethodInfo[] array = baseType.GetMethods(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			for (int i = 0; i < array.Length; i++)
			{
				MetaOp metaOp = GetOp(array[i].Name);
				if (metaOp != MetaOp.None && (op & metaOp) == 0)
				{
					list.Add(array[i]);
					op |= metaOp;
				}
			}
		}
	}

	public static void Generate(params string[] param)
	{
		Debugger.Log("Begin Generate lua Wrap for class {0}\r\n", className);
		sb = new StringBuilder();
		usingList.Add("System");
		GetTypeStr(type);
		if (wrapClassName == string.Empty)
		{
			wrapClassName = className;
		}
		if (libClassName == string.Empty)
		{
			libClassName = className;
		}
		if (type.IsEnum)
		{
			GenEnum();
			GenEnumTranslator();
			sb.AppendLine("}\r\n");
			SaveFile(AppConst.uLuaPath + "/Source/LuaWrap/" + wrapClassName + "Wrap.cs");
			return;
		}
		nameCounter = new Dictionary<string, int>();
		List<MethodInfo> list = new List<MethodInfo>();
		if (baseClassName != null)
		{
			binding |= BindingFlags.DeclaredOnly;
		}
		else if (baseClassName == null && isStaticClass)
		{
			binding |= BindingFlags.DeclaredOnly;
		}
		if (type.IsInterface)
		{
			list.AddRange(type.GetMethods());
		}
		else
		{
			list.AddRange(type.GetMethods(BindingFlags.Instance | binding));
			for (int num = list.Count - 1; num >= 0; num--)
			{
				if (list[num].Name.Contains("op_") || list[num].Name.Contains("add_") || list[num].Name.Contains("remove_"))
				{
					if (!IsNeedOp(list[num].Name))
					{
						list.RemoveAt(num);
					}
				}
				else if (IsObsolete(list[num]))
				{
					list.RemoveAt(num);
				}
			}
		}
		PropertyInfo[] ps = type.GetProperties();
		for (int i = 0; i < ps.Length; i++)
		{
			int num2 = list.FindIndex((MethodInfo m) => m.Name == "get_" + ps[i].Name);
			if (num2 >= 0 && list[num2].Name != "get_Item")
			{
				list.RemoveAt(num2);
			}
			num2 = list.FindIndex((MethodInfo m) => m.Name == "set_" + ps[i].Name);
			if (num2 >= 0 && list[num2].Name != "set_Item")
			{
				list.RemoveAt(num2);
			}
		}
		ProcessExtends(list);
		GenBaseOpFunction(list);
		methods = list.ToArray();
		sb.AppendFormat("public class {0}Wrap\r\n", wrapClassName);
		sb.AppendLine("{");
		GenRegFunc();
		GenConstruct();
		GenGetType();
		GenIndexFunc();
		GenNewIndexFunc();
		GenToStringFunc();
		GenFunction();
		sb.AppendLine("}\r\n");
		string text = AppConst.uLuaPath + "/Source/LuaWrap/";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		SaveFile(text + wrapClassName + "Wrap.cs");
	}

	private static void SaveFile(string file)
	{
		using (StreamWriter streamWriter = new StreamWriter(file, false, Encoding.UTF8))
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string @using in usingList)
			{
				stringBuilder.AppendFormat("using {0};\r\n", @using);
			}
			stringBuilder.AppendLine("using LuaInterface;");
			if (ambig == ObjAmbig.All)
			{
				stringBuilder.AppendLine("using Object = UnityEngine.Object;");
			}
			stringBuilder.AppendLine();
			streamWriter.Write(stringBuilder.ToString());
			streamWriter.Write(sb.ToString());
			streamWriter.Flush();
			streamWriter.Close();
		}
	}

	private static void GenLuaFields()
	{
		fields = type.GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField | binding);
		props = type.GetProperties(BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty | binding);
		propList.AddRange(type.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty));
		List<FieldInfo> list = new List<FieldInfo>();
		list.AddRange(fields);
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if (IsObsolete(list[num]))
			{
				list.RemoveAt(num);
			}
		}
		fields = list.ToArray();
		List<PropertyInfo> list2 = new List<PropertyInfo>();
		list2.AddRange(props);
		for (int num2 = list2.Count - 1; num2 >= 0; num2--)
		{
			if (list2[num2].Name == "Item" || IsObsolete(list2[num2]))
			{
				list2.RemoveAt(num2);
			}
		}
		props = list2.ToArray();
		for (int num3 = propList.Count - 1; num3 >= 0; num3--)
		{
			if (propList[num3].Name == "Item" || IsObsolete(propList[num3]))
			{
				propList.RemoveAt(num3);
			}
		}
		if (fields.Length == 0 && props.Length == 0 && isStaticClass && baseClassName == null)
		{
			return;
		}
		sb.AppendLine("\t\tLuaField[] fields = new LuaField[]");
		sb.AppendLine("\t\t{");
		for (int i = 0; i < fields.Length; i++)
		{
			if (fields[i].IsLiteral || fields[i].IsPrivate || fields[i].IsInitOnly)
			{
				sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, null),\r\n", fields[i].Name);
			}
			else
			{
				sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, set_{0}),\r\n", fields[i].Name);
			}
		}
		for (int j = 0; j < props.Length; j++)
		{
			if (props[j].CanRead && props[j].CanWrite && props[j].GetSetMethod(true).IsPublic)
			{
				sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, set_{0}),\r\n", props[j].Name);
			}
			else if (props[j].CanRead)
			{
				sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, null),\r\n", props[j].Name);
			}
			else if (props[j].CanWrite)
			{
				sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", null, set_{0}),\r\n", props[j].Name);
			}
		}
		sb.AppendLine("\t\t};\r\n");
	}

	private static void GenLuaMethods()
	{
		sb.AppendLine("\t\tLuaMethod[] regs = new LuaMethod[]");
		sb.AppendLine("\t\t{");
		for (int i = 0; i < methods.Length; i++)
		{
			MethodInfo methodInfo = methods[i];
			int value = 1;
			if (methodInfo.IsGenericMethod)
			{
				continue;
			}
			if (!nameCounter.TryGetValue(methodInfo.Name, out value))
			{
				if (!methodInfo.Name.Contains("op_"))
				{
					sb.AppendFormat("\t\t\tnew LuaMethod(\"{0}\", {0}),\r\n", methodInfo.Name);
				}
				nameCounter[methodInfo.Name] = 1;
			}
			else
			{
				nameCounter[methodInfo.Name] = value + 1;
			}
		}
		sb.AppendFormat("\t\t\tnew LuaMethod(\"New\", _Create{0}),\r\n", wrapClassName);
		sb.AppendLine("\t\t\tnew LuaMethod(\"GetClassType\", GetClassType),");
		int num = Array.FindIndex(methods, (MethodInfo p) => p.Name == "ToString");
		if (num >= 0 && !isStaticClass)
		{
			sb.AppendLine("\t\t\tnew LuaMethod(\"__tostring\", Lua_ToString),");
		}
		GenOperatorReg();
		sb.AppendLine("\t\t};\r\n");
	}

	private static void GenOperatorReg()
	{
		if ((op & MetaOp.Add) != MetaOp.None)
		{
			sb.AppendLine("\t\t\tnew LuaMethod(\"__add\", Lua_Add),");
		}
		if ((op & MetaOp.Sub) != MetaOp.None)
		{
			sb.AppendLine("\t\t\tnew LuaMethod(\"__sub\", Lua_Sub),");
		}
		if ((op & MetaOp.Mul) != MetaOp.None)
		{
			sb.AppendLine("\t\t\tnew LuaMethod(\"__mul\", Lua_Mul),");
		}
		if ((op & MetaOp.Div) != MetaOp.None)
		{
			sb.AppendLine("\t\t\tnew LuaMethod(\"__div\", Lua_Div),");
		}
		if ((op & MetaOp.Eq) != MetaOp.None)
		{
			sb.AppendLine("\t\t\tnew LuaMethod(\"__eq\", Lua_Eq),");
		}
		if ((op & MetaOp.Neg) != MetaOp.None)
		{
			sb.AppendLine("\t\t\tnew LuaMethod(\"__unm\", Lua_Neg),");
		}
	}

	private static void GenRegFunc()
	{
		sb.AppendLine("\tpublic static void Register(IntPtr L)");
		sb.AppendLine("\t{");
		GenLuaMethods();
		GenLuaFields();
		if (baseClassName == null)
		{
			if (isStaticClass && fields.Length == 0 && props.Length == 0)
			{
				sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", regs);\r\n", libClassName);
			}
			else
			{
				sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", typeof({1}), regs, fields, null);\r\n", libClassName, className);
			}
		}
		else
		{
			sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", typeof({1}), regs, fields, typeof({2}));\r\n", libClassName, className, baseClassName);
		}
		sb.AppendLine("\t}");
	}

	private static bool IsParams(ParameterInfo param)
	{
		return param.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
	}

	private static void GenFunction()
	{
		HashSet<string> hashSet = new HashSet<string>();
		for (int i = 0; i < methods.Length; i++)
		{
			MethodInfo methodInfo = methods[i];
			if (methodInfo.IsGenericMethod)
			{
				Debugger.Log("Generic Method {0} cannot be export to lua", methodInfo.Name);
				continue;
			}
			if (nameCounter[methodInfo.Name] > 1)
			{
				if (hashSet.Contains(methodInfo.Name))
				{
					continue;
				}
				MethodInfo methodInfo2 = GenOverrideFunc(methodInfo.Name);
				if (methodInfo2 == null)
				{
					hashSet.Add(methodInfo.Name);
					continue;
				}
				methodInfo = methodInfo2;
			}
			hashSet.Add(methodInfo.Name);
			sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
			sb.AppendFormat("\tstatic int {0}(IntPtr L)\r\n", GetFuncName(methodInfo.Name));
			sb.AppendLine("\t{");
			if (HasAttribute(methodInfo, typeof(OnlyGCAttribute)))
			{
				sb.AppendLine("\t\tLuaScriptMgr.__gc(L);");
				sb.AppendLine("\t\treturn 0;");
				sb.AppendLine("\t}");
				continue;
			}
			if (HasAttribute(methodInfo, typeof(UseDefinedAttribute)))
			{
				FieldInfo field = extendType.GetField(methodInfo.Name + "Defined");
				string value = field.GetValue(null) as string;
				sb.AppendLine(value);
				sb.AppendLine("\t}");
				continue;
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			int num = (methodInfo.IsStatic ? 1 : 2);
			if (!HasOptionalParam(parameters))
			{
				int num2 = parameters.Length + num - 1;
				sb.AppendFormat("\t\tLuaScriptMgr.CheckArgsCount(L, {0});\r\n", num2);
			}
			else
			{
				sb.AppendLine("\t\tint count = LuaDLL.lua_gettop(L);");
			}
			int num3 = ((methodInfo.ReturnType != typeof(void)) ? 1 : 0);
			num3 += ProcessParams(methodInfo, 2, false, false);
			sb.AppendFormat("\t\treturn {0};\r\n", num3);
			sb.AppendLine("\t}");
		}
	}

	private static void NoConsturct()
	{
		sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
		sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
		sb.AppendLine("\t{");
		sb.AppendFormat("\t\tLuaDLL.luaL_error(L, \"{0} class does not have a constructor function\");\r\n", className);
		sb.AppendLine("\t\treturn 0;");
		sb.AppendLine("\t}");
	}

	private static string GetPushFunction(Type t)
	{
		if (t.IsEnum)
		{
			return "Push";
		}
		if (t == typeof(bool) || t.IsPrimitive || t == typeof(string) || t == typeof(LuaTable) || t == typeof(LuaCSFunction) || t == typeof(LuaFunction) || typeof(UnityEngine.Object).IsAssignableFrom(t) || t == typeof(Type) || t == typeof(IntPtr) || typeof(Delegate).IsAssignableFrom(t) || t == typeof(LuaStringBuffer) || typeof(TrackedReference).IsAssignableFrom(t) || typeof(IEnumerator).IsAssignableFrom(t))
		{
			return "Push";
		}
		if (t == typeof(Vector3) || t == typeof(Vector2) || t == typeof(Vector4) || t == typeof(Quaternion) || t == typeof(Color) || t == typeof(RaycastHit) || t == typeof(Ray) || t == typeof(Touch) || t == typeof(Bounds))
		{
			return "Push";
		}
		if (t == typeof(object))
		{
			return "PushVarObject";
		}
		if (t.IsValueType)
		{
			return "PushValue";
		}
		if (t.IsArray)
		{
			return "PushArray";
		}
		return "PushObject";
	}

	private static void DefaultConstruct()
	{
		sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
		sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
		sb.AppendLine("\t{");
		sb.AppendLine("\t\tLuaScriptMgr.CheckArgsCount(L, 0);");
		sb.AppendFormat("\t\t{0} obj = new {0}();\r\n", className);
		string pushFunction = GetPushFunction(type);
		sb.AppendFormat("\t\tLuaScriptMgr.{0}(L, obj);\r\n", pushFunction);
		sb.AppendLine("\t\treturn 1;");
		sb.AppendLine("\t}");
	}

	private static string GetCountStr(int count)
	{
		if (count != 0)
		{
			return string.Format("count - {0}", count);
		}
		return "count";
	}

	private static void GenGetType()
	{
		sb.AppendFormat("\r\n\tstatic Type classType = typeof({0});\r\n", className);
		sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
		sb.AppendFormat("\tstatic int {0}(IntPtr L)\r\n", "GetClassType");
		sb.AppendLine("\t{");
		sb.AppendLine("\t\tLuaScriptMgr.Push(L, classType);");
		sb.AppendLine("\t\treturn 1;");
		sb.AppendLine("\t}");
	}

	private static void GenConstruct()
	{
		if (isStaticClass || typeof(MonoBehaviour).IsAssignableFrom(type))
		{
			NoConsturct();
			return;
		}
		ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | binding);
		if (extendType != null)
		{
			ConstructorInfo[] constructors2 = extendType.GetConstructors(BindingFlags.Instance | binding);
			if (constructors2 != null && constructors2.Length > 0 && HasAttribute(constructors2[0], typeof(UseDefinedAttribute)))
			{
				sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
				sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
				sb.AppendLine("\t{");
				if (HasAttribute(constructors2[0], typeof(UseDefinedAttribute)))
				{
					FieldInfo field = extendType.GetField(extendName + "Defined");
					string value = field.GetValue(null) as string;
					sb.AppendLine(value);
					sb.AppendLine("\t}");
					return;
				}
			}
		}
		if (constructors.Length == 0)
		{
			if (!type.IsValueType)
			{
				NoConsturct();
			}
			else
			{
				DefaultConstruct();
			}
			return;
		}
		List<ConstructorInfo> list = new List<ConstructorInfo>();
		for (int i = 0; i < constructors.Length; i++)
		{
			if (HasDecimal(constructors[i].GetParameters()) || IsObsolete(constructors[i]))
			{
				continue;
			}
			ConstructorInfo r = constructors[i];
			int num = list.FindIndex((ConstructorInfo p) => CompareMethod(p, r) >= 0);
			if (num >= 0)
			{
				if (CompareMethod(list[num], r) == 2)
				{
					list.RemoveAt(num);
					list.Add(r);
				}
			}
			else
			{
				list.Add(r);
			}
		}
		if (list.Count == 0)
		{
			if (!type.IsValueType)
			{
				NoConsturct();
			}
			else
			{
				DefaultConstruct();
			}
			return;
		}
		list.Sort(Compare);
		sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
		sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
		sb.AppendLine("\t{");
		sb.AppendLine("\t\tint count = LuaDLL.lua_gettop(L);");
		sb.AppendLine();
		List<ConstructorInfo> list2 = new List<ConstructorInfo>();
		for (int i2 = 0; i2 < list.Count; i2++)
		{
			int num2 = list.FindIndex((ConstructorInfo p) => p != list[i2] && p.GetParameters().Length == list[i2].GetParameters().Length);
			if (num2 >= 0 || (HasOptionalParam(list[i2].GetParameters()) && list[i2].GetParameters().Length > 1))
			{
				list2.Add(list[i2]);
			}
		}
		MethodBase methodBase = list[0];
		bool flag = list[0].GetParameters().Length == 0;
		if (HasOptionalParam(methodBase.GetParameters()))
		{
			ParameterInfo[] parameters = methodBase.GetParameters();
			ParameterInfo parameterInfo = parameters[parameters.Length - 1];
			string typeStr = GetTypeStr(parameterInfo.ParameterType.GetElementType());
			if (parameters.Length > 1)
			{
				string text = GenParamTypes(parameters, true);
				sb.AppendFormat("\t\tif (LuaScriptMgr.CheckTypes(L, 1, {0}) && LuaScriptMgr.CheckParamsType(L, typeof({1}), {2}, {3}))\r\n", text, typeStr, parameters.Length, GetCountStr(parameters.Length - 1));
			}
			else
			{
				sb.AppendFormat("\t\tif (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\r\n", typeStr, parameters.Length, GetCountStr(parameters.Length - 1));
			}
		}
		else
		{
			ParameterInfo[] parameters2 = methodBase.GetParameters();
			if (list.Count == 1 || methodBase.GetParameters().Length != list[1].GetParameters().Length)
			{
				sb.AppendFormat("\t\tif (count == {0})\r\n", parameters2.Length);
			}
			else
			{
				string arg = GenParamTypes(parameters2, true);
				sb.AppendFormat("\t\tif (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {1}))\r\n", parameters2.Length, arg);
			}
		}
		sb.AppendLine("\t\t{");
		int num3 = ProcessParams(methodBase, 3, true, list.Count > 1);
		sb.AppendFormat("\t\t\treturn {0};\r\n", num3);
		sb.AppendLine("\t\t}");
		for (int num4 = 1; num4 < list.Count; num4++)
		{
			flag = list[num4].GetParameters().Length == 0 || flag;
			methodBase = list[num4];
			ParameterInfo[] parameters3 = methodBase.GetParameters();
			if (!HasOptionalParam(methodBase.GetParameters()))
			{
				if (list2.Contains(list[num4]))
				{
					string arg2 = GenParamTypes(parameters3, true);
					sb.AppendFormat("\t\telse if (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {1}))\r\n", parameters3.Length, arg2);
				}
				else
				{
					sb.AppendFormat("\t\telse if (count == {0})\r\n", parameters3.Length);
				}
			}
			else
			{
				ParameterInfo parameterInfo2 = parameters3[parameters3.Length - 1];
				string typeStr2 = GetTypeStr(parameterInfo2.ParameterType.GetElementType());
				if (parameters3.Length > 1)
				{
					string text2 = GenParamTypes(parameters3, true);
					sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckTypes(L, 1, {0}) && LuaScriptMgr.CheckParamsType(L, typeof({1}), {2}, {3}))\r\n", text2, typeStr2, parameters3.Length, GetCountStr(parameters3.Length - 1));
				}
				else
				{
					sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\r\n", typeStr2, parameters3.Length, GetCountStr(parameters3.Length - 1));
				}
			}
			sb.AppendLine("\t\t{");
			num3 = ProcessParams(methodBase, 3, true, true);
			sb.AppendFormat("\t\t\treturn {0};\r\n", num3);
			sb.AppendLine("\t\t}");
		}
		if (type.IsValueType && !flag)
		{
			sb.AppendLine("\t\telse if (count == 0)");
			sb.AppendLine("\t\t{");
			sb.AppendFormat("\t\t\t{0} obj = new {0}();\r\n", className);
			string pushFunction = GetPushFunction(type);
			sb.AppendFormat("\t\t\tLuaScriptMgr.{0}(L, obj);\r\n", pushFunction);
			sb.AppendLine("\t\t\treturn 1;");
			sb.AppendLine("\t\t}");
		}
		sb.AppendLine("\t\telse");
		sb.AppendLine("\t\t{");
		sb.AppendFormat("\t\t\tLuaDLL.luaL_error(L, \"invalid arguments to method: {0}.New\");\r\n", className);
		sb.AppendLine("\t\t}");
		sb.AppendLine();
		sb.AppendLine("\t\treturn 0;");
		sb.AppendLine("\t}");
	}

	private static int GetOptionalParamPos(ParameterInfo[] infos)
	{
		for (int i = 0; i < infos.Length; i++)
		{
			if (IsParams(infos[i]))
			{
				return i;
			}
		}
		return -1;
	}

	private static int Compare(MethodBase lhs, MethodBase rhs)
	{
		int num = ((!lhs.IsStatic) ? 1 : 0);
		int num2 = ((!rhs.IsStatic) ? 1 : 0);
		ParameterInfo[] parameters = lhs.GetParameters();
		ParameterInfo[] parameters2 = rhs.GetParameters();
		int optionalParamPos = GetOptionalParamPos(parameters);
		int optionalParamPos2 = GetOptionalParamPos(parameters2);
		if (optionalParamPos >= 0 && optionalParamPos2 < 0)
		{
			return 1;
		}
		if (optionalParamPos < 0 && optionalParamPos2 >= 0)
		{
			return -1;
		}
		if (optionalParamPos >= 0 && optionalParamPos2 >= 0)
		{
			optionalParamPos += num;
			optionalParamPos2 += num2;
			if (optionalParamPos != optionalParamPos2)
			{
				return (optionalParamPos <= optionalParamPos2) ? 1 : (-1);
			}
			optionalParamPos -= num;
			optionalParamPos2 -= num2;
			if (parameters[optionalParamPos].ParameterType.GetElementType() == typeof(object) && parameters2[optionalParamPos2].ParameterType.GetElementType() != typeof(object))
			{
				return 1;
			}
			if (parameters[optionalParamPos].ParameterType.GetElementType() != typeof(object) && parameters2[optionalParamPos2].ParameterType.GetElementType() == typeof(object))
			{
				return -1;
			}
		}
		int num3 = num + parameters.Length;
		int num4 = num2 + parameters2.Length;
		if (num3 > num4)
		{
			return 1;
		}
		if (num3 == num4)
		{
			List<ParameterInfo> list = new List<ParameterInfo>(parameters);
			List<ParameterInfo> list2 = new List<ParameterInfo>(parameters2);
			if (list.Count > list2.Count)
			{
				if (list[0].ParameterType == typeof(object))
				{
					return 1;
				}
				list.RemoveAt(0);
			}
			else if (list2.Count > list.Count)
			{
				if (list2[0].ParameterType == typeof(object))
				{
					return -1;
				}
				list2.RemoveAt(0);
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].ParameterType == typeof(object) && list2[i].ParameterType != typeof(object))
				{
					return 1;
				}
				if (list[i].ParameterType != typeof(object) && list2[i].ParameterType == typeof(object))
				{
					return -1;
				}
			}
			return 0;
		}
		return -1;
	}

	private static bool HasOptionalParam(ParameterInfo[] infos)
	{
		for (int i = 0; i < infos.Length; i++)
		{
			if (IsParams(infos[i]))
			{
				return true;
			}
		}
		return false;
	}

	private static Type GetRefBaseType(string str)
	{
		int num = str.IndexOf("&");
		string text = ((num < 0) ? str : str.Remove(num));
		Type type = Type.GetType(text);
		if (type == null)
		{
			type = Type.GetType(text + ", UnityEngine");
		}
		if (type == null)
		{
			type = Type.GetType(text + ", Assembly-CSharp-firstpass");
		}
		return type;
	}

	private static int ProcessParams(MethodBase md, int tab, bool beConstruct, bool beLuaString, bool beCheckTypes = false)
	{
		ParameterInfo[] parameters = md.GetParameters();
		int num = parameters.Length;
		string text = string.Empty;
		for (int i = 0; i < tab; i++)
		{
			text += "\t";
		}
		if (!md.IsStatic && !beConstruct)
		{
			if (md.Name == "Equals")
			{
				if (!type.IsValueType)
				{
					sb.AppendFormat("{0}{1} obj = LuaScriptMgr.GetVarObject(L, 1) as {1};\r\n", text, className);
				}
				else
				{
					sb.AppendFormat("{0}{1} obj = ({1})LuaScriptMgr.GetVarObject(L, 1);\r\n", text, className);
				}
			}
			else if (className != "Type" && className != "System.Type")
			{
				if (typeof(UnityEngine.Object).IsAssignableFrom(type))
				{
					sb.AppendFormat("{0}{1} obj = ({1})LuaScriptMgr.GetUnityObjectSelf(L, 1, \"{1}\");\r\n", text, className);
				}
				else if (typeof(TrackedReference).IsAssignableFrom(type))
				{
					sb.AppendFormat("{0}{1} obj = ({1})LuaScriptMgr.GetTrackedObjectSelf(L, 1, \"{1}\");\r\n", text, className);
				}
				else
				{
					sb.AppendFormat("{0}{1} obj = ({1})LuaScriptMgr.GetNetObjectSelf(L, 1, \"{1}\");\r\n", text, className);
				}
			}
			else
			{
				sb.AppendFormat("{0}{1} obj = LuaScriptMgr.GetTypeObject(L, 1);\r\n", text, className);
			}
		}
		for (int j = 0; j < num; j++)
		{
			ParameterInfo parameterInfo = parameters[j];
			string typeStr = GetTypeStr(parameterInfo.ParameterType);
			string text2 = "arg" + j;
			int num2 = ((md.IsStatic || beConstruct) ? 1 : 2);
			if (parameterInfo.Attributes == ParameterAttributes.Out)
			{
				Type refBaseType = GetRefBaseType(parameterInfo.ParameterType.ToString());
				if (refBaseType.IsValueType)
				{
					sb.AppendFormat("{0}{1} {2};\r\n", text, typeStr, text2);
				}
				else
				{
					sb.AppendFormat("{0}{1} {2} = null;\r\n", text, typeStr, text2);
				}
			}
			else if (parameterInfo.ParameterType == typeof(bool))
			{
				if (beCheckTypes)
				{
					sb.AppendFormat("{2}bool {0} = LuaDLL.lua_toboolean(L, {1});\r\n", text2, j + num2, text);
				}
				else
				{
					sb.AppendFormat("{2}bool {0} = LuaScriptMgr.GetBoolean(L, {1});\r\n", text2, j + num2, text);
				}
			}
			else if (parameterInfo.ParameterType == typeof(string))
			{
				string text3 = ((!beLuaString) ? "GetLuaString" : "GetString");
				sb.AppendFormat("{2}string {0} = LuaScriptMgr.{3}(L, {1});\r\n", text2, j + num2, text, text3);
			}
			else if (parameterInfo.ParameterType.IsPrimitive)
			{
				if (beCheckTypes)
				{
					sb.AppendFormat("{3}{0} {1} = ({0})LuaDLL.lua_tonumber(L, {2});\r\n", typeStr, text2, j + num2, text);
				}
				else
				{
					sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetNumber(L, {2});\r\n", typeStr, text2, j + num2, text);
				}
			}
			else if (parameterInfo.ParameterType == typeof(LuaFunction))
			{
				if (beCheckTypes)
				{
					sb.AppendFormat("{2}LuaFunction {0} = LuaScriptMgr.ToLuaFunction(L, {1});\r\n", text2, j + num2, text);
				}
				else
				{
					sb.AppendFormat("{2}LuaFunction {0} = LuaScriptMgr.GetLuaFunction(L, {1});\r\n", text2, j + num2, text);
				}
			}
			else if (parameterInfo.ParameterType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				sb.AppendFormat("{0}{1} {2} = null;\r\n", text, typeStr, text2);
				sb.AppendFormat("{0}LuaTypes funcType{1} = LuaDLL.lua_type(L, {1});\r\n", text, j + num2);
				sb.AppendLine();
				sb.AppendFormat("{0}if (funcType{1} != LuaTypes.LUA_TFUNCTION)\r\n", text, j + num2);
				sb.AppendLine(text + "{");
				if (beCheckTypes)
				{
					sb.AppendFormat("{3} {1} = ({0})LuaScriptMgr.GetLuaObject(L, {2});\r\n", typeStr, text2, j + num2, text + "\t");
				}
				else
				{
					sb.AppendFormat("{3} {1} = ({0})LuaScriptMgr.GetNetObject(L, {2}, typeof({0}));\r\n", typeStr, text2, j + num2, text + "\t");
				}
				sb.AppendFormat("{0}}}\r\n{0}else\r\n{0}{{\r\n", text);
				sb.AppendFormat("{0}\tLuaFunction func = LuaScriptMgr.GetLuaFunction(L, {1});\r\n", text, j + num2);
				sb.AppendFormat("{0}\t{1} = ", text, text2);
				GenDelegateBody(parameterInfo.ParameterType, text + "\t", true);
				sb.AppendLine(text + "}\r\n");
			}
			else if (parameterInfo.ParameterType == typeof(LuaTable))
			{
				if (beCheckTypes)
				{
					sb.AppendFormat("{2}LuaTable {0} = LuaScriptMgr.ToLuaTable(L, {1});\r\n", text2, j + num2, text);
				}
				else
				{
					sb.AppendFormat("{2}LuaTable {0} = LuaScriptMgr.GetLuaTable(L, {1});\r\n", text2, j + num2, text);
				}
			}
			else if (parameterInfo.ParameterType == typeof(Vector2) || GetRefBaseType(parameterInfo.ParameterType.ToString()) == typeof(Vector2))
			{
				sb.AppendFormat("{2}Vector2 {0} = LuaScriptMgr.GetVector2(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(Vector3) || GetRefBaseType(parameterInfo.ParameterType.ToString()) == typeof(Vector3))
			{
				sb.AppendFormat("{2}Vector3 {0} = LuaScriptMgr.GetVector3(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(Vector4) || GetRefBaseType(parameterInfo.ParameterType.ToString()) == typeof(Vector4))
			{
				sb.AppendFormat("{2}Vector4 {0} = LuaScriptMgr.GetVector4(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(Quaternion) || GetRefBaseType(parameterInfo.ParameterType.ToString()) == typeof(Quaternion))
			{
				sb.AppendFormat("{2}Quaternion {0} = LuaScriptMgr.GetQuaternion(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(Color) || GetRefBaseType(parameterInfo.ParameterType.ToString()) == typeof(Color))
			{
				sb.AppendFormat("{2}Color {0} = LuaScriptMgr.GetColor(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(Ray) || GetRefBaseType(parameterInfo.ParameterType.ToString()) == typeof(Ray))
			{
				sb.AppendFormat("{2}Ray {0} = LuaScriptMgr.GetRay(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(Bounds) || GetRefBaseType(parameterInfo.ParameterType.ToString()) == typeof(Bounds))
			{
				sb.AppendFormat("{2}Bounds {0} = LuaScriptMgr.GetBounds(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(object))
			{
				sb.AppendFormat("{2}object {0} = LuaScriptMgr.GetVarObject(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType == typeof(Type))
			{
				sb.AppendFormat("{0}{1} {2} = LuaScriptMgr.GetTypeObject(L, {3});\r\n", text, typeStr, text2, j + num2);
			}
			else if (parameterInfo.ParameterType == typeof(LuaStringBuffer))
			{
				sb.AppendFormat("{2}LuaStringBuffer {0} = LuaScriptMgr.GetStringBuffer(L, {1});\r\n", text2, j + num2, text);
			}
			else if (parameterInfo.ParameterType.IsArray)
			{
				Type elementType = parameterInfo.ParameterType.GetElementType();
				string typeStr2 = GetTypeStr(elementType);
				string text4 = "GetArrayObject";
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				if (elementType == typeof(bool))
				{
					text4 = "GetArrayBool";
				}
				else if (elementType.IsPrimitive)
				{
					flag = true;
					text4 = "GetArrayNumber";
				}
				else if (elementType == typeof(string))
				{
					flag2 = IsParams(parameterInfo);
					text4 = ((!flag2) ? "GetArrayString" : "GetParamsString");
				}
				else
				{
					flag = true;
					flag2 = IsParams(parameterInfo);
					text4 = ((!flag2) ? "GetArrayObject" : "GetParamsObject");
					if (elementType == typeof(object))
					{
						flag3 = true;
					}
					if (elementType == typeof(UnityEngine.Object))
					{
						ambig |= ObjAmbig.U3dObj;
					}
				}
				if (flag)
				{
					if (flag2)
					{
						if (!flag3)
						{
							sb.AppendFormat("{5}{0}[] objs{2} = LuaScriptMgr.{4}<{0}>(L, {1}, {3});\r\n", typeStr2, j + num2, j, GetCountStr(j + num2 - 1), text4, text);
						}
						else
						{
							sb.AppendFormat("{4}object[] objs{1} = LuaScriptMgr.{3}(L, {0}, {2});\r\n", j + num2, j, GetCountStr(j + num2 - 1), text4, text);
						}
					}
					else
					{
						sb.AppendFormat("{4}{0}[] objs{2} = LuaScriptMgr.{3}<{0}>(L, {1});\r\n", typeStr2, j + num2, j, text4, text);
					}
				}
				else if (flag2)
				{
					sb.AppendFormat("{5}{0}[] objs{2} = LuaScriptMgr.{4}(L, {1}, {3});\r\n", typeStr2, j + num2, j, GetCountStr(j + num2 - 1), text4, text);
				}
				else
				{
					sb.AppendFormat("{5}{0}[] objs{2} = LuaScriptMgr.{4}(L, {1});\r\n", typeStr2, j + num2, j, j + num2 - 1, text4, text);
				}
			}
			else if (md.Name == "op_Equality")
			{
				if (!type.IsValueType)
				{
					sb.AppendFormat("{3}{0} {1} = LuaScriptMgr.GetLuaObject(L, {2}) as {0};\r\n", typeStr, text2, j + num2, text);
				}
				else
				{
					sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetVarObject(L, {2});\r\n", typeStr, text2, j + num2, text);
				}
			}
			else if (beCheckTypes)
			{
				sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetLuaObject(L, {2});\r\n", typeStr, text2, j + num2, text);
			}
			else if (typeof(UnityEngine.Object).IsAssignableFrom(parameterInfo.ParameterType))
			{
				sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetUnityObject(L, {2}, typeof({0}));\r\n", typeStr, text2, j + num2, text);
			}
			else if (typeof(TrackedReference).IsAssignableFrom(parameterInfo.ParameterType))
			{
				sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetTrackedObject(L, {2}, typeof({0}));\r\n", typeStr, text2, j + num2, text);
			}
			else
			{
				sb.AppendFormat("{3}{0} {1} = ({0})LuaScriptMgr.GetNetObject(L, {2}, typeof({0}));\r\n", typeStr, text2, j + num2, text);
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		List<string> list = new List<string>();
		List<Type> list2 = new List<Type>();
		for (int k = 0; k < num - 1; k++)
		{
			ParameterInfo parameterInfo2 = parameters[k];
			if (!parameterInfo2.ParameterType.IsArray)
			{
				if (!parameterInfo2.ParameterType.ToString().Contains("&"))
				{
					stringBuilder.Append("arg");
				}
				else
				{
					if (parameterInfo2.Attributes == ParameterAttributes.Out)
					{
						stringBuilder.Append("out arg");
					}
					else
					{
						stringBuilder.Append("ref arg");
					}
					list.Add("arg" + k);
					list2.Add(GetRefBaseType(parameterInfo2.ParameterType.ToString()));
				}
			}
			else
			{
				stringBuilder.Append("objs");
			}
			stringBuilder.Append(k);
			stringBuilder.Append(",");
		}
		if (num > 0)
		{
			ParameterInfo parameterInfo3 = parameters[num - 1];
			if (!parameterInfo3.ParameterType.IsArray)
			{
				if (!parameterInfo3.ParameterType.ToString().Contains("&"))
				{
					stringBuilder.Append("arg");
				}
				else
				{
					if (parameterInfo3.Attributes == ParameterAttributes.Out)
					{
						stringBuilder.Append("out arg");
					}
					else
					{
						stringBuilder.Append("ref arg");
					}
					list.Add("arg" + (num - 1));
					list2.Add(GetRefBaseType(parameterInfo3.ParameterType.ToString()));
				}
			}
			else
			{
				stringBuilder.Append("objs");
			}
			stringBuilder.Append(num - 1);
		}
		if (beConstruct)
		{
			sb.AppendFormat("{2}{0} obj = new {0}({1});\r\n", className, stringBuilder.ToString(), text);
			string pushFunction = GetPushFunction(type);
			sb.AppendFormat("{0}LuaScriptMgr.{1}(L, obj);\r\n", text, pushFunction);
			for (int l = 0; l < list.Count; l++)
			{
				pushFunction = GetPushFunction(list2[l]);
				sb.AppendFormat("{1}LuaScriptMgr.{2}(L, {0});\r\n", list[l], text, pushFunction);
			}
			return list.Count + 1;
		}
		string text5 = ((!md.IsStatic) ? "obj" : className);
		MethodInfo methodInfo = md as MethodInfo;
		if (methodInfo.ReturnType == typeof(void))
		{
			if (md.Name == "set_Item")
			{
				switch (num)
				{
				case 2:
					sb.AppendFormat("{0}{1}[arg0] = arg1;\r\n", text, text5);
					break;
				case 3:
					sb.AppendFormat("{0}{1}[arg0, arg1] = arg2;\r\n", text, text5);
					break;
				}
			}
			else
			{
				sb.AppendFormat("{3}{0}.{1}({2});\r\n", text5, md.Name, stringBuilder.ToString(), text);
			}
			if (!md.IsStatic && type.IsValueType)
			{
				sb.AppendFormat("{0}LuaScriptMgr.SetValueObject(L, 1, obj);\r\n", text);
			}
		}
		else
		{
			string typeStr3 = GetTypeStr(methodInfo.ReturnType);
			if (md.Name.Contains("op_"))
			{
				CallOpFunction(md.Name, tab, typeStr3);
			}
			else if (md.Name == "get_Item")
			{
				sb.AppendFormat("{4}{3} o = {0}[{2}];\r\n", text5, md.Name, stringBuilder.ToString(), typeStr3, text);
			}
			else if (md.Name == "Equals")
			{
				if (type.IsValueType)
				{
					sb.AppendFormat("{0}bool o = obj.Equals(arg0);\r\n", text);
				}
				else
				{
					sb.AppendFormat("{0}bool o = obj != null ? obj.Equals(arg0) : arg0 == null;\r\n", text);
				}
			}
			else
			{
				sb.AppendFormat("{4}{3} o = {0}.{1}({2});\r\n", text5, md.Name, stringBuilder.ToString(), typeStr3, text);
			}
			string pushFunction2 = GetPushFunction(methodInfo.ReturnType);
			sb.AppendFormat("{0}LuaScriptMgr.{1}(L, o);\r\n", text, pushFunction2);
		}
		for (int m = 0; m < list.Count; m++)
		{
			string pushFunction3 = GetPushFunction(list2[m]);
			sb.AppendFormat("{1}LuaScriptMgr.{2}(L, {0});\r\n", list[m], text, pushFunction3);
		}
		return list.Count;
	}

	private static bool CompareParmsCount(MethodBase l, MethodBase r)
	{
		if (l == r)
		{
			return false;
		}
		int num = ((!l.IsStatic) ? 1 : 0);
		int num2 = ((!r.IsStatic) ? 1 : 0);
		num += l.GetParameters().Length;
		num2 += r.GetParameters().Length;
		return num == num2;
	}

	private static int CompareMethod(MethodBase l, MethodBase r)
	{
		int num = 0;
		if (!CompareParmsCount(l, r))
		{
			return -1;
		}
		ParameterInfo[] parameters = l.GetParameters();
		ParameterInfo[] parameters2 = r.GetParameters();
		List<Type> list = new List<Type>();
		List<Type> list2 = new List<Type>();
		if (!l.IsStatic)
		{
			list.Add(type);
		}
		if (!r.IsStatic)
		{
			list2.Add(type);
		}
		for (int i = 0; i < parameters.Length; i++)
		{
			list.Add(parameters[i].ParameterType);
		}
		for (int j = 0; j < parameters2.Length; j++)
		{
			list2.Add(parameters2[j].ParameterType);
		}
		for (int k = 0; k < list.Count; k++)
		{
			if (!typeSize.ContainsKey(list[k]) || !typeSize.ContainsKey(list2[k]))
			{
				if (list[k] != list2[k])
				{
					return -1;
				}
			}
			else if (list[k].IsPrimitive && list2[k].IsPrimitive && num == 0)
			{
				num = ((typeSize[list[k]] >= typeSize[list2[k]]) ? 1 : 2);
			}
			else if (list[k] != list2[k])
			{
				return -1;
			}
		}
		if (num == 0 && l.IsStatic)
		{
			num = 2;
		}
		return num;
	}

	private static void Push(List<MethodInfo> list, MethodInfo r)
	{
		int num = list.FindIndex((MethodInfo p) => p.Name == r.Name && CompareMethod(p, r) >= 0);
		if (num >= 0)
		{
			if (CompareMethod(list[num], r) == 2)
			{
				list.RemoveAt(num);
				list.Add(r);
			}
		}
		else
		{
			list.Add(r);
		}
	}

	private static bool HasDecimal(ParameterInfo[] pi)
	{
		for (int i = 0; i < pi.Length; i++)
		{
			if (pi[i].ParameterType == typeof(decimal))
			{
				return true;
			}
		}
		return false;
	}

	public static MethodInfo GenOverrideFunc(string name)
	{
		List<MethodInfo> list = new List<MethodInfo>();
		for (int i = 0; i < methods.Length; i++)
		{
			if (methods[i].Name == name && !methods[i].IsGenericMethod && !HasDecimal(methods[i].GetParameters()))
			{
				Push(list, methods[i]);
			}
		}
		if (list.Count == 1)
		{
			return list[0];
		}
		list.Sort(Compare);
		sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
		sb.AppendFormat("\tstatic int {0}(IntPtr L)\r\n", GetFuncName(name));
		sb.AppendLine("\t{");
		sb.AppendLine("\t\tint count = LuaDLL.lua_gettop(L);");
		List<MethodInfo> list2 = new List<MethodInfo>();
		for (int j = 0; j < list.Count; j++)
		{
			int num = list.FindIndex((MethodInfo p) => CompareParmsCount(p, list[j]));
			if (num >= 0 || (HasOptionalParam(list[j].GetParameters()) && list[j].GetParameters().Length > 1))
			{
				list2.Add(list[j]);
			}
		}
		sb.AppendLine();
		MethodInfo methodInfo = list[0];
		int num2 = ((methodInfo.ReturnType != typeof(void)) ? 1 : 0);
		int num3 = ((!methodInfo.IsStatic) ? 1 : 0);
		int num4 = num3 + 1;
		int num5 = methodInfo.GetParameters().Length + num3;
		int num6 = list[1].GetParameters().Length + ((!list[1].IsStatic) ? 1 : 0);
		bool flag = true;
		bool beCheckTypes = true;
		if (HasOptionalParam(methodInfo.GetParameters()))
		{
			ParameterInfo[] parameters = methodInfo.GetParameters();
			ParameterInfo parameterInfo = parameters[parameters.Length - 1];
			string typeStr = GetTypeStr(parameterInfo.ParameterType.GetElementType());
			if (parameters.Length > 1)
			{
				string text = GenParamTypes(parameters, methodInfo.IsStatic);
				sb.AppendFormat("\t\tif (LuaScriptMgr.CheckTypes(L, 1, {1}) && LuaScriptMgr.CheckParamsType(L, typeof({2}), {3}, {4}))\r\n", num4, text, typeStr, parameters.Length + num3, GetCountStr(parameters.Length + num3 - 1));
			}
			else
			{
				sb.AppendFormat("\t\tif (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\r\n", typeStr, parameters.Length + num3, GetCountStr(parameters.Length + num3 - 1));
			}
		}
		else if (num5 != num6)
		{
			sb.AppendFormat("\t\tif (count == {0})\r\n", methodInfo.GetParameters().Length + num3);
			flag = false;
			beCheckTypes = false;
		}
		else
		{
			ParameterInfo[] parameters2 = methodInfo.GetParameters();
			if (parameters2.Length > 0)
			{
				string arg = GenParamTypes(parameters2, methodInfo.IsStatic);
				sb.AppendFormat("\t\tif (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {2}))\r\n", parameters2.Length + num3, num4, arg);
			}
			else
			{
				sb.AppendFormat("\t\tif (count == {0})\r\n", parameters2.Length + num3);
			}
		}
		sb.AppendLine("\t\t{");
		int num7 = ProcessParams(methodInfo, 3, false, list.Count > 1 && flag, beCheckTypes);
		sb.AppendFormat("\t\t\treturn {0};\r\n", num2 + num7);
		sb.AppendLine("\t\t}");
		for (int num8 = 1; num8 < list.Count; num8++)
		{
			flag = true;
			beCheckTypes = true;
			methodInfo = list[num8];
			num3 = ((!methodInfo.IsStatic) ? 1 : 0);
			num4 = num3 + 1;
			num2 = ((methodInfo.ReturnType != typeof(void)) ? 1 : 0);
			if (!HasOptionalParam(methodInfo.GetParameters()))
			{
				ParameterInfo[] parameters3 = methodInfo.GetParameters();
				if (list2.Contains(list[num8]))
				{
					string arg2 = GenParamTypes(parameters3, methodInfo.IsStatic);
					sb.AppendFormat("\t\telse if (count == {0} && LuaScriptMgr.CheckTypes(L, 1, {2}))\r\n", parameters3.Length + num3, num4, arg2);
				}
				else
				{
					sb.AppendFormat("\t\telse if (count == {0})\r\n", parameters3.Length + num3);
					flag = false;
					beCheckTypes = false;
				}
			}
			else
			{
				ParameterInfo[] parameters4 = methodInfo.GetParameters();
				ParameterInfo parameterInfo2 = parameters4[parameters4.Length - 1];
				string typeStr2 = GetTypeStr(parameterInfo2.ParameterType.GetElementType());
				if (parameters4.Length > 1)
				{
					string text2 = GenParamTypes(parameters4, methodInfo.IsStatic);
					sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckTypes(L, 1, {1}) && LuaScriptMgr.CheckParamsType(L, typeof({2}), {3}, {4}))\r\n", num4, text2, typeStr2, parameters4.Length + num3, GetCountStr(parameters4.Length + num3 - 1));
				}
				else
				{
					sb.AppendFormat("\t\telse if (LuaScriptMgr.CheckParamsType(L, typeof({0}), {1}, {2}))\r\n", typeStr2, parameters4.Length + num3, GetCountStr(parameters4.Length + num3 - 1));
				}
			}
			sb.AppendLine("\t\t{");
			num7 = ProcessParams(methodInfo, 3, false, flag, beCheckTypes);
			sb.AppendFormat("\t\t\treturn {0};\r\n", num2 + num7);
			sb.AppendLine("\t\t}");
		}
		sb.AppendLine("\t\telse");
		sb.AppendLine("\t\t{");
		sb.AppendFormat("\t\t\tLuaDLL.luaL_error(L, \"invalid arguments to method: {0}.{1}\");\r\n", className, name);
		sb.AppendLine("\t\t}");
		sb.AppendLine();
		sb.AppendLine("\t\treturn 0;");
		sb.AppendLine("\t}");
		return null;
	}

	private static string[] GetGenericName(Type[] types)
	{
		string[] array = new string[types.Length];
		for (int i = 0; i < types.Length; i++)
		{
			if (types[i].IsGenericType)
			{
				array[i] = GetGenericName(types[i]);
			}
			else
			{
				array[i] = GetTypeStr(types[i]);
			}
		}
		return array;
	}

	private static string GetGenericName(Type t)
	{
		Type[] genericArguments = t.GetGenericArguments();
		string fullName = t.FullName;
		string str = fullName.Substring(0, fullName.IndexOf('`'));
		str = _C(str);
		if (fullName.Contains("+"))
		{
			int num = fullName.IndexOf("+");
			int num2 = fullName.IndexOf("[");
			if (num2 > num)
			{
				string text = fullName.Substring(num + 1, num2 - num - 1);
				return str + "<" + string.Join(",", GetGenericName(genericArguments)) + ">." + text;
			}
			return str + "<" + string.Join(",", GetGenericName(genericArguments)) + ">";
		}
		return str + "<" + string.Join(",", GetGenericName(genericArguments)) + ">";
	}

	public static string GetTypeStr(Type t)
	{
		if (t.IsArray)
		{
			t = t.GetElementType();
			string typeStr = GetTypeStr(t);
			return typeStr + "[]";
		}
		if (t.IsGenericType)
		{
			return GetGenericName(t);
		}
		return _C(t.ToString());
	}

	public static string _C(string str)
	{
		if (str.Length > 1 && str[str.Length - 1] == '&')
		{
			str = str.Remove(str.Length - 1);
		}
		switch (str)
		{
		case "System.Single":
		case "Single":
			return "float";
		case "System.String":
		case "String":
			return "string";
		case "System.Int32":
		case "Int32":
			return "int";
		case "System.Int64":
		case "Int64":
			return "long";
		case "System.SByte":
		case "SByte":
			return "sbyte";
		case "System.Byte":
		case "Byte":
			return "byte";
		case "System.Int16":
		case "Int16":
			return "short";
		case "System.UInt16":
		case "UInt16":
			return "ushort";
		case "System.Char":
		case "Char":
			return "char";
		case "System.UInt32":
		case "UInt32":
			return "uint";
		case "System.UInt64":
		case "UInt64":
			return "ulong";
		case "System.Decimal":
		case "Decimal":
			return "decimal";
		case "System.Double":
		case "Double":
			return "double";
		case "System.Boolean":
		case "Boolean":
			return "bool";
		case "System.Object":
			return "object";
		default:
			if (str.Contains("."))
			{
				int num = str.LastIndexOf('.');
				string text = str.Substring(0, num);
				if (str.Length > 12 && str.Substring(0, 12) == "UnityEngine.")
				{
					switch (text)
					{
					case "UnityEngine":
						usingList.Add("UnityEngine");
						break;
					case "UnityEngine.UI":
						usingList.Add("UnityEngine.UI");
						break;
					case "UnityEngine.EventSystems":
						usingList.Add("UnityEngine.EventSystems");
						break;
					}
					if (str == "UnityEngine.Object")
					{
						ambig |= ObjAmbig.U3dObj;
					}
				}
				else if (str.Length > 7 && str.Substring(0, 7) == "System.")
				{
					switch (text)
					{
					case "System.Collections":
						usingList.Add(text);
						break;
					case "System.Collections.Generic":
						usingList.Add(text);
						break;
					case "System":
						usingList.Add(text);
						break;
					}
					if (str == "System.Object")
					{
						str = "object";
					}
				}
				if (usingList.Contains(text))
				{
					str = str.Substring(num + 1);
				}
			}
			if (str.Contains("+"))
			{
				return str.Replace('+', '.');
			}
			if (str == extendName)
			{
				return GetTypeStr(type);
			}
			return str;
		}
	}

	private static bool IsLuaTableType(Type t)
	{
		if (t.IsArray)
		{
			t = t.GetElementType();
		}
		return t == typeof(Vector3) || t == typeof(Vector2) || t == typeof(Vector4) || t == typeof(Quaternion) || t == typeof(Color) || t == typeof(Ray) || t == typeof(Bounds);
	}

	private static string GetTypeOf(Type t, string sep)
	{
		if (t == null)
		{
			return string.Format("null{0}", sep);
		}
		if (IsLuaTableType(t))
		{
			return string.Format("typeof(LuaTable{1}){0}", sep, (!t.IsArray) ? string.Empty : "[]");
		}
		return string.Format("typeof({0}){1}", GetTypeStr(t), sep);
	}

	private static string GenParamTypes(ParameterInfo[] p, bool isStatic)
	{
		StringBuilder stringBuilder = new StringBuilder();
		List<Type> list = new List<Type>();
		if (!isStatic)
		{
			list.Add(type);
		}
		for (int i = 0; i < p.Length; i++)
		{
			if (!IsParams(p[i]))
			{
				if (p[i].Attributes != ParameterAttributes.Out)
				{
					list.Add(p[i].ParameterType);
				}
				else
				{
					list.Add(null);
				}
			}
		}
		for (int j = 0; j < list.Count - 1; j++)
		{
			stringBuilder.Append(GetTypeOf(list[j], ", "));
		}
		stringBuilder.Append(GetTypeOf(list[list.Count - 1], string.Empty));
		return stringBuilder.ToString();
	}

	private static void CheckObjectNull()
	{
		if (type.IsValueType)
		{
			sb.AppendLine("\t\tif (o == null)");
		}
		else
		{
			sb.AppendLine("\t\tif (obj == null)");
		}
	}

	private static void GenIndexFunc()
	{
		for (int i = 0; i < fields.Length; i++)
		{
			sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
			sb.AppendFormat("\tstatic int get_{0}(IntPtr L)\r\n", fields[i].Name);
			sb.AppendLine("\t{");
			string pushFunction = GetPushFunction(fields[i].FieldType);
			if (fields[i].IsStatic)
			{
				sb.AppendFormat("\t\tLuaScriptMgr.{2}(L, {0}.{1});\r\n", className, fields[i].Name, pushFunction);
			}
			else
			{
				sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\r\n");
				if (!type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
				sb.AppendLine();
				CheckObjectNull();
				sb.AppendLine("\t\t{");
				sb.AppendLine("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);");
				sb.AppendLine();
				sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", fields[i].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t\telse");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", fields[i].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t}");
				sb.AppendLine();
				if (type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
				sb.AppendFormat("\t\tLuaScriptMgr.{1}(L, obj.{0});\r\n", fields[i].Name, pushFunction);
			}
			sb.AppendLine("\t\treturn 1;");
			sb.AppendLine("\t}");
		}
		for (int j = 0; j < props.Length; j++)
		{
			if (!props[j].CanRead)
			{
				continue;
			}
			bool flag = true;
			int num = propList.IndexOf(props[j]);
			if (num >= 0)
			{
				flag = false;
			}
			sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
			sb.AppendFormat("\tstatic int get_{0}(IntPtr L)\r\n", props[j].Name);
			sb.AppendLine("\t{");
			string pushFunction2 = GetPushFunction(props[j].PropertyType);
			if (flag)
			{
				sb.AppendFormat("\t\tLuaScriptMgr.{2}(L, {0}.{1});\r\n", className, props[j].Name, pushFunction2);
			}
			else
			{
				sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\r\n");
				if (!type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
				sb.AppendLine();
				CheckObjectNull();
				sb.AppendLine("\t\t{");
				sb.AppendLine("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);");
				sb.AppendLine();
				sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", props[j].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t\telse");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", props[j].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t}");
				sb.AppendLine();
				if (type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
				sb.AppendFormat("\t\tLuaScriptMgr.{1}(L, obj.{0});\r\n", props[j].Name, pushFunction2);
			}
			sb.AppendLine("\t\treturn 1;");
			sb.AppendLine("\t}");
		}
	}

	private static void GenNewIndexFunc()
	{
		for (int i = 0; i < fields.Length; i++)
		{
			if (fields[i].IsLiteral || fields[i].IsInitOnly || fields[i].IsPrivate)
			{
				continue;
			}
			sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
			sb.AppendFormat("\tstatic int set_{0}(IntPtr L)\r\n", fields[i].Name);
			sb.AppendLine("\t{");
			string o = ((!fields[i].IsStatic) ? "obj" : className);
			if (!fields[i].IsStatic)
			{
				sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\r\n");
				if (!type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
				sb.AppendLine();
				CheckObjectNull();
				sb.AppendLine("\t\t{");
				sb.AppendLine("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);");
				sb.AppendLine();
				sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", fields[i].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t\telse");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", fields[i].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t}");
				sb.AppendLine();
				if (type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
			}
			NewIndexSetValue(fields[i].FieldType, o, fields[i].Name);
			if (!fields[i].IsStatic && type.IsValueType)
			{
				sb.AppendLine("\t\tLuaScriptMgr.SetValueObject(L, 1, obj);");
			}
			sb.AppendLine("\t\treturn 0;");
			sb.AppendLine("\t}");
		}
		for (int j = 0; j < props.Length; j++)
		{
			if (!props[j].CanWrite || !props[j].GetSetMethod(true).IsPublic)
			{
				continue;
			}
			bool flag = true;
			int num = propList.IndexOf(props[j]);
			if (num >= 0)
			{
				flag = false;
			}
			sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
			sb.AppendFormat("\tstatic int set_{0}(IntPtr L)\r\n", props[j].Name);
			sb.AppendLine("\t{");
			string o2 = ((!flag) ? "obj" : className);
			if (!flag)
			{
				sb.AppendFormat("\t\tobject o = LuaScriptMgr.GetLuaObject(L, 1);\r\n");
				if (!type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
				sb.AppendLine();
				CheckObjectNull();
				sb.AppendLine("\t\t{");
				sb.AppendLine("\t\t\tLuaTypes types = LuaDLL.lua_type(L, 1);");
				sb.AppendLine();
				sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", props[j].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t\telse");
				sb.AppendLine("\t\t\t{");
				sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", props[j].Name);
				sb.AppendLine("\t\t\t}");
				sb.AppendLine("\t\t}");
				sb.AppendLine();
				if (type.IsValueType)
				{
					sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
				}
			}
			NewIndexSetValue(props[j].PropertyType, o2, props[j].Name);
			if (!flag && type.IsValueType)
			{
				sb.AppendLine("\t\tLuaScriptMgr.SetValueObject(L, 1, obj);");
			}
			sb.AppendLine("\t\treturn 0;");
			sb.AppendLine("\t}");
		}
	}

	private static void GenDelegateBody(Type t, string head, bool haveState)
	{
		eventSet.Add(t);
		MethodInfo method = t.GetMethod("Invoke");
		ParameterInfo[] parameters = method.GetParameters();
		int num = parameters.Length;
		if (num == 0)
		{
			sb.AppendLine("() =>");
			if (method.ReturnType == typeof(void))
			{
				sb.AppendFormat("{0}{{\r\n{0}\tfunc.Call();\r\n{0}}};\r\n", head);
				return;
			}
			sb.AppendFormat("{0}{{\r\n{0}\tobject[] objs = func.Call();\r\n", head);
			sb.AppendFormat("{1}\treturn ({0})objs[0];\r\n", GetTypeStr(method.ReturnType), head);
			sb.AppendFormat("{0}}};\r\n", head);
			return;
		}
		sb.AppendFormat("(param0");
		for (int i = 1; i < num; i++)
		{
			sb.AppendFormat(", param{0}", i);
		}
		sb.AppendFormat(") =>\r\n{0}{{\r\n{0}", head);
		sb.AppendLine("\tint top = func.BeginPCall();");
		if (!haveState)
		{
			sb.AppendFormat("{0}\tIntPtr L = func.GetLuaState();\r\n", head);
		}
		for (int j = 0; j < num; j++)
		{
			string pushFunction = GetPushFunction(parameters[j].ParameterType);
			sb.AppendFormat("{2}\tLuaScriptMgr.{0}(L, param{1});\r\n", pushFunction, j, head);
		}
		sb.AppendFormat("{1}\tfunc.PCall(top, {0});\r\n", num, head);
		if (method.ReturnType == typeof(void))
		{
			sb.AppendFormat("{0}\tfunc.EndPCall(top);\r\n", head);
		}
		else
		{
			sb.AppendFormat("{0}\tobject[] objs = func.PopValues(top);\r\n", head);
			sb.AppendFormat("{0}\tfunc.EndPCall(top);\r\n", head);
			sb.AppendFormat("{1}\treturn ({0})objs[0];\r\n", GetTypeStr(method.ReturnType), head);
		}
		sb.AppendFormat("{0}}};\r\n", head);
	}

	private static void NewIndexSetValue(Type t, string o, string name)
	{
		if (t.IsArray)
		{
			Type elementType = t.GetElementType();
			string typeStr = GetTypeStr(elementType);
			if (elementType == typeof(bool))
			{
				sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetArrayBool(L, 3);\r\n", o, name);
				return;
			}
			if (elementType.IsPrimitive)
			{
				sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetArrayNumber<{2}>(L, 3);\r\n", o, name, typeStr);
				return;
			}
			if (elementType == typeof(string))
			{
				sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetArrayString(L, 3);\r\n", o, name);
				return;
			}
			if (elementType == typeof(UnityEngine.Object))
			{
				ambig |= ObjAmbig.U3dObj;
			}
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetArrayObject<{2}>(L, 3);\r\n", o, name, typeStr);
		}
		else if (t == typeof(bool))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetBoolean(L, 3);\r\n", o, name);
		}
		else if (t == typeof(string))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetString(L, 3);\r\n", o, name);
		}
		else if (t.IsPrimitive)
		{
			sb.AppendFormat("\t\t{0}.{1} = ({2})LuaScriptMgr.GetNumber(L, 3);\r\n", o, name, _C(t.ToString()));
		}
		else if (t == typeof(LuaFunction))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetLuaFunction(L, 3);\r\n", o, name);
		}
		else if (t == typeof(LuaTable))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetLuaTable(L, 3);\r\n", o, name);
		}
		else if (t == typeof(object))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVarObject(L, 3);\r\n", o, name);
		}
		else if (t == typeof(Vector3))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVector3(L, 3);\r\n", o, name);
		}
		else if (t == typeof(Quaternion))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetQuaternion(L, 3);\r\n", o, name);
		}
		else if (t == typeof(Vector2))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVector2(L, 3);\r\n", o, name);
		}
		else if (t == typeof(Vector4))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetVector4(L, 3);\r\n", o, name);
		}
		else if (t == typeof(Color))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetColor(L, 3);\r\n", o, name);
		}
		else if (t == typeof(Ray))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetRay(L, 3);\r\n", o, name);
		}
		else if (t == typeof(Bounds))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetBounds(L, 3);\r\n", o, name);
		}
		else if (t == typeof(LuaStringBuffer))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetStringBuffer(L, 3);\r\n", o, name);
		}
		else if (typeof(TrackedReference).IsAssignableFrom(t))
		{
			sb.AppendFormat("\t\t{0}.{1} = ({2})LuaScriptMgr.GetTrackedObject(L, 3, typeof(2));\r\n", o, name, GetTypeStr(t));
		}
		else if (typeof(UnityEngine.Object).IsAssignableFrom(t))
		{
			sb.AppendFormat("\t\t{0}.{1} = ({2})LuaScriptMgr.GetUnityObject(L, 3, typeof({2}));\r\n", o, name, GetTypeStr(t));
		}
		else if (typeof(Delegate).IsAssignableFrom(t))
		{
			sb.AppendLine("\t\tLuaTypes funcType = LuaDLL.lua_type(L, 3);\r\n");
			sb.AppendLine("\t\tif (funcType != LuaTypes.LUA_TFUNCTION)");
			sb.AppendLine("\t\t{");
			sb.AppendFormat("\t\t\t{0}.{1} = ({2})LuaScriptMgr.GetNetObject(L, 3, typeof({2}));\r\n", o, name, GetTypeStr(t));
			sb.AppendLine("\t\t}\r\n\t\telse");
			sb.AppendLine("\t\t{");
			sb.AppendLine("\t\t\tLuaFunction func = LuaScriptMgr.ToLuaFunction(L, 3);");
			sb.AppendFormat("\t\t\t{0}.{1} = ", o, name);
			GenDelegateBody(t, "\t\t\t", true);
			sb.AppendLine("\t\t}");
		}
		else if (typeof(object).IsAssignableFrom(t) || t.IsEnum)
		{
			sb.AppendFormat("\t\t{0}.{1} = ({2})LuaScriptMgr.GetNetObject(L, 3, typeof({2}));\r\n", o, name, GetTypeStr(t));
		}
		else if (t == typeof(Type))
		{
			sb.AppendFormat("\t\t{0}.{1} = LuaScriptMgr.GetTypeObject(L, 3);\r\n", o, name);
		}
		else
		{
			Debugger.LogError("not defined type {0}", t);
		}
	}

	private static void GenToStringFunc()
	{
		int num = Array.FindIndex(methods, (MethodInfo p) => p.Name == "ToString");
		if (num >= 0 && !isStaticClass)
		{
			sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
			sb.AppendLine("\tstatic int Lua_ToString(IntPtr L)");
			sb.AppendLine("\t{");
			sb.AppendLine("\t\tobject obj = LuaScriptMgr.GetLuaObject(L, 1);\r\n");
			sb.AppendLine("\t\tif (obj != null)");
			sb.AppendLine("\t\t{");
			sb.AppendLine("\t\t\tLuaScriptMgr.Push(L, obj.ToString());");
			sb.AppendLine("\t\t}");
			sb.AppendLine("\t\telse");
			sb.AppendLine("\t\t{");
			sb.AppendFormat("\t\t\tLuaScriptMgr.Push(L, \"Table: {0}\");\r\n", libClassName);
			sb.AppendLine("\t\t}");
			sb.AppendLine();
			sb.AppendLine("\t\treturn 1;");
			sb.AppendLine("\t}");
		}
	}

	private static bool IsNeedOp(string name)
	{
		switch (name)
		{
		case "op_Addition":
			op |= MetaOp.Add;
			break;
		case "op_Subtraction":
			op |= MetaOp.Sub;
			break;
		case "op_Equality":
			op |= MetaOp.Eq;
			break;
		case "op_Multiply":
			op |= MetaOp.Mul;
			break;
		case "op_Division":
			op |= MetaOp.Div;
			break;
		case "op_UnaryNegation":
			op |= MetaOp.Neg;
			break;
		default:
			return false;
		}
		return true;
	}

	private static void CallOpFunction(string name, int count, string ret)
	{
		string text = string.Empty;
		for (int i = 0; i < count; i++)
		{
			text += "\t";
		}
		switch (name)
		{
		case "op_Addition":
			sb.AppendFormat("{0}{1} o = arg0 + arg1;\r\n", text, ret);
			break;
		case "op_Subtraction":
			sb.AppendFormat("{0}{1} o = arg0 - arg1;\r\n", text, ret);
			break;
		case "op_Equality":
			sb.AppendFormat("{0}bool o = arg0 == arg1;\r\n", text);
			break;
		case "op_Multiply":
			sb.AppendFormat("{0}{1} o = arg0 * arg1;\r\n", text, ret);
			break;
		case "op_Division":
			sb.AppendFormat("{0}{1} o = arg0 / arg1;\r\n", text, ret);
			break;
		case "op_UnaryNegation":
			sb.AppendFormat("{0}{1} o = -arg0;\r\n", text, ret);
			break;
		}
	}

	private static string GetFuncName(string name)
	{
		switch (name)
		{
		case "op_Addition":
			return "Lua_Add";
		case "op_Subtraction":
			return "Lua_Sub";
		case "op_Equality":
			return "Lua_Eq";
		case "op_Multiply":
			return "Lua_Mul";
		case "op_Division":
			return "Lua_Div";
		case "op_UnaryNegation":
			return "Lua_Neg";
		default:
			return name;
		}
	}

	public static bool IsObsolete(MemberInfo mb)
	{
		object[] customAttributes = mb.GetCustomAttributes(true);
		for (int i = 0; i < customAttributes.Length; i++)
		{
			Type type = customAttributes[i].GetType();
			if (type == typeof(ObsoleteAttribute) || type == typeof(NoToLuaAttribute))
			{
				return true;
			}
		}
		if (IsMemberFilter(mb))
		{
			return true;
		}
		return false;
	}

	public static bool HasAttribute(MemberInfo mb, Type atrtype)
	{
		object[] customAttributes = mb.GetCustomAttributes(true);
		for (int i = 0; i < customAttributes.Length; i++)
		{
			Type type = customAttributes[i].GetType();
			if (type == atrtype)
			{
				return true;
			}
		}
		return false;
	}

	private static void GenEnum()
	{
		fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
		List<FieldInfo> list = new List<FieldInfo>(fields);
		for (int num = list.Count - 1; num > 0; num--)
		{
			if (IsObsolete(list[num]))
			{
				list.RemoveAt(num);
			}
		}
		fields = list.ToArray();
		sb.AppendFormat("public class {0}Wrap\r\n", wrapClassName);
		sb.AppendLine("{");
		sb.AppendLine("\tstatic LuaMethod[] enums = new LuaMethod[]");
		sb.AppendLine("\t{");
		for (int i = 0; i < fields.Length; i++)
		{
			sb.AppendFormat("\t\tnew LuaMethod(\"{0}\", Get{0}),\r\n", fields[i].Name);
		}
		sb.AppendFormat("\t\tnew LuaMethod(\"IntToEnum\", IntToEnum),\r\n");
		sb.AppendLine("\t};");
		sb.AppendLine("\r\n\tpublic static void Register(IntPtr L)");
		sb.AppendLine("\t{");
		sb.AppendFormat("\t\tLuaScriptMgr.RegisterLib(L, \"{0}\", typeof({0}), enums);\r\n", libClassName);
		sb.AppendLine("\t}");
		for (int j = 0; j < fields.Length; j++)
		{
			sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
			sb.AppendFormat("\tstatic int Get{0}(IntPtr L)\r\n", fields[j].Name);
			sb.AppendLine("\t{");
			sb.AppendFormat("\t\tLuaScriptMgr.Push(L, {0}.{1});\r\n", className, fields[j].Name);
			sb.AppendLine("\t\treturn 1;");
			sb.AppendLine("\t}");
		}
	}

	private static void GenEnumTranslator()
	{
		sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
		sb.AppendLine("\tstatic int IntToEnum(IntPtr L)");
		sb.AppendLine("\t{");
		sb.AppendLine("\t\tint arg0 = (int)LuaDLL.lua_tonumber(L, 1);");
		sb.AppendFormat("\t\t{0} o = ({0})arg0;\r\n", className);
		sb.AppendLine("\t\tLuaScriptMgr.Push(L, o);");
		sb.AppendLine("\t\treturn 1;");
		sb.AppendLine("\t}");
	}

	public static void GenDelegates(DelegateType[] list)
	{
		usingList.Add("System");
		usingList.Add("System.Collections.Generic");
		for (int i = 0; i < list.Length; i++)
		{
			Type type = list[i].type;
			if (!typeof(Delegate).IsAssignableFrom(type))
			{
				Debug.LogError(type.FullName + " not a delegate type");
				return;
			}
		}
		sb.AppendLine("public static class DelegateFactory");
		sb.AppendLine("{");
		sb.AppendLine("\tdelegate Delegate DelegateValue(LuaFunction func);");
		sb.AppendLine("\tstatic Dictionary<Type, DelegateValue> dict = new Dictionary<Type, DelegateValue>();");
		sb.AppendLine();
		sb.AppendLine("\t[NoToLuaAttribute]");
		sb.AppendLine("\tpublic static void Register(IntPtr L)");
		sb.AppendLine("\t{");
		for (int j = 0; j < list.Length; j++)
		{
			string strType = list[j].strType;
			string name = list[j].name;
			sb.AppendFormat("\t\tdict.Add(typeof({0}), new DelegateValue({1}));\r\n", strType, name);
		}
		sb.AppendLine("\t}\r\n");
		sb.AppendLine("\t[NoToLuaAttribute]");
		sb.AppendLine("\tpublic static Delegate CreateDelegate(Type t, LuaFunction func)");
		sb.AppendLine("\t{");
		sb.AppendLine("\t\tDelegateValue create = null;\r\n");
		sb.AppendLine("\t\tif (!dict.TryGetValue(t, out create))");
		sb.AppendLine("\t\t{");
		sb.AppendLine("\t\t\tDebugger.LogError(\"Delegate {0} not register\", t.FullName);");
		sb.AppendLine("\t\t\treturn null;");
		sb.AppendLine("\t\t}");
		sb.AppendLine("\t\treturn create(func);");
		sb.AppendLine("\t}\r\n");
		for (int k = 0; k < list.Length; k++)
		{
			Type t = list[k].type;
			string strType2 = list[k].strType;
			string name2 = list[k].name;
			sb.AppendFormat("\tpublic static Delegate {0}(LuaFunction func)\r\n", name2);
			sb.AppendLine("\t{");
			sb.AppendFormat("\t\t{0} d = ", strType2);
			GenDelegateBody(t, "\t\t", false);
			sb.AppendLine("\t\treturn d;");
			sb.AppendLine("\t}\r\n");
		}
		sb.AppendLine("\tpublic static void Clear()");
		sb.AppendLine("\t{");
		sb.AppendLine("\t\tdict.Clear();");
		sb.AppendLine("\t}\r\n");
		sb.AppendLine("}");
		SaveFile(AppConst.uLuaPath + "/Source/Base/DelegateFactory.cs");
		Clear();
	}

	private static string[] GetGenericLibName(Type[] types)
	{
		string[] array = new string[types.Length];
		for (int i = 0; i < types.Length; i++)
		{
			Type type = types[i];
			if (type.IsGenericType)
			{
				array[i] = GetGenericLibName(types[i]);
			}
			else if (type.IsArray)
			{
				type = type.GetElementType();
				array[i] = _C(type.ToString()) + "s";
			}
			else
			{
				array[i] = _C(type.ToString());
			}
		}
		return array;
	}

	public static string GetGenericLibName(Type type)
	{
		Type[] genericArguments = type.GetGenericArguments();
		string name = type.Name;
		int num = name.IndexOf('`');
		string str = ((num == -1) ? name : name.Substring(0, num));
		str = _C(str);
		return str + "_" + string.Join("_", GetGenericLibName(genericArguments));
	}

	private static void ProcessExtends(List<MethodInfo> list)
	{
		extendName = "ToLua_" + libClassName.Replace(".", "_");
		extendType = Type.GetType(extendName + ", Assembly-CSharp-Editor");
		if (extendType == null)
		{
			return;
		}
		List<MethodInfo> list2 = new List<MethodInfo>();
		list2.AddRange(extendType.GetMethods(BindingFlags.Instance | binding | BindingFlags.DeclaredOnly));
		for (int i = list2.Count - 1; i >= 0; i--)
		{
			if ((!list2[i].Name.Contains("op_") && !list2[i].Name.Contains("add_") && !list2[i].Name.Contains("remove_")) || IsNeedOp(list2[i].Name))
			{
				list.RemoveAll((MethodInfo md) => md.Name == list2[i].Name);
				if (!IsObsolete(list2[i]))
				{
					list.Add(list2[i]);
				}
			}
		}
	}
}
