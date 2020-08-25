using System;

namespace Anubis
{
	// Token: 0x02000039 RID: 57
	public class KeyParameter : ICipherParameters
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0000BFFD File Offset: 0x0000A1FD
		public KeyParameter(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = (byte[])key.Clone();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000C024 File Offset: 0x0000A224
		public KeyParameter(byte[] key, int keyOff, int keyLen)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (keyOff < 0 || keyOff > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyOff");
			}
			if (keyLen < 0 || keyOff + keyLen > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyLen");
			}
			this.key = new byte[keyLen];
			Array.Copy(key, keyOff, this.key, 0, keyLen);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000C08C File Offset: 0x0000A28C
		public byte[] GetKey()
		{
			return (byte[])this.key.Clone();
		}

		// Token: 0x04000086 RID: 134
		private readonly byte[] key;
	}
}
