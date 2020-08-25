using System;

namespace Anubis
{
	// Token: 0x0200003B RID: 59
	public interface IGcmMultiplier
	{
		// Token: 0x060001CB RID: 459
		void Init(byte[] H);

		// Token: 0x060001CC RID: 460
		void MultiplyH(byte[] x);
	}
}
