using System;

namespace Anubis
{
	// Token: 0x02000040 RID: 64
	public class CryptoException : Exception
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000DC94 File Offset: 0x0000BE94
		public CryptoException()
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public CryptoException(string message) : base(message)
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000DCA5 File Offset: 0x0000BEA5
		public CryptoException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
