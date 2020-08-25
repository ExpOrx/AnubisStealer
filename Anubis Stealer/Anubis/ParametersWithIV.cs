using System;

namespace Anubis
{
	// Token: 0x02000038 RID: 56
	public class ParametersWithIV : ICipherParameters
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000BF80 File Offset: 0x0000A180
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000BF88 File Offset: 0x0000A188
		public ParametersWithIV(ICipherParameters parameters, byte[] iv) : this(parameters, iv, 0, iv.Length)
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000BF98 File Offset: 0x0000A198
		public ParametersWithIV(ICipherParameters parameters, byte[] iv, int ivOff, int ivLen)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (iv == null)
			{
				throw new ArgumentNullException("iv");
			}
			this.parameters = parameters;
			this.iv = new byte[ivLen];
			Array.Copy(iv, ivOff, this.iv, 0, ivLen);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BFEB File Offset: 0x0000A1EB
		public byte[] GetIV()
		{
			return (byte[])this.iv.Clone();
		}

		// Token: 0x04000084 RID: 132
		private readonly ICipherParameters parameters;

		// Token: 0x04000085 RID: 133
		private readonly byte[] iv;
	}
}
