using System.Runtime.Serialization;

namespace RMTFRK.Core.Data
{
    /// <summary>
    /// 是否记录日志
    /// </summary>
    [DataContract]
    public enum LogOpration
    {
        /// <summary>
        /// 根据Web.config中的配置
        /// </summary>
        [DataMember]
        Default,
        /// <summary>
        /// 开启记录日志
        /// </summary>
        [DataMember]
        Start,
        /// <summary>
        /// 禁止记录日志
        /// </summary>
        [DataMember]
        Fobid
    }
    [DataContract]
    public static class LogType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        [DataMember]
        public static string None { get { return "未定义"; } }
        /// <summary>
        /// 捕获的异常
        /// </summary>
        [DataMember]
        public static string Exception { get { return "捕获的异常"; } }
        /// <summary>
        /// 数据库的异常
        /// </summary>
        [DataMember]
        public static string DataBase { get { return "数据库的异常"; } }
        /// <summary>
        /// 出错信息
        /// </summary>
        [DataMember]
        public static string Error { get { return "出错信息"; } }
        /// <summary>
        /// 操作信息
        /// </summary>
        [DataMember]
        public static string Operation { get { return "操作信息"; } }
        /// <summary>
        /// 系统调用
        /// </summary>
        [DataMember]
        public static string System { get { return "系统"; } }
        /// <summary>
        /// 其他系统调用的服务
        /// </summary>
        [DataMember]
        public static string AnotherSystem { get { return "其他系统调用"; } }

    }
}
