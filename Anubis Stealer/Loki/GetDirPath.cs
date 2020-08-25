using System;
using System.IO;

namespace loki
{
	// Token: 0x02000017 RID: 23
	public static class GetDirPath
	{
		// Token: 0x04000047 RID: 71
		public static readonly string DefaultPath = Environment.GetEnvironmentVariable("Temp");

		// Token: 0x04000048 RID: 72
		public static readonly string User_Name = Path.Combine(GetDirPath.DefaultPath, Environment.UserName);
	}
}
