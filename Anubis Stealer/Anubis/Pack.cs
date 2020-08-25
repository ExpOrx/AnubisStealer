using System;

namespace Anubis
{
	// Token: 0x02000037 RID: 55
	internal sealed class Pack
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00003A0C File Offset: 0x00001C0C
		private Pack()
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000BD77 File Offset: 0x00009F77
		internal static void UInt32_To_BE(uint n, byte[] bs)
		{
			bs[0] = (byte)(n >> 24);
			bs[1] = (byte)(n >> 16);
			bs[2] = (byte)(n >> 8);
			bs[3] = (byte)n;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000BD95 File Offset: 0x00009F95
		internal static void UInt32_To_BE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)n;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000BDC2 File Offset: 0x00009FC2
		internal static uint BE_To_UInt32(byte[] bs)
		{
			return (uint)((int)bs[0] << 24 | (int)bs[1] << 16 | (int)bs[2] << 8 | (int)bs[3]);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000BDDB File Offset: 0x00009FDB
		internal static uint BE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] << 24 | (int)bs[++off] << 16 | (int)bs[++off] << 8 | (int)bs[++off]);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000BE04 File Offset: 0x0000A004
		internal static ulong BE_To_UInt64(byte[] bs)
		{
			ulong num = (ulong)Pack.BE_To_UInt32(bs);
			uint num2 = Pack.BE_To_UInt32(bs, 4);
			return num << 32 | (ulong)num2;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000BE28 File Offset: 0x0000A028
		internal static ulong BE_To_UInt64(byte[] bs, int off)
		{
			ulong num = (ulong)Pack.BE_To_UInt32(bs, off);
			uint num2 = Pack.BE_To_UInt32(bs, off + 4);
			return num << 32 | (ulong)num2;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000BE4D File Offset: 0x0000A04D
		internal static void UInt64_To_BE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs);
			Pack.UInt32_To_BE((uint)n, bs, 4);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000BE63 File Offset: 0x0000A063
		internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs, off);
			Pack.UInt32_To_BE((uint)n, bs, off + 4);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000BE7C File Offset: 0x0000A07C
		internal static void UInt32_To_LE(uint n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
			bs[2] = (byte)(n >> 16);
			bs[3] = (byte)(n >> 24);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000BE9A File Offset: 0x0000A09A
		internal static void UInt32_To_LE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000BEC7 File Offset: 0x0000A0C7
		internal static uint LE_To_UInt32(byte[] bs)
		{
			return (uint)((int)bs[0] | (int)bs[1] << 8 | (int)bs[2] << 16 | (int)bs[3] << 24);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		internal static uint LE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[++off] << 8 | (int)bs[++off] << 16 | (int)bs[++off] << 24);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000BF08 File Offset: 0x0000A108
		internal static ulong LE_To_UInt64(byte[] bs)
		{
			uint num = Pack.LE_To_UInt32(bs);
			return (ulong)Pack.LE_To_UInt32(bs, 4) << 32 | (ulong)num;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000BF2C File Offset: 0x0000A12C
		internal static ulong LE_To_UInt64(byte[] bs, int off)
		{
			uint num = Pack.LE_To_UInt32(bs, off);
			return (ulong)Pack.LE_To_UInt32(bs, off + 4) << 32 | (ulong)num;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000BF51 File Offset: 0x0000A151
		internal static void UInt64_To_LE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_LE((uint)n, bs);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, 4);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000BF67 File Offset: 0x0000A167
		internal static void UInt64_To_LE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_LE((uint)n, bs, off);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, off + 4);
		}
	}
}
