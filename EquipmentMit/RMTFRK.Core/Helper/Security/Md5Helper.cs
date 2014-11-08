using System.Web.Security;

namespace RMTFRK.Core.Helper.Security
{
    public class Md5Helper
    {
        public static string MD5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }
            if (code == 32)
            {
                strEncrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
            return strEncrypt;
        }
    }
}