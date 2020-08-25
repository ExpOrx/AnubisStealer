using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x02000013 RID: 19
	public abstract class JsonValue : IEnumerable
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000574A File Offset: 0x0000394A
		public virtual int Count
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B0 RID: 176
		public abstract JsonType JsonType { get; }

		// Token: 0x1700002B RID: 43
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

		// Token: 0x1700002C RID: 44
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

		// Token: 0x060000B5 RID: 181 RVA: 0x00005751 File Offset: 0x00003951
		public static JsonValue Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return JsonValue.Load(new StreamReader(stream, true));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000576D File Offset: 0x0000396D
		public static JsonValue Load(TextReader textReader)
		{
			if (textReader == null)
			{
				throw new ArgumentNullException("textReader");
			}
			return JsonValue.ToJsonValue<object>(new JavaScriptReader(textReader).Read());
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000578D File Offset: 0x0000398D
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

		// Token: 0x060000B8 RID: 184 RVA: 0x0000579D File Offset: 0x0000399D
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

		// Token: 0x060000B9 RID: 185 RVA: 0x000057B0 File Offset: 0x000039B0
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
					dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(ret, null));
				}
				if (dictionary.Count > 0)
				{
					return new JsonObject(JsonValue.ToJsonPairEnumerable(dictionary));
				}
			}
			throw new NotSupportedException(string.Format("Unexpected parser return type: {0}", ret.GetType()));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005ADD File Offset: 0x00003CDD
		public static JsonValue Parse(string jsonString)
		{
			if (jsonString == null)
			{
				throw new ArgumentNullException("jsonString");
			}
			return JsonValue.Load(new StringReader(jsonString));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000574A File Offset: 0x0000394A
		public virtual bool ContainsKey(string key)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public virtual void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.Save(new StreamWriter(stream), parsing);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005B15 File Offset: 0x00003D15
		public virtual void Save(TextWriter textWriter, bool parsing)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this.Savepublic(textWriter, parsing);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005B30 File Offset: 0x00003D30
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

		// Token: 0x060000BF RID: 191 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public string ToString(bool saving = true)
		{
			StringWriter stringWriter = new StringWriter();
			this.Save(stringWriter, saving);
			return stringWriter.ToString();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000574A File Offset: 0x0000394A
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005D04 File Offset: 0x00003F04
		private bool NeedEscape(string src, int i)
		{
			char c = src[i];
			return c < ' ' || c == '"' || c == '\\' || (c >= '\ud800' && c <= '\udbff' && (i == src.Length - 1 || src[i + 1] < '\udc00' || src[i + 1] > '\udfff')) || (c >= '\udc00' && c <= '\udfff' && (i == 0 || src[i - 1] < '\ud800' || src[i - 1] > '\udbff')) || c == '\u2028' || c == '\u2029' || (c == '/' && i > 0 && src[i - 1] == '<');
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005DCC File Offset: 0x00003FCC
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

		// Token: 0x060000C3 RID: 195 RVA: 0x00005E18 File Offset: 0x00004018
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

		// Token: 0x060000C4 RID: 196 RVA: 0x00005F4C File Offset: 0x0000414C
		public static implicit operator JsonValue(bool value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005F54 File Offset: 0x00004154
		public static implicit operator JsonValue(byte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005F5C File Offset: 0x0000415C
		public static implicit operator JsonValue(char value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005F64 File Offset: 0x00004164
		public static implicit operator JsonValue(decimal value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005F6C File Offset: 0x0000416C
		public static implicit operator JsonValue(double value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005F74 File Offset: 0x00004174
		public static implicit operator JsonValue(float value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005F7C File Offset: 0x0000417C
		public static implicit operator JsonValue(int value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005F84 File Offset: 0x00004184
		public static implicit operator JsonValue(long value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005F8C File Offset: 0x0000418C
		public static implicit operator JsonValue(sbyte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005F94 File Offset: 0x00004194
		public static implicit operator JsonValue(short value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005F9C File Offset: 0x0000419C
		public static implicit operator JsonValue(string value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005FA4 File Offset: 0x000041A4
		public static implicit operator JsonValue(uint value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005FAC File Offset: 0x000041AC
		public static implicit operator JsonValue(ulong value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005FB4 File Offset: 0x000041B4
		public static implicit operator JsonValue(ushort value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005FBC File Offset: 0x000041BC
		public static implicit operator JsonValue(DateTime value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005FC4 File Offset: 0x000041C4
		public static implicit operator JsonValue(DateTimeOffset value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005FCC File Offset: 0x000041CC
		public static implicit operator JsonValue(Guid value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005FD4 File Offset: 0x000041D4
		public static implicit operator JsonValue(TimeSpan value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005FDC File Offset: 0x000041DC
		public static implicit operator JsonValue(Uri value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005FE4 File Offset: 0x000041E4
		public static implicit operator bool(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToBoolean(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006009 File Offset: 0x00004209
		public static implicit operator byte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000602E File Offset: 0x0000422E
		public static implicit operator char(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToChar(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006053 File Offset: 0x00004253
		public static implicit operator decimal(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDecimal(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006078 File Offset: 0x00004278
		public static implicit operator double(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDouble(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000609D File Offset: 0x0000429D
		public static implicit operator float(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSingle(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000060C2 File Offset: 0x000042C2
		public static implicit operator int(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000060E7 File Offset: 0x000042E7
		public static implicit operator long(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000610C File Offset: 0x0000430C
		public static implicit operator sbyte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00006131 File Offset: 0x00004331
		public static implicit operator short(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006156 File Offset: 0x00004356
		public static implicit operator string(JsonValue value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString(true);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006164 File Offset: 0x00004364
		public static implicit operator uint(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006189 File Offset: 0x00004389
		public static implicit operator ulong(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000061AE File Offset: 0x000043AE
		public static implicit operator ushort(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000061D3 File Offset: 0x000043D3
		public static implicit operator DateTime(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTime)((JsonPrimitive)value).Value;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000061F3 File Offset: 0x000043F3
		public static implicit operator DateTimeOffset(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTimeOffset)((JsonPrimitive)value).Value;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006213 File Offset: 0x00004413
		public static implicit operator TimeSpan(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (TimeSpan)((JsonPrimitive)value).Value;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006233 File Offset: 0x00004433
		public static implicit operator Guid(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Guid)((JsonPrimitive)value).Value;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006253 File Offset: 0x00004453
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
