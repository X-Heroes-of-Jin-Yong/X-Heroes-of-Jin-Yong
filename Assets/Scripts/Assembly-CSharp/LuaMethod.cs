using LuaInterface;

public struct LuaMethod
{
	public string name;

	public LuaCSFunction func;

	public LuaMethod(string str, LuaCSFunction f)
	{
		name = str;
		func = f;
	}
}
