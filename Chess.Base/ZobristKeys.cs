﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Base
{
	public partial class Zobrist
	{
		static ulong[,] Keys;

		static void SetKeys()
		{
			Keys = new ulong[16,64] {
				{
					0x5E9EDC66CE9F98C5,
					0x2883FEE508F13B75,
					0x547D276771102FC2,
					0x8C0A67A4E936AF50,
					0x34082C05489CE030,
					0x5D9FC9C05124DA07,
					0x2562E48A95BBB5C,
					0x9B9C37C3D0C14935,
					0x6F367BEE8037717A,
					0x659EFC23D14737B1,
					0xF32080E1DDF79EDC,
					0x74FE613924D76288,
					0xB0F458C394E17EAA,
					0x468D041FD8EC9F71,
					0x75C582D9210FE9CC,
					0xE33F15045C92FD2B,
					0x53CC81659DEFC3C8,
					0xB921F11ABFEC6065,
					0xF91B30DCBC0F66CC,
					0x5B89FED8C9A032E4,
					0x93192DE8F25826C2,
					0xB28125B07A43F552,
					0xB6F3020101A8909A,
					0xC8AA5B23D909597,
					0xD62A9CB13C510105,
					0xCC2B749907C22A0C,
					0xEBFC025E03CC4FDA,
					0x5C7766AE7CBA30B3,
					0xB02C19AAA94B4541,
					0xBB07B406006AAC73,
					0xEA5BFB8F41C5372F,
					0xD8763E10A7E54846,
					0x7B61B2BDB69A1563,
					0xFD22AEA15E7837E8,
					0xE12DA0CCCE01206A,
					0xBEF31A83B92E312B,
					0x4FF7A49E4AAFC8F2,
					0x4FDABEF6A1FCC348,
					0xCE8AB8AB69832EB6,
					0xC1737C04FFBBCE8D,
					0xCE2C80E1AC43B174,
					0x82CB4786738C513B,
					0xC15BCD1D541DA6DF,
					0x6A591A5E73D8FEB2,
					0xAEE33FDCB8DF2E8B,
					0xC96F6DB8FBB427C0,
					0x34A212A34049A831,
					0xF357D3DC0820C273,
					0x95C3A4CF789508EB,
					0x6F99C383D8E3811A,
					0x2CB927B6B3E1751F,
					0x73E1F4569592DDF0,
					0xCBFE1C161FD3E43D,
					0xE836A8DBADDFFD47,
					0x6EA08AAEA7405B0E,
					0xF8236CF551FECDDC,
					0xC83D27AD693704E2,
					0x3D5BE2D2EEC53776,
					0x4CC4A20EDD94BE5F,
					0x85AB3C87BB86F373,
					0x24C88EA505FC5B0F,
					0x9DADFC28C73E5BF2,
					0xA4ADE26BE3E83EA8,
					0xCE353F80A4937D6D,
				},
				{
					0x572F50AB73C23B00,
					0xA5BB54E113801150,
					0xF8E58575C9207E89,
					0xB0D5A70B24377509,
					0x3E0EAB1165AE6DFD,
					0x710D09B50F68CC6,
					0x31DC5CE9B9C28D3A,
					0xE06F56A6A87475F7,
					0x75F6C8E7F840333C,
					0x5ABF84BECE7A28A8,
					0x1B4676DB1B3CE755,
					0xEB91BFBEF4966E6D,
					0x6E36B146D71C227A,
					0xE47A2078B8F15736,
					0x5D7DE25EC4E1CCF8,
					0x2B398994C2C99C3F,
					0x61EC8C6E34027E2F,
					0x47BF5843B728E6C0,
					0x723EE4C47D342DC9,
					0x921EDF734D9EA7B0,
					0xD5176A69053F0AE8,
					0x4B790556B5FFC5AE,
					0x73551F773FD61F1B,
					0x8047796DD497A19C,
					0x3EFED19F73711E9D,
					0x7BE32CDBA5110EF1,
					0xD1B3EEA69946E0DF,
					0x6C6CD1EE69CD28CF,
					0x88E516BDF9C166BC,
					0x8B5A6B1515670A32,
					0x49D2984A18F662FE,
					0xBF361090AF27741F,
					0xA4401302E66CFAD8,
					0xE2DE888A1385396,
					0x7ABEF9DE8A6A7682,
					0x63C9A53B76807969,
					0x6978CEBD55062CD7,
					0xFEDBA4827498F04A,
					0x2BC262C7158F5943,
					0xEE1B569C347E21E4,
					0x8D2A23A8CBC851A3,
					0x87A724007A26824B,
					0x3EED83E28FAF8D40,
					0xBBAB2447C4B513F7,
					0x71784044465BF50C,
					0xA75FCAD1DB1A83E7,
					0x1FDE7313F39CAD9E,
					0xE00E27BBF6CE39F7,
					0x91156C2FC1F2099A,
					0xEBAE6D2C27F45826,
					0xDB2B9DB2F9BEFB5A,
					0x7D775A401C0006BE,
					0x817BF343B2C88AF,
					0x438C958551040ACF,
					0x239439992560A619,
					0x6DBCEF062A71F7B5,
					0xBD0162022B525902,
					0xFF49E53218F77E54,
					0x7B2B6E6B2923B3FE,
					0x484E7A22CEF36AE6,
					0x9F76E33982427A62,
					0x6188A4513EF9A7C0,
					0x9B63BA6F185B6323,
					0xC08DDFEE711D9C28,
				},
				{
					0x7E6ADA620490487,
					0x9EF758CD67C09F4E,
					0x845FCDBE4533CE29,
					0x6F85F7D95F39A316,
					0xE10AE119FB938905,
					0x588FD4DD24CC7600,
					0xC7F0B6D613DF8E27,
					0xF1FA99394F445825,
					0xED133B7C1EFAD0F3,
					0x4EDEE17D5EB20B19,
					0x500D522E849F27E,
					0x106D919EA7848D96,
					0xDE29ABFCF8CD5D8E,
					0xF8C423551F59368F,
					0xC48A125EF621B087,
					0x3BCBC43A23BF1212,
					0x38C01654151E2A2F,
					0xFE21273E72372369,
					0x6D1EE7FFA6819F0F,
					0x489FEA93A51CC552,
					0xE994FC271A139D3A,
					0x51A015F87A21FCCE,
					0xA0A96A5B96D614B,
					0x3FE2781807AB6F78,
					0x7D553129A8504891,
					0xB30276D3FF2C337F,
					0xA6992E569D195192,
					0xB84F1DC57D0D3C6D,
					0x505DB9D7BC19C063,
					0xDDDB8EDA8A532917,
					0x26274018496B164D,
					0xA888835B8098BC25,
					0xE1C71631D9DDACA,
					0x3F0D44911BD9E58A,
					0x13E7F9C38D381CD8,
					0x1B4DE204EEF59F88,
					0xD95CD926AC3488D3,
					0x981E38FEBBF10781,
					0xA3687744194DC64B,
					0xDE11B1DBCC7B284C,
					0x4681E3E7CF47109D,
					0xE091F4B0DD79F53A,
					0xFA0D386F6E63198D,
					0x476F7DC053B8C11C,
					0xFAE9D636B218C266,
					0x6937F59D3CC2EDE3,
					0xFF9592D0516D4456,
					0x616C83B06008D990,
					0x511CFCD2DAE5B5DB,
					0x53D9CF8BE9624DD1,
					0xE5FAFDA71831EC30,
					0x7ECAE07F42BA3145,
					0x65B85F9C37F00C3B,
					0x1840C72D8D2F712C,
					0xC265DBB95469E099,
					0xC9E218AD7E42C6CE,
					0xF0E494BA2BE7509E,
					0x6FBD50B41D3F3F97,
					0x168E01996ACF8344,
					0xCC18DF33717EB7C2,
					0xD74CAEC93B62947E,
					0xE2D3350BE3DF77A6,
					0xCA816AA95ED97BBE,
					0xC9BBAAB7D1450FC6,
				},
				{
					0xA623D8206F347500,
					0x2262AF412E2D1290,
					0x9F88980453C4F5F4,
					0xEE291B9A0FA7032D,
					0x4E540C983065148,
					0x6C6C25685F9EDB9D,
					0xCE0F18BAD57D766B,
					0xB1C0992EAE726156,
					0xDF9CAAEF7EDE9ECA,
					0x8D96BDB1E2E04874,
					0xB9CA571D57BE0D51,
					0x3E3B9ECB183116C8,
					0xBF5CFB837A59569F,
					0x47E99AFDCE43F486,
					0xF0FC424917FC9D9,
					0x6829578D4C6B5130,
					0x88D844E1DCC532A9,
					0xD2A866E9C434ED98,
					0xAD01B7EA803803AB,
					0xB182FC0DBB9525FA,
					0x806F33FEE6CD4D98,
					0xE8B81365B8A50C50,
					0x3941288FDA936EB1,
					0xFC39228629475874,
					0x20EC09304A02DDD3,
					0xBE3369B046587162,
					0xE40E78CD772E01A4,
					0x6BCB4AC2391D15F2,
					0x87335C0332B2C09,
					0x9E09EC5A887EDB9D,
					0xC46CB97096BC62B9,
					0xCA9EA2B204964EA,
					0x3DB8C22EBD2235EE,
					0x1BC6B9CF93B84503,
					0xB601F91153354A5A,
					0xC90905A077B0EDED,
					0x61DFA94BD3DC9153,
					0x45BB550FB13956AD,
					0xDE01096FCAAB45B8,
					0x64964F1ADB957E91,
					0x4F4217982EAF29B0,
					0x67DBB3275B42A67B,
					0x935616D0E56996C5,
					0x1B70AC9A69663305,
					0x11C86E750FD82C82,
					0x8C85991422BD9FD1,
					0x6DAC6BDF3109F08A,
					0x46288CFB6172FEED,
					0xB29108FEC8BF44B0,
					0xC2FCA9A3EEAD7BA2,
					0x31443FE1D65DD0AC,
					0x277AFD863016F97B,
					0x9F9AE5A09B06E5B5,
					0xA2D375E979862B5,
					0xC298EA76C84A07EB,
					0x87274BA697F518DE,
					0x4416079090705473,
					0xE8EF0A6A8FFD75C3,
					0x304684FFE649DCF5,
					0x43CC410FAD3E01E1,
					0x7205024327A8BC02,
					0xA49523089E2281EB,
					0x1C24342BAF4387B5,
					0xA445C8C728F18579,
				},
				{
					0xF2D4564A66A1EE96,
					0x4E002F01BB3A2C61,
					0xB86C9B4C80E256BC,
					0xA9F1570792A7F32C,
					0xBB0B1D9E99365D0A,
					0xCD9C8511E0A875AB,
					0x506B40494C0E2BDD,
					0x8F732D17B32F2E16,
					0x99E12F5253223CF1,
					0x190E7C47468E6042,
					0x3BAB3FB4459F10AC,
					0x7647D91AB8EA9B70,
					0xAFCAA7372B61D6D7,
					0xFB31693AD56EFB54,
					0xEEFE8BB712B02812,
					0xB82C244BD929C0CE,
					0x17F2A68EF635B590,
					0x1A938FCDBC5CFDB3,
					0x188A5D074EE9B214,
					0xF36979A054E86F81,
					0x92C5F2AF20171A78,
					0x8E61552A7F0405BE,
					0x2E8F7E6B5796385F,
					0xA4115A1174F9E9E9,
					0xBB564C06AC347DE5,
					0x6CA4CC243E585557,
					0xFBBB6F3D119400BA,
					0x897028E802C91F37,
					0x468152666780E9CD,
					0xD281BAD515086B0E,
					0xA46D92639B1546ED,
					0x9B33A90A7D3DDE8C,
					0xA15E515F2F5C39D9,
					0x1A35E928E71C298F,
					0x428D518F56B5114,
					0x581CD45A6A78B858,
					0x824AC6A058493F81,
					0x6F2C3324E680E74B,
					0xBCABE8AAA576371B,
					0xCC3245E4FE4BFE28,
					0x63AF7EF8D7743692,
					0x73804FF842F95F2D,
					0x4D58C4EBA2BB514F,
					0xD6CBF5A98125D449,
					0x2C8B1164E93242FA,
					0x4BA96D728A46D595,
					0xBC79C7EFC80577D8,
					0xA0F03D521FEB9E38,
					0x13419D03C98919C6,
					0x7F0BFC01F8919C6B,
					0x6A84466B1CEEA1E8,
					0x3627D502F5766AF7,
					0xB787C26E002F6707,
					0x8625C18FE6A80C14,
					0xEF162B7DA9A0AC36,
					0x1C91B4A3B1B13F99,
					0xEC2FAF8208C01FEB,
					0x28C6781277AE0DC7,
					0x80E3E2BC104FA72B,
					0x603A3C7FB83D8D35,
					0xBD328EB89F173F6A,
					0x67A63181AE3FE6B6,
					0x1B4CCF75A63157C7,
					0x7370D004F4742A17,
				},
				{
					0xA7FED14AF91A8235,
					0xB66BCAC2A2458E4F,
					0x9BE7668CBF343030,
					0x6BB1712AD53A4C75,
					0x946C1617150E1414,
					0xF7B25793ED89FBC8,
					0xD2392327E572B4C8,
					0x8E64580FDC2773B,
					0x3E51493ED56B9ACC,
					0xAF12F867BD8E3A67,
					0x4386BD655F7068D2,
					0xB5A1396B3D0C5A31,
					0x27EAE3571077FC6A,
					0x9645C35C5B2A0894,
					0x736A4966507E3E45,
					0xE86670886BE683C6,
					0x959DB7BC18B5AB9A,
					0xEEAE8343493FEF05,
					0x50CBB056068BC0BB,
					0x14A7A1CBDD4C62DC,
					0xD3EA34469E726F4C,
					0xD146F21C493A6289,
					0x51C7E7064ED3220,
					0xD65B703D7F1A4F02,
					0x82F70E3B08D0D761,
					0x42D08C2388503A05,
					0x24CA26700D1CE100,
					0x227A0220B84C48F0,
					0xA30E5EC683A9F6AC,
					0x42FC704D8C290CBD,
					0x36F13C85B066BCCF,
					0xAADA1EA984DD7C76,
					0x4131FC837ABFCCAF,
					0xE04AF0A792B160FE,
					0x524CE2B60A52F92B,
					0x69F325D267D9CB8A,
					0xC8DB857FDA5E3DFA,
					0x1E4891C0A31BB264,
					0x2AB316BD12C3AECE,
					0x6C8851D8FB2A2EE1,
					0xBF9BCCE60EA9C350,
					0xBC2C68C40B65DE4,
					0xBF3C2AD87905CD71,
					0xD42290FD6827043A,
					0x7EE44F534E970857,
					0xEC0F753B4FD1C103,
					0x75C2228B78B043DA,
					0x2F5A47B0BA4BFB0F,
					0x85C8674E1D01394D,
					0x5B8C5F916C0F1A03,
					0x977D5585E5AC6EC5,
					0xCC5CBCF0160E98,
					0x30E03EA6574AC7DB,
					0x34037D415A33C887,
					0xF4C3F45EF5B42A8C,
					0x9DA54D7AFBDC0469,
					0xE318F4CE0C202442,
					0xE3C4AC048C6AE148,
					0x41E1230AA410039B,
					0xEEB0F37E54B670A6,
					0x796ADFD29D9B72DD,
					0xE81A4723A34A379F,
					0x2DC28D2AD11B5A96,
					0xE7E586E726BA75F,
				},
				{
					0xB88495D2D3E10211,
					0xEDCCAD41E5890686,
					0xDAE3BFED6D6CEA86,
					0xC84825F28EAAAEDD,
					0xE7E82853D1E80B09,
					0xE90CA9EE277EE03F,
					0xE8CD5A7DBECA8D0F,
					0x52A371DCCD118F67,
					0x11311AEE1B47145B,
					0x9A1F216C2A8AD08A,
					0xAC1A6EC57DB8408E,
					0x6DA4AB469C7C89E1,
					0x206701BCCF71EC11,
					0x561FDE9C4404D953,
					0x3053EF88C935972,
					0x4DF68504BC377A42,
					0x381B1B4830F1CE0F,
					0x1394D37D86BAF439,
					0xA78FD725B5F6F9AD,
					0x6E27C2A44C1A37DC,
					0x7A4517C008E48065,
					0x550F14E1C79D1482,
					0xC3C8D94A711FBBEF,
					0xCD458C62FBFCC537,
					0x89F8F9A2961F853E,
					0x6AA1728CEEC861B5,
					0x7C986136BBD84B42,
					0x33CF74845ECDB6BB,
					0xBEA96896AED6451A,
					0x45CE7863825FCC1A,
					0xA20D1F426C8FC99,
					0x4B53B80F876EFC,
					0xCD86A780BC0738A9,
					0x81B1D7F088DF0CA6,
					0xD9EAB4109374B88B,
					0xDA140184C04C8FF6,
					0xE0F42A43FE07AE27,
					0x8C227690D80078F7,
					0x69A84170D052E038,
					0x9864268D56400372,
					0x2D8C3F10E37DB2E0,
					0x608DC454F0691796,
					0x321CD258B4A38445,
					0x9E812138584D8D84,
					0xAD91389E98DA27DB,
					0xDD03EF060EFA10AC,
					0x6CF68813309840CF,
					0xD03BFB2E325E44C8,
					0x7A396BD596E6A54,
					0x1C9D4A751214A63A,
					0xFCC59FCCC63399C1,
					0xA4AF0EB414C5F178,
					0x66014466E9F91D4A,
					0xD8FC01AA389F359B,
					0xC8D636FCA129FB29,
					0x934F9B950B0B2C5D,
					0x55F681487A1D8F46,
					0x5BE383BCC4EB396A,
					0xB0D5AB8B72880823,
					0x77B2B59CAB0646D6,
					0x1AD65A43382CEC50,
					0x293800E16D382ECF,
					0xA02627C7B280C781,
					0xA2DA516B7CAC7DF1,
				},
				{
					0x61FA1B85EBAFB4,
					0x8EE10CE1D9AC5930,
					0x1E7E1E975CB27C3D,
					0xBEA26D54598CE90C,
					0x9FB4AE3A5B65CBFF,
					0xA790D15ECD81D7CC,
					0x45728279F2DABFEC,
					0x941BE5DE372715F9,
					0x43454296A0666AAA,
					0xC2321CA2DCC8799C,
					0xA8F70067233616A6,
					0xFAF757B0E3C274B9,
					0x563E8C55962AC70C,
					0xDB877B2A1DCD1EB9,
					0x2DBBCDFC16E2E7D8,
					0x5EDB7D84F3F9CC58,
					0x59CF47CE2AE52AB5,
					0x13C1F64F7E73B92C,
					0x3300F8E5DB1EBC7D,
					0x59DCCE70896A4F88,
					0x7674AE44453E32F9,
					0xF4B81BC90DA67AF3,
					0x760F66A2D191E0,
					0x8C0F12D63272E37E,
					0xD28FC590CB3E8EBA,
					0xACD03F6783075236,
					0xE2CA177F6164EA18,
					0x36690126FB9B3D46,
					0x3E1DE02ACD948059,
					0xC5B969C7112AF61B,
					0x8A14A239348BD463,
					0x6AA6266EF1A1A16F,
					0xF9B520F6C1A38DBC,
					0xE6D0041D95C6A8E5,
					0x79D6436201F29E98,
					0x99970DA32D1CDD86,
					0x36738E8542FB32F0,
					0x10F7C4241ADF7ECF,
					0xE16DCE07A41D5B6,
					0x78A374C89A41794B,
					0x419562A79FADB85A,
					0x126F7D0FD6A4ADDB,
					0xB1C266B0C3D25859,
					0x51F14E0DD1A28940,
					0xEAE3EFF1F4122B2E,
					0x51B30A27A00C3FB6,
					0x8F35D73E0CB627F1,
					0x60A371360F012027,
					0x4A01FADB60168884,
					0x70062D257894E7A0,
					0xC4A25A593F10A75B,
					0x868ADF1BF6FB4AEF,
					0x40528AFB9726D3EB,
					0x54E34C3569FA6956,
					0x60A2DF41E0E48670,
					0xF983DCAEB5D66392,
					0x27E4639EF93EAB12,
					0xC4A3EFC715B7618B,
					0x373EA27CB822EA07,
					0x915853FFF87172D4,
					0x6C085F022F8564DD,
					0x4B0AF2BEA8E56DBB,
					0xA37681209B6707AC,
					0xA075876B11CDB1D8,
				},
				{
					0xF8E111E33641BE97,
					0xA76211BC08B9E10B,
					0xC6F1DE52080E9257,
					0xEAC874387A614E35,
					0x36482BBB3F191DCF,
					0xDE53A9E8063A0CBE,
					0xC47A1CEBD5DE42E0,
					0x7B0CE39355CA168A,
					0xBE71D26FE08AB8D8,
					0x14BE78A782ACE1F6,
					0xFF7C12A23754E939,
					0x364146F4E4F5AF52,
					0xEE6C83D60BC8052C,
					0x3B3F596EB24E7C0C,
					0xEFA23500993591B6,
					0x7B007CC731D0B25A,
					0xBCA8866A24A388FB,
					0xCFDD967DE4E36F64,
					0xA532D085F23F6281,
					0x606CBF9A1A7B2A09,
					0x8E8971A79A72617,
					0x72E643207DC43FC7,
					0x5E040B301AEFE8F6,
					0xB2C29428BF9D8A50,
					0xD06D335DF7C3F70D,
					0xDFBB48B4E9FCE0F1,
					0x640DE6A1A1B0DD9B,
					0xE166C2E7BB229328,
					0x6A8B063719D673A1,
					0x60FB8B8D329C8C0B,
					0x792740F178FF666B,
					0xD3AA106E37AE8BD,
					0x8741FF0D7A928F07,
					0xC02F461048F5A095,
					0x5C038F7EE9F6DC3,
					0x5466CD1F73DC865B,
					0x86DA42E4FC1D1D77,
					0xF41839D6B99FB131,
					0x6112A0667C2C141F,
					0x44A94533203C4EC6,
					0x9CF1B90D0C6949F5,
					0x6B60D7840E2596,
					0x700A3631E2BC103A,
					0xFF819F5D0B2E5677,
					0x9B9679F456EAD0F9,
					0x2147E090352AD4FB,
					0xE9C24E544105BCD5,
					0x135C82B370B2373E,
					0x4F52C8C0BE1BE789,
					0x1805D17C497DC8F6,
					0x9372E57C35535A53,
					0x61B2AB07636B84C3,
					0xAE3208E985D390C9,
					0x29CF4E6CDF0A752A,
					0x2DBB5FB7645D21F7,
					0xA7F273C0FFE112EB,
					0x84129744375F2D9C,
					0x4FCEBC7272DEA3F3,
					0x82B66473CEAC3EAD,
					0xCA5921D11388EA44,
					0x4E8A076C84506372,
					0x991FD6C943859072,
					0xE62FA362CA6E84C6,
					0x289A85077330A77D,
				},
				{
					0xCFEA15838C3D6478,
					0x44EC4AC737166F69,
					0x89B8486C8919E696,
					0xECCDA6C09BDB458F,
					0x7569A987788915BA,
					0xB52561B5607EA15,
					0x854253F577C25A44,
					0x65E4A97F7F9F5007,
					0x4A37D2DC572887DA,
					0xCC9D4AE64527F0D3,
					0xF79E46DDDA91C3CE,
					0xBD46D78B416E3D89,
					0xA4A54974C863C3BC,
					0xA137345FF254EAAD,
					0xC933D15D16440C54,
					0xAC84196D735DB43,
					0xD949ABAFA69A8261,
					0xAC74019CB0D0B40F,
					0xE9AB5A8B167338F1,
					0xEC79FBE40CE12A0C,
					0x7D8323FDC15C95DC,
					0x17BC3E9253083B42,
					0x3F420A983FD50788,
					0x832644FB2AD1F05F,
					0x78E95EAA1EF7297C,
					0x3759BBA55ADFE715,
					0x54791E2B4E6C5A39,
					0xAFB29ADB877A3DF8,
					0xC2AA9411A28D0F3F,
					0xD1A2E245EC9E9C30,
					0x1B943A2DD4FA913E,
					0xEF5041859CFE00E6,
					0xAA66DC844BFFE31E,
					0x6E73D8BD51181319,
					0xF6522A5B57FC800,
					0xC8EC363BB5BC5C4C,
					0x1CEDC07D92FEC8D2,
					0x7F45CA22AA9F8E7F,
					0xCEF1E9CA23411BA2,
					0xFA999253CACE815A,
					0xC6D2CC7171C37E0A,
					0xD280DF97620867A2,
					0x3065A5EBA3295DF8,
					0x96E8993F0B5C999F,
					0xE57C71B935A07964,
					0xA3B854A74D22BC7B,
					0x251279730663E674,
					0x91C61408803736EE,
					0x12634118863B4D33,
					0x926C34C5C4531C7,
					0x3F33A6AA9B917751,
					0xEFE121B49B69B4EB,
					0x4CFF8E997C06C3BE,
					0x46AD296DD04B8C05,
					0xBA6324DE89A1724C,
					0x6D77530437446296,
					0x2891AB0E866462DF,
					0xA627F722035E64BA,
					0xA3A84DCAA04C7610,
					0xB75FE9B0B45094A2,
					0x715788FC48643CAC,
					0xDE98D8FC4115FD43,
					0xEDFAD3D372B70CB7,
					0x43040E30A21AA954,
				},
				{
					0x235AE7268F577CFA,
					0x9510E0D5DBA914FA,
					0x4659FCBBB169FE99,
					0xC23AE4E8388F9E5F,
					0x2435B01E2CFCECA3,
					0x8CF883F013F8C1C5,
					0x86DA69C27965C2FB,
					0x3C6ECC52940ED2A5,
					0x93365A61D7530DC8,
					0x820D8EF17C9C5346,
					0x6F78FAD9E952FC26,
					0x44482554AFF9FDDC,
					0x308343E1525DAD79,
					0x2A889B69BDE44C74,
					0xA4C153C83F63B005,
					0xEEDE632943F0450E,
					0xFFACB0EC2A2B9136,
					0x751D3E22AF2C8802,
					0x4BCD4A5B1DBE93CC,
					0x392F3D8F3DEEAB0A,
					0xB8EF17CB7A02A94E,
					0xFD3E45093EADB55D,
					0xB19AD5F6CB888E5C,
					0x7B8FA73BF4E5F96F,
					0x2CE40F55D49023A4,
					0xE67B0FD674D4C7F1,
					0x90DB013A6438B01A,
					0xCF27E09437FD3F3F,
					0x27884DC50170D377,
					0x1736C0D1076E0439,
					0x806941325228BB1D,
					0x6EF58F13FC969F0E,
					0x37B78D085CA7C9B2,
					0x4841AA3730713155,
					0xD13249B36859ADAF,
					0x743F2B46C72FBA64,
					0x3583A3DDC02AB184,
					0xAFA6778FF89E1FA5,
					0xA27A671E204F03CF,
					0xADE80A1B2F0B7E24,
					0x5330E9293649C908,
					0x312F19DDE306E1B7,
					0xCE2857A12EE252D,
					0xDA7838CA2349329D,
					0x6E6E08C8860B2683,
					0xC4D7A8899DA53787,
					0x64148FFE843BEEAE,
					0x6B341E929DEC2B68,
					0xCE1A5E584659D69E,
					0x95DD28699C73A0A2,
					0x8C8771AC65A04BCA,
					0xF5DD091B538BE1C6,
					0xF988D582D350C7DB,
					0x1E9D745ECB312DE,
					0x6F8818920218A954,
					0x19691D795BE3B8F8,
					0x8EBD4470035953C0,
					0x539D42AFC7F50273,
					0xE95B2EDE1D7D75A0,
					0x52E4B150F02CF30A,
					0xDCDB50FE8B6BEFB0,
					0xF5674A2939D76569,
					0x73E7EECAAB42E6FE,
					0xB2BF60A2414420A8,
				},
				{
					0xA4D7FB6F423018A6,
					0x35CAB4E7BDE53C2A,
					0xA14AECDD12759DBC,
					0x10311F4A280831FC,
					0xD6B56C89D01B2E2A,
					0x700767E8048A4849,
					0xDE09897E7BB2581D,
					0xA3594438B75E9980,
					0x5B6522E18193BD2C,
					0xCCBB931FA46D85F7,
					0xA6E7215E9F32C90C,
					0x8C115FB7CBE663CA,
					0xFA2F5B537E40E0B7,
					0x7AC0EEF7539EA8C6,
					0xA53993E49C283D21,
					0x196A0B9C1DB0DA4B,
					0x43D11EF085AAFA11,
					0x5E20718C8679D375,
					0xAF5BB9A62B738C7A,
					0x962CF96AC3065C6B,
					0x9DB5408C078D1B92,
					0xD0694271A89626D,
					0x3DEC5E0520F7E93C,
					0x9F0CDDFB7D3CBDF0,
					0x17D1A4BD0B46DBE2,
					0xF46E6C2FC445EAF7,
					0xCD435E52A2EA88B3,
					0xC0B0E80F08DB7F9D,
					0xB2F5F2103B967384,
					0x3EA93CBDFB510370,
					0x152EC77AE1BA9CE8,
					0xB623388CE87F8F48,
					0xEB14F71641241C45,
					0x4FEB67DE8FA6ED25,
					0xD86CE53E68207E0A,
					0x7E35814850C0BAF,
					0x39129B2505E3A53B,
					0x74B5B98A7A2368B1,
					0x1F7753EF3EB9D18A,
					0xF0CBE3E27EEF6EF8,
					0x52CD50C9F536DD00,
					0x931E8BA95DCB19B6,
					0x1593D50BA07B9D3F,
					0xB952635E9EAA2DCD,
					0x2CC9708223D43B19,
					0x7F1336C0D47830B9,
					0x6B6BE8380802E2CE,
					0xA4F2341961B9BE26,
					0xCFA02CB764BD12E7,
					0xDC704F2A914D45A1,
					0x5D41C688BC7B17CE,
					0x71B0C18E2D121635,
					0xA29170BF4CC26092,
					0x989F031F138F2C08,
					0x565CA757A31221B0,
					0x35D184FE5DC6E77F,
					0xABA9EEE5B3F11433,
					0x9EDEE2F979E667C7,
					0x426177C8DB584A13,
					0x33AA994854FCB87,
					0x324B0713BB1336B,
					0x54E72687E5F1EF7D,
					0xF01E9BC01B70E53,
					0xD46CFE0F740E993B,
				},
				{
					0x1FACDE388BAB8146,
					0xD747E04988101C2B,
					0x400BDE53B95DA9DA,
					0xB88A54592F87F0EC,
					0x6BE3FD098CA9B61C,
					0x5F935F3102292A7,
					0x456FC5F090E2359F,
					0x6E09639C99D54BAC,
					0x94F5A4BF6192565D,
					0x6B51FCFF5A12DF0A,
					0x4A35E580B48D118C,
					0x19C23C89C3D29A0,
					0xC8855A115A0CC120,
					0xCDED17445AD37A58,
					0x689657056336DD12,
					0xEF5DF1231FC13338,
					0x7208CD1738542587,
					0x4057537EFE0531AB,
					0xD39CA362A867B86F,
					0xAFF9535EDAE3E3C8,
					0xAFCCC81FCBCEBAD5,
					0xDBE3D98120368974,
					0xB7D80AA757851D7A,
					0x558C613056A8AC6F,
					0xD36F2674579C0A7E,
					0xA89DEA7F8F561FAB,
					0x77DD51BC6542A3A2,
					0xE3E6C3FC5D6E9018,
					0xC3537377C0EAD92C,
					0xE82F9EB970160639,
					0x5B6C98D491C7F38D,
					0x40E908C347B3867A,
					0x9EC0B9DCED1DDB7,
					0x961743806E8220EE,
					0xB4C9FD424A546BCB,
					0x72B4E7D9B7D56E73,
					0xEDAA953F904E96B7,
					0xDB59767A8558F7AB,
					0x34C9757CFF48A6D2,
					0xACB6C47A7364AD8A,
					0xFA48E60435FBB030,
					0x7AB6D5FB7AA26026,
					0x7B7B14017988824,
					0xF5D85E36689E586B,
					0x50789FB56D2CEA89,
					0xCD1F19B478601B06,
					0xCC0B5FF27BF4D01E,
					0x5620CB61308BDCE4,
					0xE541FCC80BCF7877,
					0xABDA08195785415E,
					0x1B6EBFFD3204002E,
					0x3C869FC861223BE8,
					0x2837C058C27A1BAF,
					0x296B88F2418665CA,
					0x3F968D2229C749F1,
					0x34C6405F24B38C58,
					0x828BFD7B3D9624C7,
					0x6948F321B78C4FC8,
					0xF9E486379623AEDF,
					0xDD4064FEC625D9D,
					0xC581430951775680,
					0x7329EBF7D79F680,
					0xF520B1549664B851,
					0x9EAB23B9466F70E0,
				},
				{
					0x5DA25C30CD6D793D,
					0xD2BD0BE560892806,
					0xB71A166905761551,
					0x9E66EFB7AB8C6C7F,
					0x16513BAD4BFFB572,
					0x1EE0DACC82A5B97E,
					0xFE331DFE9557D9E,
					0x59E4ACAE50AF1F3D,
					0x97FF5089F3AE6CB6,
					0x407D91195F19EEA8,
					0xAC4FFBF1D2F8DE6A,
					0x67B090784F122435,
					0x299A92CB367EDEF8,
					0xDE6E7207E2CCD580,
					0xFCA760A60EA1721A,
					0x878160AE53F04064,
					0x9DAF1F94FB216B7B,
					0xB92449006BEF1BA6,
					0xF017313F1B58A0CA,
					0x532EE04DF9C78140,
					0xAFB8033C69EDA629,
					0xEBFF4F76D1F29A14,
					0x8B578EE9177397,
					0xF680386050793B98,
					0x33C968695BD33A16,
					0x252FE8D95839ECFC,
					0xEC76AD5C6B9D0DBE,
					0x9BB5D24ED0EAA67A,
					0x39E5BC3174BCCD7E,
					0x6D2E91E063C2A915,
					0x384155EB68938360,
					0x6F9F484DC58CB70A,
					0x5C674F4043376556,
					0xEC10E5F458E8DA32,
					0x40B064B137296EC6,
					0x3F064D3F1F669EF0,
					0xE6AE919129AF9C5B,
					0x664E2125D6921CDF,
					0x1C32BB598D4CB657,
					0x2648B1654E9CA0F,
					0x4FA3DD78EA629056,
					0xAAAC53E4CBCE88FC,
					0x50239A707D99EFB0,
					0xDC7FF8F3A9439AAA,
					0x156A6B7041251BBC,
					0x2896D32EE44858A1,
					0x2B07B5D7F84CD162,
					0xDB97BFADBBB2225,
					0xF6A1F985ABDE6A0A,
					0x9EB788E5D5E53DAC,
					0x19A0717EF4207DC5,
					0xD140A3F3B89EF51F,
					0x33D254399FB0F3B5,
					0x8A1E7AA845B85559,
					0xDDA2E68E3926C385,
					0x59ED4EA4C0E72F59,
					0x3610A62A32005E33,
					0x189B001A6D1512D9,
					0x915DEF4657FA7A29,
					0x380CEA44EEF8BE8D,
					0x23C20582D972798D,
					0xBDFEC66E7BAAC4,
					0xA338E5A37580E8AC,
					0xDA01E7E8E2DB4EA6,
				},
				{
					0xF4F8BA41199FD088,
					0x608F9FC03C6BC5A2,
					0x858225ECAACDB1A6,
					0x9843B13B71360DA0,
					0x335DD3EB468F97E0,
					0xFD9754DB68A17878,
					0x741992D06ADA48DD,
					0x1CB8F2BAC180795E,
					0x5EC62B021578141C,
					0x79280D4A326CA072,
					0x50857E8B3C88BB67,
					0x8F1B7047208B4EAE,
					0x8C99000D6944CFD7,
					0xA3177DFE0E152DB9,
					0xE4419CB765A2D828,
					0xCC27DFB251CDAF25,
					0x1E5EFFF893C668BA,
					0x48A3AD9AE342D2D,
					0xA6231A322C1389DA,
					0xA0D8EC4B5E0B7222,
					0x2C2B472BA160BE6E,
					0xA41D77D72781684E,
					0x8F9B28F5529FBBA4,
					0xED17DEC61506DCB7,
					0x4FE767F20E70CB41,
					0xD17D90526C46DD33,
					0xAC108CA1A46955B,
					0x8AD5D13CDE917E9B,
					0xEF7E1E22D31CA05E,
					0xB2AAA6C69321BD74,
					0x43411B8AB103897F,
					0x2D622F3264436982,
					0x40B629E508507BAD,
					0xB8D1D38DE3496117,
					0x2889C6C7F02C0F22,
					0x6B5D546B3B18CC07,
					0xE7F39085BAC93E41,
					0x3701065A0B231611,
					0x5F2C687CB186BDA9,
					0x25D90B2B4915F3D2,
					0xA59E0022B577A6F2,
					0x3297F9BE3D4FB3C6,
					0x815E31B14E1379CF,
					0x14B678C3096005F1,
					0x78BB731067BC7B69,
					0x1474FA4C3ABCBF33,
					0x84DFDF2D07BCA3B4,
					0x43FA1B8A761C694C,
					0xC440A9FEFB019714,
					0x7D2F1E411D0E53EB,
					0xB18BD3B698E4CBE,
					0xDABF95B57E20EE5B,
					0x5E455F13A258DCF4,
					0x5A37549D9B508563,
					0x8786F95752D68396,
					0xC4938EBAA076E71D,
					0x63699A962E55DD2,
					0xC67BCEF362C66CA6,
					0xF32959A07FD41375,
					0x4BC7D62620A6124,
					0x11DC9CFC8C6BBE0A,
					0x5F3CA97A4AE589F6,
					0xC77FC1CB6BA59110,
					0x1FB4C1DC7502DD74,
				},
				{
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
					0x0,
				},
			};
		}
	}
}
