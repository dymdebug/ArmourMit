using System.Management;

namespace RMTFRK.Core.Helper.HD
{
    public class HDMAC_HDSN
    {

        /// <summary>

        /// ��ȡ�豸

        /// </summary>
        public HDMAC_HDSN()

        { }

        /// <summary>

        /// ȡ���豸������MAC��ַ

        /// </summary>
        public  static string GetNetCardMacAddress()
        {

           ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

          ManagementObjectCollection  moc = mc.GetInstances();

            string str = "";

            foreach (ManagementObject mo in moc)
            {

                if ((bool)mo["IPEnabled"] == true)

                    str = mo["MacAddress"].ToString();


            }

            return str;

        }

        /// <summary>

        /// ȡ���豸Ӳ�̵ľ���

        /// </summary>

        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {

          ManagementClass  mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

         ManagementObject   disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");

            disk.Get();

            return disk.GetPropertyValue("VolumeSerialNumber").ToString();

        }

    }

}
