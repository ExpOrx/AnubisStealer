using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Anubis
{
	// Token: 0x02000059 RID: 89
	public class JsonArray : JsonValue, IList<JsonValue>, ICollection<JsonValue>, IEnumerable<JsonValue>, IEnumerable
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00011B21 File Offset: 0x0000FD21
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00004F0A File Offset: 0x0000310A
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700007B RID: 123
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00004F2A File Offset: 0x0000312A
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Array;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00011B4B File Offset: 0x0000FD4B
		public JsonArray(params JsonValue[] items)
		{
			this.list = new List<JsonValue>();
			this.AddRange(items);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00011B65 File Offset: 0x0000FD65
		public JsonArray(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list = new List<JsonValue>(items);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00011B87 File Offset: 0x0000FD87
		public void Add(JsonValue item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.list.Add(item);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00011BA3 File Offset: 0x0000FDA3
		public void AddRange(IEnumerable<JsonValue> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.list.AddRange(items);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00011BBF File Offset: 0x0000FDBF
		public void AddRange(params JsonValue[] items)
		{
			if (items != null)
			{
				this.list.AddRange(items);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		public void Clear()
		{
			this.list.Clear();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00011BDD File Offset: 0x0000FDDD
		public bool Contains(JsonValue item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00011BEB File Offset: 0x0000FDEB
		public void CopyTo(JsonValue[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011BFA File Offset: 0x0000FDFA
		public int IndexOf(JsonValue item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00011C08 File Offset: 0x0000FE08
		public void Insert(int index, JsonValue item)
		{
			this.list.Insert(index, item);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00011C17 File Offset: 0x0000FE17
		public bool Remove(JsonValue item)
		{
			return this.list.Remove(item);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011C25 File Offset: 0x0000FE25
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011C34 File Offset: 0x0000FE34
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

		// Token: 0x060002BB RID: 699 RVA: 0x00011CCA File Offset: 0x0000FECA
		IEnumerator<JsonValue> IEnumerable<JsonValue>.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00011CCA File Offset: 0x0000FECA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x040000F8 RID: 248
		private List<JsonValue> list;
	}
}
