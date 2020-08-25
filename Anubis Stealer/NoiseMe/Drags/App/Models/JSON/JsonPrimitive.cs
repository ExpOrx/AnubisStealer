using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x02000011 RID: 17
	public class JsonPrimitive : JsonValue
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00005397 File Offset: 0x00003597
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000053A0 File Offset: 0x000035A0
		public override JsonType JsonType
		{
			get
			{
				if (this.value == null)
				{
					return JsonType.String;
				}
				TypeCode typeCode = Type.GetTypeCode(this.value.GetType());
				switch (typeCode)
				{
				case TypeCode.Object:
				case TypeCode.Char:
					break;
				case TypeCode.DBNull:
					return JsonType.Number;
				case TypeCode.Boolean:
					return JsonType.Boolean;
				default:
					if (typeCode != TypeCode.DateTime && typeCode != TypeCode.String)
					{
						return JsonType.Number;
					}
					break;
				}
				return JsonType.String;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000053F1 File Offset: 0x000035F1
		public JsonPrimitive(bool value)
		{
			this.value = value;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005405 File Offset: 0x00003605
		public JsonPrimitive(byte value)
		{
			this.value = value;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005419 File Offset: 0x00003619
		public JsonPrimitive(char value)
		{
			this.value = value;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000542D File Offset: 0x0000362D
		public JsonPrimitive(decimal value)
		{
			this.value = value;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005441 File Offset: 0x00003641
		public JsonPrimitive(double value)
		{
			this.value = value;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005455 File Offset: 0x00003655
		public JsonPrimitive(float value)
		{
			this.value = value;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005469 File Offset: 0x00003669
		public JsonPrimitive(int value)
		{
			this.value = value;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000547D File Offset: 0x0000367D
		public JsonPrimitive(long value)
		{
			this.value = value;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005491 File Offset: 0x00003691
		public JsonPrimitive(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000054A5 File Offset: 0x000036A5
		public JsonPrimitive(short value)
		{
			this.value = value;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000054B9 File Offset: 0x000036B9
		public JsonPrimitive(string value)
		{
			this.value = value;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000054C8 File Offset: 0x000036C8
		public JsonPrimitive(DateTime value)
		{
			this.value = value;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000054DC File Offset: 0x000036DC
		public JsonPrimitive(uint value)
		{
			this.value = value;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000054F0 File Offset: 0x000036F0
		public JsonPrimitive(ulong value)
		{
			this.value = value;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005504 File Offset: 0x00003704
		public JsonPrimitive(ushort value)
		{
			this.value = value;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005518 File Offset: 0x00003718
		public JsonPrimitive(DateTimeOffset value)
		{
			this.value = value;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000552C File Offset: 0x0000372C
		public JsonPrimitive(Guid value)
		{
			this.value = value;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005540 File Offset: 0x00003740
		public JsonPrimitive(TimeSpan value)
		{
			this.value = value;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000054B9 File Offset: 0x000036B9
		public JsonPrimitive(Uri value)
		{
			this.value = value;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000054B9 File Offset: 0x000036B9
		public JsonPrimitive(object value)
		{
			this.value = value;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005554 File Offset: 0x00003754
		public override void Save(Stream stream, bool parsing)
		{
			JsonType jsonType = this.JsonType;
			if (jsonType == JsonType.String)
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(base.EscapeString(this.value.ToString()));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				return;
			}
			if (jsonType != JsonType.Boolean)
			{
				byte[] bytes2 = Encoding.UTF8.GetBytes(this.GetFormattedString());
				stream.Write(bytes2, 0, bytes2.Length);
				return;
			}
			if ((bool)this.value)
			{
				stream.Write(JsonPrimitive.true_bytes, 0, 4);
				return;
			}
			stream.Write(JsonPrimitive.false_bytes, 0, 5);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000055EC File Offset: 0x000037EC
		public string GetFormattedString()
		{
			JsonType jsonType = this.JsonType;
			if (jsonType != JsonType.String)
			{
				if (jsonType != JsonType.Number)
				{
					throw new InvalidOperationException();
				}
				string text = (!(this.value is float) && !(this.value is double)) ? ((IFormattable)this.value).ToString("G", NumberFormatInfo.InvariantInfo) : ((IFormattable)this.value).ToString("R", NumberFormatInfo.InvariantInfo);
				if (text == "NaN" || text == "Infinity" || text == "-Infinity")
				{
					return "\"" + text + "\"";
				}
				return text;
			}
			else if (this.value is string || this.value == null)
			{
				string text2 = this.value as string;
				if (string.IsNullOrEmpty(text2))
				{
					return "null";
				}
				return text2.Trim(new char[]
				{
					'"'
				});
			}
			else
			{
				if (this.value is char)
				{
					return this.value.ToString();
				}
				string str = "GetFormattedString from value type ";
				Type type = this.value.GetType();
				throw new NotImplementedException(str + ((type != null) ? type.ToString() : null));
			}
		}

		// Token: 0x0400003A RID: 58
		private object value;

		// Token: 0x0400003B RID: 59
		private static readonly byte[] true_bytes = Encoding.UTF8.GetBytes("true");

		// Token: 0x0400003C RID: 60
		private static readonly byte[] false_bytes = Encoding.UTF8.GetBytes("false");
	}
}
