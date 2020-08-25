using System;
using System.IO;
using System.Management;
using NoFile;

namespace Reborn
{
	// Token: 0x02000014 RID: 20
	internal static class Identification
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00006274 File Offset: 0x00004474
		public static string GetWindowsVersion()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM CIM_OperatingSystem");
			string text = string.Empty;
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				text = ((ManagementObject)managementBaseObject)["Caption"].ToString();
			}
			if (text.Contains("8"))
			{
				return "Windows 8";
			}
			if (text.Contains("8.1"))
			{
				return "Windows 8.1";
			}
			if (text.Contains("10"))
			{
				return "Windows 10";
			}
			if (text.Contains("XP"))
			{
				return "Windows XP";
			}
			if (text.Contains("7"))
			{
				return "Windows 7";
			}
			if (text.Contains("Server"))
			{
				return "Windows Server";
			}
			return "Unknown";
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006354 File Offset: 0x00004554
		public static string GetId()
		{
			return Identification.DiskId("") + Identification.ProcessorId();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000636C File Offset: 0x0000456C
		private static string DiskId(string diskLetter = "")
		{
			if (diskLetter == string.Empty)
			{
				foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
				{
					if (driveInfo.IsReady)
					{
						diskLetter = driveInfo.RootDirectory.ToString();
						break;
					}
				}
			}
			if (diskLetter.EndsWith(":\\"))
			{
				diskLetter = diskLetter.Substring(0, diskLetter.Length - 2);
			}
			ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + diskLetter + ":\"");
			managementObject.Get();
			string result = managementObject["VolumeSerialNumber"].ToString();
			managementObject.Dispose();
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006408 File Offset: 0x00004608
		private static string ProcessorId()
		{
			string result;
			try
			{
				ManagementObjectCollection instances = new ManagementClass("win32_processor").GetInstances();
				string text = string.Empty;
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						text = enumerator.Current.Properties["processorID"].Value.ToString();
					}
				}
				result = text;
			}
			catch
			{
				result = "-WRONGID-" + Helper.GetRandomString();
			}
			return result;
		}
	}
}
