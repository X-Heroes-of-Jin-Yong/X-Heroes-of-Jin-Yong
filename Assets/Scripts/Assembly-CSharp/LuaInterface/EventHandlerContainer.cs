using System;
using System.Collections.Generic;

namespace LuaInterface
{
	internal class EventHandlerContainer : IDisposable
	{
		private Dictionary<Delegate, RegisterEventHandler> dict = new Dictionary<Delegate, RegisterEventHandler>();

		public void Add(Delegate handler, RegisterEventHandler eventInfo)
		{
			dict.Add(handler, eventInfo);
		}

		public void Remove(Delegate handler)
		{
			bool flag = dict.Remove(handler);
		}

		public void Dispose()
		{
			foreach (KeyValuePair<Delegate, RegisterEventHandler> item in dict)
			{
				item.Value.RemovePending(item.Key);
			}
			dict.Clear();
		}
	}
}
