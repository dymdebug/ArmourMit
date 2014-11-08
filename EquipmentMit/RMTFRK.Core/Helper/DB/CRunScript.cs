using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// CRunScript 的摘要说明。
	/// </summary>
	public class CRunScript
	{
		public static void ExecuteSqlInFile( string connectionString, string pathToScriptFile ) 
		{
			try
			{
				StreamReader _reader   = null;

				string sql = "";

				if( false == System.IO.File.Exists( pathToScriptFile )) 
				{
					throw new Exception("文件 " + pathToScriptFile + " 未找到");
				}
				Stream stream = System.IO.File.OpenRead( pathToScriptFile );
				_reader = new StreamReader( stream );

				SqlConnection connection = new SqlConnection( connectionString );
				SqlCommand command = new SqlCommand();

				connection.Open();
				command.Connection = connection;
				command.CommandType = System.Data.CommandType.Text;

				while( null != (sql = ReadNextStatementFromStream( _reader ) )) 
				{
					command.CommandText = sql;

					command.ExecuteNonQuery();
				}

				_reader.Close();
			}
			catch(Exception ex)
			{
				Trace.WriteLine("在文件： " + pathToScriptFile + " 中产生异常。");
				Trace.WriteLine(ex.Message);
				//MessageBox.Show("处理文件错误", "Error");
			}
		}

		private static string ReadNextStatementFromStream( StreamReader _reader ) 
		{

			StringBuilder sb = new StringBuilder();

			string lineOfText;
   
			while(true) 
			{
				lineOfText = _reader.ReadLine();

				if( lineOfText == null ) 
				{
					if( sb.Length > 0 ) 
					{
						return sb.ToString();
					}
					else 
					{
						return null;
					}
				}

				if( lineOfText.TrimEnd().ToUpper() == "GO" ) 
				{
					break;
				}
    
				sb.AppendFormat("{0}\r\n", lineOfText );
			}

			return sb.ToString();
		}

	}
}
