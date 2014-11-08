using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// ���OleDb���ӵĹ���
	/// </summary>
	public class OleDbFactory : AbstractDbFactory
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public OleDbFactory()
		{
		}

		/// <summary>
		/// ����Ĭ��Connection����
		/// </summary>
		/// <returns>Connection����</returns>
		public IDbConnection CreateConnection()
		{
			return new OleDbConnection();
		}

		/// <summary>
		/// ���������ַ�������Connection����
		/// </summary>
		/// <param name="strConn">�����ַ���</param>
		/// <returns>Connection����</returns>
		public IDbConnection CreateConnection(string strConn)
		{
			return new OleDbConnection(strConn);
		}

		/// <summary>
		/// ����Command����
		/// </summary>
		/// <returns>Command����</returns>
		public IDbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		/// <summary>
		/// ����DataAdapter����
		/// </summary>
		/// <returns>DataAdapter����</returns>
		public IDbDataAdapter CreateDataAdapter()
		{
			return new OleDbDataAdapter();
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
			string strProvider = ConfigurationSettings.AppSettings["OleDbProvider"];
			string strDataSource = ConfigurationSettings.AppSettings["OleDbDataSource"];
			string strOleDbUid=ConfigurationSettings.AppSettings["OleDbUid"];
			string strOleDbPwd=ConfigurationSettings.AppSettings["OleDbPwd"];
			string strOleDbDataBase=ConfigurationSettings.AppSettings["OleDbDataBase"];
			string strConnectionString = "Provider = " + strProvider + ";Data Source = " + strDataSource + ";Initial Catalog="+strOleDbDataBase+";User ID="+strOleDbUid+";Password="+strOleDbPwd+";";
			return strConnectionString;
		}

	}
}


