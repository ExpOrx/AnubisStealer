using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Anubis
{
	// Token: 0x0200005B RID: 91
	public class JsonObject : JsonValue, IDictionary<string, JsonValue>, ICollection<KeyValuePair<string, JsonValue>>, IEnumerable<KeyValuePair<string, JsonValue>>, IEnumerable
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00011CF6 File Offset: 0x0000FEF6
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x1700007E RID: 126
		public sealed override JsonValue this[string key]
		{
			get
			{
				return this.map[key];
			}
			set
			{
				this.map[key] = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00005104 File Offset: 0x00003304
		public override JsonType JsonType
		{
			get
			{
				return JsonType.Object;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00011D20 File Offset: 0x0000FF20
		public ICollection<string> Keys
		{
			get
			{
				return this.map.Keys;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00011D2D File Offset: 0x0000FF2D
		public ICollection<JsonValue> Values
		{
			get
			{
				return this.map.Values;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00004F0A File Offset: 0x0000310A
		bool ICollection<KeyValuePair<string, JsonValue>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011D3A File Offset: 0x0000FF3A
		public JsonObject(params KeyValuePair<string, JsonValue>[] items)
		{
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			if (items != null)
			{
				this.AddRange(items);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public JsonObject(IEnumerable<KeyValuePair<string, JsonValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.map = new SortedDictionary<string, JsonValue>(StringComparer.Ordinal);
			this.AddRange(items);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011D89 File Offset: 0x0000FF89
		public IEnumerator<KeyValuePair<string, JsonValue>> GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00011D89 File Offset: 0x0000FF89
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.map.GetEnumerator();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011D9B File Offset: 0x0000FF9B
		public void Add(string key, JsonValue value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.map.Add(key, value);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		public void Add(KeyValuePair<string, JsonValue> pair)
		{
			this.Add(pair.Key, pair.Value);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00011DD0 File Offset: 0x0000FFD0
		public void AddRange(IEnumerable<KeyValuePair<string, JsonValue>> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			foreach (KeyValuePair<string, JsonValue> keyValuePair in items)
			{
				this.map.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00011E38 File Offset: 0x00010038
		public void AddRange(params KeyValuePair<string, JsonValue>[] items)
		{
			this.AddRange(items);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00011E41 File Offset: 0x00010041
		public void Clear()
		{
			this.map.Clear();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011E4E File Offset: 0x0001004E
		bool ICollection<KeyValuePair<string, JsonValue>>.Contains(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Contains(item);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00011E5C File Offset: 0x0001005C
		bool ICollection<KeyValuePair<string, JsonValue>>.Remove(KeyValuePair<string, JsonValue> item)
		{
			return ((ICollection<KeyValuePair<string, JsonValue>>)this.map).Remove(item);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00011E6A File Offset: 0x0001006A
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.ContainsKey(key);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00011E86 File Offset: 0x00010086
		public void CopyTo(KeyValuePair<string, JsonValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<string, JsonValue>>)this.map).CopyTo(array, arrayIndex);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011E95 File Offset: 0x00010095
		public bool Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.map.Remove(key);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011EB4 File Offset: 0x000100B4
		public override void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			stream.WriteByte(123);
			foreach (KeyValuePair<string, JsonValue> keyValuePair in this.map)
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(base.EscapeString(keyValuePair.Key));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				stream.WriteByte(44);
				stream.WriteByte(32);
				if (keyValuePair.Value == null)
				{
					stream.WriteByte(110);
					stream.WriteByte(117);
					stream.WriteByte(108);
					stream.WriteByte(108);
				}
				else
				{
					keyValuePair.Value.Save(stream, parsing);
				}
			}
			stream.WriteByte(125);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011FA0 File Offset: 0x000101A0
		public bool TryGetValue(string key, out JsonValue value)
		{
			return this.map.TryGetValue(key, out value);
		}

		// Token: 0x040000F9 RID: 249
		private SortedDictionary<string, JsonValue> map;
	}
}
