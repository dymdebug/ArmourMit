using System.Configuration;
using System.Data;
using System.Data.Odbc;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// ���Odbc���ӵĹ���
	/// </summary>
	public class OdbcFactory : AbstractDbFactory
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public OdbcFactory()
		{
		}

		/// <summary>
		/// ����Ĭ��Connection����
		/// </summary>
		/// <returns>Connection����</returns>
		public IDbConnection CreateConnection()
		{
			return new OdbcConnection();
		}

		/// <summary>
		/// ���������ַ�������Connection����
		/// </summary>
		/// <param name="strConn">�����ַ���</param>
		/// <returns>Connection����</returns>
		public IDbConnection CreateConnection(string strConn)
		{
			return new OdbcConnection(strConn);
		}

		/// <summary>
		/// ����Command����
		/// </summary>
		/// <returns>Command����</returns>
		public IDbCommand CreateCommand()
		{
			return new OdbcCommand();
		}

		/// <summary>
		/// ����DataAdapter����
		/// </summary>
		/// <returns>DataAdapter����</returns>
		public IDbDataAdapter CreateDataAdapter()
		{
			return new OdbcDataAdapter();
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
		/// <returns></returns>
		public string GetConnectionString()
		{
			string strDriver = ConfigurationSettings.AppSettings["OdbcDriver"];
			string strDBQ = ConfigurationSettings.AppSettings["OdbcDBQ"];
			string strConnectionString = "Driver={" + strDriver + "}; DBQ=" + strDBQ + ";";
			return strConnectionString;   
		}

	}
}



