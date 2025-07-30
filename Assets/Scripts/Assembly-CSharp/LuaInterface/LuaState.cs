using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;

namespace LuaInterface
{
	public class LuaState : IDisposable
	{
		public IntPtr L;

		internal LuaCSFunction tracebackFunction;

		internal ObjectTranslator translator;

		internal LuaCSFunction panicCallback;

		internal LuaCSFunction printFunction;

		internal LuaCSFunction loadfileFunction;

		internal LuaCSFunction loaderFunction;

		internal LuaCSFunction dofileFunction;

		internal LuaCSFunction import_wrapFunction;

		public object this[string fullPath]
		{
			get
			{
				object obj = null;
				int newTop = LuaDLL.lua_gettop(L);
				string[] array = fullPath.Split('.');
				LuaDLL.lua_getglobal(L, array[0]);
				obj = translator.getObject(L, -1);
				if (array.Length > 1)
				{
					string[] array2 = new string[array.Length - 1];
					Array.Copy(array, 1, array2, 0, array.Length - 1);
					obj = getObject(array2);
				}
				LuaDLL.lua_settop(L, newTop);
				return obj;
			}
			set
			{
				int newTop = LuaDLL.lua_gettop(L);
				string[] array = fullPath.Split('.');
				if (array.Length == 1)
				{
					translator.push(L, value);
					LuaDLL.lua_setglobal(L, fullPath);
				}
				else
				{
					LuaDLL.lua_rawglobal(L, array[0]);
					if (LuaDLL.lua_type(L, -1) == LuaTypes.LUA_TNIL)
					{
						Debugger.LogError("Table {0} not exists", array[0]);
						LuaDLL.lua_settop(L, newTop);
						return;
					}
					string[] array2 = new string[array.Length - 1];
					Array.Copy(array, 1, array2, 0, array.Length - 1);
					setObject(array2, value);
				}
				LuaDLL.lua_settop(L, newTop);
			}
		}

		public LuaState()
		{
			L = LuaDLL.luaL_newstate();
			LuaDLL.luaL_openlibs(L);
			LuaDLL.lua_pushstring(L, "LUAINTERFACE LOADED");
			LuaDLL.lua_pushboolean(L, true);
			LuaDLL.lua_settable(L, LuaIndexes.LUA_REGISTRYINDEX);
			LuaDLL.lua_newtable(L);
			LuaDLL.lua_setglobal(L, "luanet");
			LuaDLL.lua_pushvalue(L, LuaIndexes.LUA_GLOBALSINDEX);
			LuaDLL.lua_getglobal(L, "luanet");
			LuaDLL.lua_pushstring(L, "getmetatable");
			LuaDLL.lua_getglobal(L, "getmetatable");
			LuaDLL.lua_settable(L, -3);
			LuaDLL.lua_pushstring(L, "rawget");
			LuaDLL.lua_getglobal(L, "rawget");
			LuaDLL.lua_settable(L, -3);
			LuaDLL.lua_pushstring(L, "rawset");
			LuaDLL.lua_getglobal(L, "rawset");
			LuaDLL.lua_settable(L, -3);
			LuaDLL.lua_replace(L, LuaIndexes.LUA_GLOBALSINDEX);
			translator = new ObjectTranslator(this, L);
			LuaDLL.lua_replace(L, LuaIndexes.LUA_GLOBALSINDEX);
			translator.PushTranslator(L);
			panicCallback = LuaStatic.panic;
			LuaDLL.lua_atpanic(L, panicCallback);
			printFunction = LuaStatic.print;
			LuaDLL.lua_pushstdcallcfunction(L, printFunction);
			LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "print");
			loadfileFunction = LuaStatic.loadfile;
			LuaDLL.lua_pushstdcallcfunction(L, loadfileFunction);
			LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "loadfile");
			dofileFunction = LuaStatic.dofile;
			LuaDLL.lua_pushstdcallcfunction(L, dofileFunction);
			LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "dofile");
			import_wrapFunction = LuaStatic.importWrap;
			LuaDLL.lua_pushstdcallcfunction(L, import_wrapFunction);
			LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "import");
			loaderFunction = LuaStatic.loader;
			LuaDLL.lua_pushstdcallcfunction(L, loaderFunction);
			int index = LuaDLL.lua_gettop(L);
			LuaDLL.lua_getfield(L, LuaIndexes.LUA_GLOBALSINDEX, "package");
			LuaDLL.lua_getfield(L, -1, "loaders");
			int num = LuaDLL.lua_gettop(L);
			for (int num2 = LuaDLL.luaL_getn(L, num) + 1; num2 > 1; num2--)
			{
				LuaDLL.lua_rawgeti(L, num, num2 - 1);
				LuaDLL.lua_rawseti(L, num, num2);
			}
			LuaDLL.lua_pushvalue(L, index);
			LuaDLL.lua_rawseti(L, num, 1);
			LuaDLL.lua_settop(L, 0);
			DoString(LuaStatic.init_luanet);
			tracebackFunction = LuaStatic.traceback;
		}

		public void Close()
		{
			if (L != IntPtr.Zero)
			{
				translator.Destroy();
				LuaDLL.lua_close(L);
			}
		}

		public ObjectTranslator GetTranslator()
		{
			return translator;
		}

		internal void ThrowExceptionFromError(int oldTop)
		{
			string text = LuaDLL.lua_tostring(L, -1);
			LuaDLL.lua_settop(L, oldTop);
			if (text == null)
			{
				text = "Unknown Lua Error";
			}
			throw new LuaScriptException(text, string.Empty);
		}

		internal int SetPendingException(Exception e)
		{
			if (e != null)
			{
				translator.throwError(L, e.ToString());
				LuaDLL.lua_pushnil(L);
				return 1;
			}
			return 0;
		}

		public LuaFunction LoadString(string chunk, string name, LuaTable env)
		{
			int oldTop = LuaDLL.lua_gettop(L);
			byte[] bytes = Encoding.UTF8.GetBytes(chunk);
			if (LuaDLL.luaL_loadbuffer(L, bytes, bytes.Length, name) != 0)
			{
				ThrowExceptionFromError(oldTop);
			}
			if (env != null)
			{
				env.push(L);
				LuaDLL.lua_setfenv(L, -2);
			}
			LuaFunction function = translator.getFunction(L, -1);
			translator.popValues(L, oldTop);
			return function;
		}

		public LuaFunction LoadString(string chunk, string name)
		{
			return LoadString(chunk, name, null);
		}

		public LuaFunction LoadFile(string fileName)
		{
			int oldTop = LuaDLL.lua_gettop(L);
			byte[] array = null;
			string path = Util.LuaPath(fileName);
			using (FileStream fileStream = new FileStream(path, FileMode.Open))
			{
				BinaryReader binaryReader = new BinaryReader(fileStream);
				array = binaryReader.ReadBytes((int)fileStream.Length);
				fileStream.Close();
			}
			if (LuaDLL.luaL_loadbuffer(L, array, array.Length, fileName) != 0)
			{
				ThrowExceptionFromError(oldTop);
			}
			LuaFunction function = translator.getFunction(L, -1);
			translator.popValues(L, oldTop);
			return function;
		}

		public object[] DoString(string chunk)
		{
			return DoString(chunk, "chunk", null);
		}

		public object[] DoString(string chunk, string chunkName, LuaTable env)
		{
			int oldTop = LuaDLL.lua_gettop(L);
			byte[] bytes = Encoding.UTF8.GetBytes(chunk);
			if (LuaDLL.luaL_loadbuffer(L, bytes, bytes.Length, chunkName) == 0)
			{
				if (env != null)
				{
					env.push(L);
					LuaDLL.lua_setfenv(L, -2);
				}
				if (LuaDLL.lua_pcall(L, 0, -1, 0) == 0)
				{
					return translator.popValues(L, oldTop);
				}
				ThrowExceptionFromError(oldTop);
			}
			else
			{
				ThrowExceptionFromError(oldTop);
			}
			return null;
		}

		public object[] DoFile(string fileName)
		{
			return DoFile(fileName, null);
		}

		public object[] DoFile(string fileName, LuaTable env)
		{
			LuaDLL.lua_pushstdcallcfunction(L, tracebackFunction);
			int oldTop = LuaDLL.lua_gettop(L);
			byte[] array = LuaStatic.Load(fileName);
			if (array == null)
			{
				if (!fileName.Contains("mobdebug"))
				{
					Debugger.LogError("Loader lua file failed: {0}", fileName);
				}
				LuaDLL.lua_pop(L, 1);
				return null;
			}
			string name = Util.LuaPath(fileName);
			if (LuaDLL.luaL_loadbuffer(L, array, array.Length, name) == 0)
			{
				if (env != null)
				{
					env.push(L);
					LuaDLL.lua_setfenv(L, -2);
				}
				if (LuaDLL.lua_pcall(L, 0, -1, -2) == 0)
				{
					object[] result = translator.popValues(L, oldTop);
					LuaDLL.lua_pop(L, 1);
					return result;
				}
				ThrowExceptionFromError(oldTop);
				LuaDLL.lua_pop(L, 1);
			}
			else
			{
				ThrowExceptionFromError(oldTop);
				LuaDLL.lua_pop(L, 1);
			}
			return null;
		}

		internal object getObject(string[] remainingPath)
		{
			object obj = null;
			for (int i = 0; i < remainingPath.Length; i++)
			{
				LuaDLL.lua_pushstring(L, remainingPath[i]);
				LuaDLL.lua_gettable(L, -2);
				obj = translator.getObject(L, -1);
				if (obj == null)
				{
					break;
				}
			}
			return obj;
		}

		public double GetNumber(string fullPath)
		{
			return (double)this[fullPath];
		}

		public string GetString(string fullPath)
		{
			return (string)this[fullPath];
		}

		public LuaTable GetTable(string fullPath)
		{
			return (LuaTable)this[fullPath];
		}

		public LuaFunction GetFunction(string fullPath)
		{
			object obj = this[fullPath];
			return (!(obj is LuaCSFunction)) ? ((LuaFunction)obj) : new LuaFunction((LuaCSFunction)obj, this);
		}

		public Delegate GetFunction(Type delegateType, string fullPath)
		{
			translator.throwError(L, "function delegates not implemnented");
			return null;
		}

		internal void setObject(string[] remainingPath, object val)
		{
			for (int i = 0; i < remainingPath.Length - 1; i++)
			{
				LuaDLL.lua_pushstring(L, remainingPath[i]);
				LuaDLL.lua_gettable(L, -2);
			}
			LuaDLL.lua_pushstring(L, remainingPath[remainingPath.Length - 1]);
			translator.push(L, val);
			LuaDLL.lua_settable(L, -3);
		}

		public void NewTable(string fullPath)
		{
			string[] array = fullPath.Split('.');
			int newTop = LuaDLL.lua_gettop(L);
			if (array.Length == 1)
			{
				LuaDLL.lua_newtable(L);
				LuaDLL.lua_setglobal(L, fullPath);
			}
			else
			{
				LuaDLL.lua_getglobal(L, array[0]);
				for (int i = 1; i < array.Length - 1; i++)
				{
					LuaDLL.lua_pushstring(L, array[i]);
					LuaDLL.lua_gettable(L, -2);
				}
				LuaDLL.lua_pushstring(L, array[array.Length - 1]);
				LuaDLL.lua_newtable(L);
				LuaDLL.lua_settable(L, -3);
			}
			LuaDLL.lua_settop(L, newTop);
		}

		public LuaTable NewTable()
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_newtable(L);
			LuaTable result = (LuaTable)translator.getObject(L, -1);
			LuaDLL.lua_settop(L, newTop);
			return result;
		}

		public ListDictionary GetTableDict(LuaTable table)
		{
			ListDictionary listDictionary = new ListDictionary();
			int newTop = LuaDLL.lua_gettop(L);
			translator.push(L, table);
			LuaDLL.lua_pushnil(L);
			while (LuaDLL.lua_next(L, -2) != 0)
			{
				listDictionary[translator.getObject(L, -2)] = translator.getObject(L, -1);
				LuaDLL.lua_settop(L, -2);
			}
			LuaDLL.lua_settop(L, newTop);
			return listDictionary;
		}

		internal void dispose(int reference)
		{
			if (L != IntPtr.Zero)
			{
				LuaDLL.lua_unref(L, reference);
			}
		}

		internal object rawGetObject(int reference, string field)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_getref(L, reference);
			LuaDLL.lua_pushstring(L, field);
			LuaDLL.lua_rawget(L, -2);
			object result = translator.getObject(L, -1);
			LuaDLL.lua_settop(L, newTop);
			return result;
		}

		internal object getObject(int reference, string field)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_getref(L, reference);
			object result = getObject(field.Split('.'));
			LuaDLL.lua_settop(L, newTop);
			return result;
		}

		internal object getObject(int reference, object field)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_getref(L, reference);
			translator.push(L, field);
			LuaDLL.lua_gettable(L, -2);
			object result = translator.getObject(L, -1);
			LuaDLL.lua_settop(L, newTop);
			return result;
		}

		internal void setObject(int reference, string field, object val)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_getref(L, reference);
			setObject(field.Split('.'), val);
			LuaDLL.lua_settop(L, newTop);
		}

		internal void setObject(int reference, object field, object val)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_getref(L, reference);
			translator.push(L, field);
			translator.push(L, val);
			LuaDLL.lua_settable(L, -3);
			LuaDLL.lua_settop(L, newTop);
		}

		public LuaFunction RegisterFunction(string path, object target, MethodBase function)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaMethodWrapper luaMethodWrapper = new LuaMethodWrapper(translator, target, function.DeclaringType, function);
			translator.push(L, new LuaCSFunction(luaMethodWrapper.call));
			this[path] = translator.getObject(L, -1);
			LuaFunction function2 = GetFunction(path);
			LuaDLL.lua_settop(L, newTop);
			return function2;
		}

		public LuaFunction CreateFunction(object target, MethodBase function)
		{
			int newTop = LuaDLL.lua_gettop(L);
			LuaMethodWrapper luaMethodWrapper = new LuaMethodWrapper(translator, target, function.DeclaringType, function);
			translator.push(L, new LuaCSFunction(luaMethodWrapper.call));
			object obj = translator.getObject(L, -1);
			LuaFunction result = ((!(obj is LuaCSFunction)) ? ((LuaFunction)obj) : new LuaFunction((LuaCSFunction)obj, this));
			LuaDLL.lua_settop(L, newTop);
			return result;
		}

		internal bool compareRef(int ref1, int ref2)
		{
			if (ref1 == ref2)
			{
				return true;
			}
			int newTop = LuaDLL.lua_gettop(L);
			LuaDLL.lua_getref(L, ref1);
			LuaDLL.lua_getref(L, ref2);
			int num = LuaDLL.lua_equal(L, -1, -2);
			LuaDLL.lua_settop(L, newTop);
			return num != 0;
		}

		internal void pushCSFunction(LuaCSFunction function)
		{
			translator.pushFunction(L, function);
		}

		public void Dispose()
		{
			Dispose(true);
			L = IntPtr.Zero;
			GC.SuppressFinalize(this);
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public virtual void Dispose(bool dispose)
		{
			if (dispose && translator != null)
			{
				translator.pendingEvents.Dispose();
				translator = null;
			}
		}
	}
}
