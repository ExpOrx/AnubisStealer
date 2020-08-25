using System;

namespace Anubis
{
	// Token: 0x02000057 RID: 87
	public static class IsNullExtension
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00011274 File Offset: 0x0000F474
		public static bool IsNotNull<T>(this T data)
		{
			return data != null;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0001127F File Offset: 0x0000F47F
		public static string IsNull(this string value, string defaultValue)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value;
			}
			return defaultValue;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00008352 File Offset: 0x00006552
		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001128C File Offset: 0x0000F48C
		public static bool IsNull(this bool? value, bool def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000112A0 File Offset: 0x0000F4A0
		public static T IsNull<T>(this T value) where T : class
		{
			if (value == null)
			{
				return Activator.CreateInstance<T>();
			}
			return value;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000112B1 File Offset: 0x0000F4B1
		public static T IsNull<T>(this T value, T def) where T : class
		{
			if (value != null)
			{
				return value;
			}
			if (def == null)
			{
				return Activator.CreateInstance<T>();
			}
			return def;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000112CC File Offset: 0x0000F4CC
		public static int IsNull(this int? value, int def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public static long IsNull(this long? value, long def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000112F4 File Offset: 0x0000F4F4
		public static double IsNull(this double? value, double def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00011308 File Offset: 0x0000F508
		public static DateTime IsNull(this DateTime? value, DateTime def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001131C File Offset: 0x0000F51C
		public static Guid IsNull(this Guid? value, Guid def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}
	}
}
