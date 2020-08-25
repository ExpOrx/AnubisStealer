using System;

namespace Anubis
{
	// Token: 0x0200003D RID: 61
	public class GcmBlockCipher : IAeadBlockCipher
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000C57C File Offset: 0x0000A77C
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCM";
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C593 File Offset: 0x0000A793
		public GcmBlockCipher(IBlockCipher c) : this(c, null)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public GcmBlockCipher(IBlockCipher c, IGcmMultiplier m)
		{
			if (c.GetBlockSize() != 16)
			{
				throw new ArgumentException("cipher required with a block size of " + 16.ToString() + ".");
			}
			if (m == null)
			{
				m = new Tables8kGcmMultiplier();
			}
			this.cipher = c;
			this.multiplier = m;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			this.macBlock = null;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				this.nonce = aeadParameters.GetNonce();
				this.A = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 96 || num > 128 || num % 8 != 0)
				{
					throw new ArgumentException("Invalid value for MAC size: " + num.ToString());
				}
				this.macSize = num / 8;
				this.keyParam = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to GCM");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				this.nonce = parametersWithIV.GetIV();
				this.A = null;
				this.macSize = 16;
				this.keyParam = (KeyParameter)parametersWithIV.Parameters;
			}
			int num2 = forEncryption ? 16 : (16 + this.macSize);
			this.bufBlock = new byte[num2];
			if (this.nonce == null || this.nonce.Length < 1)
			{
				throw new ArgumentException("IV must be at least 1 byte");
			}
			if (this.A == null)
			{
				this.A = new byte[0];
			}
			this.cipher.Init(true, this.keyParam);
			this.H = new byte[16];
			this.cipher.ProcessBlock(this.H, 0, this.H, 0);
			this.multiplier.Init(this.H);
			this.initS = this.gHASH(this.A);
			if (this.nonce.Length == 12)
			{
				this.J0 = new byte[16];
				Array.Copy(this.nonce, 0, this.J0, 0, this.nonce.Length);
				this.J0[15] = 1;
			}
			else
			{
				this.J0 = this.gHASH(this.nonce);
				byte[] array = new byte[16];
				GcmBlockCipher.packLength((ulong)((long)this.nonce.Length * 8L), array, 8);
				GcmUtilities.Xor(this.J0, array);
				this.multiplier.MultiplyH(this.J0);
			}
			this.S = Arrays.Clone(this.initS);
			this.counter = Arrays.Clone(this.J0);
			this.bufOff = 0;
			this.totalLength = 0UL;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C830 File Offset: 0x0000AA30
		public virtual byte[] GetMac()
		{
			return Arrays.Clone(this.macBlock);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C83D File Offset: 0x0000AA3D
		public virtual int GetOutputSize(int len)
		{
			if (this.forEncryption)
			{
				return len + this.bufOff + this.macSize;
			}
			return len + this.bufOff - this.macSize;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C866 File Offset: 0x0000AA66
		public virtual int GetUpdateOutputSize(int len)
		{
			return (len + this.bufOff) / 16 * 16;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C876 File Offset: 0x0000AA76
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			return this.Process(input, output, outOff);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C884 File Offset: 0x0000AA84
		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			int num = 0;
			for (int num2 = 0; num2 != len; num2++)
			{
				byte[] array = this.bufBlock;
				int num3 = this.bufOff;
				this.bufOff = num3 + 1;
				array[num3] = input[inOff + num2];
				if (this.bufOff == this.bufBlock.Length)
				{
					this.gCTRBlock(this.bufBlock, 16, output, outOff + num);
					if (!this.forEncryption)
					{
						Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
					}
					this.bufOff = this.bufBlock.Length - 16;
					num += 16;
				}
			}
			return num;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C91C File Offset: 0x0000AB1C
		private int Process(byte input, byte[] output, int outOff)
		{
			byte[] array = this.bufBlock;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = input;
			if (this.bufOff == this.bufBlock.Length)
			{
				this.gCTRBlock(this.bufBlock, 16, output, outOff);
				if (!this.forEncryption)
				{
					Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
				}
				this.bufOff = this.bufBlock.Length - 16;
				return 16;
			}
			return 0;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public int DoFinal(byte[] output, int outOff)
		{
			int num = this.bufOff;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num -= this.macSize;
			}
			if (num > 0)
			{
				byte[] array = new byte[16];
				Array.Copy(this.bufBlock, 0, array, 0, num);
				this.gCTRBlock(array, num, output, outOff);
			}
			byte[] array2 = new byte[16];
			GcmBlockCipher.packLength((ulong)((long)this.A.Length * 8L), array2, 0);
			GcmBlockCipher.packLength(this.totalLength * 8UL, array2, 8);
			GcmUtilities.Xor(this.S, array2);
			this.multiplier.MultiplyH(this.S);
			byte[] array3 = new byte[16];
			this.cipher.ProcessBlock(this.J0, 0, array3, 0);
			GcmUtilities.Xor(array3, this.S);
			int num2 = num;
			this.macBlock = new byte[this.macSize];
			Array.Copy(array3, 0, this.macBlock, 0, this.macSize);
			if (this.forEncryption)
			{
				Array.Copy(this.macBlock, 0, output, outOff + this.bufOff, this.macSize);
				num2 += this.macSize;
			}
			else
			{
				byte[] array4 = new byte[this.macSize];
				Array.Copy(this.bufBlock, num, array4, 0, this.macSize);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, array4))
				{
					throw new InvalidCipherTextException("mac check in GCM failed");
				}
			}
			this.Reset(false);
			return num2;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000CB07 File Offset: 0x0000AD07
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000CB10 File Offset: 0x0000AD10
		private void Reset(bool clearMac)
		{
			this.S = Arrays.Clone(this.initS);
			this.counter = Arrays.Clone(this.J0);
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.bufBlock != null)
			{
				Array.Clear(this.bufBlock, 0, this.bufBlock.Length);
			}
			if (clearMac)
			{
				this.macBlock = null;
			}
			this.cipher.Reset();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000CB80 File Offset: 0x0000AD80
		private void gCTRBlock(byte[] buf, int bufCount, byte[] output, int outOff)
		{
			for (int i = 15; i >= 12; i--)
			{
				byte[] array = this.counter;
				int num = i;
				byte b = array[num] + 1;
				array[num] = b;
				if (b != 0)
				{
					break;
				}
			}
			byte[] array2 = new byte[16];
			this.cipher.ProcessBlock(this.counter, 0, array2, 0);
			byte[] val;
			if (this.forEncryption)
			{
				Array.Copy(GcmBlockCipher.Zeroes, bufCount, array2, bufCount, 16 - bufCount);
				val = array2;
			}
			else
			{
				val = buf;
			}
			for (int j = bufCount - 1; j >= 0; j--)
			{
				byte[] array3 = array2;
				int num2 = j;
				array3[num2] ^= buf[j];
				output[outOff + j] = array2[j];
			}
			GcmUtilities.Xor(this.S, val);
			this.multiplier.MultiplyH(this.S);
			this.totalLength += (ulong)((long)bufCount);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000CC48 File Offset: 0x0000AE48
		private byte[] gHASH(byte[] b)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < b.Length; i += 16)
			{
				byte[] array2 = new byte[16];
				int length = Math.Min(b.Length - i, 16);
				Array.Copy(b, i, array2, 0, length);
				GcmUtilities.Xor(array, array2);
				this.multiplier.MultiplyH(array);
			}
			return array;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000BE63 File Offset: 0x0000A063
		private static void packLength(ulong len, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(len >> 32), bs, off);
			Pack.UInt32_To_BE((uint)len, bs, off + 4);
		}

		// Token: 0x04000088 RID: 136
		private const int BlockSize = 16;

		// Token: 0x04000089 RID: 137
		private static readonly byte[] Zeroes = new byte[16];

		// Token: 0x0400008A RID: 138
		private readonly IBlockCipher cipher;

		// Token: 0x0400008B RID: 139
		private readonly IGcmMultiplier multiplier;

		// Token: 0x0400008C RID: 140
		private bool forEncryption;

		// Token: 0x0400008D RID: 141
		private int macSize;

		// Token: 0x0400008E RID: 142
		private byte[] nonce;

		// Token: 0x0400008F RID: 143
		private byte[] A;

		// Token: 0x04000090 RID: 144
		private KeyParameter keyParam;

		// Token: 0x04000091 RID: 145
		private byte[] H;

		// Token: 0x04000092 RID: 146
		private byte[] initS;

		// Token: 0x04000093 RID: 147
		private byte[] J0;

		// Token: 0x04000094 RID: 148
		private byte[] bufBlock;

		// Token: 0x04000095 RID: 149
		private byte[] macBlock;

		// Token: 0x04000096 RID: 150
		private byte[] S;

		// Token: 0x04000097 RID: 151
		private byte[] counter;

		// Token: 0x04000098 RID: 152
		private int bufOff;

		// Token: 0x04000099 RID: 153
		private ulong totalLength;
	}
}
