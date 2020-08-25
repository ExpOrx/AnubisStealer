using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Loki.Gecko
{
	// Token: 0x0200000B RID: 11
	public class BerkeleyDB
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004394 File Offset: 0x00002594
		// (set) Token: 0x06000058 RID: 88 RVA: 0x0000439C File Offset: 0x0000259C
		public string Version { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000043A5 File Offset: 0x000025A5
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000043AD File Offset: 0x000025AD
		public List<KeyValuePair<string, string>> Keys { get; private set; }

		// Token: 0x0600005B RID: 91 RVA: 0x000043B8 File Offset: 0x000025B8
		public BerkeleyDB(string FileName)
		{
			List<byte> list = new List<byte>();
			this.Keys = new List<KeyValuePair<string, string>>();
			using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(FileName)))
			{
				int i = 0;
				int num = (int)binaryReader.BaseStream.Length;
				while (i < num)
				{
					list.Add(binaryReader.ReadByte());
					i++;
				}
			}
			string value = BitConverter.ToString(this.Extract(list.ToArray(), 0, 4, false)).Replace("-", "");
			string text = BitConverter.ToString(this.Extract(list.ToArray(), 4, 4, false)).Replace("-", "");
			int num2 = BitConverter.ToInt32(this.Extract(list.ToArray(), 12, 4, true), 0);
			if (!string.IsNullOrEmpty(value))
			{
				this.Version = "Berkelet DB";
				if (text.Equals("00000002"))
				{
					this.Version += " 1.85 (Hash, version 2, native byte-order)";
				}
				int num3 = int.Parse(BitConverter.ToString(this.Extract(list.ToArray(), 56, 4, false)).Replace("-", ""));
				int num4 = 1;
				while (this.Keys.Count < num3)
				{
					string[] array = new string[(num3 - this.Keys.Count) * 2];
					for (int j = 0; j < (num3 - this.Keys.Count) * 2; j++)
					{
						array[j] = BitConverter.ToString(this.Extract(list.ToArray(), num2 * num4 + 2 + j * 2, 2, true)).Replace("-", "");
					}
					Array.Sort<string>(array);
					for (int k = 0; k < array.Length; k += 2)
					{
						int num5 = Convert.ToInt32(array[k], 16) + num2 * num4;
						int num6 = Convert.ToInt32(array[k + 1], 16) + num2 * num4;
						int num7 = (k + 2 >= array.Length) ? (num2 + num2 * num4) : (Convert.ToInt32(array[k + 2], 16) + num2 * num4);
						string @string = Encoding.ASCII.GetString(this.Extract(list.ToArray(), num6, num7 - num6, false));
						string value2 = BitConverter.ToString(this.Extract(list.ToArray(), num5, num6 - num5, false));
						if (!string.IsNullOrEmpty(@string))
						{
							this.Keys.Add(new KeyValuePair<string, string>(@string, value2));
						}
					}
					num4++;
				}
				return;
			}
			this.Version = "Unknow database format";
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000464C File Offset: 0x0000284C
		private byte[] Extract(byte[] source, int start, int length, bool littleEndian)
		{
			byte[] array = new byte[length];
			int num = 0;
			for (int i = start; i < start + length; i++)
			{
				array[num] = source[i];
				num++;
			}
			if (littleEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}
	}
}
