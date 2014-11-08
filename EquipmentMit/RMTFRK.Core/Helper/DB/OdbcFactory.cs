using System.Configuration;
using System.Data;
using System.Data.Odbc;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// 针对Odbc连接的工厂
	/// </summary>
	public class OdbcFactory : AbstractDbFactory
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public OdbcFactory()
		{
		}

		/// <summary>
		/// 建立默认Connection对象
		/// </summary>
		/// <returns>Connection对象</returns>
		public IDbConnection CreateConnection()
		{
			return new OdbcConnection();
		}

		/// <summary>
		/// 根据连接字符串建立Connection对象
		/// </summary>
		/// <param name="strConn">连接字符串</param>
		/// <returns>Connection对象</returns>
		public IDbConnection CreateConnection(string strConn)
		{
			return new OdbcConnection(strConn);
		}

		/// <summary>
		/// 建立Command对象
		/// </summary>
		/// <returns>Command对象</returns>
		public IDbCommand CreateCommand()
		{
			return new OdbcCommand();
		}

		/// <summary>
		/// 建立DataAdapter对象
		/// </summary>
		/// <returns>DataAdapter对象</returns>
		public IDbDataAdapter CreateDataAdapter()
		{
			return new OdbcDataAdapter();
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



