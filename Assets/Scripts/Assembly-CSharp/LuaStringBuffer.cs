using System;
using System.Runtime.InteropServices;

public class LuaStringBuffer
{
	public byte[] buffer;

	public LuaStringBuffer(IntPtr source, int len)
	{
		buffer = new byte[len];
		Marshal.Copy(source, buffer, 0, len);
	}

	public LuaStringBuffer(byte[] buf)
	{
		buffer = buf;
	}
}
