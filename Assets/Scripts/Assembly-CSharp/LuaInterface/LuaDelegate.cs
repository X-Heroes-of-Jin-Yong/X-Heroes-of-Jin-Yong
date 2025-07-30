using System;

namespace LuaInterface
{
	public class LuaDelegate
	{
		public Type[] returnTypes;

		public LuaFunction function;

		public LuaDelegate()
		{
			function = null;
			returnTypes = null;
		}

		public object callFunction(object[] args, object[] inArgs, int[] outArgs)
		{
			object[] array = function.call(inArgs, returnTypes);
			object result;
			int num;
			if (returnTypes[0] == typeof(void))
			{
				result = null;
				num = 0;
			}
			else
			{
				result = array[0];
				num = 1;
			}
			for (int i = 0; i < outArgs.Length; i++)
			{
				args[outArgs[i]] = array[num];
				num++;
			}
			return result;
		}
	}
}
