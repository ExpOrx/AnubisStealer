using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Anubis
{
	// Token: 0x02000058 RID: 88
	public class JavaScriptReader
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00011330 File Offset: 0x0000F530
		public JavaScriptReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.SBuilder = new StringBuilder();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001135F File Offset: 0x0000F55F
		public object Read()
		{
			object result = this.ReadCore();
			this.SkipSpaces();
			if (this.ReadChar() >= 0)
			{
				throw this.JsonError("extra characters in JSON input");
			}
			return result;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00011384 File Offset: 0x0000F584
		private object ReadCore()
		{
			this.SkipSpaces();
			int num = this.PeekChar();
			if (num < 0)
			{
				throw this.JsonError("Incomplete JSON input");
			}
			if (num <= 102)
			{
				if (num == 34)
				{
					return this.ReadStringLiteral();
				}
				if (num != 91)
				{
					if (num == 102)
					{
						this.Expect("false");
						return false;
					}
				}
				else
				{
					this.ReadChar();
					List<object> list = new List<object>();
					this.SkipSpaces();
					if (this.PeekChar() == 93)
					{
						this.ReadChar();
						return list;
					}
					for (;;)
					{
						object item = this.ReadCore();
						list.Add(item);
						this.SkipSpaces();
						num = this.PeekChar();
						if (num != 44)
						{
							break;
						}
						this.ReadChar();
					}
					if (this.ReadChar() != 93)
					{
						throw this.JsonError("JSON array must end with ']'");
					}
					return list.ToArray();
				}
			}
			else
			{
				if (num == 110)
				{
					this.Expect("null");
					return null;
				}
				if (num == 116)
				{
					this.Expect("true");
					return true;
				}
				if (num == 123)
				{
					this.ReadChar();
					Dictionary<string, object> dictionary = new Dictionary<string, object>();
					this.SkipSpaces();
					if (this.PeekChar() == 125)
					{
						this.ReadChar();
						return dictionary;
					}
					for (;;)
					{
						this.SkipSpaces();
						if (this.PeekChar() == 125)
						{
							break;
						}
						string key = this.ReadStringLiteral();
						this.SkipSpaces();
						this.Expect(':');
						this.SkipSpaces();
						dictionary[key] = this.ReadCore();
						this.SkipSpaces();
						num = this.ReadChar();
						if (num != 44 && num == 125)
						{
							goto IL_142;
						}
					}
					this.ReadChar();
					IL_142:
					int num2 = 0;
					KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[dictionary.Count];
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						array[num2++] = keyValuePair;
					}
					return array;
				}
			}
			if ((48 <= num && num <= 57) || num == 45)
			{
				return this.ReadNumericLiteral();
			}
			throw this.JsonError(string.Format("Unexpected character '{0}'", (char)num));
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00011598 File Offset: 0x0000F798
		private int PeekChar()
		{
			if (!this.HasPeek)
			{
				this.Peek = this.Reader.Read();
				this.HasPeek = true;
			}
			return this.Peek;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000115C0 File Offset: 0x0000F7C0
		private int ReadChar()
		{
			object obj = this.HasPeek ? this.Peek : this.Reader.Read();
			this.HasPeek = false;
			if (this.Prev_Lf)
			{
				this.Line++;
				this.Column = 0;
				this.Prev_Lf = false;
			}
			object obj2 = obj;
			if (obj2 == 10)
			{
				this.Prev_Lf = true;
			}
			this.Column++;
			return obj2;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00011630 File Offset: 0x0000F830
		private void SkipSpaces()
		{
			for (;;)
			{
				int num = this.PeekChar();
				if (num - 9 > 1 && num != 13 && num != 32)
				{
					break;
				}
				this.ReadChar();
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00011660 File Offset: 0x0000F860
		private object ReadNumericLiteral()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.PeekChar() == 45)
			{
				stringBuilder.Append((char)this.ReadChar());
			}
			int num = 0;
			bool flag = this.PeekChar() == 48;
			int num2;
			for (;;)
			{
				num2 = this.PeekChar();
				if (num2 < 48 || 57 < num2)
				{
					goto IL_63;
				}
				stringBuilder.Append((char)this.ReadChar());
				if (flag && num == 1)
				{
					break;
				}
				num++;
			}
			throw this.JsonError("leading zeros are not allowed");
			IL_63:
			if (num == 0)
			{
				throw this.JsonError("Invalid JSON numeric literal; no digit found");
			}
			bool flag2 = false;
			int num3 = 0;
			if (this.PeekChar() == 46)
			{
				flag2 = true;
				stringBuilder.Append((char)this.ReadChar());
				if (this.PeekChar() < 0)
				{
					throw this.JsonError("Invalid JSON numeric literal; extra dot");
				}
				for (;;)
				{
					num2 = this.PeekChar();
					if (num2 < 48 || 57 < num2)
					{
						break;
					}
					stringBuilder.Append((char)this.ReadChar());
					num3++;
				}
				if (num3 == 0)
				{
					throw this.JsonError("Invalid JSON numeric literal; extra dot");
				}
			}
			num2 = this.PeekChar();
			if (num2 != 101 && num2 != 69)
			{
				if (!flag2)
				{
					int num4;
					if (int.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num4))
					{
						return num4;
					}
					long num5;
					if (long.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num5))
					{
						return num5;
					}
					ulong num6;
					if (ulong.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num6))
					{
						return num6;
					}
				}
				decimal num7;
				if (decimal.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num7) && num7 != 0m)
				{
					return num7;
				}
			}
			else
			{
				stringBuilder.Append((char)this.ReadChar());
				if (this.PeekChar() < 0)
				{
					throw new ArgumentException("Invalid JSON numeric literal; incomplete exponent");
				}
				int num8 = this.PeekChar();
				if (num8 != 43)
				{
					if (num8 == 45)
					{
						stringBuilder.Append((char)this.ReadChar());
					}
				}
				else
				{
					stringBuilder.Append((char)this.ReadChar());
				}
				if (this.PeekChar() < 0)
				{
					throw this.JsonError("Invalid JSON numeric literal; incomplete exponent");
				}
				for (;;)
				{
					num2 = this.PeekChar();
					if (num2 < 48 || 57 < num2)
					{
						break;
					}
					stringBuilder.Append((char)this.ReadChar());
				}
			}
			return double.Parse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000118A4 File Offset: 0x0000FAA4
		private string ReadStringLiteral()
		{
			if (this.PeekChar() != 34)
			{
				throw this.JsonError("Invalid JSON string literal format");
			}
			this.ReadChar();
			this.SBuilder.Length = 0;
			for (;;)
			{
				int num = this.ReadChar();
				if (num < 0)
				{
					goto IL_1BE;
				}
				if (num == 34)
				{
					break;
				}
				if (num != 92)
				{
					this.SBuilder.Append((char)num);
				}
				else
				{
					num = this.ReadChar();
					if (num < 0)
					{
						goto Block_5;
					}
					if (num <= 92)
					{
						if (num != 34 && num != 47 && num != 92)
						{
							goto Block_9;
						}
						this.SBuilder.Append((char)num);
					}
					else if (num <= 102)
					{
						if (num != 98)
						{
							if (num != 102)
							{
								goto Block_12;
							}
							this.SBuilder.Append('\f');
						}
						else
						{
							this.SBuilder.Append('\b');
						}
					}
					else
					{
						if (num != 110)
						{
							switch (num)
							{
							case 114:
								this.SBuilder.Append('\r');
								continue;
							case 116:
								this.SBuilder.Append('\t');
								continue;
							case 117:
							{
								ushort num2 = 0;
								for (int i = 0; i < 4; i++)
								{
									num2 = (ushort)(num2 << 4);
									if ((num = this.ReadChar()) < 0)
									{
										goto Block_15;
									}
									if (48 <= num && num <= 57)
									{
										num2 += (ushort)(num - 48);
									}
									if (65 <= num && num <= 70)
									{
										num2 += (ushort)(num - 65 + 10);
									}
									if (97 <= num && num <= 102)
									{
										num2 += (ushort)(num - 97 + 10);
									}
								}
								this.SBuilder.Append((char)num2);
								continue;
							}
							}
							goto Block_14;
						}
						this.SBuilder.Append('\n');
					}
				}
			}
			return this.SBuilder.ToString();
			Block_5:
			throw this.JsonError("Invalid JSON string literal; incomplete escape sequence");
			Block_9:
			Block_12:
			Block_14:
			goto IL_1B2;
			Block_15:
			throw this.JsonError("Incomplete unicode character escape literal");
			IL_1B2:
			throw this.JsonError("Invalid JSON string literal; unexpected escape character");
			IL_1BE:
			throw this.JsonError("JSON string is not closed");
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00011A7C File Offset: 0x0000FC7C
		private void Expect(char expected)
		{
			int num;
			if ((num = this.ReadChar()) != (int)expected)
			{
				throw this.JsonError(string.Format("Expected '{0}', got '{1}'", expected, (char)num));
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		private void Expect(string expected)
		{
			for (int i = 0; i < expected.Length; i++)
			{
				if (this.ReadChar() != (int)expected[i])
				{
					throw this.JsonError(string.Format("Expected '{0}', differed at {1}", expected, i));
				}
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00011AF9 File Offset: 0x0000FCF9
		private Exception JsonError(string msg)
		{
			return new ArgumentException(string.Format("{0}. At line {1}, column {2}", msg, this.Line, this.Column));
		}

		// Token: 0x040000F1 RID: 241
		private readonly StringBuilder SBuilder;

		// Token: 0x040000F2 RID: 242
		private readonly TextReader Reader;

		// Token: 0x040000F3 RID: 243
		private int Line = 1;

		// Token: 0x040000F4 RID: 244
		private int Column;

		// Token: 0x040000F5 RID: 245
		private int Peek;

		// Token: 0x040000F6 RID: 246
		private bool HasPeek;

		// Token: 0x040000F7 RID: 247
		private bool Prev_Lf;
	}
}
