using System;

namespace LuaInterface
{
	public class LuaScriptException : LuaException
	{
		private bool isNet;

		private readonly string source;

		public bool IsNetException
		{
			get
			{
				return isNet;
			}
			set
			{
				isNet = value;
			}
		}

		public override string Source
		{
			get
			{
				return source;
			}
		}

		public LuaScriptException(string message, string source)
			: base(message)
		{
			this.source = source;
		}

		public LuaScriptException(Exception innerException, string source)
			: base(innerException.Message, innerException)
		{
			this.source = source;
			IsNetException = true;
		}

		public override string ToString()
		{
			return GetType().FullName + ": " + source + Message;
		}
	}
}
