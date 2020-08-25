using System;

namespace Anubis
{
	// Token: 0x0200005F RID: 95
	public struct ROW
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00012E8B File Offset: 0x0001108B
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00012E93 File Offset: 0x00011093
		public long ID { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00012E9C File Offset: 0x0001109C
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00012EA4 File Offset: 0x000110A4
		public string[] RowData { get; set; }
	}
}
