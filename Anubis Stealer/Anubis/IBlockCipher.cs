using System;

namespace Anubis
{
	// Token: 0x02000042 RID: 66
	public interface IBlockCipher
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000203 RID: 515
		string AlgorithmName { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000204 RID: 516
		bool IsPartialBlockOkay { get; }

		// Token: 0x06000205 RID: 517
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06000206 RID: 518
		int GetBlockSize();

		// Token: 0x06000207 RID: 519
		int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);

		// Token: 0x06000208 RID: 520
		void Reset();
	}
}
