using System;
using System.IO;
using RMTFRK.Core.Helper.Config;
using RMTFRK.Core.Helper.Validate;

namespace RMTFRK.Core.Helper.Log
{
    public class LogHelper : IDisposable
    {
        private string LogFile;
        private static StreamWriter sw;
        private string logIsWrite = ConfigHelper.GetAppSettings("LogIsWrite");

        public LogHelper()
        {
            this.CreateLoggerFile(null);
        }

        public LogHelper(string _log)
        {
            this.CreateLoggerFile(_log);
        }

        private void CreateLoggerFile(string fileName)
        {
            if (this.logIsWrite == "true")
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = DateTimeHelper.GetToday();
                }
                object _myLogPath = null;
                if (_myLogPath == null)
                {
                    this.LogFile = ConfigHelper.GetAppSettings("LogFilePath");
                    if (System.IO.File.Exists(this.LogFile))
                    {
                        Directory.CreateDirectory(this.LogFile);
                    }
                }
                else
                {
                    this.LogFile = _myLogPath.ToString();
                }
                if (1 > this.LogFile.Length)
                {
                    Console.WriteLine("配置文件中没有指定日志文件目录！");
                }
                else
                {
                    if (!Directory.Exists(this.LogFile))
                    {
                        Console.WriteLine("配置文件中指定日志文件目录不存在！");
                    }
                    else
                    {
                        if (this.LogFile.Substring(this.LogFile.Length - 1, 1).Equals("/") || this.LogFile.Substring(this.LogFile.Length - 1, 1).Equals("\\"))
                        {
                            this.LogFile = this.LogFile + fileName + ".log";
                        }
                        else
                        {
                            this.LogFile = this.LogFile + "\\" + fileName + ".log";
                        }
                        try
                        {
                            FileStream fs = new FileStream(this.LogFile, FileMode.OpenOrCreate);
                            fs.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }
            }
        }

        private void writeInfos(string messagestr)
        {
            //DateTime DateNow = default(DateTime);
            try
            {
                this.FileOpen();
                string DateStr = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
                string sWrite = DateStr + "\n" + messagestr;
                LogHelper.sw.WriteLine(sWrite);
                LogHelper.sw.Flush();
                LogHelper.sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //封装出现错误的地方，变化点的封装
        public static void WriteLog2(string logTxt)
        {
            //把错误信息写到日志文件里面去

        }

        private void FileOpen()
        {
            LogHelper.sw = new StreamWriter(this.LogFile, true);
        }

        private void FileClose()
        {
            if (LogHelper.sw != null)
            {
                LogHelper.sw.Flush();
                LogHelper.sw.Close();
                LogHelper.sw.Dispose();
                LogHelper.sw = null;
            }
        }

        public void WriteLog(string msg)
        {
            if (msg != null)
            {
                this.writeInfos(msg.ToString());
            }
        }

        public void Dispose()
        {
        }
    }
}