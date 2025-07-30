using System;

public class DelegateType
{
	public string name;

	public Type type;

	public string strType = string.Empty;

	public DelegateType(Type t)
	{
		type = t;
		strType = ToLuaExport.GetTypeStr(t);
		if (t.IsGenericType)
		{
			name = ToLuaExport.GetGenericLibName(t);
			return;
		}
		name = ToLuaExport.GetTypeStr(t);
		name = name.Replace(".", "_");
	}

	public DelegateType SetName(string str)
	{
		name = str;
		return this;
	}
}
