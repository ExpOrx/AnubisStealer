using System;
using System.IO;

namespace NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x0200000F RID: 15
	public static class JsonExt
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000050C0 File Offset: 0x000032C0
		public static JsonValue FromJSON(this string json)
		{
			return JsonValue.Load(new StringReader(json));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000050CD File Offset: 0x000032CD
		public static string ToJSON<T>(this T instance)
		{
			return JsonValue.ToJsonValue<T>(instance);
		}
	}
}
