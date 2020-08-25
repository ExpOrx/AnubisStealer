using System;

namespace Anubis
{
	// Token: 0x02000056 RID: 86
	public struct FF
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0001120E File Offset: 0x0000F40E
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00011216 File Offset: 0x0000F416
		public long ID { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0001121F File Offset: 0x0000F41F
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00011227 File Offset: 0x0000F427
		public string Type { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00011230 File Offset: 0x0000F430
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00011238 File Offset: 0x0000F438
		public string Name { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00011241 File Offset: 0x0000F441
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00011249 File Offset: 0x0000F449
		public string AstableName { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00011252 File Offset: 0x0000F452
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0001125A File Offset: 0x0000F45A
		public long RootNum { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00011263 File Offset: 0x0000F463
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0001126B File Offset: 0x0000F46B
		public string SqlStatement { get; set; }
	}
}
