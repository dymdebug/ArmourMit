using System;
using System.Data;
using System.Data.OleDb;
using RMTFRK.Core.Helper.Validate;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// DBUtilitys 的摘要说明。
	/// </summary>
	public class PKUtilitys
	{
		/// <summary>
		/// 产生码表小主键
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
		/// 产生随机类型大主键
		/// </summary>
		/// <param name="IDPRE"></param>
		/// <param name="rad">取值3-5</param>
		/// <returns></returns>
		public static string GetRAMPK(string PKType, int rad)
		{
			IdentityGenerator ig = new IdentityGenerator();
			string pkm = PKType + ig.GetIdentity(rad).ToString();
			return pkm;
		}

		/// <summary>
		/// 获得大主键 该方法已经取消，建议使用GetRAMPK 
		/// 这样生成的数形如2004061517391154.其中日期是2004年6月15日，下午17时39分11秒发生。后面两位是随机数。这样保证了一同一秒内可以同时允许不大于99个同时发生的数。如果要扩充也可以，把longRandom = myRandom.Next(1,100);后面的100改一下就好了。
		/// </summary>
		/// <param name="PKType">主键用途：LK：链接，U：注册用户</param>
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
