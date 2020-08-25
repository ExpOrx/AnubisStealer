using System;
using System.IO;
using System.Text;
using loki.sqlite.strings;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace loki.sqlite
{
	// Token: 0x0200001D RID: 29
	public class CNT
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006DE4 File Offset: 0x00004FE4
		private byte[] DataArray { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006DEC File Offset: 0x00004FEC
		private ulong DataEncoding { get; }

		// Token: 0x06000112 RID: 274 RVA: 0x00006DF4 File Offset: 0x00004FF4
		public string[] GetFields()
		{
			return this.fields;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006DFC File Offset: 0x00004FFC
		public void SetFields(string[] value)
		{
			this.fields = value;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006E05 File Offset: 0x00005005
		public int RowLength
		{
			get
			{
				return this.SqlRows.Length;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006E0F File Offset: 0x0000500F
		private ushort PageSize { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006E17 File Offset: 0x00005017
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00006E1F File Offset: 0x0000501F
		private FF[] DataEntries { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006E28 File Offset: 0x00005028
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00006E30 File Offset: 0x00005030
		private ROW[] SqlRows { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006E39 File Offset: 0x00005039
		private byte[] SQLDataTypeSize { get; }

		// Token: 0x0600011B RID: 283 RVA: 0x00006E44 File Offset: 0x00005044
		public CNT(string baseName)
		{
			this.SQLDataTypeSize = new byte[]
			{
				0,
				1,
				2,
				3,
				4,
				6,
				8,
				8,
				0,
				0
			};
			if (File.Exists(baseName))
			{
				FileSystem.FileOpen(1, baseName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
				string s = Strings.Space((int)FileSystem.LOF(1));
				FileSystem.FileGet(1, ref s, -1L, false);
				FileSystem.FileClose(new int[]
				{
					1
				});
				this.DataArray = Encoding.Default.GetBytes(s);
				this.PageSize = (ushort)this.ToUInt64(16, 2);
				this.DataEncoding = this.ToUInt64(56, 4);
				if (decimal.Compare(new decimal(this.DataEncoding), 0m) == 0)
				{
					this.DataEncoding = 1L;
				}
				this.ReadDataEntries(100UL);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006F08 File Offset: 0x00005108
		public string[] ParseTables()
		{
			string[] array = null;
			int num = 0;
			int num2 = this.DataEntries.Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (this.DataEntries[i].Type == "table")
				{
					array = (string[])Utils.CopyArray(array, new string[num + 1]);
					array[num] = this.DataEntries[i].Name;
					num++;
				}
			}
			return array;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006F7A File Offset: 0x0000517A
		public string ParseValue(int rowIndex, int fieldIndex)
		{
			if (rowIndex >= this.SqlRows.Length)
			{
				return null;
			}
			if (fieldIndex < this.SqlRows[rowIndex].RowData.Length)
			{
				return this.SqlRows[rowIndex].RowData[fieldIndex];
			}
			return null;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006FB4 File Offset: 0x000051B4
		public string ParseValue(int rowIndex, string fieldName)
		{
			int num = -1;
			int num2 = this.GetFields().Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (this.GetFields()[i].ToLower().Trim().CompareTo(fieldName.ToLower().Trim()) == 0)
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				return this.ParseValue(rowIndex, num);
			}
			return null;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007010 File Offset: 0x00005210
		public bool ReadTable(string tableName)
		{
			int num = -1;
			int num2 = this.DataEntries.Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (this.DataEntries[i].Name.ToLower().CompareTo(tableName.ToLower()) == 0)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return false;
			}
			string[] array = this.DataEntries[num].SqlStatement.Substring(this.DataEntries[num].SqlStatement.IndexOf("(") + 1).Split(new char[]
			{
				','
			});
			int num3 = array.Length - 1;
			for (int j = 0; j <= num3; j++)
			{
				array[j] = array[j].TrimStart(Array.Empty<char>());
				int num4 = array[j].IndexOf(" ");
				if (num4 > 0)
				{
					array[j] = array[j].Substring(0, num4);
				}
				if (array[j].IndexOf("UNIQUE") == 0)
				{
					break;
				}
				this.SetFields((string[])Utils.CopyArray(this.GetFields(), new string[j + 1]));
				this.GetFields()[j] = array[j];
			}
			return this.ReadDataEntriesFromOffsets((ulong)((this.DataEntries[num].RootNum - 1L) * (long)((ulong)this.PageSize)));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007158 File Offset: 0x00005358
		private ulong ToUInt64(int startIndex, int Size)
		{
			if (Size <= 8 && Size != 0)
			{
				ulong num = 0UL;
				for (int i = 0; i <= Size - 1; i++)
				{
					num = (num << 8 | (ulong)this.DataArray[startIndex + i]);
				}
				return num;
			}
			return 0UL;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007194 File Offset: 0x00005394
		private long CalcVertical(int startIndex, int endIndex)
		{
			endIndex++;
			byte[] array = new byte[8];
			int num = endIndex - startIndex;
			bool flag = false;
			if (num == 0 | num > 9)
			{
				return 0L;
			}
			if (num != 1)
			{
				if (num == 9)
				{
					flag = true;
				}
				int num2 = 1;
				int num3 = 7;
				int num4 = 0;
				if (flag)
				{
					array[0] = this.DataArray[endIndex - 1];
					endIndex--;
					num4 = 1;
				}
				for (int i = endIndex - 1; i >= startIndex; i += -1)
				{
					if (i - 1 >= startIndex)
					{
						array[num4] = (byte)(((int)((byte)(this.DataArray[i] >> (num2 - 1 & 7))) & 255 >> num2) | (int)((byte)(this.DataArray[i - 1] << (num3 & 7))));
						num2++;
						num4++;
						num3--;
					}
					else if (!flag)
					{
						array[num4] = (byte)((int)((byte)(this.DataArray[i] >> (num2 - 1 & 7))) & 255 >> num2);
					}
				}
				return BitConverter.ToInt64(array, 0);
			}
			array[0] = (this.DataArray[startIndex] & 127);
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000729C File Offset: 0x0000549C
		private int GetValues(int startIndex)
		{
			if (startIndex <= this.DataArray.Length)
			{
				int num = startIndex + 8;
				for (int i = startIndex; i <= num; i++)
				{
					if (i > this.DataArray.Length - 1)
					{
						return 0;
					}
					if ((this.DataArray[i] & 128) != 128)
					{
						return i;
					}
				}
				return startIndex + 8;
			}
			return 0;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000072EF File Offset: 0x000054EF
		public static bool ItIsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000072FC File Offset: 0x000054FC
		private void ReadDataEntries(ulong Offset)
		{
			if (this.DataArray[(int)((uint)Offset)] == 13)
			{
				ushort num = (this.ToUInt64((Offset.ForceTo<decimal>() + 3m).ForceTo<int>(), 2).ForceTo<decimal>() - 1m).ForceTo<ushort>();
				int num2 = 0;
				if (this.DataEntries != null)
				{
					num2 = this.DataEntries.Length;
					this.DataEntries = (FF[])Utils.CopyArray(this.DataEntries, new FF[this.DataEntries.Length + (int)num + 1]);
				}
				else
				{
					this.DataEntries = new FF[(int)(num + 1)];
				}
				int num3 = (int)num;
				for (int i = 0; i <= num3; i++)
				{
					ulong num4 = this.ToUInt64((Offset.ForceTo<decimal>() + 8m + (i * 2).ForceTo<decimal>()).ForceTo<int>(), 2);
					if (decimal.Compare(Offset.ForceTo<decimal>(), 100m) != 0)
					{
						num4 += Offset;
					}
					int num5 = this.GetValues(num4.ForceTo<int>());
					this.CalcVertical(num4.ForceTo<int>(), num5);
					int num6 = this.GetValues((num4.ForceTo<decimal>() + num5.ForceTo<decimal>() - num4.ForceTo<decimal>() + 1m).ForceTo<int>());
					this.DataEntries[num2 + i].ID = this.CalcVertical((num4.ForceTo<decimal>() + num5.ForceTo<decimal>() - num4.ForceTo<decimal>() + 1m).ForceTo<int>(), num6);
					num4 = (num4.ForceTo<decimal>() + num6.ForceTo<decimal>() - num4.ForceTo<decimal>() + 1m).ForceTo<ulong>();
					num5 = this.GetValues(num4.ForceTo<int>());
					num6 = num5;
					long value = this.CalcVertical(num4.ForceTo<int>(), num5);
					long[] array = new long[5];
					int num7 = 0;
					do
					{
						num5 = num6 + 1;
						num6 = this.GetValues(num5);
						array[num7] = this.CalcVertical(num5, num6);
						if (array[num7] > 9L)
						{
							array[num7] = (CNT.ItIsOdd(array[num7]) ? ((long)Math.Round((double)(array[num7] - 13L) / 2.0)) : ((long)Math.Round((double)(array[num7] - 12L) / 2.0)));
						}
						else
						{
							array[num7] = (long)((ulong)this.SQLDataTypeSize[(int)array[num7]]);
						}
						num7++;
					}
					while (num7 <= 4);
					Encoding encoding = Encoding.Default;
					decimal value2 = this.DataEncoding.ForceTo<decimal>();
					if (!1m.Equals(value2))
					{
						if (!2m.Equals(value2))
						{
							if (3m.Equals(value2))
							{
								encoding = Encoding.BigEndianUnicode;
							}
						}
						else
						{
							encoding = Encoding.Unicode;
						}
					}
					else
					{
						encoding = Encoding.Default;
					}
					this.DataEntries[num2 + i].Type = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
					this.DataEntries[num2 + i].Name = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
					this.DataEntries[num2 + i].RootNum = (long)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
					this.DataEntries[num2 + i].SqlStatement = encoding.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
				}
				return;
			}
			if (this.DataArray[(int)((uint)Offset)] == 5)
			{
				int num8 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
				for (int j = 0; j <= num8; j++)
				{
					ushort num9 = (ushort)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(j * 2))), 2);
					if (decimal.Compare(new decimal(Offset), 100m) == 0)
					{
						this.ReadDataEntries(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64((int)num9, 4)), 1m), new decimal((int)this.PageSize))));
					}
					else
					{
						this.ReadDataEntries(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64((int)(Offset + (ulong)num9), 4)), 1m), new decimal((int)this.PageSize))));
					}
				}
				this.ReadDataEntries(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.PageSize))));
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007910 File Offset: 0x00005B10
		private bool ReadDataEntriesFromOffsets(ulong Offset)
		{
			if (this.DataArray[(int)((uint)Offset)] == 13)
			{
				int num = Convert.ToInt32(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
				int num2 = 0;
				if (this.SqlRows != null)
				{
					num2 = this.SqlRows.Length;
					this.SqlRows = (ROW[])Utils.CopyArray(this.SqlRows, new ROW[this.SqlRows.Length + num + 1]);
				}
				else
				{
					this.SqlRows = new ROW[num + 1];
				}
				int num3 = num;
				for (int i = 0; i <= num3; i++)
				{
					SZ[] array = null;
					ulong num4 = this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(i * 2))), 2);
					if (decimal.Compare(new decimal(Offset), 100m) != 0)
					{
						num4 += Offset;
					}
					int num5 = this.GetValues((int)num4);
					this.CalcVertical((int)num4, num5);
					int num6 = this.GetValues(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
					this.SqlRows[num2 + i].ID = this.CalcVertical(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
					num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
					num5 = this.GetValues((int)num4);
					num6 = num5;
					long num7 = this.CalcVertical((int)num4, num5);
					long num8 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num4), new decimal(num5)), 1m));
					int num9 = 0;
					while (num8 < num7)
					{
						array = (SZ[])Utils.CopyArray(array, new SZ[num9 + 1]);
						num5 = num6 + 1;
						num6 = this.GetValues(num5);
						array[num9].Type = this.CalcVertical(num5, num6);
						if (array[num9].Type > 9L)
						{
							array[num9].Size = (CNT.ItIsOdd(array[num9].Type) ? ((long)Math.Round((double)(array[num9].Type - 13L) / 2.0)) : ((long)Math.Round((double)(array[num9].Type - 12L) / 2.0)));
						}
						else
						{
							array[num9].Size = (long)((ulong)this.SQLDataTypeSize[(int)array[num9].Type]);
						}
						num8 = num8 + (long)(num6 - num5) + 1L;
						num9++;
					}
					this.SqlRows[num2 + i].RowData = new string[array.Length - 1 + 1];
					int num10 = 0;
					int num11 = array.Length - 1;
					for (int j = 0; j <= num11; j++)
					{
						if (array[j].Type > 9L)
						{
							if (!CNT.ItIsOdd(array[j].Type))
							{
								if (decimal.Compare(new decimal(this.DataEncoding), 1m) == 0)
								{
									this.SqlRows[num2 + i].RowData[j] = Encoding.Default.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
								}
								else if (decimal.Compare(new decimal(this.DataEncoding), 2m) == 0)
								{
									this.SqlRows[num2 + i].RowData[j] = Encoding.Unicode.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
								}
								else if (decimal.Compare(new decimal(this.DataEncoding), 3m) == 0)
								{
									this.SqlRows[num2 + i].RowData[j] = Encoding.BigEndianUnicode.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
								}
							}
							else
							{
								this.SqlRows[num2 + i].RowData[j] = Encoding.Default.GetString(this.DataArray, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size);
							}
						}
						else
						{
							this.SqlRows[num2 + i].RowData[j] = Convert.ToString(this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].Size));
						}
						num10 += (int)array[j].Size;
					}
				}
			}
			else if (this.DataArray[(int)((uint)Offset)] == 5)
			{
				int num12 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
				for (int k = 0; k <= num12; k++)
				{
					ushort num13 = (ushort)this.ToUInt64(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(k * 2))), 2);
					this.ReadDataEntriesFromOffsets(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64((int)(Offset + (ulong)num13), 4)), 1m), new decimal((int)this.PageSize))));
				}
				this.ReadDataEntriesFromOffsets(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ToUInt64(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.PageSize))));
			}
			return true;
		}

		// Token: 0x04000059 RID: 89
		private string[] fields;
	}
}
