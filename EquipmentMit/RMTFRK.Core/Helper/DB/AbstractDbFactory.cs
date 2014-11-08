using System.Data;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// 数据库抽象工厂接口
	/// </summary>
	public interface AbstractDbFactory
	{
		/// <summary>
		/// 建立默认连接
		/// </summary>
		/// <returns>数据库连接</returns>
		IDbConnection CreateConnection();

		/// <summary>
		/// 根据连接字符串建立Connection对象
		/// </summary>
		/// <param name="strConn">连接字符串</param>
		/// <returns>Connection对象</returns>
		IDbConnection CreateConnection(string strConn);

		/// <summary>
		/// 建立Command对象
		/// </summary>
		/// <returns>Command对象</returns>
		IDbCommand CreateCommand();

		/// <summary>
		/// 建立DataAdapter对象
		/// </summary>
		/// <returns>DataAdapter对象</returns>
		IDbDataAdapter CreateDataAdapter();

		/// <summary>
		/// 根据Connection建立Transaction
		/// </summary>
		/// <param name="myDbConnection">Connection对象</param>
		/// <returns>Transaction对象</returns>
		IDbTransaction CreateTransaction(IDbConnection myDbConnection);

		/// <summary>
		/// 根据Command建立DataReader
		/// </summary>
		/// <param name="myDbCommand">Command对象</param>
		/// <returns>DataReader对象</returns>
		IDataReader CreateDataReader(IDbCommand myDbCommand);

		/// <summary>
		/// 获得连接字符串
		/// </summary>
		/// <returns>连接字符串</returns>
		string GetConnectionString();
	}
}


