using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// 针对OleDb连接的工厂
	/// </summary>
	public class OleDbFactory : AbstractDbFactory
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public OleDbFactory()
		{
		}

		/// <summary>
		/// 建立默认Connection对象
		/// </summary>
		/// <returns>Connection对象</returns>
		public IDbConnection CreateConnection()
		{
			return new OleDbConnection();
		}

		/// <summary>
		/// 根据连接字符串建立Connection对象
		/// </summary>
		/// <param name="strConn">连接字符串</param>
		/// <returns>Connection对象</returns>
		public IDbConnection CreateConnection(string strConn)
		{
			return new OleDbConnection(strConn);
		}

		/// <summary>
		/// 建立Command对象
		/// </summary>
		/// <returns>Command对象</returns>
		public IDbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		/// <summary>
		/// 建立DataAdapter对象
		/// </summary>
		/// <returns>DataAdapter对象</returns>
		public IDbDataAdapter CreateDataAdapter()
		{
			return new OleDbDataAdapter();
		}

		/// <summary>
		/// 根据Connection建立Transaction
		/// </summary>
		/// <param name="myDbConnection">Connection对象</param>
		/// <returns>Transaction对象</returns>
		public IDbTransaction CreateTransaction(IDbConnection myDbConnection)
		{
			return myDbConnection.BeginTransaction();   
		}

		/// <summary>
		/// 根据Command建立DataReader
		/// </summary>
		/// <param name="myDbCommand">Command对象</param>
		/// <returns>DataReader对象</returns>
		public IDataReader CreateDataReader(IDbCommand myDbCommand)
		{
			return myDbCommand.ExecuteReader();
		}

		/// <summary>
		/// 获得连接字符串
		/// </summary>
		/// <returns>连接字符串</returns>
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


