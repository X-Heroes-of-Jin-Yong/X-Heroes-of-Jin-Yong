using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JyGame
{
    /// <summary>
    /// Lua 工具类（已迁移至 HybridCLR）
    /// 原 XLua 依赖已被替换为 C# 原生类型
    /// 保留此文件以维持接口兼容
    /// </summary>
    public static class LuaTool
    {
        public static CommonSettings.VoidCallBack MakeVoidCallBack(System.Action fun)
        {
            return delegate
            {
                fun();
            };
        }

        public static CommonSettings.StringCallBack MakeStringCallBack(System.Action<string> fun)
        {
            return delegate(string rst)
            {
                fun(rst);
            };
        }

        public static CommonSettings.IntCallBack MakeIntCallBack(System.Action<int> fun)
        {
            return delegate(int rst)
            {
                fun(rst);
            };
        }

        public static CommonSettings.ObjectCallBack MakeObjectCallBack(System.Action<object> fun)
        {
            return delegate(object rst)
            {
                fun(rst);
            };
        }

        public static string[] MakeStringArray(IEnumerable<string> values)
        {
            List<string> list = new List<string>();
            foreach (string value in values)
            {
                list.Add(value);
            }
            return list.ToArray();
        }

        /// <summary>创建 LuaTable 的替代方法，返回 Dictionary</summary>
        public static Dictionary<string, object> CreateLuaTable()
        {
            return new Dictionary<string, object>();
        }

        public static Dictionary<string, object> CreateLuaTable(IEnumerable objs)
        {
            var table = new Dictionary<string, object>();
            int num = 0;
            foreach (object obj in objs)
            {
                table[num.ToString()] = obj;
                num++;
            }
            return table;
        }

        public static Dictionary<string, object> CreateLuaTable(IList objs)
        {
            return CreateLuaTable((IEnumerable)objs);
        }

        public static Dictionary<string, object> CreateLuaTable(IDictionary objs)
        {
            var table = new Dictionary<string, object>();
            foreach (object key in objs.Keys)
            {
                table[key.ToString()] = objs[key];
            }
            return table;
        }

        public static Dictionary<string, object> toLuaTable(this IEnumerable objs)
        {
            return CreateLuaTable(objs);
        }

        public static Dictionary<string, object> toLuaTable(this IList objs)
        {
            return CreateLuaTable(objs);
        }

        public static Dictionary<string, object> toLuaTable(this IDictionary objs)
        {
            return CreateLuaTable(objs);
        }
    }
}
