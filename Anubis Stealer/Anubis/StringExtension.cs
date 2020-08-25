using System;
using System.Globalization;
using System.Text;

namespace Anubis
{
	// Token: 0x02000061 RID: 97
	public static class StringExtension
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00013FB7 File Offset: 0x000121B7
		public static T ForceTo<T>(this object @this)
		{
			return (T)((object)Convert.ChangeType(@this, typeof(T)));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00013FCE File Offset: 0x000121CE
		public static string Remove(this string input, string strToRemove)
		{
			if (input.IsNullOrEmpty())
			{
				return null;
			}
			return input.Replace(strToRemove, "");
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00013FE6 File Offset: 0x000121E6
		public static string Left(this string input, int minusRight = 1)
		{
			if (input.IsNullOrEmpty() || input.Length <= minusRight)
			{
				return null;
			}
			return input.Substring(0, input.Length - minusRight);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001400A File Offset: 0x0001220A
		public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture)
		{
			if (!culture.IsNullOrEmpty())
			{
				return defaultCulture;
			}
			return new CultureInfo(culture);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001401C File Offset: 0x0001221C
		public static string ToCamelCasing(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
			}
			return value;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001404C File Offset: 0x0001224C
		public static double? ToDouble(this string value, string culture = "en-US")
		{
			double? result;
			try
			{
				result = new double?(double.Parse(value, new CultureInfo(culture)));
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0001408C File Offset: 0x0001228C
		public static bool? ToBoolean(this string value)
		{
			bool value2 = false;
			if (bool.TryParse(value, out value2))
			{
				return new bool?(value2);
			}
			return null;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000140B8 File Offset: 0x000122B8
		public static int? ToInt32(this string value)
		{
			int value2 = 0;
			if (int.TryParse(value, out value2))
			{
				return new int?(value2);
			}
			return null;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000140E4 File Offset: 0x000122E4
		public static long? ToInt64(this string value)
		{
			long value2 = 0L;
			if (long.TryParse(value, out value2))
			{
				return new long?(value2);
			}
			return null;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00014110 File Offset: 0x00012310
		public static string AddQueyString(this string url, string queryStringKey, string queryStringValue)
		{
			string text = (url.Split(new char[]
			{
				'?'
			}).Length <= 1) ? "?" : "&";
			return string.Concat(new string[]
			{
				url,
				text,
				queryStringKey,
				"=",
				queryStringValue
			});
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000081F9 File Offset: 0x000063F9
		public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US")
		{
			return CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001416C File Offset: 0x0001236C
		public static string FillLeftWithZeros(this string value, int decimalDigits)
		{
			if (!string.IsNullOrEmpty(value))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(value);
				string[] array = value.Split(new char[]
				{
					','
				});
				for (int i = array[array.Length - 1].Length; i < decimalDigits; i++)
				{
					stringBuilder.Append("0");
				}
				value = stringBuilder.ToString();
			}
			return value;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000141CC File Offset: 0x000123CC
		public static string FormatWithDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits)
		{
			if (value.IsNullOrEmpty())
			{
				return value;
			}
			if (!value.IndexOf(",").Equals(-1))
			{
				string[] array = value.Split(new char[]
				{
					','
				});
				if (array.Length.Equals(2) && array[1].Length > 0)
				{
					value = array[0] + "," + array[1].Substring(0, (array[1].Length >= decimalDigits.Value) ? decimalDigits.Value : array[1].Length);
				}
			}
			if (decimalDigits == null)
			{
				return value;
			}
			return value.FillLeftWithZeros(decimalDigits.Value);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00014276 File Offset: 0x00012476
		public static string FormatWithoutDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits, CultureInfo culture)
		{
			if (removeCurrencySymbol)
			{
				value = value.Remove(culture.NumberFormat.CurrencySymbol).Trim();
			}
			return value;
		}
	}
}
