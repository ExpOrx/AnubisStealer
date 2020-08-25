using System;

namespace loki.sqlite
{
	// Token: 0x0200001E RID: 30
	public struct FF
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00007FB4 File Offset: 0x000061B4
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00007FBC File Offset: 0x000061BC
		public long ID { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007FC5 File Offset: 0x000061C5
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00007FCD File Offset: 0x000061CD
		public string Type { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00007FD6 File Offset: 0x000061D6
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00007FDE File Offset: 0x000061DE
		public string Name { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00007FE7 File Offset: 0x000061E7
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00007FEF File Offset: 0x000061EF
		public string AstableName { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007FF8 File Offset: 0x000061F8
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00008000 File Offset: 0x00006200
		public long RootNum { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00008009 File Offset: 0x00006209
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00008011 File Offset: 0x00006211
		public string SqlStatement { get; set; }
	}
}
