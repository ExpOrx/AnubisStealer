using System;

namespace loki.sqlite.nulls
{
	// Token: 0x02000022 RID: 34
	public static class IsNullExtension
	{
		// Token: 0x06000148 RID: 328 RVA: 0x0000833A File Offset: 0x0000653A
		public static bool IsNotNull<T>(this T data)
		{
			return data != null;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008345 File Offset: 0x00006545
		public static string IsNull(this string value, string defaultValue)
		{
			if (string.IsNullOrEmpty(value))
			{
				return defaultValue;
			}
			return value;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008352 File Offset: 0x00006552
		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000835C File Offset: 0x0000655C
		public static bool IsNull(this bool? value, bool def)
		{
			bool? flag = value;
			if (flag == null)
			{
				return def;
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000837D File Offset: 0x0000657D
		public static T IsNull<T>(this T value) where T : class
		{
			T result = value;
			if (value == null)
			{
				result = Activator.CreateInstance<T>();
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000838E File Offset: 0x0000658E
		public static T IsNull<T>(this T value, T def) where T : class
		{
			T result = value;
			if (value == null)
			{
				result = def;
				if (def == null)
				{
					result = Activator.CreateInstance<T>();
				}
			}
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000083AC File Offset: 0x000065AC
		public static int IsNull(this int? value, int def)
		{
			int? num = value;
			if (num == null)
			{
				return def;
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000083D0 File Offset: 0x000065D0
		public static long IsNull(this long? value, long def)
		{
			long? num = value;
			if (num == null)
			{
				return def;
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000083F4 File Offset: 0x000065F4
		public static double IsNull(this double? value, double def)
		{
			double? num = value;
			if (num == null)
			{
				return def;
			}
			return num.GetValueOrDefault();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008418 File Offset: 0x00006618
		public static DateTime IsNull(this DateTime? value, DateTime def)
		{
			DateTime? dateTime = value;
			if (dateTime == null)
			{
				return def;
			}
			return dateTime.GetValueOrDefault();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000843C File Offset: 0x0000663C
		public static Guid IsNull(this Guid? value, Guid def)
		{
			Guid? guid = value;
			if (guid == null)
			{
				return def;
			}
			return guid.GetValueOrDefault();
		}
	}
}
