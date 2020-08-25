using System;

namespace Anubis
{
	// Token: 0x0200003A RID: 58
	internal abstract class GcmUtilities
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000C09E File Offset: 0x0000A29E
		internal static byte[] OneAsBytes()
		{
			byte[] array = new byte[16];
			array[0] = 128;
			return array;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		internal static uint[] OneAsUints()
		{
			uint[] array = new uint[4];
			array[0] = 2147483648U;
			return array;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C0BF File Offset: 0x0000A2BF
		internal static uint[] AsUints(byte[] bs)
		{
			return new uint[]
			{
				Pack.BE_To_UInt32(bs, 0),
				Pack.BE_To_UInt32(bs, 4),
				Pack.BE_To_UInt32(bs, 8),
				Pack.BE_To_UInt32(bs, 12)
			};
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
		internal static void Multiply(byte[] block, byte[] val)
		{
			byte[] array = Arrays.Clone(block);
			byte[] array2 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte b = val[i];
				for (int j = 7; j >= 0; j--)
				{
					if (((int)b & 1 << j) != 0)
					{
						GcmUtilities.Xor(array2, array);
					}
					bool flag = (array[15] & 1) > 0;
					GcmUtilities.ShiftRight(array);
					if (flag)
					{
						byte[] array3 = array;
						int num = 0;
						array3[num] ^= 225;
					}
				}
			}
			Array.Copy(array2, 0, block, 0, 16);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C16C File Offset: 0x0000A36C
		internal static void MultiplyP(uint[] x)
		{
			bool flag = (x[3] & 1U) > 0U;
			GcmUtilities.ShiftRight(x);
			if (flag)
			{
				x[0] ^= 3774873600U;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C190 File Offset: 0x0000A390
		internal static void MultiplyP8(uint[] x)
		{
			uint num = x[3];
			GcmUtilities.ShiftRightN(x, 8);
			for (int i = 7; i >= 0; i--)
			{
				if (((ulong)num & (ulong)(1L << (i & 31))) != 0UL)
				{
					x[0] ^= 3774873600U >> 7 - i;
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		internal static void ShiftRight(byte[] block)
		{
			int num = 0;
			byte b = 0;
			for (;;)
			{
				byte b2 = block[num];
				block[num] = (byte)(b2 >> 1 | (int)b);
				if (++num == 16)
				{
					break;
				}
				b = (byte)(b2 << 7);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C208 File Offset: 0x0000A408
		internal static void ShiftRight(uint[] block)
		{
			int num = 0;
			uint num2 = 0U;
			for (;;)
			{
				uint num3 = block[num];
				block[num] = (num3 >> 1 | num2);
				if (++num == 4)
				{
					break;
				}
				num2 = num3 << 31;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C234 File Offset: 0x0000A434
		internal static void ShiftRightN(uint[] block, int n)
		{
			int num = 0;
			uint num2 = 0U;
			for (;;)
			{
				uint num3 = block[num];
				block[num] = (num3 >> n | num2);
				if (++num == 4)
				{
					break;
				}
				num2 = num3 << 32 - n;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C268 File Offset: 0x0000A468
		internal static void Xor(byte[] block, byte[] val)
		{
			for (int i = 15; i >= 0; i--)
			{
				int num = i;
				block[num] ^= val[i];
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C294 File Offset: 0x0000A494
		internal static void Xor(uint[] block, uint[] val)
		{
			for (int i = 3; i >= 0; i--)
			{
				block[i] ^= val[i];
			}
		}
	}
}
