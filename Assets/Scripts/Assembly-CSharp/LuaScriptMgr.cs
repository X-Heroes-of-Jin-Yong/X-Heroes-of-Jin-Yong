using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using JyGame;
using LuaInterface;
using UnityEngine;

public class LuaScriptMgr
{
	public LuaState lua;

	public HashSet<string> fileList;

	private Dictionary<string, LuaBase> dict;

	private LuaFunction updateFunc;

	private LuaFunction lateUpdateFunc;

	private LuaFunction fixedUpdateFunc;

	private LuaFunction levelLoaded;

	private int unpackVec3;

	private int unpackVec2;

	private int unpackVec4;

	private int unpackQuat;

	private int unpackColor;

	private int unpackRay;

	private int unpackBounds;

	private int packVec3;

	private int packVec2;

	private int packVec4;

	private int packQuat;

	private LuaFunction packTouch;

	private int packRay;

	private LuaFunction packRaycastHit;

	private int packColor;

	private int packBounds;

	private int enumMetaRef;

	private int typeMetaRef;

	private int delegateMetaRef;

	private int iterMetaRef;

	private int arrayMetaRef;

	public static LockFreeQueue<LuaRef> refGCList = new LockFreeQueue<LuaRef>(1024);

	private static ObjectTranslator _translator = null;

	private static HashSet<Type> checkBaseType = new HashSet<Type>();

	private static LuaFunction traceback = null;

	private string luaIndex = "        \n        local rawget = rawget\n        local rawset = rawset\n        local getmetatable = getmetatable      \n        local type = type  \n        local function index(obj,name)  \n            local o = obj            \n            local meta = getmetatable(o)            \n            local parent = meta\n            local v = nil\n            \n            while meta~= nil do\n                v = rawget(meta, name)\n                \n                if v~= nil then\n                    if parent ~= meta then rawset(parent, name, v) end\n\n                    local t = type(v)\n\n                    if t == 'function' then                    \n                        return v\n                    else\n                        local func = v[1]\n                \n                        if func ~= nil then\n                            return func(obj)                         \n                        end\n                    end\n                    break\n                end\n                \n                meta = getmetatable(meta)\n            end\n\n           error('unknown member name '..name, 2)\n           return nil\t        \n        end\n        return index";

	private string luaNewIndex = "\n        local rawget = rawget\n        local getmetatable = getmetatable   \n        local rawset = rawset     \n        local function newindex(obj, name, val)            \n            local meta = getmetatable(obj)            \n            local parent = meta\n            local v = nil\n            \n            while meta~= nil do\n                v = rawget(meta, name)\n                \n                if v~= nil then\n                    if parent ~= meta then rawset(parent, name, v) end\n                    local func = v[2]\n                    if func ~= nil then                        \n                        return func(obj, nil, val)                        \n                    end\n                    break\n                end\n                \n                meta = getmetatable(meta)\n            end  \n       \n            error('field or property '..name..' does not exist', 2)\n            return nil\t\t\n        end\n        return newindex";

	private string luaTableCall = "\n        local rawget = rawget\n        local getmetatable = getmetatable     \n\n        local function call(obj, ...)\n            local meta = getmetatable(obj)\n            local fun = rawget(meta, 'New')\n            \n            if fun ~= nil then\n                return fun(...)\n            else\n                error('unknow function __call',2)\n            end\n        end\n\n        return call\n    ";

	private string luaEnumIndex = "\n        local rawget = rawget                \n        local getmetatable = getmetatable         \n\n        local function indexEnum(obj,name)\n            local v = rawget(obj, name)\n            \n            if v ~= nil then\n                return v\n            end\n\n            local meta = getmetatable(obj)  \n            local func = rawget(meta, name)            \n            \n            if func ~= nil then\n                v = func()\n                rawset(obj, name, v)\n                return v\n            else\n                error('field '..name..' does not exist', 2)\n            end\n        end\n\n        return indexEnum\n    ";

	private static Type monoType = typeof(Type).GetType();

	private static Dictionary<Enum, object> enumMap = new Dictionary<Enum, object>();

	public static LuaScriptMgr Instance { get; private set; }

	public LuaScriptMgr()
	{
		if (Util.CheckEnvironment())
		{
			Instance = this;
			LuaStatic.Load = Loader;
			lua = new LuaState();
			_translator = lua.GetTranslator();
			LuaDLL.luaopen_pb(lua.L);
			LuaDLL.luaopen_protobuf_c(lua.L);
			LuaDLL.luaopen_lpeg(lua.L);
			LuaDLL.luaopen_cjson(lua.L);
			LuaDLL.luaopen_cjson_safe(lua.L);
			LuaDLL.luaopen_sproto_core(lua.L);
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				LuaDLL.luaopen_bit(lua.L);
			}
			LuaDLL.luaopen_sproto_core(lua.L);
			LuaDLL.tolua_openlibs(lua.L);
			fileList = new HashSet<string>();
			dict = new Dictionary<string, LuaBase>();
			LuaDLL.lua_pushstring(lua.L, "ToLua_Index");
			LuaDLL.luaL_dostring(lua.L, luaIndex);
			LuaDLL.lua_rawset(lua.L, LuaIndexes.LUA_REGISTRYINDEX);
			LuaDLL.lua_pushstring(lua.L, "ToLua_NewIndex");
			LuaDLL.luaL_dostring(lua.L, luaNewIndex);
			LuaDLL.lua_rawset(lua.L, LuaIndexes.LUA_REGISTRYINDEX);
			LuaDLL.lua_pushstring(lua.L, "ToLua_TableCall");
			LuaDLL.luaL_dostring(lua.L, luaTableCall);
			LuaDLL.lua_rawset(lua.L, LuaIndexes.LUA_REGISTRYINDEX);
			LuaDLL.lua_pushstring(lua.L, "ToLua_EnumIndex");
			LuaDLL.luaL_dostring(lua.L, luaEnumIndex);
			LuaDLL.lua_rawset(lua.L, LuaIndexes.LUA_REGISTRYINDEX);
			Bind();
			LuaDLL.lua_pushnumber(lua.L, 0.0);
			LuaDLL.lua_setglobal(lua.L, "_LuaScriptMgr");
		}
	}

	private void SendGMmsg(params string[] param)
	{
		Debugger.Log("SendGMmsg");
		string text = string.Empty;
		int num = 0;
		foreach (string text2 in param)
		{
			if (num > 0)
			{
				text = text + " " + text2;
				Debugger.Log(text2);
			}
			num++;
		}
		CallLuaFunction("GMMsg", text);
	}

	private void InitLayers(IntPtr L)
	{
		LuaTable luaTable = GetLuaTable("Layer");
		luaTable.push(L);
		for (int i = 0; i < 32; i++)
		{
			string text = LayerMask.LayerToName(i);
			if (text != string.Empty)
			{
				LuaDLL.lua_pushstring(L, text);
				Push(L, i);
				LuaDLL.lua_rawset(L, -3);
			}
		}
		LuaDLL.lua_settop(L, 0);
	}

	private void Bind()
	{
		IntPtr l = lua.L;
		BindArray(l);
		DelegateFactory.Register(l);
		LuaBinder.Bind(l);
	}

	private void BindArray(IntPtr L)
	{
		LuaDLL.luaL_newmetatable(L, "luaNet_array");
		LuaDLL.lua_pushstring(L, "__index");
		LuaDLL.lua_pushstdcallcfunction(L, IndexArray);
		LuaDLL.lua_rawset(L, -3);
		LuaDLL.lua_pushstring(L, "__gc");
		LuaDLL.lua_pushstdcallcfunction(L, __gc);
		LuaDLL.lua_rawset(L, -3);
		LuaDLL.lua_pushstring(L, "__newindex");
		LuaDLL.lua_pushstdcallcfunction(L, NewIndexArray);
		LuaDLL.lua_rawset(L, -3);
		arrayMetaRef = LuaDLL.luaL_ref(lua.L, LuaIndexes.LUA_REGISTRYINDEX);
		LuaDLL.lua_settop(L, 0);
	}

	public IntPtr GetL()
	{
		return lua.L;
	}

	private void PrintLua(params string[] param)
	{
		if (param.Length != 2)
		{
			Debugger.Log("PrintLua [ModuleName]");
			return;
		}
		CallLuaFunction("PrintLua", param[1]);
	}

	public void LuaGC(params string[] param)
	{
		LuaDLL.lua_gc(lua.L, LuaGCOptions.LUA_GCCOLLECT, 0);
	}

	private void LuaMem(params string[] param)
	{
		CallLuaFunction("mem_report");
	}

	public void Start()
	{
		OnBundleLoaded();
		enumMetaRef = GetTypeMetaRef(typeof(Enum));
		typeMetaRef = GetTypeMetaRef(typeof(Type));
		delegateMetaRef = GetTypeMetaRef(typeof(Delegate));
		iterMetaRef = GetTypeMetaRef(typeof(IEnumerator));
		foreach (Type item in checkBaseType)
		{
			Debugger.LogWarning("BaseType {0} not register to lua", item.FullName);
		}
		checkBaseType.Clear();
	}

	private int GetLuaReference(string str)
	{
		LuaFunction luaFunction = GetLuaFunction(str);
		if (luaFunction != null)
		{
			return luaFunction.GetReference();
		}
		return -1;
	}

	private int GetTypeMetaRef(Type t)
	{
		string assemblyQualifiedName = t.AssemblyQualifiedName;
		LuaDLL.luaL_getmetatable(lua.L, assemblyQualifiedName);
		return LuaDLL.luaL_ref(lua.L, LuaIndexes.LUA_REGISTRYINDEX);
	}

	private void OnBundleLoaded()
	{
		DoFile("System.Global");
		InitLayers(lua.L);
		unpackVec3 = GetLuaReference("Vector3.Get");
		unpackVec2 = GetLuaReference("Vector2.Get");
		unpackVec4 = GetLuaReference("Vector4.Get");
		unpackQuat = GetLuaReference("Quaternion.Get");
		unpackColor = GetLuaReference("Color.Get");
		unpackRay = GetLuaReference("Ray.Get");
		unpackBounds = GetLuaReference("Bounds.Get");
		packVec3 = GetLuaReference("Vector3.New");
		packVec2 = GetLuaReference("Vector2.New");
		packVec4 = GetLuaReference("Vector4.New");
		packQuat = GetLuaReference("Quaternion.New");
		packRaycastHit = GetLuaFunction("Raycast.New");
		packColor = GetLuaReference("Color.New");
		packRay = GetLuaReference("Ray.New");
		packTouch = GetLuaFunction("Touch.New");
		packBounds = GetLuaReference("Bounds.New");
		traceback = GetLuaFunction("traceback");
		DoFile("System.Main");
		updateFunc = GetLuaFunction("Update");
		lateUpdateFunc = GetLuaFunction("LateUpdate");
		fixedUpdateFunc = GetLuaFunction("FixedUpdate");
		levelLoaded = GetLuaFunction("OnLevelWasLoaded");
		CallLuaFunction("Main");
	}

	public void OnLevelLoaded(int level)
	{
		levelLoaded.Call(level);
	}

	public void Update()
	{
		if (updateFunc != null)
		{
			int oldTop = updateFunc.BeginPCall();
			IntPtr luaState = updateFunc.GetLuaState();
			Push(luaState, Time.deltaTime);
			Push(luaState, Time.unscaledDeltaTime);
			updateFunc.PCall(oldTop, 2);
			updateFunc.EndPCall(oldTop);
		}
		while (!refGCList.IsEmpty())
		{
			LuaRef luaRef = refGCList.Dequeue();
			LuaDLL.lua_unref(luaRef.L, luaRef.reference);
		}
	}

	public void LateUpate()
	{
		if (lateUpdateFunc != null)
		{
			lateUpdateFunc.Call();
		}
	}

	public void FixedUpdate()
	{
		if (fixedUpdateFunc != null)
		{
			fixedUpdateFunc.Call(Time.fixedDeltaTime);
		}
	}

	private void SafeRelease(ref LuaFunction func)
	{
		if (func != null)
		{
			func.Release();
			func = null;
		}
	}

	private void SafeUnRef(ref int reference)
	{
		if (reference > 0)
		{
			LuaDLL.lua_unref(lua.L, reference);
			reference = -1;
		}
	}

	public void Destroy()
	{
		Instance = null;
		SafeUnRef(ref enumMetaRef);
		SafeUnRef(ref typeMetaRef);
		SafeUnRef(ref delegateMetaRef);
		SafeUnRef(ref iterMetaRef);
		SafeUnRef(ref arrayMetaRef);
		SafeRelease(ref packRaycastHit);
		SafeRelease(ref packTouch);
		SafeRelease(ref updateFunc);
		SafeRelease(ref lateUpdateFunc);
		SafeRelease(ref fixedUpdateFunc);
		LuaDLL.lua_gc(lua.L, LuaGCOptions.LUA_GCCOLLECT, 0);
		foreach (KeyValuePair<string, LuaBase> item in dict)
		{
			item.Value.Dispose();
		}
		dict.Clear();
		fileList.Clear();
		lua.Close();
		lua.Dispose();
		lua = null;
		DelegateFactory.Clear();
		LuaBinder.wrapList.Clear();
		Debugger.Log("Lua module destroy");
	}

	public object[] DoString(string str)
	{
		return lua.DoString(str);
	}

	public object[] DoFile(string fileName)
	{
		if (!fileList.Contains(fileName))
		{
			return lua.DoFile(fileName, null);
		}
		return null;
	}

	public object[] CallLuaFunction(string name, params object[] args)
	{
		LuaBase value = null;
		if (dict.TryGetValue(name, out value))
		{
			LuaFunction luaFunction = value as LuaFunction;
			return luaFunction.Call(args);
		}
		IntPtr l = lua.L;
		LuaFunction luaFunction2 = null;
		int newTop = LuaDLL.lua_gettop(l);
		if (PushLuaFunction(l, name))
		{
			int reference = LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX);
			luaFunction2 = new LuaFunction(reference, lua);
			LuaDLL.lua_settop(l, newTop);
			object[] result = luaFunction2.Call(args);
			luaFunction2.Dispose();
			return result;
		}
		return null;
	}

	public LuaFunction GetLuaFunction(string name)
	{
		LuaBase value = null;
		if (!dict.TryGetValue(name, out value))
		{
			IntPtr l = lua.L;
			int newTop = LuaDLL.lua_gettop(l);
			if (PushLuaFunction(l, name))
			{
				int reference = LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX);
				value = new LuaFunction(reference, lua);
				value.name = name;
				dict.Add(name, value);
			}
			else
			{
				Debugger.LogError("Lua function {0} not exists", name);
			}
			LuaDLL.lua_settop(l, newTop);
		}
		else
		{
			value.AddRef();
		}
		return value as LuaFunction;
	}

	public int GetFunctionRef(string name)
	{
		IntPtr l = lua.L;
		int newTop = LuaDLL.lua_gettop(l);
		int result = -1;
		if (PushLuaFunction(l, name))
		{
			result = LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX);
		}
		else
		{
			Debugger.LogWarning("Lua function {0} not exists", name);
		}
		LuaDLL.lua_settop(l, newTop);
		return result;
	}

	public bool IsFuncExists(string name)
	{
		IntPtr l = lua.L;
		int newTop = LuaDLL.lua_gettop(l);
		if (PushLuaFunction(l, name))
		{
			LuaDLL.lua_settop(l, newTop);
			return true;
		}
		return false;
	}

	public byte[] Loader(string name)
	{
		byte[] array = null;
		fileList.Add(name);
		string path = Util.LuaPath(name);
		return LuaManager.JyGameLuaLoader(path);
	}

	private static bool PushLuaTable(IntPtr L, string fullPath)
	{
		string[] array = fullPath.Split('.');
		int num = LuaDLL.lua_gettop(L);
		LuaDLL.lua_pushstring(L, array[0]);
		LuaDLL.lua_rawget(L, LuaIndexes.LUA_GLOBALSINDEX);
		LuaTypes luaTypes = LuaDLL.lua_type(L, -1);
		if (luaTypes != LuaTypes.LUA_TTABLE)
		{
			LuaDLL.lua_settop(L, num);
			LuaDLL.lua_pushnil(L);
			Debugger.LogError("Push lua table {0} failed", array[0]);
			return false;
		}
		for (int i = 1; i < array.Length; i++)
		{
			LuaDLL.lua_pushstring(L, array[i]);
			LuaDLL.lua_rawget(L, -2);
			luaTypes = LuaDLL.lua_type(L, -1);
			if (luaTypes != LuaTypes.LUA_TTABLE)
			{
				LuaDLL.lua_settop(L, num);
				Debugger.LogError("Push lua table {0} failed", fullPath);
				return false;
			}
		}
		if (array.Length > 1)
		{
			LuaDLL.lua_insert(L, num + 1);
			LuaDLL.lua_settop(L, num + 1);
		}
		return true;
	}

	private static bool PushLuaFunction(IntPtr L, string fullPath)
	{
		int num = LuaDLL.lua_gettop(L);
		int num2 = fullPath.LastIndexOf('.');
		if (num2 > 0)
		{
			string fullPath2 = fullPath.Substring(0, num2);
			if (PushLuaTable(L, fullPath2))
			{
				string str = fullPath.Substring(num2 + 1);
				LuaDLL.lua_pushstring(L, str);
				LuaDLL.lua_rawget(L, -2);
			}
			LuaTypes luaTypes = LuaDLL.lua_type(L, -1);
			if (luaTypes != LuaTypes.LUA_TFUNCTION)
			{
				LuaDLL.lua_settop(L, num);
				return false;
			}
			LuaDLL.lua_insert(L, num + 1);
			LuaDLL.lua_settop(L, num + 1);
		}
		else
		{
			LuaDLL.lua_getglobal(L, fullPath);
			LuaTypes luaTypes2 = LuaDLL.lua_type(L, -1);
			if (luaTypes2 != LuaTypes.LUA_TFUNCTION)
			{
				LuaDLL.lua_settop(L, num);
				return false;
			}
		}
		return true;
	}

	public LuaTable GetLuaTable(string tableName)
	{
		LuaBase value = null;
		if (!dict.TryGetValue(tableName, out value))
		{
			IntPtr l = lua.L;
			int newTop = LuaDLL.lua_gettop(l);
			if (PushLuaTable(l, tableName))
			{
				int reference = LuaDLL.luaL_ref(l, LuaIndexes.LUA_REGISTRYINDEX);
				value = new LuaTable(reference, lua);
				value.name = tableName;
				dict.Add(tableName, value);
			}
			LuaDLL.lua_settop(l, newTop);
		}
		else
		{
			value.AddRef();
		}
		return value as LuaTable;
	}

	public void RemoveLuaRes(string name)
	{
		dict.Remove(name);
	}

	private static void CreateTable(IntPtr L, string fullPath)
	{
		string[] array = fullPath.Split('.');
		int num = LuaDLL.lua_gettop(L);
		if (array.Length > 1)
		{
			LuaDLL.lua_getglobal(L, array[0]);
			if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
			{
				LuaDLL.lua_pop(L, 1);
				LuaDLL.lua_createtable(L, 0, 0);
				LuaDLL.lua_pushstring(L, array[0]);
				LuaDLL.lua_pushvalue(L, -2);
				LuaDLL.lua_settable(L, LuaIndexes.LUA_GLOBALSINDEX);
			}
			for (int i = 1; i < array.Length - 1; i++)
			{
				LuaDLL.lua_pushstring(L, array[i]);
				LuaDLL.lua_rawget(L, -2);
				if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
				{
					LuaDLL.lua_pop(L, 1);
					LuaDLL.lua_createtable(L, 0, 0);
					LuaDLL.lua_pushstring(L, array[i]);
					LuaDLL.lua_pushvalue(L, -2);
					LuaDLL.lua_rawset(L, -4);
				}
			}
			LuaDLL.lua_pushstring(L, array[array.Length - 1]);
			LuaDLL.lua_rawget(L, -2);
			if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
			{
				LuaDLL.lua_pop(L, 1);
				LuaDLL.lua_createtable(L, 0, 0);
				LuaDLL.lua_pushstring(L, array[array.Length - 1]);
				LuaDLL.lua_pushvalue(L, -2);
				LuaDLL.lua_rawset(L, -4);
			}
		}
		else
		{
			LuaDLL.lua_getglobal(L, array[0]);
			if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
			{
				LuaDLL.lua_pop(L, 1);
				LuaDLL.lua_createtable(L, 0, 0);
				LuaDLL.lua_pushstring(L, array[0]);
				LuaDLL.lua_pushvalue(L, -2);
				LuaDLL.lua_settable(L, LuaIndexes.LUA_GLOBALSINDEX);
			}
		}
		LuaDLL.lua_insert(L, num + 1);
		LuaDLL.lua_settop(L, num + 1);
	}

	public static void RegisterLib(IntPtr L, string libName, Type t, LuaMethod[] regs)
	{
		CreateTable(L, libName);
		LuaDLL.luaL_getmetatable(L, t.AssemblyQualifiedName);
		if (LuaDLL.lua_isnil(L, -1))
		{
			LuaDLL.lua_pop(L, 1);
			LuaDLL.luaL_newmetatable(L, t.AssemblyQualifiedName);
		}
		LuaDLL.lua_pushstring(L, "ToLua_EnumIndex");
		LuaDLL.lua_rawget(L, LuaIndexes.LUA_REGISTRYINDEX);
		LuaDLL.lua_setfield(L, -2, "__index");
		LuaDLL.lua_pushstring(L, "__gc");
		LuaDLL.lua_pushstdcallcfunction(L, __gc);
		LuaDLL.lua_rawset(L, -3);
		for (int i = 0; i < regs.Length - 1; i++)
		{
			LuaDLL.lua_pushstring(L, regs[i].name);
			LuaDLL.lua_pushstdcallcfunction(L, regs[i].func);
			LuaDLL.lua_rawset(L, -3);
		}
		int num = regs.Length - 1;
		LuaDLL.lua_pushstring(L, regs[num].name);
		LuaDLL.lua_pushstdcallcfunction(L, regs[num].func);
		LuaDLL.lua_rawset(L, -4);
		LuaDLL.lua_setmetatable(L, -2);
		LuaDLL.lua_settop(L, 0);
	}

	public static void RegisterLib(IntPtr L, string libName, LuaMethod[] regs)
	{
		CreateTable(L, libName);
		for (int i = 0; i < regs.Length; i++)
		{
			LuaDLL.lua_pushstring(L, regs[i].name);
			LuaDLL.lua_pushstdcallcfunction(L, regs[i].func);
			LuaDLL.lua_rawset(L, -3);
		}
		LuaDLL.lua_settop(L, 0);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int __gc(IntPtr luaState)
	{
		int num = LuaDLL.luanet_rawnetobj(luaState, 1);
		if (num != -1)
		{
			ObjectTranslator objectTranslator = ObjectTranslator.FromState(luaState);
			objectTranslator.collectObject(num);
		}
		return 0;
	}

	public static void RegisterLib(IntPtr L, string libName, Type t, LuaMethod[] regs, LuaField[] fields, Type baseType)
	{
		CreateTable(L, libName);
		LuaDLL.luaL_getmetatable(L, t.AssemblyQualifiedName);
		if (LuaDLL.lua_isnil(L, -1))
		{
			LuaDLL.lua_pop(L, 1);
			LuaDLL.luaL_newmetatable(L, t.AssemblyQualifiedName);
		}
		if (baseType != null)
		{
			LuaDLL.luaL_getmetatable(L, baseType.AssemblyQualifiedName);
			if (LuaDLL.lua_isnil(L, -1))
			{
				LuaDLL.lua_pop(L, 1);
				LuaDLL.luaL_newmetatable(L, baseType.AssemblyQualifiedName);
				checkBaseType.Add(baseType);
			}
			else
			{
				checkBaseType.Remove(baseType);
			}
			LuaDLL.lua_setmetatable(L, -2);
		}
		LuaDLL.tolua_setindex(L);
		LuaDLL.tolua_setnewindex(L);
		LuaDLL.lua_pushstring(L, "__call");
		LuaDLL.lua_pushstring(L, "ToLua_TableCall");
		LuaDLL.lua_rawget(L, LuaIndexes.LUA_REGISTRYINDEX);
		LuaDLL.lua_rawset(L, -3);
		LuaDLL.lua_pushstring(L, "__gc");
		LuaDLL.lua_pushstdcallcfunction(L, __gc);
		LuaDLL.lua_rawset(L, -3);
		for (int i = 0; i < regs.Length; i++)
		{
			LuaDLL.lua_pushstring(L, regs[i].name);
			LuaDLL.lua_pushstdcallcfunction(L, regs[i].func);
			LuaDLL.lua_rawset(L, -3);
		}
		for (int j = 0; j < fields.Length; j++)
		{
			LuaDLL.lua_pushstring(L, fields[j].name);
			LuaDLL.lua_createtable(L, 2, 0);
			if (fields[j].getter != null)
			{
				LuaDLL.lua_pushstdcallcfunction(L, fields[j].getter);
				LuaDLL.lua_rawseti(L, -2, 1);
			}
			if (fields[j].setter != null)
			{
				LuaDLL.lua_pushstdcallcfunction(L, fields[j].setter);
				LuaDLL.lua_rawseti(L, -2, 2);
			}
			LuaDLL.lua_rawset(L, -3);
		}
		LuaDLL.lua_setmetatable(L, -2);
		LuaDLL.lua_settop(L, 0);
		checkBaseType.Remove(t);
	}

	public static double GetNumber(IntPtr L, int stackPos)
	{
		if (LuaDLL.lua_isnumber(L, stackPos))
		{
			return LuaDLL.lua_tonumber(L, stackPos);
		}
		LuaDLL.luaL_typerror(L, stackPos, "number");
		return 0.0;
	}

	public static bool GetBoolean(IntPtr L, int stackPos)
	{
		if (LuaDLL.lua_isboolean(L, stackPos))
		{
			return LuaDLL.lua_toboolean(L, stackPos);
		}
		LuaDLL.luaL_typerror(L, stackPos, "boolean");
		return false;
	}

	public static string GetString(IntPtr L, int stackPos)
	{
		string luaString = GetLuaString(L, stackPos);
		if (luaString == null)
		{
			LuaDLL.luaL_typerror(L, stackPos, "string");
		}
		return luaString;
	}

	public static LuaFunction GetFunction(IntPtr L, int stackPos)
	{
		LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
		if (luaTypes != LuaTypes.LUA_TFUNCTION)
		{
			return null;
		}
		LuaDLL.lua_pushvalue(L, stackPos);
		return new LuaFunction(LuaDLL.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX), GetTranslator(L).interpreter);
	}

	public static LuaFunction ToLuaFunction(IntPtr L, int stackPos)
	{
		LuaDLL.lua_pushvalue(L, stackPos);
		return new LuaFunction(LuaDLL.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX), GetTranslator(L).interpreter);
	}

	public static LuaFunction GetLuaFunction(IntPtr L, int stackPos)
	{
		LuaFunction function = GetFunction(L, stackPos);
		if (function == null)
		{
			LuaDLL.luaL_typerror(L, stackPos, "function");
			return null;
		}
		return function;
	}

	private static LuaTable ToLuaTable(IntPtr L, int stackPos)
	{
		LuaDLL.lua_pushvalue(L, stackPos);
		return new LuaTable(LuaDLL.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX), GetTranslator(L).interpreter);
	}

	private static LuaTable GetTable(IntPtr L, int stackPos)
	{
		LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
		if (luaTypes != LuaTypes.LUA_TTABLE)
		{
			return null;
		}
		LuaDLL.lua_pushvalue(L, stackPos);
		return new LuaTable(LuaDLL.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX), GetTranslator(L).interpreter);
	}

	public static LuaTable GetLuaTable(IntPtr L, int stackPos)
	{
		LuaTable table = GetTable(L, stackPos);
		if (table == null)
		{
			LuaDLL.luaL_typerror(L, stackPos, "table");
			return null;
		}
		return table;
	}

	public static object GetLuaObject(IntPtr L, int stackPos)
	{
		return GetTranslator(L).getRawNetObject(L, stackPos);
	}

	public static object GetNetObjectSelf(IntPtr L, int stackPos, string type)
	{
		object rawNetObject = GetTranslator(L).getRawNetObject(L, stackPos);
		if (rawNetObject == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type));
			return null;
		}
		return rawNetObject;
	}

	public static object GetUnityObjectSelf(IntPtr L, int stackPos, string type)
	{
		object rawNetObject = GetTranslator(L).getRawNetObject(L, stackPos);
		UnityEngine.Object obj = (UnityEngine.Object)rawNetObject;
		if (obj == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type));
			return null;
		}
		return rawNetObject;
	}

	public static object GetTrackedObjectSelf(IntPtr L, int stackPos, string type)
	{
		object rawNetObject = GetTranslator(L).getRawNetObject(L, stackPos);
		TrackedReference trackedReference = (TrackedReference)rawNetObject;
		if (trackedReference == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type));
			return null;
		}
		return rawNetObject;
	}

	public static T GetNetObject<T>(IntPtr L, int stackPos)
	{
		return (T)GetNetObject(L, stackPos, typeof(T));
	}

	public static object GetNetObject(IntPtr L, int stackPos, Type type)
	{
		if (LuaDLL.lua_isnil(L, stackPos))
		{
			return null;
		}
		object luaObject = GetLuaObject(L, stackPos);
		if (luaObject == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type.Name));
			return null;
		}
		Type type2 = luaObject.GetType();
		if (type == type2 || type.IsAssignableFrom(type2))
		{
			return luaObject;
		}
		LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got {1}", type.Name, type2.Name));
		return null;
	}

	public static T GetUnityObject<T>(IntPtr L, int stackPos) where T : UnityEngine.Object
	{
		return (T)GetUnityObject(L, stackPos, typeof(T));
	}

	public static UnityEngine.Object GetUnityObject(IntPtr L, int stackPos, Type type)
	{
		if (LuaDLL.lua_isnil(L, stackPos))
		{
			return null;
		}
		object luaObject = GetLuaObject(L, stackPos);
		if (luaObject == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type.Name));
			return null;
		}
		UnityEngine.Object obj = (UnityEngine.Object)luaObject;
		if (obj == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type.Name));
			return null;
		}
		Type type2 = obj.GetType();
		if (type == type2 || type2.IsSubclassOf(type))
		{
			return obj;
		}
		LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got {1}", type.Name, type2.Name));
		return null;
	}

	public static T GetTrackedObject<T>(IntPtr L, int stackPos) where T : TrackedReference
	{
		return (T)GetTrackedObject(L, stackPos, typeof(T));
	}

	public static TrackedReference GetTrackedObject(IntPtr L, int stackPos, Type type)
	{
		if (LuaDLL.lua_isnil(L, stackPos))
		{
			return null;
		}
		object luaObject = GetLuaObject(L, stackPos);
		if (luaObject == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type.Name));
			return null;
		}
		TrackedReference trackedReference = luaObject as TrackedReference;
		if (trackedReference == null)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", type.Name));
			return null;
		}
		Type type2 = luaObject.GetType();
		if (type == type2 || type2.IsSubclassOf(type))
		{
			return trackedReference;
		}
		LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got {1}", type.Name, type2.Name));
		return null;
	}

	public static Type GetTypeObject(IntPtr L, int stackPos)
	{
		object luaObject = GetLuaObject(L, stackPos);
		if (luaObject == null || luaObject.GetType() != monoType)
		{
			LuaDLL.luaL_argerror(L, stackPos, string.Format("Type expected, got {0}", (luaObject != null) ? luaObject.GetType().Name : "nil"));
		}
		return (Type)luaObject;
	}

	public static bool IsClassOf(Type child, Type parent)
	{
		return child == parent || parent.IsAssignableFrom(child);
	}

	private static ObjectTranslator GetTranslator(IntPtr L)
	{
		if (_translator == null)
		{
			return ObjectTranslator.FromState(L);
		}
		return _translator;
	}

	public static void PushVarObject(IntPtr L, object o)
	{
		if (o == null)
		{
			LuaDLL.lua_pushnil(L);
			return;
		}
		Type type = o.GetType();
		if (type.IsValueType)
		{
			if (type == typeof(bool))
			{
				bool value = (bool)o;
				LuaDLL.lua_pushboolean(L, value);
			}
			else if (type.IsEnum)
			{
				Push(L, (Enum)o);
			}
			else if (type.IsPrimitive)
			{
				double number = Convert.ToDouble(o);
				LuaDLL.lua_pushnumber(L, number);
			}
			else if (type == typeof(Vector3))
			{
				Push(L, (Vector3)o);
			}
			else if (type == typeof(Vector2))
			{
				Push(L, (Vector2)o);
			}
			else if (type == typeof(Vector4))
			{
				Push(L, (Vector4)o);
			}
			else if (type == typeof(Quaternion))
			{
				Push(L, (Quaternion)o);
			}
			else if (type == typeof(Color))
			{
				Push(L, (Color)o);
			}
			else if (type == typeof(RaycastHit))
			{
				Push(L, (RaycastHit)o);
			}
			else if (type == typeof(Touch))
			{
				Push(L, (Touch)o);
			}
			else if (type == typeof(Ray))
			{
				Push(L, (Ray)o);
			}
			else
			{
				PushValue(L, o);
			}
		}
		else if (type.IsArray)
		{
			PushArray(L, (Array)o);
		}
		else if (type == typeof(LuaCSFunction))
		{
			GetTranslator(L).pushFunction(L, (LuaCSFunction)o);
		}
		else if (type.IsSubclassOf(typeof(Delegate)))
		{
			Push(L, (Delegate)o);
		}
		else if (IsClassOf(type, typeof(IEnumerator)))
		{
			Push(L, (IEnumerator)o);
		}
		else if (type == typeof(string))
		{
			string str = (string)o;
			LuaDLL.lua_pushstring(L, str);
		}
		else if (type == typeof(LuaStringBuffer))
		{
			LuaStringBuffer luaStringBuffer = (LuaStringBuffer)o;
			LuaDLL.lua_pushlstring(L, luaStringBuffer.buffer, luaStringBuffer.buffer.Length);
		}
		else if (type.IsSubclassOf(typeof(UnityEngine.Object)))
		{
			UnityEngine.Object obj = (UnityEngine.Object)o;
			if (obj == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				PushObject(L, o);
			}
		}
		else if (type == typeof(LuaTable))
		{
			((LuaTable)o).push(L);
		}
		else if (type == typeof(LuaFunction))
		{
			((LuaFunction)o).push(L);
		}
		else if (type == monoType)
		{
			Push(L, (Type)o);
		}
		else if (type.IsSubclassOf(typeof(TrackedReference)))
		{
			TrackedReference trackedReference = (TrackedReference)o;
			if (trackedReference == null)
			{
				LuaDLL.lua_pushnil(L);
			}
			else
			{
				PushObject(L, o);
			}
		}
		else
		{
			PushObject(L, o);
		}
	}

	public static void PushObject(IntPtr L, object o)
	{
		GetTranslator(L).pushObject(L, o, "luaNet_metatable");
	}

	public static void Push(IntPtr L, UnityEngine.Object obj)
	{
		PushObject(L, (!(obj == null)) ? obj : null);
	}

	public static void Push(IntPtr L, TrackedReference obj)
	{
		PushObject(L, (!(obj == null)) ? obj : null);
	}

	private static void PushMetaObject(IntPtr L, ObjectTranslator translator, object o, int metaRef)
	{
		if (o == null)
		{
			LuaDLL.lua_pushnil(L);
			return;
		}
		int weakTableRef = translator.weakTableRef;
		int value = -1;
		if (translator.objectsBackMap.TryGetValue(o, out value))
		{
			if (LuaDLL.tolua_pushudata(L, weakTableRef, value))
			{
				return;
			}
			translator.collectObject(value);
		}
		value = translator.addObject(o, false);
		LuaDLL.tolua_pushnewudata(L, metaRef, weakTableRef, value);
	}

	public static void Push(IntPtr L, Type o)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		ObjectTranslator translator = mgrFromLuaState.lua.translator;
		PushMetaObject(L, translator, o, mgrFromLuaState.typeMetaRef);
	}

	public static void Push(IntPtr L, Delegate o)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		ObjectTranslator translator = mgrFromLuaState.lua.translator;
		PushMetaObject(L, translator, o, mgrFromLuaState.delegateMetaRef);
	}

	public static void Push(IntPtr L, IEnumerator o)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		ObjectTranslator translator = mgrFromLuaState.lua.translator;
		PushMetaObject(L, translator, o, mgrFromLuaState.iterMetaRef);
	}

	public static void PushArray(IntPtr L, Array o)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		ObjectTranslator translator = mgrFromLuaState.lua.translator;
		PushMetaObject(L, translator, o, mgrFromLuaState.arrayMetaRef);
	}

	public static void PushValue(IntPtr L, object obj)
	{
		GetTranslator(L).PushValueResult(L, obj);
	}

	public static void Push(IntPtr L, bool b)
	{
		LuaDLL.lua_pushboolean(L, b);
	}

	public static void Push(IntPtr L, string str)
	{
		LuaDLL.lua_pushstring(L, str);
	}

	public static void Push(IntPtr L, char d)
	{
		LuaDLL.lua_pushinteger(L, d);
	}

	public static void Push(IntPtr L, sbyte d)
	{
		LuaDLL.lua_pushinteger(L, d);
	}

	public static void Push(IntPtr L, byte d)
	{
		LuaDLL.lua_pushinteger(L, d);
	}

	public static void Push(IntPtr L, short d)
	{
		LuaDLL.lua_pushinteger(L, d);
	}

	public static void Push(IntPtr L, ushort d)
	{
		LuaDLL.lua_pushinteger(L, d);
	}

	public static void Push(IntPtr L, int d)
	{
		LuaDLL.lua_pushinteger(L, d);
	}

	public static void Push(IntPtr L, uint d)
	{
		LuaDLL.lua_pushnumber(L, d);
	}

	public static void Push(IntPtr L, long d)
	{
		LuaDLL.lua_pushnumber(L, d);
	}

	public static void Push(IntPtr L, ulong d)
	{
		LuaDLL.lua_pushnumber(L, d);
	}

	public static void Push(IntPtr L, float d)
	{
		LuaDLL.lua_pushnumber(L, d);
	}

	public static void Push(IntPtr L, decimal d)
	{
		LuaDLL.lua_pushnumber(L, (double)d);
	}

	public static void Push(IntPtr L, double d)
	{
		LuaDLL.lua_pushnumber(L, d);
	}

	public static void Push(IntPtr L, IntPtr p)
	{
		LuaDLL.lua_pushlightuserdata(L, p);
	}

	public static void Push(IntPtr L, ILuaGeneratedType o)
	{
		if (o == null)
		{
			LuaDLL.lua_pushnil(L);
			return;
		}
		LuaTable luaTable = o.__luaInterface_getLuaTable();
		luaTable.push(L);
	}

	public static void Push(IntPtr L, LuaTable table)
	{
		if (table == null)
		{
			LuaDLL.lua_pushnil(L);
		}
		else
		{
			table.push(L);
		}
	}

	public static void Push(IntPtr L, LuaFunction func)
	{
		if (func == null)
		{
			LuaDLL.lua_pushnil(L);
		}
		else
		{
			func.push();
		}
	}

	public static void Push(IntPtr L, LuaCSFunction func)
	{
		if (func == null)
		{
			LuaDLL.lua_pushnil(L);
		}
		else
		{
			GetTranslator(L).pushFunction(L, func);
		}
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0)
	{
		return CheckType(L, type0, begin);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2) && CheckType(L, type3, begin + 3);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2) && CheckType(L, type3, begin + 3) && CheckType(L, type4, begin + 4);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2) && CheckType(L, type3, begin + 3) && CheckType(L, type4, begin + 4) && CheckType(L, type5, begin + 5);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2) && CheckType(L, type3, begin + 3) && CheckType(L, type4, begin + 4) && CheckType(L, type5, begin + 5) && CheckType(L, type6, begin + 6);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6, Type type7)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2) && CheckType(L, type3, begin + 3) && CheckType(L, type4, begin + 4) && CheckType(L, type5, begin + 5) && CheckType(L, type6, begin + 6) && CheckType(L, type7, begin + 7);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6, Type type7, Type type8)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2) && CheckType(L, type3, begin + 3) && CheckType(L, type4, begin + 4) && CheckType(L, type5, begin + 5) && CheckType(L, type6, begin + 6) && CheckType(L, type7, begin + 7) && CheckType(L, type8, begin + 8);
	}

	public static bool CheckTypes(IntPtr L, int begin, Type type0, Type type1, Type type2, Type type3, Type type4, Type type5, Type type6, Type type7, Type type8, Type type9)
	{
		return CheckType(L, type0, begin) && CheckType(L, type1, begin + 1) && CheckType(L, type2, begin + 2) && CheckType(L, type3, begin + 3) && CheckType(L, type4, begin + 4) && CheckType(L, type5, begin + 5) && CheckType(L, type6, begin + 6) && CheckType(L, type7, begin + 7) && CheckType(L, type8, begin + 8) && CheckType(L, type9, begin + 9);
	}

	public static bool CheckTypes(IntPtr L, int begin, params Type[] types)
	{
		for (int i = 0; i < types.Length; i++)
		{
			if (!CheckType(L, types[i], i + begin))
			{
				return false;
			}
		}
		return true;
	}

	public static bool CheckParamsType(IntPtr L, Type t, int begin, int count)
	{
		if (t == typeof(object))
		{
			return true;
		}
		for (int i = 0; i < count; i++)
		{
			if (!CheckType(L, t, i + begin))
			{
				return false;
			}
		}
		return true;
	}

	private static bool CheckTableType(IntPtr L, Type t, int stackPos)
	{
		if (t.IsArray)
		{
			return true;
		}
		if (t == typeof(LuaTable))
		{
			return true;
		}
		if (t.IsValueType)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_pushvalue(L, stackPos);
			LuaDLL.lua_pushstring(L, "class");
			LuaDLL.lua_gettable(L, -2);
			string text = LuaDLL.lua_tostring(L, -1);
			LuaDLL.lua_settop(L, newTop);
			switch (text)
			{
			case "Vector3":
				return t == typeof(Vector3);
			case "Vector2":
				return t == typeof(Vector2);
			case "Quaternion":
				return t == typeof(Quaternion);
			case "Color":
				return t == typeof(Color);
			case "Vector4":
				return t == typeof(Vector4);
			case "Ray":
				return t == typeof(Ray);
			}
		}
		return false;
	}

	public static bool CheckType(IntPtr L, Type t, int pos)
	{
		if (t == typeof(object))
		{
			return true;
		}
		LuaTypes luaTypes = LuaDLL.lua_type(L, pos);
		switch (luaTypes)
		{
		case LuaTypes.LUA_TNUMBER:
			return t.IsPrimitive;
		case LuaTypes.LUA_TSTRING:
			return t == typeof(string);
		case LuaTypes.LUA_TUSERDATA:
			return CheckUserData(L, luaTypes, t, pos);
		case LuaTypes.LUA_TBOOLEAN:
			return t == typeof(bool);
		case LuaTypes.LUA_TFUNCTION:
			return t == typeof(LuaFunction);
		case LuaTypes.LUA_TTABLE:
			return CheckTableType(L, t, pos);
		case LuaTypes.LUA_TNIL:
			return t == null;
		default:
			return false;
		}
	}

	private static bool CheckUserData(IntPtr L, LuaTypes luaType, Type t, int pos)
	{
		object luaObject = GetLuaObject(L, pos);
		Type type = luaObject.GetType();
		if (t == type)
		{
			return true;
		}
		if (t == typeof(Type))
		{
			return type == monoType;
		}
		return t.IsAssignableFrom(type);
	}

	public static object[] GetParamsObject(IntPtr L, int stackPos, int count)
	{
		List<object> list = new List<object>();
		object obj = null;
		while (count > 0)
		{
			obj = GetVarObject(L, stackPos);
			stackPos++;
			count--;
			if (obj != null)
			{
				list.Add(obj);
				continue;
			}
			LuaDLL.luaL_argerror(L, stackPos, "object expected, got nil");
			break;
		}
		return list.ToArray();
	}

	public static T[] GetParamsObject<T>(IntPtr L, int stackPos, int count)
	{
		List<T> list = new List<T>();
		T val = default(T);
		while (count > 0)
		{
			object luaObject = GetLuaObject(L, stackPos);
			stackPos++;
			count--;
			if (luaObject != null && luaObject.GetType() == typeof(T))
			{
				val = (T)luaObject;
				list.Add(val);
				continue;
			}
			LuaDLL.luaL_argerror(L, stackPos, string.Format("{0} expected, got nil", typeof(T).Name));
			break;
		}
		return list.ToArray();
	}

	public static T[] GetArrayObject<T>(IntPtr L, int stackPos)
	{
		switch (LuaDLL.lua_type(L, stackPos))
		{
		case LuaTypes.LUA_TTABLE:
		{
			int num = 1;
			T val = default(T);
			List<T> list = new List<T>();
			LuaDLL.lua_pushvalue(L, stackPos);
			Type typeFromHandle = typeof(T);
			while (true)
			{
				LuaDLL.lua_rawgeti(L, -1, num);
				if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
				{
					LuaDLL.lua_pop(L, 1);
					return list.ToArray();
				}
				if (!CheckType(L, typeFromHandle, -1))
				{
					break;
				}
				val = (T)GetVarObject(L, -1);
				list.Add(val);
				LuaDLL.lua_pop(L, 1);
				num++;
			}
			LuaDLL.lua_pop(L, 1);
			break;
		}
		case LuaTypes.LUA_TUSERDATA:
		{
			T[] netObject = GetNetObject<T[]>(L, stackPos);
			if (netObject != null)
			{
				return netObject;
			}
			break;
		}
		case LuaTypes.LUA_TNIL:
			return null;
		}
		LuaDLL.luaL_error(L, string.Format("invalid arguments to method: {0}, pos {1}", GetErrorFunc(1), stackPos));
		return null;
	}

	private static string GetErrorFunc(int skip)
	{
		StackFrame stackFrame = null;
		string empty = string.Empty;
		StackTrace stackTrace = new StackTrace(skip, true);
		int num = 0;
		do
		{
			stackFrame = stackTrace.GetFrame(num++);
			empty = stackFrame.GetFileName();
			empty = Path.GetFileName(empty);
		}
		while (!empty.Contains("Wrap."));
		int num2 = empty.LastIndexOf('\\');
		int num3 = empty.LastIndexOf("Wrap.");
		string arg = empty.Substring(num2 + 1, num3 - num2 - 1);
		return string.Format("{0}.{1}", arg, stackFrame.GetMethod().Name);
	}

	public static string GetLuaString(IntPtr L, int stackPos)
	{
		LuaTypes luaTypes = LuaDLL.lua_type(L, stackPos);
		string result = null;
		switch (luaTypes)
		{
		case LuaTypes.LUA_TSTRING:
			result = LuaDLL.lua_tostring(L, stackPos);
			break;
		case LuaTypes.LUA_TUSERDATA:
		{
			object luaObject = GetLuaObject(L, stackPos);
			if (luaObject == null)
			{
				LuaDLL.luaL_argerror(L, stackPos, "string expected, got nil");
				return string.Empty;
			}
			result = ((luaObject.GetType() != typeof(string)) ? luaObject.ToString() : ((string)luaObject));
			break;
		}
		case LuaTypes.LUA_TNUMBER:
			result = LuaDLL.lua_tonumber(L, stackPos).ToString();
			break;
		case LuaTypes.LUA_TBOOLEAN:
			result = LuaDLL.lua_toboolean(L, stackPos).ToString();
			break;
		case LuaTypes.LUA_TNIL:
			return result;
		default:
			LuaDLL.lua_getglobal(L, "tostring");
			LuaDLL.lua_pushvalue(L, stackPos);
			LuaDLL.lua_call(L, 1, 1);
			result = LuaDLL.lua_tostring(L, -1);
			LuaDLL.lua_pop(L, 1);
			break;
		}
		return result;
	}

	public static string[] GetParamsString(IntPtr L, int stackPos, int count)
	{
		List<string> list = new List<string>();
		string text = null;
		while (count > 0)
		{
			text = GetLuaString(L, stackPos);
			stackPos++;
			count--;
			if (text == null)
			{
				LuaDLL.luaL_argerror(L, stackPos, "string expected, got nil");
				break;
			}
			list.Add(text);
		}
		return list.ToArray();
	}

	public static string[] GetArrayString(IntPtr L, int stackPos)
	{
		switch (LuaDLL.lua_type(L, stackPos))
		{
		case LuaTypes.LUA_TTABLE:
		{
			int num = 1;
			string text = null;
			List<string> list = new List<string>();
			LuaDLL.lua_pushvalue(L, stackPos);
			while (true)
			{
				LuaDLL.lua_rawgeti(L, -1, num);
				if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
				{
					break;
				}
				text = GetLuaString(L, -1);
				list.Add(text);
				LuaDLL.lua_pop(L, 1);
				num++;
			}
			LuaDLL.lua_pop(L, 1);
			return list.ToArray();
		}
		case LuaTypes.LUA_TUSERDATA:
		{
			string[] netObject = GetNetObject<string[]>(L, stackPos);
			if (netObject != null)
			{
				return netObject;
			}
			break;
		}
		case LuaTypes.LUA_TNIL:
			return null;
		}
		LuaDLL.luaL_error(L, string.Format("invalid arguments to method: {0}, pos {1}", GetErrorFunc(1), stackPos));
		return null;
	}

	public static T[] GetArrayNumber<T>(IntPtr L, int stackPos)
	{
		switch (LuaDLL.lua_type(L, stackPos))
		{
		case LuaTypes.LUA_TTABLE:
		{
			int num = 1;
			T val = default(T);
			List<T> list = new List<T>();
			LuaDLL.lua_pushvalue(L, stackPos);
			while (true)
			{
				LuaDLL.lua_rawgeti(L, -1, num);
				switch (LuaDLL.lua_type(L, -1))
				{
				case LuaTypes.LUA_TNIL:
					LuaDLL.lua_pop(L, 1);
					return list.ToArray();
				case LuaTypes.LUA_TNUMBER:
					goto IL_0059;
				}
				break;
				IL_0059:
				val = (T)Convert.ChangeType(LuaDLL.lua_tonumber(L, -1), typeof(T));
				list.Add(val);
				LuaDLL.lua_pop(L, 1);
				num++;
			}
			break;
		}
		case LuaTypes.LUA_TUSERDATA:
		{
			T[] netObject = GetNetObject<T[]>(L, stackPos);
			if (netObject != null)
			{
				return netObject;
			}
			break;
		}
		case LuaTypes.LUA_TNIL:
			return null;
		}
		LuaDLL.luaL_error(L, string.Format("invalid arguments to method: {0}, pos {1}", GetErrorFunc(1), stackPos));
		return null;
	}

	public static bool[] GetArrayBool(IntPtr L, int stackPos)
	{
		switch (LuaDLL.lua_type(L, stackPos))
		{
		case LuaTypes.LUA_TTABLE:
		{
			int num = 1;
			List<bool> list = new List<bool>();
			LuaDLL.lua_pushvalue(L, stackPos);
			while (true)
			{
				LuaDLL.lua_rawgeti(L, -1, num);
				switch (LuaDLL.lua_type(L, -1))
				{
				case LuaTypes.LUA_TNIL:
					LuaDLL.lua_pop(L, 1);
					return list.ToArray();
				case LuaTypes.LUA_TNUMBER:
					goto IL_004e;
				}
				break;
				IL_004e:
				bool item = LuaDLL.lua_toboolean(L, -1);
				list.Add(item);
				LuaDLL.lua_pop(L, 1);
				num++;
			}
			break;
		}
		case LuaTypes.LUA_TUSERDATA:
		{
			bool[] netObject = GetNetObject<bool[]>(L, stackPos);
			if (netObject != null)
			{
				return netObject;
			}
			break;
		}
		case LuaTypes.LUA_TNIL:
			return null;
		}
		LuaDLL.luaL_error(L, string.Format("invalid arguments to method: {0}, pos {1}", GetErrorFunc(1), stackPos));
		return null;
	}

	public static LuaStringBuffer GetStringBuffer(IntPtr L, int stackPos)
	{
		switch (LuaDLL.lua_type(L, stackPos))
		{
		case LuaTypes.LUA_TNIL:
			return null;
		default:
			LuaDLL.luaL_typerror(L, stackPos, "string");
			return null;
		case LuaTypes.LUA_TSTRING:
		{
			int strLen = 0;
			IntPtr source = LuaDLL.lua_tolstring(L, stackPos, out strLen);
			return new LuaStringBuffer(source, strLen);
		}
		}
	}

	public static void SetValueObject(IntPtr L, int pos, object obj)
	{
		GetTranslator(L).SetValueObject(L, pos, obj);
	}

	public static void CheckArgsCount(IntPtr L, int count)
	{
		int num = LuaDLL.lua_gettop(L);
		if (num != count)
		{
			string message = string.Format("no overload for method '{0}' takes '{1}' arguments", GetErrorFunc(1), num);
			LuaDLL.luaL_error(L, message);
		}
	}

	public static object GetVarTable(IntPtr L, int stackPos)
	{
		object obj = null;
		int num = LuaDLL.lua_gettop(L);
		LuaDLL.lua_pushvalue(L, stackPos);
		LuaDLL.lua_pushstring(L, "class");
		LuaDLL.lua_gettable(L, -2);
		if (LuaDLL.lua_isnil(L, -1))
		{
			LuaDLL.lua_settop(L, num);
			LuaDLL.lua_pushvalue(L, stackPos);
			return new LuaTable(LuaDLL.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX), GetTranslator(L).interpreter);
		}
		string text = LuaDLL.lua_tostring(L, -1);
		LuaDLL.lua_settop(L, num);
		stackPos = ((stackPos <= 0) ? (stackPos + num + 1) : stackPos);
		switch (text)
		{
		case "Vector3":
			return GetVector3(L, stackPos);
		case "Vector2":
			return GetVector2(L, stackPos);
		case "Quaternion":
			return GetQuaternion(L, stackPos);
		case "Color":
			return GetColor(L, stackPos);
		case "Vector4":
			return GetVector4(L, stackPos);
		case "Ray":
			return GetRay(L, stackPos);
		case "Bounds":
			return GetBounds(L, stackPos);
		default:
			LuaDLL.lua_pushvalue(L, stackPos);
			return new LuaTable(LuaDLL.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX), GetTranslator(L).interpreter);
		}
	}

	public static object GetVarObject(IntPtr L, int stackPos)
	{
		switch (LuaDLL.lua_type(L, stackPos))
		{
		case LuaTypes.LUA_TNUMBER:
			return LuaDLL.lua_tonumber(L, stackPos);
		case LuaTypes.LUA_TSTRING:
			return LuaDLL.lua_tostring(L, stackPos);
		case LuaTypes.LUA_TUSERDATA:
		{
			int num = LuaDLL.luanet_rawnetobj(L, stackPos);
			if (num != -1)
			{
				object value = null;
				GetTranslator(L).objects.TryGetValue(num, out value);
				return value;
			}
			return null;
		}
		case LuaTypes.LUA_TBOOLEAN:
			return LuaDLL.lua_toboolean(L, stackPos);
		case LuaTypes.LUA_TTABLE:
			return GetVarTable(L, stackPos);
		case LuaTypes.LUA_TFUNCTION:
			LuaDLL.lua_pushvalue(L, stackPos);
			return new LuaFunction(LuaDLL.luaL_ref(L, LuaIndexes.LUA_REGISTRYINDEX), GetTranslator(L).interpreter);
		default:
			return null;
		}
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int IndexArray(IntPtr L)
	{
		Array array = GetLuaObject(L, 1) as Array;
		if (array == null)
		{
			LuaDLL.luaL_error(L, "trying to index an invalid Array reference");
			LuaDLL.lua_pushnil(L);
			return 1;
		}
		switch (LuaDLL.lua_type(L, 2))
		{
		case LuaTypes.LUA_TNUMBER:
		{
			int num = (int)LuaDLL.lua_tonumber(L, 2);
			if (num >= array.Length)
			{
				LuaDLL.luaL_error(L, "array index out of bounds: " + num + " " + array.Length);
				return 0;
			}
			object value = array.GetValue(num);
			if (value == null)
			{
				LuaDLL.luaL_error(L, string.Format("array index {0} is null", num));
				return 0;
			}
			PushVarObject(L, value);
			break;
		}
		case LuaTypes.LUA_TSTRING:
		{
			string luaString = GetLuaString(L, 2);
			if (luaString == "Length")
			{
				Push(L, array.Length);
			}
			break;
		}
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int NewIndexArray(IntPtr L)
	{
		Array array = GetLuaObject(L, 1) as Array;
		if (array == null)
		{
			LuaDLL.luaL_error(L, "trying to index and invalid object reference");
			return 0;
		}
		int index = (int)GetNumber(L, 2);
		object varObject = GetVarObject(L, 3);
		Type elementType = array.GetType().GetElementType();
		if (!CheckType(L, elementType, 3))
		{
			LuaDLL.luaL_error(L, "trying to set object type is not correct");
			return 0;
		}
		varObject = Convert.ChangeType(varObject, elementType);
		array.SetValue(varObject, index);
		return 0;
	}

	public static void DumpStack(IntPtr L)
	{
		int num = LuaDLL.lua_gettop(L);
		for (int i = 1; i <= num; i++)
		{
			LuaTypes luaTypes = LuaDLL.lua_type(L, i);
			switch (luaTypes)
			{
			case LuaTypes.LUA_TSTRING:
				Debugger.Log(LuaDLL.lua_tostring(L, i));
				break;
			case LuaTypes.LUA_TBOOLEAN:
				Debugger.Log(LuaDLL.lua_toboolean(L, i).ToString());
				break;
			case LuaTypes.LUA_TNUMBER:
				Debugger.Log(LuaDLL.lua_tonumber(L, i).ToString());
				break;
			default:
				Debugger.Log(luaTypes.ToString());
				break;
			}
		}
	}

	private static object GetEnumObj(Enum e)
	{
		object value = null;
		if (!enumMap.TryGetValue(e, out value))
		{
			value = e;
			enumMap.Add(e, value);
		}
		return value;
	}

	public static void Push(IntPtr L, Enum e)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		ObjectTranslator translator = mgrFromLuaState.lua.translator;
		int weakTableRef = translator.weakTableRef;
		object enumObj = GetEnumObj(e);
		int value = -1;
		if (translator.objectsBackMap.TryGetValue(enumObj, out value))
		{
			if (LuaDLL.tolua_pushudata(L, weakTableRef, value))
			{
				return;
			}
			translator.collectObject(value);
		}
		value = translator.addObject(enumObj, false);
		LuaDLL.tolua_pushnewudata(L, mgrFromLuaState.enumMetaRef, weakTableRef, value);
	}

	public static void Push(IntPtr L, LuaStringBuffer lsb)
	{
		if (lsb != null && lsb.buffer != null)
		{
			LuaDLL.lua_pushlstring(L, lsb.buffer, lsb.buffer.Length);
		}
		else
		{
			LuaDLL.lua_pushnil(L);
		}
	}

	public static LuaScriptMgr GetMgrFromLuaState(IntPtr L)
	{
		return Instance;
	}

	public static void ThrowLuaException(IntPtr L)
	{
		string text = LuaDLL.lua_tostring(L, -1);
		if (text == null)
		{
			text = "Unknown Lua Error";
		}
		throw new LuaScriptException(text.ToString(), string.Empty);
	}

	public static Vector3 GetVector3(IntPtr L, int stackPos)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		LuaDLL.tolua_getfloat3(L, mgrFromLuaState.unpackVec3, stackPos, ref x, ref y, ref z);
		return new Vector3(x, y, z);
	}

	public static void Push(IntPtr L, Vector3 v3)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		LuaDLL.tolua_pushfloat3(L, mgrFromLuaState.packVec3, v3.x, v3.y, v3.z);
	}

	public static void Push(IntPtr L, Quaternion q)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		LuaDLL.tolua_pushfloat4(L, mgrFromLuaState.packQuat, q.x, q.y, q.z, q.w);
	}

	public static Quaternion GetQuaternion(IntPtr L, int stackPos)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float w = 1f;
		LuaDLL.tolua_getfloat4(L, mgrFromLuaState.unpackQuat, stackPos, ref x, ref y, ref z, ref w);
		return new Quaternion(x, y, z, w);
	}

	public static Vector2 GetVector2(IntPtr L, int stackPos)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		float x = 0f;
		float y = 0f;
		LuaDLL.tolua_getfloat2(L, mgrFromLuaState.unpackVec2, stackPos, ref x, ref y);
		return new Vector2(x, y);
	}

	public static void Push(IntPtr L, Vector2 v2)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		LuaDLL.tolua_pushfloat2(L, mgrFromLuaState.packVec2, v2.x, v2.y);
	}

	public static Vector4 GetVector4(IntPtr L, int stackPos)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float w = 0f;
		LuaDLL.tolua_getfloat4(L, mgrFromLuaState.unpackVec4, stackPos, ref x, ref y, ref z, ref w);
		return new Vector4(x, y, z, w);
	}

	public static void Push(IntPtr L, Vector4 v4)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		LuaDLL.tolua_pushfloat4(L, mgrFromLuaState.packVec4, v4.x, v4.y, v4.z, v4.w);
	}

	public static void Push(IntPtr L, RaycastHit hit)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		mgrFromLuaState.packRaycastHit.push(L);
		Push(L, hit.collider);
		Push(L, hit.distance);
		Push(L, hit.normal);
		Push(L, hit.point);
		Push(L, hit.rigidbody);
		Push(L, hit.transform);
		LuaDLL.lua_call(L, 6, -1);
	}

	public static void Push(IntPtr L, Ray ray)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		LuaDLL.lua_getref(L, mgrFromLuaState.packRay);
		LuaDLL.tolua_pushfloat3(L, mgrFromLuaState.packVec3, ray.direction.x, ray.direction.y, ray.direction.z);
		LuaDLL.tolua_pushfloat3(L, mgrFromLuaState.packVec3, ray.origin.x, ray.origin.y, ray.origin.z);
		LuaDLL.lua_call(L, 2, -1);
	}

	public static Ray GetRay(IntPtr L, int stackPos)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float x2 = 0f;
		float y2 = 0f;
		float z2 = 0f;
		LuaDLL.tolua_getfloat6(L, mgrFromLuaState.unpackRay, stackPos, ref x, ref y, ref z, ref x2, ref y2, ref z2);
		Vector3 origin = new Vector3(x, y, z);
		Vector3 direction = new Vector3(x2, y2, z2);
		return new Ray(origin, direction);
	}

	public static Bounds GetBounds(IntPtr L, int stackPos)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float x2 = 0f;
		float y2 = 0f;
		float z2 = 0f;
		LuaDLL.tolua_getfloat6(L, mgrFromLuaState.unpackBounds, stackPos, ref x, ref y, ref z, ref x2, ref y2, ref z2);
		Vector3 center = new Vector3(x, y, z);
		Vector3 size = new Vector3(x2, y2, z2);
		return new Bounds(center, size);
	}

	public static Color GetColor(IntPtr L, int stackPos)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		float x = 0f;
		float y = 0f;
		float z = 0f;
		float w = 0f;
		LuaDLL.tolua_getfloat4(L, mgrFromLuaState.unpackColor, stackPos, ref x, ref y, ref z, ref w);
		return new Color(x, y, z, w);
	}

	public static void Push(IntPtr L, Color clr)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		LuaDLL.tolua_pushfloat4(L, mgrFromLuaState.packColor, clr.r, clr.g, clr.b, clr.a);
	}

	public static void Push(IntPtr L, Touch touch)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		mgrFromLuaState.packTouch.push(L);
		LuaDLL.lua_pushinteger(L, touch.fingerId);
		LuaDLL.tolua_pushfloat2(L, mgrFromLuaState.packVec2, touch.position.x, touch.position.y);
		LuaDLL.tolua_pushfloat2(L, mgrFromLuaState.packVec2, touch.rawPosition.x, touch.rawPosition.y);
		LuaDLL.tolua_pushfloat2(L, mgrFromLuaState.packVec2, touch.deltaPosition.x, touch.deltaPosition.y);
		LuaDLL.lua_pushnumber(L, touch.deltaTime);
		LuaDLL.lua_pushinteger(L, touch.tapCount);
		LuaDLL.lua_pushinteger(L, (int)touch.phase);
		LuaDLL.lua_call(L, 7, -1);
	}

	public static void Push(IntPtr L, Bounds bound)
	{
		LuaScriptMgr mgrFromLuaState = GetMgrFromLuaState(L);
		LuaDLL.lua_getref(L, mgrFromLuaState.packBounds);
		Push(L, bound.center);
		Push(L, bound.size);
		LuaDLL.lua_call(L, 2, -1);
	}

	public static void PushTraceBack(IntPtr L)
	{
		if (traceback == null)
		{
			LuaDLL.lua_getglobal(L, "traceback");
		}
		else
		{
			traceback.push();
		}
	}
}
