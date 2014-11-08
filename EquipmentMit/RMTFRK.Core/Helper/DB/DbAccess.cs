using System;
using System.Data;
using System.Data.OleDb;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// 数据库访问类，即大家需要真正使用的类
	/// </summary>
	public class DbAccess
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public DbAccess()
		{
		}

		/// <summary>
		/// 无条件查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumn">列名组</param>
		/// <returns>查询结果</returns>
		public static DataSet Select(string strTableName, string[] strColumn)
		{
			
			DataSet ds = new DataSet();
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;
			IDbDataAdapter concreteDbAdapter = abstractDbFactory.CreateDataAdapter();

			string strSql = "SELECT ";
			for(int i = 0; i < strColumn.Length - 1; i++)
			{
				strSql += (strColumn[i] + ", ");
			}
			strSql += (strColumn[strColumn.Length - 1] + " FROM " + strTableName);
  
			try
			{
				concreteDbCommand.CommandText = strSql;
				concreteDbAdapter.SelectCommand = concreteDbCommand;
				//Debug.WriteLine(strSql);
				concreteDbAdapter.Fill(ds);
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				ds.Clear();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
  
			return ds;
		}

  

		/// <summary>
		/// 条件查询操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumn">列名组</param>
		/// <param name="strCondition">条件</param>
		/// <returns>查询结果</returns>
		public static DataSet Select(string strTableName, string[] strColumn, string strCondition)
		{
			DataSet ds = new DataSet();

			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();

			concreteDbConn.Open();
			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;

			IDbDataAdapter concreteDbAdapter = abstractDbFactory.CreateDataAdapter();

			string strSql = "SELECT ";
			for(int i = 0; i < strColumn.Length - 1; i++)
			{
				strSql += (strColumn[i] + ", ");
			}
			strSql += (strColumn[strColumn.Length - 1] + " FROM " + strTableName + " WHERE " + strCondition);
			
			try
			{
				concreteDbCommand.CommandText = strSql;
				concreteDbAdapter.SelectCommand = concreteDbCommand;    
	
				concreteDbAdapter.Fill(ds);
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				ds.Clear();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
			return ds;
		}

  

		/// <summary>
		/// 插入操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumn">列名组</param>
		/// <param name="strValue">值组</param>
		public static void Insert(string strTableName, string[] strColumn, object[] strValue)
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
   
			concreteDbConn.Open();
			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;

			string strSql = "INSERT INTO " + strTableName + " (";

			for(int i = 0; i < strColumn.Length - 1; i++)
			{
				strSql += (strColumn[i] + ", ");
			}
			strSql += (strColumn[strColumn.Length - 1] + ") VALUES ('");

			for(int i = 0; i < strValue.Length - 1; i++)
			{
				strSql += (strValue[i] + "', '");
			}
			strSql += (strValue[strValue.Length - 1] + "')");
   
			try
			{
				concreteDbCommand.CommandText = strSql;
				
				concreteDbCommand.ExecuteNonQuery();
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
   
		}
 

		/// <summary>
		/// 删除操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strCondition">条件</param>
		public static void Delete(string strTableName, string strCondition)
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();

			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();

			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;

			string strSql = "DELETE FROM " + strTableName + " WHERE " + strCondition;
			try
			{
				concreteDbCommand.CommandText = strSql;
				concreteDbCommand.ExecuteNonQuery();
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
   
		}

  


		/// <summary>
		/// 更新操作
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="strColumn">列名组</param>
		/// <param name="strValue">值组</param>
		/// <param name="strCondition">条件</param>
		public static void Update(string strTableName, string[] strColumn, object[] strValue, string strCondition)
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
   
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;

			string strSql = "UPDATE " + strTableName + " SET ";
			for(int i = 0; i < strColumn.Length - 1; i++)
			{
				strSql += (strColumn[i] + " = '" + strValue[i] + "', ");
			}
			strSql += (strColumn[strColumn.Length - 1] + " = '" + strValue[strValue.Length - 1] + "' " + " WHERE " + strCondition);
   
			try
			{
				concreteDbCommand.CommandText = strSql;
				concreteDbCommand.ExecuteNonQuery();
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
		}
		/// <summary>
		/// Make input param.
		/// </summary>
		/// <param name="ParamName">Name of param.</param>
		/// <param name="DbType">Param type.</param>
		/// <param name="Size">Param size.</param>
		/// <param name="Value">Param value.</param>
		/// <returns>New parameter.</returns>
		public OleDbParameter MakeInParam(string ParamName, OleDbType DbType, int Size, object Value) 
		{
			return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
		}		

		/// <summary>
		/// Make input param.
		/// </summary>
		/// <param name="ParamName">Name of param.</param>
		/// <param name="DbType">Param type.</param>
		/// <param name="Size">Param size.</param>
		/// <returns>New parameter.</returns>
		public  OleDbParameter MakeOutParam(string ParamName, OleDbType DbType, int Size) 
		{
			return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
		}		

		/// <summary>
		/// Make stored procedure param.
		/// </summary>
		/// <param name="ParamName">Name of param.</param>
		/// <param name="DbType">Param type.</param>
		/// <param name="Size">Param size.</param>
		/// <param name="Direction">Parm direction.</param>
		/// <param name="Value">Param value.</param>
		/// <returns>New parameter.</returns>
		public static OleDbParameter MakeParam(string ParamName, OleDbType DbType, Int32 Size, ParameterDirection Direction, object Value) 
		{
			OleDbParameter param;

			if(Size > 0)
				param = new OleDbParameter(ParamName, DbType, Size);
			else
				param = new OleDbParameter(ParamName, DbType);

			param.Direction = Direction;
			if (!(Direction == ParameterDirection.Output && Value == null))
				param.Value = Value;

			return param;
		}

		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <returns>Stored procedure return value.</returns>
		
		public static int RunProc(string procName) 
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;
			IDbDataAdapter concreteDbAdapter = abstractDbFactory.CreateDataAdapter();
			
			try
			{
				concreteDbCommand.CommandType = CommandType.StoredProcedure;
				concreteDbCommand.CommandText=procName;
				concreteDbCommand.ExecuteNonQuery();
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
			
			return (int)concreteDbCommand.Parameters["ReturnValue"];
		}

		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="prams">Stored procedure params.</param>
		/// <returns>Stored procedure return value.</returns>
		public static int RunProc(string procName, OleDbParameter[] prams) 
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;
			IDbDataAdapter concreteDbAdapter = abstractDbFactory.CreateDataAdapter();
			
			try
			{
				concreteDbCommand.CommandType = CommandType.StoredProcedure;
					concreteDbCommand.CommandText=procName;
				// add proc parameters
				if (prams != null) 
				{
					foreach (OleDbParameter parameter in prams)
						concreteDbCommand.Parameters.Add(parameter);
				}
			
				// return param
				concreteDbCommand.Parameters.Add(
					new OleDbParameter("ReturnValue", OleDbType.Integer, 4,
					ParameterDirection.ReturnValue, false, 0, 0,
					string.Empty, DataRowVersion.Default, null));
				concreteDbCommand.ExecuteNonQuery();
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
			
			return (int)concreteDbCommand.Parameters["ReturnValue"];
		}

		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="dataReader">Return result of procedure.</param>
		public static void RunProc(string procName, out OleDbDataReader dataReader) 
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;
			IDbDataAdapter concreteDbAdapter = abstractDbFactory.CreateDataAdapter();
			
			try
			{
				concreteDbCommand.CommandType = CommandType.StoredProcedure;
				concreteDbCommand.CommandText=procName;
				concreteDbCommand.ExecuteNonQuery();
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
			
			
			dataReader = (OleDbDataReader)concreteDbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
		}

		/// <summary>
		/// Run stored procedure.
		/// </summary>
		/// <param name="procName">Name of stored procedure.</param>
		/// <param name="prams">Stored procedure params.</param>
		/// <param name="dataReader">Return result of procedure.</param>
		public static void RunProc(string procName, OleDbParameter[] prams, out OleDbDataReader dataReader) 
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;
			IDbDataAdapter concreteDbAdapter = abstractDbFactory.CreateDataAdapter();
			
			try
			{
				concreteDbCommand.CommandType = CommandType.StoredProcedure;
				concreteDbCommand.CommandText=procName;
				// add proc parameters
				if (prams != null) 
				{
					foreach (OleDbParameter parameter in prams)
						concreteDbCommand.Parameters.Add(parameter);
				}
			
				// return param
				concreteDbCommand.Parameters.Add(
					new OleDbParameter("ReturnValue", OleDbType.Integer, 4,
					ParameterDirection.ReturnValue, false, 0, 0,
					string.Empty, DataRowVersion.Default, null));
				concreteDbCommand.ExecuteNonQuery();
				dataReader = (OleDbDataReader)concreteDbCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
			
					
			
		}
		public static string getProcOutValue(string procName, OleDbParameter[] prams) 
		{
			Factory factory = Factory.GetInstance();
			AbstractDbFactory abstractDbFactory = factory.CreateInstance();
			IDbConnection concreteDbConn = abstractDbFactory.CreateConnection();
			concreteDbConn.ConnectionString = abstractDbFactory.GetConnectionString();
			concreteDbConn.Open();

			IDbCommand concreteDbCommand = abstractDbFactory.CreateCommand();

			IDbTransaction concreteDbTrans = abstractDbFactory.CreateTransaction(concreteDbConn);
			concreteDbCommand.Connection = concreteDbConn;
			concreteDbCommand.Transaction = concreteDbTrans;
			IDbDataAdapter concreteDbAdapter = abstractDbFactory.CreateDataAdapter();
			string retval="";
			try
			{
				concreteDbCommand.CommandType = CommandType.StoredProcedure;
				concreteDbCommand.CommandText=procName;
				// add proc parameters
				if (prams != null) 
				{
					foreach (OleDbParameter parameter in prams)
						concreteDbCommand.Parameters.Add(parameter);
				}
			
				// return param
				/*concreteDbCommand.Parameters.Add(
					new OleDbParameter("RETURN_VALUE", OleDbType.Integer, 4,
					ParameterDirection.ReturnValue, false, 0, 0,
					string.Empty, DataRowVersion.Default, null));
				*/
				concreteDbCommand.ExecuteNonQuery();
				OleDbParameter pam=new OleDbParameter();
				for(int i=0;i<=prams.Length-1;i++)
				{
					if(prams[i].Direction==ParameterDirection.Output)
					{
						pam=(OleDbParameter)concreteDbCommand.Parameters[i];
						retval=pam.Value.ToString();
					}
				}
				concreteDbTrans.Commit();
			}
			catch
			{
				concreteDbTrans.Rollback();
				throw;
			}
			finally
			{
				concreteDbConn.Close();
			}
			
				return retval;
			
		}

		
		
		
	}
}

