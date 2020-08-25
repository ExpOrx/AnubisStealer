using System;
using System.Text;

namespace Anubis
{
	// Token: 0x02000036 RID: 54
	public sealed class Arrays
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00003A0C File Offset: 0x00001C0C
		private Arrays()
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000BB80 File Offset: 0x00009D80
		public static bool AreEqual(bool[] a, bool[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000BB97 File Offset: 0x00009D97
		public static bool AreEqual(char[] a, char[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000BBAE File Offset: 0x00009DAE
		public static bool AreEqual(byte[] a, byte[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000BBC5 File Offset: 0x00009DC5
		[Obsolete("Use 'AreEqual' method instead")]
		public static bool AreSame(byte[] a, byte[] b)
		{
			return Arrays.AreEqual(a, b);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		public static bool ConstantTimeAreEqual(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			int num2 = 0;
			while (num != 0)
			{
				num--;
				num2 |= (int)(a[num] ^ b[num]);
			}
			return num2 == 0;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000BC02 File Offset: 0x00009E02
		public static bool AreEqual(int[] a, int[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000BC1C File Offset: 0x00009E1C
		private static bool HaveSameContents(bool[] a, bool[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000BC48 File Offset: 0x00009E48
		private static bool HaveSameContents(char[] a, char[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000BC74 File Offset: 0x00009E74
		private static bool HaveSameContents(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		private static bool HaveSameContents(int[] a, int[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000BCCC File Offset: 0x00009ECC
		public static string ToString(object[] a)
		{
			StringBuilder stringBuilder = new StringBuilder(91);
			if (a.Length != 0)
			{
				stringBuilder.Append(a[0]);
				for (int i = 1; i < a.Length; i++)
				{
					stringBuilder.Append(", ").Append(a[i]);
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000BD20 File Offset: 0x00009F20
		public static int GetHashCode(byte[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= (int)data[num];
			}
			return num2;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000BD53 File Offset: 0x00009F53
		public static byte[] Clone(byte[] data)
		{
			if (data != null)
			{
				return (byte[])data.Clone();
			}
			return null;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000BD65 File Offset: 0x00009F65
		public static int[] Clone(int[] data)
		{
			if (data != null)
			{
				return (int[])data.Clone();
			}
			return null;
		}
	}
}
