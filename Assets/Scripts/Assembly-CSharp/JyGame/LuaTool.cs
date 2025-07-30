using System.Collections;
using LuaInterface;

namespace JyGame
{
	public static class LuaTool
	{
		public static CommonSettings.VoidCallBack MakeVoidCallBack(LuaFunction fun)
		{
			return delegate
			{
				fun.Call();
			};
		}

		public static CommonSettings.StringCallBack MakeStringCallBack(LuaFunction fun)
		{
			return delegate(string rst)
			{
				fun.call(new object[1] { rst }, null);
			};
		}

		public static CommonSettings.IntCallBack MakeIntCallBack(LuaFunction fun)
		{
			return delegate(int rst)
			{
				fun.call(new object[1] { rst }, null);
			};
		}

		public static CommonSettings.ObjectCallBack MakeObjectCallBack(LuaFunction fun)
		{
			return delegate(object rst)
			{
				fun.call(new object[1] { rst }, null);
			};
		}

		public static string[] MakeStringArray(LuaTable table)
		{
			return table.ToArray<string>();
		}

		public static LuaTable CreateLuaTable()
		{
			return (LuaTable)LuaManager._lua.DoString("return {}")[0];
		}

		public static LuaTable CreateLuaTable(IEnumerable objs)
		{
			LuaTable luaTable = CreateLuaTable();
			int num = 0;
			foreach (object obj in objs)
			{
				luaTable[num.ToString()] = obj;
				num++;
			}
			return luaTable;
		}

		public static LuaTable CreateLuaTable(IList objs)
		{
			LuaTable luaTable = CreateLuaTable();
			int num = 0;
			foreach (object obj in objs)
			{
				luaTable[num.ToString()] = obj;
				num++;
			}
			return luaTable;
		}

		public static LuaTable CreateLuaTable(IDictionary objs)
		{
			LuaTable luaTable = CreateLuaTable();
			foreach (object key in objs.Keys)
			{
				luaTable[key] = objs[key];
			}
			return luaTable;
		}

		public static LuaTable toLuaTable(this IEnumerable objs)
		{
			return CreateLuaTable(objs);
		}

		public static LuaTable toLuaTable(this IList objs)
		{
			return CreateLuaTable(objs);
		}

		public static LuaTable toLuaTable(this IDictionary objs)
		{
			return CreateLuaTable(objs);
		}
	}
}
