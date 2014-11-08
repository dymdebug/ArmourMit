/**************************************
* 作用：转换配置字符类型
* 作者：Nick.Yan
* 日期: 2008-02-11
* 网址：www.redglove.com.cn
**************************************/

using System.Configuration;
using System.Xml;

namespace RMTFRK.Core.Helper.Config
{
    public sealed class ConfigHelper
    {
        public static string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }

        public static void SetValue(XmlDocument xmlDocument, string selectPath, string key, string keyValue)
        {
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    xmlNode.Attributes["value"].Value = keyValue;
                    break;
                }
            }
        }

    }

}
