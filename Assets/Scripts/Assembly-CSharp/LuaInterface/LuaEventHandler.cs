namespace LuaInterface
{
	public class LuaEventHandler
	{
		public LuaFunction handler;

		public void handleEvent(object[] args)
		{
			handler.Call(args);
		}
	}
}
