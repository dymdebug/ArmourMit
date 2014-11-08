using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// CRunScript ��ժҪ˵����
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
					throw new Exception("�ļ� " + pathToScriptFile + " δ�ҵ�");
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
				Trace.WriteLine("���ļ��� " + pathToScriptFile + " �в����쳣��");
				Trace.WriteLine(ex.Message);
				//MessageBox.Show("�����ļ�����", "Error");
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
