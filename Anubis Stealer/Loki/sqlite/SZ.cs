using System;

namespace loki.sqlite
{
	// Token: 0x02000020 RID: 32
	public struct SZ
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000803C File Offset: 0x0000623C
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00008044 File Offset: 0x00006244
		public long Size { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000804D File Offset: 0x0000624D
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00008055 File Offset: 0x00006255
		public long Type { get; set; }
	}
}
