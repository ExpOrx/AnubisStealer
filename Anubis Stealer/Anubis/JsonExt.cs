using System;
using System.IO;

namespace Anubis
{
	// Token: 0x0200005A RID: 90
	public static class JsonExt
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00011CDC File Offset: 0x0000FEDC
		public static JsonValue FromJSON(this string json)
		{
			return JsonValue.Load(new StringReader(json));
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011CE9 File Offset: 0x0000FEE9
		public static string ToJSON<T>(this T instance)
		{
			return JsonValue.ToJsonValue<T>(instance);
		}
	}
}
