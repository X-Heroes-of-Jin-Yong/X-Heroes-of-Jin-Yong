using LuaInterface;

public struct LuaField
{
	public string name;

	public LuaCSFunction getter;

	public LuaCSFunction setter;

	public LuaField(string str, LuaCSFunction g, LuaCSFunction s)
	{
		name = str;
		getter = g;
		setter = s;
	}
}
