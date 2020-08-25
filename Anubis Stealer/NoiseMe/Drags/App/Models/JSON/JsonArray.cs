using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x0200000E RID: 14
	public class JsonArray : JsonValue, IList<JsonValue>, ICollection<JsonValue>, IEnumerable<JsonValue>, IEnumerable
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004EFD File Offset: 0x000030FD
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00004F0A File Offset: 0x0000310A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001F RID: 31
		public sealed override JsonValue this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004F2A File Offset: 0x0000312A
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Array;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004F2D File Offset: 0x0000312D
		public JsonArray(params JsonValue[] items)
		{
			this.list = new List<JsonValue>();
			this.AddRange(items);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004F47 File Offset: 0x00003147
		public JsonArray(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list = new List<JsonValue>(items);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004F69 File Offset: 0x00003169
		public void Add(JsonValue item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.list.Add(item);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004F85 File Offset: 0x00003185
		public void AddRange(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list.AddRange(items);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004FA1 File Offset: 0x000031A1
		public void AddRange(params JsonValue[] items)
		{
			if (items != null)
			{
				this.list.AddRange(items);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004FB2 File Offset: 0x000031B2
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004FBF File Offset: 0x000031BF
		public bool Contains(JsonValue item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004FCD File Offset: 0x000031CD
		public void CopyTo(JsonValue[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004FDC File Offset: 0x000031DC
		public int IndexOf(JsonValue item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004FEA File Offset: 0x000031EA
		public void Insert(int index, JsonValue item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004FF9 File Offset: 0x000031F9
		public bool Remove(JsonValue item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005007 File Offset: 0x00003207
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005018 File Offset: 0x00003218
		public override void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.WriteByte(91);
			for (int i = 0; i < this.list.Count; i++)
			{
				JsonValue jsonValue = this.list[i];
				if (jsonValue != null)
				{
					jsonValue.Save(stream, parsing);
				}
				else
				{
					stream.WriteByte(110);
					stream.WriteByte(117);
					stream.WriteByte(108);
					stream.WriteByte(108);
				}
				if (i < this.Count - 1)
				{
					stream.WriteByte(44);
					stream.WriteByte(32);
				}
			}
			stream.WriteByte(93);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000050AE File Offset: 0x000032AE
		IEnumerator<JsonValue> IEnumerable<JsonValue>.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000050AE File Offset: 0x000032AE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04000038 RID: 56
		private List<JsonValue> list;
	}
}
