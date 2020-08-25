using System;

namespace Anubis
{
	// Token: 0x02000041 RID: 65
	public class DataLengthException : CryptoException
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000DCAF File Offset: 0x0000BEAF
		public DataLengthException()
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000DCB7 File Offset: 0x0000BEB7
		public DataLengthException(string message) : base(message)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		public DataLengthException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
