using System;
using System.Data;
using System.Data.OleDb;
using RMTFRK.Core.Helper.Validate;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// DBUtilitys ��ժҪ˵����
	/// </summary>
	public class PKUtilitys
	{
		/// <summary>
		/// �������С����
		/// </summary>
		/// <param name="IDPRE"></param>
		/// <returns></returns>
		public static string GetNewIDPreKey(string IDPRE)
		{
			 
			string TempId="";
			OleDbParameter[] aParams={DbAccess.MakeParam("IDPrefixCode",OleDbType.VarChar,10,ParameterDirection.Input,IDPRE),DbAccess.MakeParam("formnum",OleDbType.VarChar,20,ParameterDirection.Output,"")};
			/*OleDbParameter[] aParams=new OleDbParameter[]{new OleDbParameter("@IDPrefixCode",OleDbType.VarChar,10,"@IDPrefixCode"),new OleDbParameter("@formnum",OleDbType.VarChar,20,"@formnum")};
			aParams[0].Value=IDPRE;
			aParams[0].Direction=ParameterDirection.Input;
			aParams[1].Value="";
			aParams[1].Direction=ParameterDirection.Output;
			*/
			TempId=DbAccess.getProcOutValue("sp_getIDPreKey",aParams);
			return TempId;
		}
		/// <summary>
		/// ����������ʹ�����
		/// </summary>
		/// <param name="IDPRE"></param>
		/// <param name="rad">ȡֵ3-5</param>
		/// <returns></returns>
		public static string GetRAMPK(string PKType, int rad)
		{
			IdentityGenerator ig = new IdentityGenerator();
			string pkm = PKType + ig.GetIdentity(rad).ToString();
			return pkm;
		}

		/// <summary>
		/// ��ô����� �÷����Ѿ�ȡ��������ʹ��GetRAMPK 
		/// �������ɵ�������2004061517391154.����������2004��6��15�գ�����17ʱ39��11�뷢����������λ���������������֤��һͬһ���ڿ���ͬʱ��������99��ͬʱ�������������Ҫ����Ҳ���ԣ���longRandom = myRandom.Next(1,100);�����100��һ�¾ͺ��ˡ�
		/// </summary>
		/// <param name="PKType">������;��LK�����ӣ�U��ע���û�</param>
		/// <returns></returns>
		public static string GetPK(string PKType)
		{
			long longRandom;
			Random myRandom = new Random(unchecked((int)DateTime.Now.Ticks));
			longRandom = myRandom.Next(1, 1000);
			//string currentTime=System.DateTime.Now.ToString();
			string year = "", day = "", month = "", hour = "", minute = "", second = "", randomnum = "";
			year = DateTime.Now.Year.ToString();
			month = DateTime.Now.Month.ToString();
			day = DateTime.Now.Day.ToString();
			hour = DateTime.Now.Hour.ToString();
			minute = DateTime.Now.Minute.ToString();
			second = DateTime.Now.Second.ToString();
			randomnum = longRandom.ToString();
			if (int.Parse(month) < 10)
			{
				month = "0" + month;
			}
			if (int.Parse(day) < 10)
			{
				day = "0" + day;
			}
			if (int.Parse(hour) < 10)
			{
				hour = "0" + hour;
			}
			if (int.Parse(minute) < 10)
			{
				minute = "0" + minute;
			}
			if (int.Parse(second) < 10)
			{
				second = "0" + second;
			}
			if (int.Parse(randomnum) < 10)
			{
				randomnum = "0" + randomnum;
			}
			string nowdate = year + month + day + hour + minute + second;
			//string PK=PKType+nowdate+randomnum;
			string PK = PKType + nowdate + GeneralFun.RndString(5);
			return PK;
		}
		
	}
}
