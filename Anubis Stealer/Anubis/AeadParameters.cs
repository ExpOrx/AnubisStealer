using System;

namespace Anubis
{
	// Token: 0x02000046 RID: 70
	public class AeadParameters : ICipherParameters
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000DD64 File Offset: 0x0000BF64
		public virtual KeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		public virtual int MacSize
		{
			get
			{
				return this.macSize;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000DD74 File Offset: 0x0000BF74
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText)
		{
			this.key = key;
			this.nonce = nonce;
			this.macSize = macSize;
			this.associatedText = associatedText;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000DD99 File Offset: 0x0000BF99
		public virtual byte[] GetAssociatedText()
		{
			return this.associatedText;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000DDA1 File Offset: 0x0000BFA1
		public virtual byte[] GetNonce()
		{
			return this.nonce;
		}

		// Token: 0x040000B0 RID: 176
		private readonly byte[] associatedText;

		// Token: 0x040000B1 RID: 177
		private readonly byte[] nonce;

		// Token: 0x040000B2 RID: 178
		private readonly KeyParameter key;

		// Token: 0x040000B3 RID: 179
		private readonly int macSize;
	}
}
