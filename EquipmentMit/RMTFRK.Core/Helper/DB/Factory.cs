using System.Configuration;

namespace RMTFRK.Core.Helper.DB
{
	/// <summary>
	/// Factory类
	/// </summary>
	public sealed class Factory
	{
		private static volatile Factory singleFactory = null;
		private static object syncObj = new object();
		/// <summary>
		/// Factory类构造函数
		/// </summary>
		private Factory()
		{
		}

		/// <summary>
		/// 获得Factory类的实例
		/// </summary>
		/// <returns>Factory类实例</returns>
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
		/// 建立Factory类实例
		/// </summary>
		/// <returns>Factory类实例</returns>
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


