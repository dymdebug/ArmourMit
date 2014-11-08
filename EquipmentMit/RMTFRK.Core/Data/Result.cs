using System;

namespace RMTFRK.Core.Data
{
    /// <summary>
    /// 系统公共使用部分
    /// </summary>
    public static class Result
    { 
        /// <summary>
        /// 获取字符串类型的主键
        /// </summary>
        /// <returns></returns>
        public static string GetNewId()
        {
            //return Guid.NewGuid().ToString();
            return CreateNewId();
        }
        /// <summary>
        /// 创建不重复的Id
        /// </summary>
        /// <returns></returns>
        private static string CreateNewId()
        {
            string id = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            id += guid.Substring(0, 10);
            return id;
        } 
        /// <summary>
        /// 返回成功
        /// </summary>
        public static string Succeed { get { return "成功"; } }
        /// <summary>
        /// 返回失败
        /// </summary>
        public static string Fail { get { return "失败"; } }
        /// <summary>
        /// 返回异常
        /// </summary>
        public static string Exception { get { return "异常"; } }
    }
}
