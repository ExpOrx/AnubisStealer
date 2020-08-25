using System;

namespace Loki.Gecko
{
	// Token: 0x02000004 RID: 4
	public static class Asn1Der
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00003A14 File Offset: 0x00001C14
		public static Asn1DerObject Parse(byte[] dataToParse)
		{
			Asn1DerObject asn1DerObject = new Asn1DerObject();
			for (int i = 0; i < dataToParse.Length; i++)
			{
				Type type = (Type)dataToParse[i];
				switch (type)
				{
				case Type.Integer:
				{
					asn1DerObject.Objects.Add(new Asn1DerObject
					{
						Type = Type.Integer,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array, 0, length);
					asn1DerObject.Objects[asn1DerObject.Objects.Count - 1].SetObjectData(array);
					i = i + 1 + asn1DerObject.Objects[asn1DerObject.Objects.Count - 1].ObjectLength;
					break;
				}
				case Type.BitString:
				case Type.Null:
					break;
				case Type.OctetString:
				{
					asn1DerObject.Objects.Add(new Asn1DerObject
					{
						Type = Type.OctetString,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array2 = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array2, 0, length);
					asn1DerObject.Objects[asn1DerObject.Objects.Count - 1].SetObjectData(array2);
					i = i + 1 + asn1DerObject.Objects[asn1DerObject.Objects.Count - 1].ObjectLength;
					break;
				}
				case Type.ObjectIdentifier:
				{
					asn1DerObject.Objects.Add(new Asn1DerObject
					{
						Type = Type.ObjectIdentifier,
						ObjectLength = (int)dataToParse[i + 1]
					});
					byte[] array3 = new byte[(int)dataToParse[i + 1]];
					int length = (i + 2 + (int)dataToParse[i + 1] > dataToParse.Length) ? (dataToParse.Length - (i + 2)) : ((int)dataToParse[i + 1]);
					Array.Copy(dataToParse, i + 2, array3, 0, length);
					asn1DerObject.Objects[asn1DerObject.Objects.Count - 1].SetObjectData(array3);
					i = i + 1 + asn1DerObject.Objects[asn1DerObject.Objects.Count - 1].ObjectLength;
					break;
				}
				default:
					if (type == Type.Sequence)
					{
						byte[] array4;
						if (asn1DerObject.ObjectLength == 0)
						{
							asn1DerObject.Type = Type.Sequence;
							asn1DerObject.ObjectLength = dataToParse.Length - (i + 2);
							array4 = new byte[asn1DerObject.ObjectLength];
						}
						else
						{
							asn1DerObject.Objects.Add(new Asn1DerObject
							{
								Type = Type.Sequence,
								ObjectLength = (int)dataToParse[i + 1]
							});
							array4 = new byte[(int)dataToParse[i + 1]];
						}
						if (array4.Length <= dataToParse.Length - (i + 2))
						{
							int num = array4.Length;
						}
						else
						{
							int num2 = dataToParse.Length;
						}
						Array.Copy(dataToParse, i + 2, array4, 0, array4.Length);
						asn1DerObject.Objects.Add(Asn1Der.Parse(array4));
						i = i + 1 + (int)dataToParse[i + 1];
					}
					break;
				}
			}
			return asn1DerObject;
		}
	}
}
