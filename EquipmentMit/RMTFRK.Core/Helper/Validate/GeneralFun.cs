using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace RMTFRK.Core.Helper.Validate
{
	/// <summary>
	/// GeneralFun 的摘要说明。
	/// </summary>
	public class GeneralFun 
	{

		public static DataSet ConvertIListToDS(IList lst, System.Type typ)
		{

			DataSet ds = new DataSet();
			DataTable tbl = ds.Tables.Add(typ.Name);
			System.Reflection.PropertyInfo[] myPropertyInfo = typ.GetProperties((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
			;
			foreach (PropertyInfo pi in myPropertyInfo)
			{
				tbl.Columns.Add(pi.Name, System.Type.GetType(pi.PropertyType.ToString()));
			}
			foreach (object obj in lst)
			{
				DataRow dr = tbl.NewRow();
				foreach (PropertyInfo pi in myPropertyInfo)
				{
					dr[pi.Name] = pi.GetValue(obj, null);
				}
				tbl.Rows.Add(dr);
			}
			return ds;
		} 
		
		
		public static string GetRandomCode(int number) 
		{ 
			string[] arrList = new string[] {"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F","G", 
												"H","I","J","K","L","M","N","O","P","Q","R","S","T","U","W","X","Y","Z"} ; 
			StringBuilder sb = new StringBuilder("") ; 
			Random random = new Random() ; 
 
			for( int i = 0 ; i < number ; i++ ) 
			{ 
				sb.Append(arrList[(int)random.Next(0,arrList.Length)]) ; 
			} 
 
			return sb.ToString() ; 
		} 

		public static string RndString(int len)
		{
			//验证码len=5
			string text1 = "";
			Random random1 = new Random();
			for (int num1 = 0; num1 < len; num1++)
			{
				text1 = text1 + random1.Next(9);
			}
			return text1;
		}
		

		public static ArrayList StrToArray(string content,string delimit)
		{
			/*string delimStr = "|";
				char [] delimiter = delimStr.ToCharArray();
				aContent=article.Content.Split(delimiter);
				*/
			string text2 = delimit;
			ArrayList list1 = new ArrayList();
			while (content != "")
			{
				int num2 = content.IndexOf(text2);
				if (num2 >= 0)
				{
					list1.Add(content.Substring(0, num2));
					content = content.Remove(0, num2 + text2.Length);
					continue;
				}
				list1.Add(content);
				content = "";
			}
			return list1;
		}
		public static bool IsInArray(string[] array, string element) 
		{
			foreach (string arrayElement in array) 
			{
				if (arrayElement == element) 
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// 编码
		/// </summary>
		/// <param name="code_type"></param>
		/// <param name="code"></param>
		/// <returns></returns>
			public static string EncodeBase64(string code_type,string code)
			{
				string encode = "";
				byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
				try
				{
					encode = Convert.ToBase64String(bytes);
				}
				catch
				{
					encode = code;
				}
				return encode;
			}
		/// <summary>
		/// 解码
		/// </summary>
		/// <param name="code_type"></param>
		/// <param name="code"></param>
		/// <returns></returns>

			public static string DecodeBase64(string code_type,string code)
			{
				string decode = "";
				byte[] bytes = Convert.FromBase64String(code);
				try
				{
					decode = Encoding.GetEncoding(code_type).GetString(bytes);
				}
				catch
				{
					decode = code;
				}
				return decode;
			} 

		
		/// <summary>
		/// 生成服务端文件名称
		/// </summary>
		/// <param name="extName"></param>
		/// <returns></returns>
		public static string GenServerName(string extName)
		{
		
			string serverName="FL"+System.DateTime.Now.Year;
			if(System.DateTime.Now.Month.ToString().Length<2)
			{
				serverName+="0"+System.DateTime.Now.Month.ToString();
			}
			else
			{
				serverName+=System.DateTime.Now.Month.ToString();
			}
			if(System.DateTime.Now.Day.ToString().Length<2)
			{
				serverName+="0"+System.DateTime.Now.Day.ToString();
			}
			else
			{
				serverName+=System.DateTime.Now.Day.ToString();
			}
			if(System.DateTime.Now.Hour.ToString().Length<2)
			{
				serverName+="0"+System.DateTime.Now.Hour.ToString();
			}
			else
			{
				serverName+=System.DateTime.Now.Hour.ToString();
			}
			if(System.DateTime.Now.Minute.ToString().Length<2)
			{
				serverName+="0"+System.DateTime.Now.Minute.ToString();
			}
			else
			{
				serverName+=System.DateTime.Now.Minute.ToString();
			}
			if(System.DateTime.Now.Second.ToString().Length<2)
			{
				serverName+="0"+System.DateTime.Now.Second.ToString();
			}
			else
			{
				serverName+=System.DateTime.Now.Second.ToString();
			}

				serverName+=GeneralFun.GetRandNum(4)+extName;
				
			return serverName;
		}
		/// <summary>
		/// 删除服务端文件
		/// </summary>
		/// <param name="filepath"></param>
		public static void DeleteFile(string filepath)
		{
			if (System.IO.File.Exists(filepath))
				{
					System.IO.File.Delete(filepath);
					
				}
		}
		/// <summary>
		/// 取得随机数
		/// </summary>
		/// <param name="randNumLength">随机数的长度</param>
		public static string GetRandNum( int randNumLength )
		{
			System.Random randNum = new System.Random( unchecked( ( int ) DateTime.Now.Ticks ) );
			StringBuilder sb = new StringBuilder( randNumLength );
			for ( int i = 0; i < randNumLength; i++ )
			{
				sb.Append( randNum.Next( 0, 9 ) );
			}
			return sb.ToString();
		}
		/// <summary>
		/// 字符串截取函数
		/// </summary>
		/// <param name="inputString"></param>
		/// <param name="len"></param>
		/// <returns></returns>
		public static string CutString(string inputString,int len)
		{

			ASCIIEncoding ascii =  new ASCIIEncoding();
			int tempLen=0;
			string tempString="";
			byte[] s = ascii.GetBytes(inputString);
			for(int i=0;i<s.Length;i++)
			{
				if((int)s[i]==63)
				{
					tempLen+=2;
				}
				else
				{
					tempLen+=1;
				}
				
				try
				{
					tempString+=inputString.Substring(i,1);
				}
				catch
				{
					break;
				}

				if(tempLen>len)
					break;
			}
			//如果截过则加上半个省略号
			byte[] mybyte=System.Text.Encoding.Default.GetBytes(inputString);
			if(mybyte.Length>len)
				tempString+="…";


			return tempString;
		}
	
		public static object changecode(string str)
		{
			
			str = Strings.Replace(str, " ", " ", 1, -1, CompareMethod.Binary);
			str = Strings.Replace(str, "\t", "&nbsp;", 1, -1, CompareMethod.Binary);
			str = Strings.Replace(str, "\"", "&quot;", 1, -1, CompareMethod.Binary);
			str = Strings.Replace(str, "\'", "&#39;", 1, -1, CompareMethod.Binary);
			str = Strings.Replace(str, "\r", "", 1, -1, CompareMethod.Binary);
			str = Strings.Replace(str, "\n\n", "</P><P>", 1, -1, CompareMethod.Binary);
			str = Strings.Replace(str, "\n", "<BR> ", 1, -1, CompareMethod.Binary);
			return str;
			
		}
		public static string chkDBNull(object str)
		{
			if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(str)))
			{
				str = "";
			}
			return StringType.FromObject(str);
		}
		public static string Decode1(string NameValueStr)
		{
			int num1=0;
			uint num2;
			char ch1;
			if ((StringType.StrCmp(NameValueStr, null, false) == 0) | (StringType.StrCmp(NameValueStr, "", false) == 0))
			{
				return "";
			}
			string text4 = Strings.Trim(NameValueStr);
			text4 = text4.Replace("X", "1U1");
			text4 = text4.Replace("Y", "2U2");
			text4 = text4.Replace("Z", "3U3");
			text4 = text4.Replace("V", "4U5");
			text4 = text4.Replace("W", "7U6");
			text4 = text4.Replace("O", "8U7");
			text4 = text4.Replace("P", "0U8");
			text4 = text4.Replace("Q", "0U9");
			text4 = text4.Replace("R", "1U0");
			text4 = text4.Replace("S", "2U4");
			text4 = text4.Replace("x", "U1");
			text4 = text4.Replace("y", "U2");
			text4 = text4.Replace("z", "U3");
			text4 = text4.Replace("u", "U4");
			text4 = text4.Replace("v", "U5");
			text4 = text4.Replace("w", "U6");
			text4 = text4.Replace("o", "U7");
			text4 = text4.Replace("p", "U8");
			text4 = text4.Replace("q", "U9");
			text4 = text4.Replace("r", "U0");
			string text2 = "";
			string text3 = "";
			int num3 = (num1 + 1);
			int num4 = text4.Length;
			for (num1 = 1; (((num3 >> 31) ^ num1) <= ((num3 >> 31) ^ num4)); num1 += num3)
			{
				if (StringType.StrCmp(Strings.Mid(text4, num1, 1), "U", false) == 0)
				{
					if (StringType.StrCmp(text2, "", false) != 0)
					{
						num2 = Convert.ToUInt32(text2);
						ch1 = Convert.ToChar(num2);
						text3 = text3 + ch1.ToString();
						text2 = "";
					}
				}
				else
				{
					text2 = text2 + Strings.Mid(text4, num1, 1).ToString();
				}
			}
			if (StringType.StrCmp(text2, "", false) != 0)
			{
				num2 = Convert.ToUInt32(text2);
				ch1 = Convert.ToChar(num2);
				text3 = text3 + ch1.ToString();
			}
			return text3;
		}
		public static string Decrypto(string str)
		{
			
			if ((StringType.StrCmp(str, "", false) != 0) & (str != null))
			{
				return HttpUtility.UrlDecode(crypt.crypt.Decrypto(str));
			}
			else
			{
				return "";
			}
			
		}
		public static string Encode1(string NameValueStr)
		{
			int num1=0;
			ushort num2;
			if ((StringType.StrCmp(NameValueStr, null, false) == 0) | (StringType.StrCmp(NameValueStr, "", false) == 0))
			{
				return "";
			}
			string text2 = "";
			NameValueStr = Strings.Trim(NameValueStr);
			int num3 = (num1 + 1);
			int num4 = NameValueStr.Length;
			for (num1 = 1; (((num3 >> 31) ^ num1) <= ((num3 >> 31) ^ num4)); num1 += num3)
			{
				num2 = Convert.ToUInt16(Strings.Mid(NameValueStr, num1, 1));
				text2 = text2 + "U" + num2.ToString();
			}
			text2 = text2.Replace("1U1", "X");
			text2 = text2.Replace("2U2", "Y");
			text2 = text2.Replace("3U3", "Z");
			text2 = text2.Replace("4U5", "V");
			text2 = text2.Replace("7U6", "W");
			text2 = text2.Replace("8U7", "O");
			text2 = text2.Replace("0U8", "P");
			text2 = text2.Replace("0U9", "Q");
			text2 = text2.Replace("1U0", "R");
			text2 = text2.Replace("2U4", "S");
			text2 = text2.Replace("U1", "x");
			text2 = text2.Replace("U2", "y");
			text2 = text2.Replace("U3", "z");
			text2 = text2.Replace("U4", "u");
			text2 = text2.Replace("U5", "v");
			text2 = text2.Replace("U6", "w");
			text2 = text2.Replace("U7", "o");
			text2 = text2.Replace("U8", "p");
			text2 = text2.Replace("U9", "q");
			return text2.Replace("U0", "r");
		}
		public static string Encrypto(string str)
		{
			
			if ((StringType.StrCmp(str, "", false) != 0) & (str != null))
			{
				return HttpUtility.UrlEncode(crypt.crypt.Encrypto(str));
			}
			else
			{
				return "";
			}
			
		}
		
		public static string RndNum(int VcodeNum)
		{
			int num1;
			string text1 = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z";
			string[] textArray1 = Strings.Split(text1, ",", -1, CompareMethod.Binary);
			string text2 = "";
			int num2 = VcodeNum;
			for (num1 = 1; num1 <= num2; num1++)
			{
				//Random ro=new Random();
				VBMath.Randomize();
				text2 = text2 + textArray1[((int) Math.Round(((double) Conversion.Int((35f * VBMath.Rnd())))))];
			}
			return text2;
		}
		public static string GenRndNum(int CodeLength)
		{
			string Vchar="0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
			string[]VcArray=Vchar.Split(new char[]{','}); //将字符串生成数组
			string VNum ="";
   
			for(byte i=1;i<=CodeLength;i++)
			{
				Random ro=new Random();

				VNum+=VcArray[((int) Math.Round(((double) Conversion.Int((35f * ro.NextDouble())))))]; //数组一般从0开始读取，所以这里为35*Rnd
			}
		   
			return VNum;
		}
		public static object sqlstr(string s)
		{
			string[] textArray1;
			string text1;
			int num1;
			string[] textArray2;
			string[] textArray3;
			s = Strings.Trim(s);
			if (StringType.StrCmp(s, "", false) != 0)
			{
				textArray3 = new string[5];
				textArray3[0] = "select";
				textArray3[1] = "union";
				textArray3[2] = "insert";
				textArray3[3] = "delete";
				textArray3[4] = "--";
				textArray1 = textArray3;
				textArray2 = textArray1;
				for (num1 = 0; (num1 < textArray2.Length); ++num1)
				{
					text1 = textArray2[num1];
					if (Strings.InStr(s.ToLower(), text1, CompareMethod.Binary) > 0)
					{
						s = Strings.Replace(s.ToLower(), text1, Strings.StrConv(text1, VbStrConv.Wide, 0), 1, -1, CompareMethod.Binary);
					}
				}
				s = s.Replace("\'", "\'\'");
				return s;
			}
			return "";
		}
		public static object strDeEnc(string str)
		{
			string text1="";
			string[] textArray1 = Strings.Split(str, "|", -1, CompareMethod.Binary);
			int num1 = 0;
			int num2 = Information.UBound(textArray1, 1);
			for (num1 = 1; (num1 <= num2); ++num1)
			{
				text1 = text1 + Strings.Trim(StringType.FromChar(Strings.Chr(((int) Math.Round((DoubleType.FromString(textArray1[num1]) / 3f)))))).ToString();
			}
			return text1;
		}
		public static object strEnc(string str)
		{
			string text1="";
			int num1 = 0;
			int num2 = Strings.Len(str);
			for (num1 = 1; (num1 <= num2); ++num1)
			{
				text1 = text1 + "|" + Strings.Trim(StringType.FromInteger((Strings.Asc(Strings.Mid(str, num1, 1)) * 3))).ToString();
			}
			return text1;
		}
		/// <summary>
		/// 加密密码
		/// </summary>
		/// <param name="PasswordString">密码明码</param>
		/// <param name="PasswordFormat">密码算法 SHA1或者MD5</param>
		public string EncryptPassword(string PasswordString,string PasswordFormat) 
	{ 
		if(PasswordFormat=="SHA1")
		{ 
		
			//return FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString ,"SHA1"); 
			HashAlgorithm  HashCryptoService = new SHA1Managed();
			byte[] bytIn = UTF8Encoding.UTF8.GetBytes(PasswordString);
			byte[] bytOut = HashCryptoService.ComputeHash(bytIn);
			return Convert.ToBase64String(bytOut);

		} 
		else if(PasswordFormat=="MD5")
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] bytIn = System.Text.Encoding.Default.GetBytes(PasswordString); 
			byte[] bytOut = md5.ComputeHash(bytIn); 
			return System.Text.Encoding.Default.GetString(bytOut); 
		
		} 
		else 
		{ 
			return ""; 
		}
		}
		/// <summary>
		/// 使用FormsAuthentication加密密码
		/// </summary>
		/// <param name="PasswordString">密码明码</param>
		/// <param name="PasswordFormat">密码算法 SHA1或者MD5</param>
		/// <returns></returns>
		public string EncryptFormPassword(string PasswordString,string PasswordFormat) 
	{ 
		if(PasswordFormat=="SHA1")
		{ 
		
			return FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString ,"SHA1"); 
			

		} 
		else if(PasswordFormat=="MD5")
		{
			return FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString ,"MD5"); 
			
		} 
		else 
		{ 
			return ""; 
		}
		}

		public static string CNStringLeft(string str, int len)
		{
			Encoding encoding1 = Encoding.GetEncoding(936);
			int num1 = encoding1.GetByteCount(str);
			if (num1 > len)
			{
				if(str.Length>0)
				{
					while (num1 > (len - 3))
					{
						str = str.Substring(0, str.Length - 1);
						num1 = encoding1.GetByteCount(str);
					}
					str = str + "...";
				}
			}
			return str;
		}
 
		public static int CNStringLength(string str)
		{
			Encoding encoding1 = Encoding.GetEncoding(936);
			return encoding1.GetByteCount(str);
		}
		
		
		
		
	
	}
}
