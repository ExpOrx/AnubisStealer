using System;
using System.Globalization;
using System.Text;
using loki.sqlite.nulls;

namespace loki.sqlite.strings
{
	// Token: 0x02000021 RID: 33
	public static class StringExtension
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000805E File Offset: 0x0000625E
		public static T ForceTo<T>(this object @this)
		{
			return (T)((object)Convert.ChangeType(@this, typeof(T)));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00008075 File Offset: 0x00006275
		public static string Remove(this string input, string strToRemove)
		{
			if (!input.IsNullOrEmpty())
			{
				return input.Replace(strToRemove, "");
			}
			return null;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000808D File Offset: 0x0000628D
		public static string Left(this string input, int minusRight = 1)
		{
			if (!input.IsNullOrEmpty() && input.Length > minusRight)
			{
				return input.Substring(0, input.Length - minusRight);
			}
			return null;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000080B1 File Offset: 0x000062B1
		public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture)
		{
			if (culture.IsNullOrEmpty())
			{
				return new CultureInfo(culture);
			}
			return defaultCulture;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000080C3 File Offset: 0x000062C3
		public static string ToCamelCasing(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000080F0 File Offset: 0x000062F0
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

		// Token: 0x06000140 RID: 320 RVA: 0x00008130 File Offset: 0x00006330
		public static bool? ToBoolean(this string value)
		{
			bool value2;
			if (!bool.TryParse(value, out value2))
			{
				return null;
			}
			return new bool?(value2);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008158 File Offset: 0x00006358
		public static int? ToInt32(this string value)
		{
			int value2;
			if (!int.TryParse(value, out value2))
			{
				return null;
			}
			return new int?(value2);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008180 File Offset: 0x00006380
		public static long? ToInt64(this string value)
		{
			long value2;
			if (!long.TryParse(value, out value2))
			{
				return null;
			}
			return new long?(value2);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000081A8 File Offset: 0x000063A8
		public static string AddQueyString(this string url, string queryStringKey, string queryStringValue)
		{
			return string.Concat(new string[]
			{
				url,
				(url.Split(new char[]
				{
					'?'
				}).Length <= 1) ? "?" : "&",
				queryStringKey,
				"=",
				queryStringValue
			});
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000081F9 File Offset: 0x000063F9
		public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US")
		{
			return CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000820C File Offset: 0x0000640C
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

		// Token: 0x06000146 RID: 326 RVA: 0x0000826C File Offset: 0x0000646C
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
			if (decimalDigits != null)
			{
				return value.FillLeftWithZeros(decimalDigits.Value);
			}
			return value;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008319 File Offset: 0x00006519
		public static string FormatWithoutDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits, CultureInfo culture)
		{
			if (!removeCurrencySymbol)
			{
				return value;
			}
			value = value.Remove(culture.NumberFormat.CurrencySymbol).Trim();
			return value;
		}
	}
}
