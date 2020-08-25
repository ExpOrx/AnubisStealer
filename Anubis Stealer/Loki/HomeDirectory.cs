using System;
using System.IO;

namespace loki
{
	// Token: 0x02000018 RID: 24
	public static class HomeDirectory
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00006BCC File Offset: 0x00004DCC
		public static void Create(string HomeDir, bool Recursive)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(HomeDir);
			if (directoryInfo.Exists)
			{
				FileInfo[] files = directoryInfo.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					files[i].Delete();
				}
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				for (int i = 0; i < directories.Length; i++)
				{
					directories[i].Delete(Recursive);
				}
				directoryInfo.Attributes |= FileAttributes.Hidden;
				return;
			}
			directoryInfo.Create();
			directoryInfo.Refresh();
			directoryInfo.Attributes |= FileAttributes.Hidden;
		}
	}
}
