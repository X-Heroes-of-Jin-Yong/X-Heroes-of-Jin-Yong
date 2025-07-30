using System;

namespace LuaInterface
{
	[AttributeUsage(AttributeTargets.Method)]
	public class LuaGlobalAttribute : Attribute
	{
		private string name;

		private string descript;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string Description
		{
			get
			{
				return descript;
			}
			set
			{
				descript = value;
			}
		}
	}
}
