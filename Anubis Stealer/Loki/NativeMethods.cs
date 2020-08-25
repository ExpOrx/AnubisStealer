using System;
using System.Runtime.InteropServices;

namespace loki
{
	// Token: 0x0200001A RID: 26
	internal static class NativeMethods
	{
		// Token: 0x06000105 RID: 261
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

		// Token: 0x06000106 RID: 262
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWow64Process([In] IntPtr hProcess, out bool lpSystemInfo);

		// Token: 0x06000107 RID: 263
		[DllImport("avicap32.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr capCreateCaptureWindowA(string string_0, int int_0, int int_1, int int_2, int int_3, int int_4, int int_5, int int_6);

		// Token: 0x06000108 RID: 264
		[DllImport("user32")]
		public static extern int SendMessage(IntPtr intptr_0, uint uint_0, int int_0, int int_1);
	}
}
