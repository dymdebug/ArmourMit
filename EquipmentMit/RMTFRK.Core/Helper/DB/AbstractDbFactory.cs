using System.Data;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// ���ݿ���󹤳��ӿ�
	/// </summary>
	public interface AbstractDbFactory
	{
		/// <summary>
		/// ����Ĭ������
		/// </summary>
		/// <returns>���ݿ�����</returns>
		IDbConnection CreateConnection();

		/// <summary>
		/// ���������ַ�������Connection����
		/// </summary>
		/// <param name="strConn">�����ַ���</param>
		/// <returns>Connection����</returns>
		IDbConnection CreateConnection(string strConn);

		/// <summary>
		/// ����Command����
		/// </summary>
		/// <returns>Command����</returns>
		IDbCommand CreateCommand();

		/// <summary>
		/// ����DataAdapter����
		/// </summary>
		/// <returns>DataAdapter����</returns>
		IDbDataAdapter CreateDataAdapter();

		/// <summary>
		/// ����Connection����Transaction
		/// </summary>
		/// <param name="myDbConnection">Connection����</param>
		/// <returns>Transaction����</returns>
		IDbTransaction CreateTransaction(IDbConnection myDbConnection);

		/// <summary>
		/// ����Command����DataReader
		/// </summary>
		/// <param name="myDbCommand">Command����</param>
		/// <returns>DataReader����</returns>
		IDataReader CreateDataReader(IDbCommand myDbCommand);

		/// <summary>
		/// ��������ַ���
		/// </summary>
		/// <returns>�����ַ���</returns>
		string GetConnectionString();
	}
}


