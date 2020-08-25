using System;
using System.IO;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Anubis
{
	// Token: 0x02000055 RID: 85
	public class CNT
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00010039 File Offset: 0x0000E239
		private byte[] DataArray { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00010041 File Offset: 0x0000E241
		private ulong DataEncoding { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00010049 File Offset: 0x0000E249
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00010051 File Offset: 0x0000E251
		public string[] Fields { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0001005A File Offset: 0x0000E25A
		public int RowLength
		{
			get
			{
				return this.SqlRows.Length;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00010064 File Offset: 0x0000E264
		private ushort PageSize { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0001006C File Offset: 0x0000E26C
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00010074 File Offset: 0x0000E274
		private FF[] DataEntries { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0001007D File Offset: 0x0000E27D
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00010085 File Offset: 0x0000E285
		private ROW[] SqlRows { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0001008E File Offset: 0x0000E28E
		private byte[] SQLDataTypeSize { get; }

		// Token: 0x0600027C RID: 636 RVA: 0x00010098 File Offset: 0x0000E298
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

		// Token: 0x0600027D RID: 637 RVA: 0x0001015C File Offset: 0x0000E35C
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

		// Token: 0x0600027E RID: 638 RVA: 0x000101CE File Offset: 0x0000E3CE
		public string ParseValue(int rowIndex, int fieldIndex)
		{
			if (rowIndex >= this.SqlRows.Length)
			{
				return null;
			}
			if (fieldIndex >= this.SqlRows[rowIndex].RowData.Length)
			{
				return null;
			}
			return this.SqlRows[rowIndex].RowData[fieldIndex];
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00010208 File Offset: 0x0000E408
		public string ParseValue(int rowIndex, string fieldName)
		{
			int num = -1;
			int num2 = this.Fields.Length - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (this.Fields[i].ToLower().Trim().CompareTo(fieldName.ToLower().Trim()) == 0)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return null;
			}
			return this.ParseValue(rowIndex, num);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00010264 File Offset: 0x0000E464
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
				this.Fields = (string[])Utils.CopyArray(this.Fields, new string[j + 1]);
				this.Fields[j] = array[j];
			}
			return this.ReadDataEntriesFromOffsets((ulong)((this.DataEntries[num].RootNum - 1L) * (long)((ulong)this.PageSize)));
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000103AC File Offset: 0x0000E5AC
		private ulong ToUInt64(int startIndex, int Size)
		{
			if (Size > 8 || Size == 0)
			{
				return 0UL;
			}
			ulong num = 0UL;
			for (int i = 0; i <= Size - 1; i++)
			{
				num = (num << 8 | (ulong)this.DataArray[startIndex + i]);
			}
			return num;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000103E8 File Offset: 0x0000E5E8
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

		// Token: 0x06000283 RID: 643 RVA: 0x000104F0 File Offset: 0x0000E6F0
		private int GetValues(int startIndex)
		{
			if (startIndex > this.DataArray.Length)
			{
				return 0;
			}
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

		// Token: 0x06000284 RID: 644 RVA: 0x000072EF File Offset: 0x000054EF
		public static bool ItIsOdd(long value)
		{
			return (value & 1L) == 1L;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00010544 File Offset: 0x0000E744
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
							if (CNT.ItIsOdd(array[num7]))
							{
								array[num7] = (long)Math.Round((double)(array[num7] - 13L) / 2.0);
							}
							else
							{
								array[num7] = (long)Math.Round((double)(array[num7] - 12L) / 2.0);
							}
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
			if (this.DataArray[(int)((uint)Offset)] != 5)
			{
				return;
			}
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

		// Token: 0x06000286 RID: 646 RVA: 0x00010B5C File Offset: 0x0000ED5C
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
							if (CNT.ItIsOdd(array[num9].Type))
							{
								array[num9].Size = (long)Math.Round((double)(array[num9].Type - 13L) / 2.0);
							}
							else
							{
								array[num9].Size = (long)Math.Round((double)(array[num9].Type - 12L) / 2.0);
							}
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
	}
}
