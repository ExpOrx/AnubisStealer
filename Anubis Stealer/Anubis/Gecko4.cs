using System;
using System.Collections.Generic;
using System.Text;

namespace Anubis
{
	// Token: 0x0200004F RID: 79
	public class Gecko4
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000F6A2 File Offset: 0x0000D8A2
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000F6AA File Offset: 0x0000D8AA
		public Gecko2 ObjectType { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000F6B3 File Offset: 0x0000D8B3
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000F6BB File Offset: 0x0000D8BB
		public byte[] ObjectData { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public int ObjectLength { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000F6DD File Offset: 0x0000D8DD
		public List<Gecko4> Objects { get; set; }

		// Token: 0x0600023F RID: 575 RVA: 0x0000F6E6 File Offset: 0x0000D8E6
		public Gecko4()
		{
			this.Objects = new List<Gecko4>();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			Gecko2 objectType = this.ObjectType;
			switch (objectType)
			{
			case Gecko2.Integer:
				foreach (byte b in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b);
				}
				stringBuilder.Append("\tINTEGER ").Append(stringBuilder2).AppendLine();
				break;
			case Gecko2.BitString:
			case Gecko2.Null:
				break;
			case Gecko2.OctetString:
				foreach (byte b2 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b2);
				}
				stringBuilder.Append("\tOCTETSTRING ").AppendLine(stringBuilder2.ToString());
				break;
			case Gecko2.ObjectIdentifier:
				foreach (byte b3 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b3);
				}
				stringBuilder.Append("\tOBJECTIDENTIFIER ").AppendLine(stringBuilder2.ToString());
				break;
			default:
				if (objectType == Gecko2.Sequence)
				{
					stringBuilder.AppendLine("SEQUENCE {");
				}
				break;
			}
			foreach (Gecko4 gecko in this.Objects)
			{
				stringBuilder.Append(gecko.ToString());
			}
			if (this.ObjectType == Gecko2.Sequence)
			{
				stringBuilder.AppendLine("}");
			}
			stringBuilder2.Remove(0, stringBuilder2.Length - 1);
			return stringBuilder.ToString();
		}
	}
}
