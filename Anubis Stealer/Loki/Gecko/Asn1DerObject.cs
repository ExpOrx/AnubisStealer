using System;
using System.Collections.Generic;
using System.Text;

namespace Loki.Gecko
{
	// Token: 0x02000007 RID: 7
	public class Asn1DerObject
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00003D21 File Offset: 0x00001F21
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00003D29 File Offset: 0x00001F29
		public Type Type { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x00003D32 File Offset: 0x00001F32
		public byte[] GetObjectData()
		{
			return this.Lenght;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003D3A File Offset: 0x00001F3A
		public void SetObjectData(byte[] value)
		{
			this.Lenght = value;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00003D43 File Offset: 0x00001F43
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00003D4B File Offset: 0x00001F4B
		public int ObjectLength { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003D54 File Offset: 0x00001F54
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00003D5C File Offset: 0x00001F5C
		public List<Asn1DerObject> Objects { get; set; }

		// Token: 0x06000028 RID: 40 RVA: 0x00003D65 File Offset: 0x00001F65
		public Asn1DerObject()
		{
			this.Objects = new List<Asn1DerObject>();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003D78 File Offset: 0x00001F78
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			Type type = this.Type;
			switch (type)
			{
			case Type.Integer:
				foreach (byte b in this.GetObjectData())
				{
					stringBuilder2.AppendFormat("{0:X2}", b);
				}
				stringBuilder.Append("\tINTEGER ").Append(stringBuilder2).AppendLine();
				break;
			case Type.BitString:
			case Type.Null:
				break;
			case Type.OctetString:
				foreach (byte b2 in this.GetObjectData())
				{
					stringBuilder2.AppendFormat("{0:X2}", b2);
				}
				stringBuilder.Append("\tOCTETSTRING ").AppendLine(stringBuilder2.ToString());
				break;
			case Type.ObjectIdentifier:
				foreach (byte b3 in this.GetObjectData())
				{
					stringBuilder2.AppendFormat("{0:X2}", b3);
				}
				stringBuilder.Append("\tOBJECTIDENTIFIER ").AppendLine(stringBuilder2.ToString());
				break;
			default:
				if (type == Type.Sequence)
				{
					stringBuilder.AppendLine("SEQUENCE {");
				}
				break;
			}
			foreach (Asn1DerObject asn1DerObject in this.Objects)
			{
				stringBuilder.Append(asn1DerObject.ToString());
			}
			if (this.Type == Type.Sequence)
			{
				stringBuilder.AppendLine("}");
			}
			stringBuilder2.Remove(0, stringBuilder2.Length - 1);
			return stringBuilder.ToString();
		}

		// Token: 0x04000016 RID: 22
		private byte[] Lenght;
	}
}
