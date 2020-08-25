using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace loki.loki.Utilies.CryptoGrafy
{
	// Token: 0x0200002B RID: 43
	internal class crypt
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00009C54 File Offset: 0x00007E54
		public static string loki_decrypt()
		{
			if (string.IsNullOrEmpty(crypt.password_aes))
			{
				throw new ArgumentNullException("sifreliMetin");
			}
			if (string.IsNullOrEmpty(crypt.password))
			{
				throw new ArgumentNullException("sifre");
			}
			RijndaelManaged rijndaelManaged = null;
			string result = null;
			try
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(crypt.password, crypt._salt);
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(crypt.password_aes)))
				{
					rijndaelManaged = new RijndaelManaged();
					rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
					rijndaelManaged.IV = crypt.ReadByteArray(memoryStream);
					ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			finally
			{
				if (rijndaelManaged != null)
				{
					rijndaelManaged.Clear();
				}
			}
			return result;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009D6C File Offset: 0x00007F6C
		public static string AESDecript(string stringa)
		{
			if (string.IsNullOrEmpty(stringa))
			{
				throw new ArgumentNullException("sifreliMetin");
			}
			if (string.IsNullOrEmpty(crypt.loki_decrypt()))
			{
				throw new ArgumentNullException("sifre");
			}
			RijndaelManaged rijndaelManaged = null;
			string result = null;
			try
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(crypt.loki_decrypt(), crypt._salt);
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(stringa)))
				{
					rijndaelManaged = new RijndaelManaged();
					rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
					rijndaelManaged.IV = crypt.ReadByteArray(memoryStream);
					ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			finally
			{
				if (rijndaelManaged != null)
				{
					rijndaelManaged.Clear();
				}
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009E7C File Offset: 0x0000807C
		private static byte[] ReadByteArray(MemoryStream ms)
		{
			byte[] array = new byte[4];
			if (ms.Read(array, 0, array.Length) != array.Length)
			{
				throw new SystemException("Stream did not contain properly formatted byte array");
			}
			byte[] array2 = new byte[BitConverter.ToInt32(array, 0)];
			if (ms.Read(array2, 0, array2.Length) != array2.Length)
			{
				throw new SystemException("Did not read byte array properly");
			}
			return array2;
		}

		// Token: 0x0400006A RID: 106
		public static string password_aes = "EAAAALZtWlYn5RSRzzQv25kWmX6INGcLlC5iBzugw0VI7IKL + 7wOaADOJ/daOYUHJx8wkw==";

		// Token: 0x0400006B RID: 107
		public static string password = "goisjgpoerkjgokkbjiushgporwagmwibuts0gp[mvkntiusopjfij";

		// Token: 0x0400006C RID: 108
		private static byte[] _salt = Encoding.ASCII.GetBytes("4326443888886662222");
	}
}
