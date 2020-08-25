using System;

namespace loki.sqlite
{
	// Token: 0x0200001F RID: 31
	public struct ROW
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000801A File Offset: 0x0000621A
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00008022 File Offset: 0x00006222
		public long ID { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000802B File Offset: 0x0000622B
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00008033 File Offset: 0x00006233
		public string[] RowData { get; set; }
	}
}
