namespace RMTFRK.Web.Helper.File
{
    public class UploadInfo
    {
        public bool IsReady { get; set; }
        public int ContentLength { get; set; }
        public int UploadedLength { get; set; }
        public string FileName { get; set; }
    }
}
