using System;
using System.Reflection;

namespace LuaInterface
{
	internal class RegisterEventHandler
	{
		private object target;

		private EventInfo eventInfo;

		private EventHandlerContainer pendingEvents;

		public RegisterEventHandler(EventHandlerContainer pendingEvents, object target, EventInfo eventInfo)
		{
			this.target = target;
			this.eventInfo = eventInfo;
			this.pendingEvents = pendingEvents;
		}

		public Delegate Add(LuaFunction function)
		{
			Delegate obj = CodeGeneration.Instance.GetDelegate(eventInfo.EventHandlerType, function);
			eventInfo.AddEventHandler(target, obj);
			pendingEvents.Add(obj, this);
			return obj;
		}

		public void Remove(Delegate handlerDelegate)
		{
			RemovePending(handlerDelegate);
			pendingEvents.Remove(handlerDelegate);
		}

		internal void RemovePending(Delegate handlerDelegate)
		{
			eventInfo.RemoveEventHandler(target, handlerDelegate);
		}
	}
}
