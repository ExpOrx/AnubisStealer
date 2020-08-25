using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Anubis
{
	// Token: 0x0200005E RID: 94
	public abstract class JsonValue : IEnumerable
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000574A File Offset: 0x0000394A
		public virtual int Count
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002F0 RID: 752
		public abstract JsonType JsonType { get; }

		// Token: 0x17000087 RID: 135
		public virtual JsonValue this[int index]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000088 RID: 136
		public virtual JsonValue this[string key]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012362 File Offset: 0x00010562
		public static JsonValue Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return JsonValue.Load(new StreamReader(stream, true));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001237E File Offset: 0x0001057E
		public static JsonValue Load(TextReader textReader)
		{
			if (textReader == null)
			{
				throw new ArgumentNullException("textReader");
			}
			return JsonValue.ToJsonValue<object>(new JavaScriptReader(textReader).Read());
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001239E File Offset: 0x0001059E
		private static IEnumerable<KeyValuePair<string, JsonValue>> ToJsonPairEnumerable(IEnumerable<KeyValuePair<string, object>> kvpc)
		{
			foreach (KeyValuePair<string, object> keyValuePair in kvpc)
			{
				yield return new KeyValuePair<string, JsonValue>(keyValuePair.Key, JsonValue.ToJsonValue<object>(keyValuePair.Value));
			}
			IEnumerator<KeyValuePair<string, object>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000123AE File Offset: 0x000105AE
		private static IEnumerable<JsonValue> ToJsonValueEnumerable(IEnumerable arr)
		{
			foreach (object ret in arr)
			{
				yield return JsonValue.ToJsonValue<object>(ret);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000123C0 File Offset: 0x000105C0
		public static JsonValue ToJsonValue<T>(T ret)
		{
			if (ret == null)
			{
				return null;
			}
			T t;
			if ((t = ret) is bool)
			{
				return new JsonPrimitive((bool)((object)t));
			}
			if ((t = ret) is byte)
			{
				return new JsonPrimitive((byte)((object)t));
			}
			if ((t = ret) is char)
			{
				return new JsonPrimitive((char)((object)t));
			}
			if ((t = ret) is decimal)
			{
				return new JsonPrimitive((decimal)((object)t));
			}
			if ((t = ret) is double)
			{
				return new JsonPrimitive((double)((object)t));
			}
			if ((t = ret) is float)
			{
				return new JsonPrimitive((float)((object)t));
			}
			if ((t = ret) is int)
			{
				return new JsonPrimitive((int)((object)t));
			}
			if ((t = ret) is long)
			{
				return new JsonPrimitive((long)((object)t));
			}
			if ((t = ret) is sbyte)
			{
				return new JsonPrimitive((sbyte)((object)t));
			}
			if ((t = ret) is short)
			{
				return new JsonPrimitive((short)((object)t));
			}
			string value;
			if ((value = (ret as string)) != null)
			{
				return new JsonPrimitive(value);
			}
			if ((t = ret) is uint)
			{
				return new JsonPrimitive((uint)((object)t));
			}
			if ((t = ret) is ulong)
			{
				return new JsonPrimitive((ulong)((object)t));
			}
			if ((t = ret) is ushort)
			{
				return new JsonPrimitive((ushort)((object)t));
			}
			if ((t = ret) is DateTime)
			{
				return new JsonPrimitive((DateTime)((object)t));
			}
			if ((t = ret) is DateTimeOffset)
			{
				return new JsonPrimitive((DateTimeOffset)((object)t));
			}
			if ((t = ret) is Guid)
			{
				return new JsonPrimitive((Guid)((object)t));
			}
			if ((t = ret) is TimeSpan)
			{
				return new JsonPrimitive((TimeSpan)((object)t));
			}
			Uri value2;
			if ((value2 = (ret as Uri)) != null)
			{
				return new JsonPrimitive(value2);
			}
			IEnumerable<KeyValuePair<string, object>> enumerable = ret as IEnumerable<KeyValuePair<string, object>>;
			if (enumerable != null)
			{
				return new JsonObject(JsonValue.ToJsonPairEnumerable(enumerable));
			}
			IEnumerable enumerable2 = ret as IEnumerable;
			if (enumerable2 != null)
			{
				return new JsonArray(JsonValue.ToJsonValueEnumerable(enumerable2));
			}
			if (!(ret is IEnumerable))
			{
				PropertyInfo[] properties = ret.GetType().GetProperties();
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (PropertyInfo propertyInfo in properties)
				{
					dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(ret, null).IsNull("null"));
				}
				if (dictionary.Count > 0)
				{
					return new JsonObject(JsonValue.ToJsonPairEnumerable(dictionary));
				}
			}
			throw new NotSupportedException(string.Format("Unexpected parser return type: {0}", ret.GetType()));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000126F7 File Offset: 0x000108F7
		public static JsonValue Parse(string jsonString)
		{
			if (jsonString == null)
			{
				throw new ArgumentNullException("jsonString");
			}
			return JsonValue.Load(new StringReader(jsonString));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000574A File Offset: 0x0000394A
		public virtual bool ContainsKey(string key)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00012712 File Offset: 0x00010912
		public virtual void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.Save(new StreamWriter(stream), parsing);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001272F File Offset: 0x0001092F
		public virtual void Save(TextWriter textWriter, bool parsing)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this.Savepublic(textWriter, parsing);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00012748 File Offset: 0x00010948
		private void Savepublic(TextWriter w, bool saving)
		{
			switch (this.JsonType)
			{
			case JsonType.String:
				if (saving)
				{
					w.Write('"');
				}
				w.Write(this.EscapeString(((JsonPrimitive)this).GetFormattedString()));
				if (saving)
				{
					w.Write('"');
					return;
				}
				return;
			case JsonType.Object:
			{
				w.Write('{');
				bool flag = false;
				foreach (KeyValuePair<string, JsonValue> keyValuePair in ((JsonObject)this))
				{
					if (flag)
					{
						w.Write(", ");
					}
					w.Write('"');
					w.Write(this.EscapeString(keyValuePair.Key));
					w.Write("\": ");
					if (keyValuePair.Value == null)
					{
						w.Write("null");
					}
					else
					{
						keyValuePair.Value.Savepublic(w, saving);
					}
					flag = true;
				}
				w.Write('}');
				return;
			}
			case JsonType.Array:
			{
				w.Write('[');
				bool flag2 = false;
				foreach (JsonValue jsonValue in ((IEnumerable<JsonValue>)((JsonArray)this)))
				{
					if (flag2)
					{
						w.Write(", ");
					}
					if (jsonValue != null)
					{
						jsonValue.Savepublic(w, saving);
					}
					else
					{
						w.Write("null");
					}
					flag2 = true;
				}
				w.Write(']');
				return;
			}
			case JsonType.Boolean:
				w.Write(this ? "true" : "false");
				return;
			}
			w.Write(((JsonPrimitive)this).GetFormattedString());
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000128F8 File Offset: 0x00010AF8
		public string ToString(bool saving = true)
		{
			StringWriter stringWriter = new StringWriter();
			this.Save(stringWriter, saving);
			return stringWriter.ToString();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000574A File Offset: 0x0000394A
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001291C File Offset: 0x00010B1C
		private bool NeedEscape(string src, int i)
		{
			char c = src[i];
			return c < ' ' || c == '"' || c == '\\' || (c >= '\ud800' && c <= '\udbff' && (i == src.Length - 1 || src[i + 1] < '\udc00' || src[i + 1] > '\udfff')) || (c >= '\udc00' && c <= '\udfff' && (i == 0 || src[i - 1] < '\ud800' || src[i - 1] > '\udbff')) || c == '\u2028' || c == '\u2029' || (c == '/' && i > 0 && src[i - 1] == '<');
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000129E4 File Offset: 0x00010BE4
		public string EscapeString(string src)
		{
			if (src == null)
			{
				return null;
			}
			for (int i = 0; i < src.Length; i++)
			{
				if (this.NeedEscape(src, i))
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (i > 0)
					{
						stringBuilder.Append(src, 0, i);
					}
					return this.DoEscapeString(stringBuilder, src, i);
				}
			}
			return src;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00012A30 File Offset: 0x00010C30
		private string DoEscapeString(StringBuilder sb, string src, int cur)
		{
			int num = cur;
			for (int i = cur; i < src.Length; i++)
			{
				if (this.NeedEscape(src, i))
				{
					sb.Append(src, num, i - num);
					char c = src[i];
					if (c <= '"')
					{
						switch (c)
						{
						case '\b':
							sb.Append("\\b");
							break;
						case '\t':
							sb.Append("\\t");
							break;
						case '\n':
							sb.Append("\\n");
							break;
						case '\v':
							goto IL_D5;
						case '\f':
							sb.Append("\\f");
							break;
						case '\r':
							sb.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_D5;
							}
							sb.Append("\\\"");
							break;
						}
					}
					else if (c != '/')
					{
						if (c != '\\')
						{
							goto IL_D5;
						}
						sb.Append("\\\\");
					}
					else
					{
						sb.Append("\\/");
					}
					IL_FC:
					num = i + 1;
					goto IL_100;
					IL_D5:
					sb.Append("\\u");
					sb.Append(((int)src[i]).ToString("x04"));
					goto IL_FC;
				}
				IL_100:;
			}
			sb.Append(src, num, src.Length - num);
			return sb.ToString();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00012B64 File Offset: 0x00010D64
		public static implicit operator JsonValue(bool value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00012B6C File Offset: 0x00010D6C
		public static implicit operator JsonValue(byte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00012B74 File Offset: 0x00010D74
		public static implicit operator JsonValue(char value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00012B7C File Offset: 0x00010D7C
		public static implicit operator JsonValue(decimal value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00012B84 File Offset: 0x00010D84
		public static implicit operator JsonValue(double value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00012B8C File Offset: 0x00010D8C
		public static implicit operator JsonValue(float value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00012B94 File Offset: 0x00010D94
		public static implicit operator JsonValue(int value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00012B9C File Offset: 0x00010D9C
		public static implicit operator JsonValue(long value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00012BA4 File Offset: 0x00010DA4
		public static implicit operator JsonValue(sbyte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00012BAC File Offset: 0x00010DAC
		public static implicit operator JsonValue(short value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00012BB4 File Offset: 0x00010DB4
		public static implicit operator JsonValue(string value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00012BBC File Offset: 0x00010DBC
		public static implicit operator JsonValue(uint value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00012BC4 File Offset: 0x00010DC4
		public static implicit operator JsonValue(ulong value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00012BCC File Offset: 0x00010DCC
		public static implicit operator JsonValue(ushort value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00012BD4 File Offset: 0x00010DD4
		public static implicit operator JsonValue(DateTime value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00012BDC File Offset: 0x00010DDC
		public static implicit operator JsonValue(DateTimeOffset value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public static implicit operator JsonValue(Guid value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00012BEC File Offset: 0x00010DEC
		public static implicit operator JsonValue(TimeSpan value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00012BF4 File Offset: 0x00010DF4
		public static implicit operator JsonValue(Uri value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00012BFC File Offset: 0x00010DFC
		public static implicit operator bool(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToBoolean(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00012C21 File Offset: 0x00010E21
		public static implicit operator byte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00012C46 File Offset: 0x00010E46
		public static implicit operator char(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToChar(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00012C6B File Offset: 0x00010E6B
		public static implicit operator decimal(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDecimal(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00012C90 File Offset: 0x00010E90
		public static implicit operator double(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDouble(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00012CB5 File Offset: 0x00010EB5
		public static implicit operator float(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSingle(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00012CDA File Offset: 0x00010EDA
		public static implicit operator int(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00012CFF File Offset: 0x00010EFF
		public static implicit operator long(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00012D24 File Offset: 0x00010F24
		public static implicit operator sbyte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00012D49 File Offset: 0x00010F49
		public static implicit operator short(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00012D6E File Offset: 0x00010F6E
		public static implicit operator string(JsonValue value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString(true);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00012D7C File Offset: 0x00010F7C
		public static implicit operator uint(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00012DA1 File Offset: 0x00010FA1
		public static implicit operator ulong(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00012DC6 File Offset: 0x00010FC6
		public static implicit operator ushort(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00012DEB File Offset: 0x00010FEB
		public static implicit operator DateTime(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTime)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00012E0B File Offset: 0x0001100B
		public static implicit operator DateTimeOffset(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTimeOffset)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00012E2B File Offset: 0x0001102B
		public static implicit operator TimeSpan(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (TimeSpan)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00012E4B File Offset: 0x0001104B
		public static implicit operator Guid(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Guid)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00012E6B File Offset: 0x0001106B
		public static implicit operator Uri(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Uri)((JsonPrimitive)value).Value;
		}
	}
}
