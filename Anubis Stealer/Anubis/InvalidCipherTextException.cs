using System;

namespace Anubis
{
	// Token: 0x02000044 RID: 68
	public class InvalidCipherTextException : CryptoException
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000DCAF File Offset: 0x0000BEAF
		public InvalidCipherTextException()
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000DCB7 File Offset: 0x0000BEB7
		public InvalidCipherTextException(string message) : base(message)
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public InvalidCipherTextException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
