using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace loki.Properties
{
	// Token: 0x0200001C RID: 28
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00003A0C File Offset: 0x00001C0C
		internal Resources()
		{
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006D93 File Offset: 0x00004F93
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Anubis.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006DBF File Offset: 0x00004FBF
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006DC6 File Offset: 0x00004FC6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006DCE File Offset: 0x00004FCE
		internal static string Domains
		{
			get
			{
				return Resources.ResourceManager.GetString("Domains", Resources.resourceCulture);
			}
		}

		// Token: 0x04000055 RID: 85
		private static ResourceManager resourceMan;

		// Token: 0x04000056 RID: 86
		private static CultureInfo resourceCulture;
	}
}
