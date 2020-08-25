using System;
using System.IO;
using System.Security.Cryptography;

namespace loki.loki.Utilies
{
	// Token: 0x02000024 RID: 36
	internal class Class1
	{
		// Token: 0x0600015B RID: 347 RVA: 0x000088A8 File Offset: 0x00006AA8
		public static void Main()
		{
			string text = "Here is some data to encrypt!";
			using (Aes aes = Aes.Create())
			{
				string arg = Class1.DecryptStringFromBytes_Aes(Class1.EncryptStringToBytes_Aes(text, aes.Key, aes.IV), aes.Key, aes.IV);
				Console.WriteLine("Original:   {0}", text);
				Console.WriteLine("Round Trip: {0}", arg);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008918 File Offset: 0x00006B18
		private static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
		{
			if (plainText == null || plainText.Length <= 0)
			{
				throw new ArgumentNullException("plainText");
			}
			if (Key == null || Key.Length == 0)
			{
				throw new ArgumentNullException("Key");
			}
			if (IV == null || IV.Length == 0)
			{
				throw new ArgumentNullException("IV");
			}
			byte[] result;
			using (Aes aes = Aes.Create())
			{
				aes.Key = Key;
				aes.IV = IV;
				ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
						{
							streamWriter.Write(plainText);
						}
						result = memoryStream.ToArray();
					}
				}
			}
			return result;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008A14 File Offset: 0x00006C14
		private static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			if (cipherText == null || cipherText.Length == 0)
			{
				throw new ArgumentNullException("cipherText");
			}
			if (Key == null || Key.Length == 0)
			{
				throw new ArgumentNullException("Key");
			}
			if (IV == null || IV.Length == 0)
			{
				throw new ArgumentNullException("IV");
			}
			string result = null;
			using (Aes aes = Aes.Create())
			{
				aes.Key = Key;
				aes.IV = IV;
				ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
				using (MemoryStream memoryStream = new MemoryStream(cipherText))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			return result;
		}
	}
}
