using System.IO;
using System.IO.Compression;

namespace ZyGames.Framework.RPC.IO
{
	public static class GzipUtils
	{
		public static byte[] EnCompress(Stream aSourceStream)
		{
			MemoryStream memoryStream = new MemoryStream();
			aSourceStream.Seek(0L, SeekOrigin.Begin);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			try
			{
				using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress))
				{
					byte[] array = new byte[1024];
					int num = 0;
					do
					{
						num = aSourceStream.Read(array, 0, array.Length);
						gZipStream.Write(array, 0, num);
					}
					while (num > 0);
				}
			}
			finally
			{
				memoryStream.Dispose();
			}
			return memoryStream.ToArray();
		}

		public static byte[] EnCompress(byte[] aSourceStream, int index, int count)
		{
			using (MemoryStream aSourceStream2 = new MemoryStream(aSourceStream, index, count))
			{
				return EnCompress(aSourceStream2);
			}
		}

		public static byte[] DeCompress(Stream aSourceStream)
		{
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				GZipStream gZipStream = new GZipStream(aSourceStream, CompressionMode.Decompress);
				try
				{
					byte[] array2 = new byte[1024];
					int num = 0;
					do
					{
						num = gZipStream.Read(array2, 0, array2.Length);
						memoryStream.Write(array2, 0, num);
					}
					while (num > 0);
					gZipStream.Close();
				}
				finally
				{
					gZipStream.Dispose();
				}
				return memoryStream.ToArray();
			}
		}

		public static byte[] DeCompress(byte[] aSourceByte, int index, int count)
		{
			using (MemoryStream aSourceStream = new MemoryStream(aSourceByte, index, count))
			{
				return DeCompress(aSourceStream);
			}
		}
	}
}
