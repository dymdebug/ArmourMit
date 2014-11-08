using System.Configuration;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// Factory��
	/// </summary>
	public sealed class Factory
	{
		private static volatile Factory singleFactory = null;
		private static object syncObj = new object();
		/// <summary>
		/// Factory�๹�캯��
		/// </summary>
		private Factory()
		{
		}

		/// <summary>
		/// ���Factory���ʵ��
		/// </summary>
		/// <returns>Factory��ʵ��</returns>
		public static Factory GetInstance()
		{
			if(singleFactory == null)
			{
				lock(syncObj)
				{
					if(singleFactory == null)
					{
						singleFactory = new Factory();
					}
				}
			}
			return singleFactory;
		}

		/// <summary>
		/// ����Factory��ʵ��
		/// </summary>
		/// <returns>Factory��ʵ��</returns>
		public AbstractDbFactory CreateInstance()
		{
			AbstractDbFactory abstractDbFactory = null;
			switch(ConfigurationSettings.AppSettings["DatabaseType"].ToLower())
			{
				case "sqlserver":
				{
					abstractDbFactory = new SqlFactory();
					break;
				}
				case "oledb":
				{
					abstractDbFactory = new OleDbFactory();
					break;
				}
				case "odbc":
				{
					abstractDbFactory = new OdbcFactory();
					break;
				}
			}   
			return abstractDbFactory;
		}
	}
}


