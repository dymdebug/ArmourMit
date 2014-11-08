using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// ���SqlServerר�����ӵĹ���
	/// </summary>
	public class SqlFactory : AbstractDbFactory
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public SqlFactory()
		{
		}

		/// <summary>
		/// ����Ĭ��Connection����
		/// </summary>
		/// <returns>Connection����</returns>
		public IDbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		/// <summary>
		/// ���������ַ�������Connection����
		/// </summary>
		/// <param name="strConn">�����ַ���</param>
		/// <returns>Connection����</returns>
		public IDbConnection CreateConnection(string strConn)
		{
			return new SqlConnection(strConn);
		}

		/// <summary>
		/// ����Command����
		/// </summary>
		/// <returns>Command����</returns>
		public IDbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		/// <summary>
		/// ����DataAdapter����
		/// </summary>
		/// <returns>DataAdapter����</returns>
		public IDbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>
		/// ����Connection����Transaction
		/// </summary>
		/// <param name="myDbConnection">Connection����</param>
		/// <returns>Transaction����</returns>
		public IDbTransaction CreateTransaction(IDbConnection myDbConnection)
		{
			return myDbConnection.BeginTransaction();
		}

		/// <summary>
		/// ����Command����DataReader
		/// </summary>
		/// <param name="myDbCommand">Command����</param>
		/// <returns>DataReader����</returns>
		public IDataReader CreateDataReader(IDbCommand myDbCommand)
		{
			return myDbCommand.ExecuteReader();
		}

		/// <summary>
		/// ��������ַ���
		/// </summary>
		/// <returns>�����ַ���</returns>
		public string GetConnectionString()
		{
			string strServer = ConfigurationSettings.AppSettings["SqlServerServer"];
			string strDatabase = ConfigurationSettings.AppSettings["SqlServerDatabase"];
			string strUid = ConfigurationSettings.AppSettings["SqlServerUid"];
			string strPwd = ConfigurationSettings.AppSettings["SqlServerPwd"];
			string strConnectionString = "Server = " + strServer + "; Database = " + strDatabase + "; Uid = " + strUid + "; Pwd = " + strPwd + ";";
			return strConnectionString;
		}

	}
}


