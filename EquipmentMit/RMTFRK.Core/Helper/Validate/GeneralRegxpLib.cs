using System.Text.RegularExpressions;
using System.Web;

namespace RMTFRK.Core.Helper.Validate
{
	/// <summary>
	/// GeneralRegxpLib ��ժҪ˵����
	/// </summary>
	public class CregexpLib
	{
		public static readonly string UidRange = @"\w{2,8}";
		public static readonly string Require = @"^\w{2}";
		public static readonly string Email = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Z
																									a-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-z
A-Z]{2,6}$";
		public static readonly string Mobile = @"^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}$";
		public static readonly string IdCard = @"^\d{15}(\d{2}[A-Za-z0-9])?$";
		public static readonly string Chinese = @"^[\u0391-\uFFE5]+$";
		public static readonly string TelePhone = @"^((\(\d{3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}$";
		public static readonly string Url = @"^(ht|f)tp(s?)\:\/\/[a-zA-Z0-9\-\._]+(\.[a-zA-Z0-9\-\._]+){2,
}(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";
		public static readonly string Currency = @"^\d+(\.\d+)?$";
		public static readonly string Number = @"^\d+$";
		public static readonly string Zip = @"^[1-9]\d{5}$";
		public static readonly string QQ = @"^[1-9]\d{4,8}$";
		public static readonly string Integer = @"^[-\+]?\d+$";
		public static readonly string Double = @"^[-\+]?\d+(\.\d+)?$";
		public static readonly string English = @"^[A-Za-z]+$";
		public static readonly string DateTime = @"(?n:^(?=\d)((?<month>(0?[13578])|1[02]|(0?[469]|11)(?!.31)|0
?2(?(.29)(?=.29.((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][
26])|(16|[2468][048]|[3579][26])00))|(?!.3[01])))(?<sep>[-./
])(?<day>0?[1-9]|[12]\d|3[01])\k<sep>(?<year>(1[6-9]|[2-9]\d
)\d{2})(?(?=\x20\d)\x20|$))?(?<time>((0?[1-9]|1[012])(:[0-5]
\d){0,2}(?i:\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$)";

		private static Regex RegNumber = new Regex("^[0-9]+$");
		private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
		private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
		private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //�ȼ���^[+-]?\d+[.]?\d+$ 
		private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
		private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

		/// <summary> 
		/// ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶����� 
		/// </summary> 
		/// <param name="req">Request</param> 
		/// <param name="inputKey">Request�ļ�ֵ</param> 
		/// <param name="maxLen">��󳤶�</param> 
		/// <returns>����Request��ѯ�ַ���</returns> 
		public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
		{



			string retVal = string.Empty;



			if (inputKey != null && inputKey != string.Empty)
			{



				retVal = req.QueryString[inputKey];



				if (null == retVal)



					retVal = req.Form[inputKey];



				if (null != retVal)
				{



					retVal = SqlText(retVal, maxLen);



					if (!IsNumber(retVal))



						retVal = string.Empty;



				}



			}



			if (retVal == null)



				retVal = string.Empty;



			return retVal;



		}

		/// <summary> 
		/// �Ƿ������ַ��� 
		/// </summary> 
		/// <param name="inputData">�����ַ���</param> 
		/// <returns></returns> 
		public static bool IsNumber(string inputData)
		{

			Match m = RegNumber.Match(inputData);
			return m.Success;

		}

		/// <summary> 
		/// �Ƿ������ַ����ɴ������� 
		/// </summary> 
		/// <param name="inputData">�����ַ���</param> 
		/// <returns></returns> 
		public static bool IsNumberSign(string inputData)
		{



			Match m = RegNumberSign.Match(inputData);



			return m.Success;



		}

		/// <summary> 
		/// �Ƿ��Ǹ����� 
		/// </summary> 
		/// <param name="inputData">�����ַ���</param> 
		/// <returns></returns> 
		public static bool IsDecimal(string inputData)
		{



			Match m = RegDecimal.Match(inputData);



			return m.Success;



		}

		/// <summary> 
		/// �Ƿ��Ǹ������ɴ������� 



		/// </summary> 



		/// <param name="inputData">�����ַ���</param> 



		/// <returns></returns> 
	   public static bool IsDecimalSign(string inputData)
		{



			Match m = RegDecimalSign.Match(inputData);



			return m.Success;



		}
	
		/// <summary> 
		/// ����Ƿ��������ַ� 
		/// </summary> 
		/// <param name="inputData"></param> 
		/// <returns></returns> 
		public static bool IsHasCHZN(string inputData)
		{

			Match m = RegCHZN.Match(inputData);
			return m.Success;

		}

		/// <summary> 
		/// �ʼ���ַ 
	   /// </summary> 
		/// <param name="inputData">�����ַ���</param> 
		/// <returns></returns> 
		public static bool IsEmail(string inputData)
		{



			Match m = RegEmail.Match(inputData);



			return m.Success;



		}
   
		/// <summary> 
		/// ����ַ�����󳤶ȣ�����ָ�����ȵĴ� 
		/// </summary> 
		/// <param name="sqlInput">�����ַ���</param> 
		/// <param name="maxLength">��󳤶�</param> 
		/// <returns></returns>          
		public static string SqlText(string sqlInput, int maxLength)
		{

			if (sqlInput != null && sqlInput != string.Empty)
			{



				sqlInput = sqlInput.Trim();



				if (sqlInput.Length > maxLength)//����󳤶Ƚ�ȡ�ַ��� 



					sqlInput = sqlInput.Substring(0, maxLength);



			}


			return sqlInput;



		}

		/**/
		/// <summary> 
		/// �жϺ�������ͨ���ƶ��������е��ĸ�,��ʹ�ñ�����ǰ��������֤����ĺϷ��� 
		/// ����ǰ��λΪ130-133 ��ͨ ��ǰ��λΪ135-139��ǰ��λΪ1340-1348 �ƶ��� ������Ӧ��Ϊ���� 
		/// </summary> 
		/// <param name="mobile">Ҫ�жϵĺ���</param> 
		/// <returns>������Ӧ���ͣ�1������ͨ��2�����ƶ���3�������</returns> 
		public static int GetMobileType(string mobile)
		{

			if (IsChinaUnicomNumber(mobile))
				return 1;

			if (IsChinaMobileNumber(mobile))
				return 2;

			return 3;
		}

		//�Ƿ�����ͨ�ĺ��� ����ͨ�� 
		private static bool IsChinaUnicomNumber(string mobile)
		{
			string sPattern = "^(130|131|132|133|186)[0-9]{8}";
			bool isChinaUnicom = Regex.IsMatch(mobile, sPattern);

			return isChinaUnicom;
		}

		//�Ƿ����ƶ��ĺ��� ����ͨ�� 
		private static bool IsChinaMobileNumber(string mobile)
		{
			string sPattern = "^(186|135|136|137|138|139|1340|1341|1342|1343|1344|1345|1346|1347|1348)[1-9]{7,8}";

			return Regex.IsMatch(mobile, sPattern);
		} 

	}
	
}
