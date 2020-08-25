using System;

namespace Anubis
{
	// Token: 0x0200004E RID: 78
	public class Gecko3
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000F65E File Offset: 0x0000D85E
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000F666 File Offset: 0x0000D866
		public int nextId { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000F66F File Offset: 0x0000D86F
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000F677 File Offset: 0x0000D877
		public Gecko5[] logins { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000F680 File Offset: 0x0000D880
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000F688 File Offset: 0x0000D888
		public object[] disabledHosts { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000F691 File Offset: 0x0000D891
		// (set) Token: 0x06000235 RID: 565 RVA: 0x0000F699 File Offset: 0x0000D899
		public int version { get; set; }
	}
}
