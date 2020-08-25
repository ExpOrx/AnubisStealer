using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Anubis
{
	// Token: 0x0200005C RID: 92
	public class JsonPrimitive : JsonValue
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00011FAF File Offset: 0x000101AF
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00011FB8 File Offset: 0x000101B8
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

		// Token: 0x060002D8 RID: 728 RVA: 0x00012009 File Offset: 0x00010209
		public JsonPrimitive(bool value)
		{
			this.value = value;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001201D File Offset: 0x0001021D
		public JsonPrimitive(byte value)
		{
			this.value = value;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00012031 File Offset: 0x00010231
		public JsonPrimitive(char value)
		{
			this.value = value;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00012045 File Offset: 0x00010245
		public JsonPrimitive(decimal value)
		{
			this.value = value;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00012059 File Offset: 0x00010259
		public JsonPrimitive(double value)
		{
			this.value = value;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001206D File Offset: 0x0001026D
		public JsonPrimitive(float value)
		{
			this.value = value;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00012081 File Offset: 0x00010281
		public JsonPrimitive(int value)
		{
			this.value = value;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00012095 File Offset: 0x00010295
		public JsonPrimitive(long value)
		{
			this.value = value;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000120A9 File Offset: 0x000102A9
		public JsonPrimitive(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000120BD File Offset: 0x000102BD
		public JsonPrimitive(short value)
		{
			this.value = value;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000120D1 File Offset: 0x000102D1
		public JsonPrimitive(string value)
		{
			this.value = value;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000120E0 File Offset: 0x000102E0
		public JsonPrimitive(DateTime value)
		{
			this.value = value;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000120F4 File Offset: 0x000102F4
		public JsonPrimitive(uint value)
		{
			this.value = value;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00012108 File Offset: 0x00010308
		public JsonPrimitive(ulong value)
		{
			this.value = value;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001211C File Offset: 0x0001031C
		public JsonPrimitive(ushort value)
		{
			this.value = value;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00012130 File Offset: 0x00010330
		public JsonPrimitive(DateTimeOffset value)
		{
			this.value = value;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00012144 File Offset: 0x00010344
		public JsonPrimitive(Guid value)
		{
			this.value = value;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00012158 File Offset: 0x00010358
		public JsonPrimitive(TimeSpan value)
		{
			this.value = value;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000120D1 File Offset: 0x000102D1
		public JsonPrimitive(Uri value)
		{
			this.value = value;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000120D1 File Offset: 0x000102D1
		public JsonPrimitive(object value)
		{
			this.value = value;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001216C File Offset: 0x0001036C
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

		// Token: 0x060002ED RID: 749 RVA: 0x00012204 File Offset: 0x00010404
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

		// Token: 0x040000FA RID: 250
		private object value;

		// Token: 0x040000FB RID: 251
		private static readonly byte[] true_bytes = Encoding.UTF8.GetBytes("true");

		// Token: 0x040000FC RID: 252
		private static readonly byte[] false_bytes = Encoding.UTF8.GetBytes("false");
	}
}
