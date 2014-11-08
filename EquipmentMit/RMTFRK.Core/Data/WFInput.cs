namespace RMTFRK.Core.Data
{
    /// <summary>
    /// 工作流输入项
    /// </summary>
    public class WFInput
    {
        /// <summary>
        /// 同意还是驳回
        /// </summary>
        public bool YesNo { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 接收者
        /// </summary>
        public string Recver { get; set; }
        /// <summary>
        /// 工作流类型
        /// </summary>
        public string WorkType { get; set; }
        /// <summary>
        /// 提出的建议
        /// </summary>
        public string Sugestion { get; set; }
    }
}
