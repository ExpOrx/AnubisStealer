using System;
using System.Globalization;

namespace Anubis
{
	// Token: 0x02000052 RID: 82
	public class Gecko7
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public string EntrySalt { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		public string OID { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000FA34 File Offset: 0x0000DC34
		public string Passwordcheck { get; }

		// Token: 0x06000262 RID: 610 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		public Gecko7(string DataToParse)
		{
			int num = int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2;
			this.EntrySalt = DataToParse.Substring(6, num);
			int num2 = DataToParse.Length - (6 + num + 36);
			this.OID = DataToParse.Substring(6 + num + 36, num2);
			this.Passwordcheck = DataToParse.Substring(6 + num + 4 + num2);
		}
	}
}
