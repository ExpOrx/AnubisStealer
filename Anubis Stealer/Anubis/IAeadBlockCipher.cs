using System;

namespace Anubis
{
	// Token: 0x0200003E RID: 62
	public interface IAeadBlockCipher
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001E2 RID: 482
		string AlgorithmName { get; }

		// Token: 0x060001E3 RID: 483
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060001E4 RID: 484
		int GetBlockSize();

		// Token: 0x060001E5 RID: 485
		int ProcessByte(byte input, byte[] outBytes, int outOff);

		// Token: 0x060001E6 RID: 486
		int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff);

		// Token: 0x060001E7 RID: 487
		int DoFinal(byte[] outBytes, int outOff);

		// Token: 0x060001E8 RID: 488
		byte[] GetMac();

		// Token: 0x060001E9 RID: 489
		int GetUpdateOutputSize(int len);

		// Token: 0x060001EA RID: 490
		int GetOutputSize(int len);

		// Token: 0x060001EB RID: 491
		void Reset();
	}
}
