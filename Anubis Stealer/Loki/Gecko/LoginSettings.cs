using System;

namespace Loki.Gecko
{
	// Token: 0x02000006 RID: 6
	public class LoginSettings
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00003CDD File Offset: 0x00001EDD
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00003CE5 File Offset: 0x00001EE5
		public int NextId { get; set; }

		// Token: 0x06000019 RID: 25 RVA: 0x00003CEE File Offset: 0x00001EEE
		public DataSettings[] GetLogins()
		{
			return this.logins;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003CF6 File Offset: 0x00001EF6
		public void SetLogins(DataSettings[] value)
		{
			this.logins = value;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003CFF File Offset: 0x00001EFF
		public object[] GetDisabledHosts()
		{
			return this.disabledHosts;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003D07 File Offset: 0x00001F07
		public void SetDisabledHosts(object[] value)
		{
			this.disabledHosts = value;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003D10 File Offset: 0x00001F10
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00003D18 File Offset: 0x00001F18
		public int Version { get; set; }

		// Token: 0x04000012 RID: 18
		private DataSettings[] logins;

		// Token: 0x04000013 RID: 19
		private object[] disabledHosts;
	}
}
