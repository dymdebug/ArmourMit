using System.Collections.Generic;
using System.Text;

namespace RMTFRK.Core.Data
{
    public static class AutoSearch
    {
        public static string Search(List<string> listString)
        {
            if (listString == null || listString.Count < 1)
            {
                return string.Empty;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            int i = 0;
            foreach (var item in listString)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                if (i == 0)
                {
                    i++;
                    stringBuilder.Append(@"{@label@:@" + item + "@}");
                }
                else
                {
                    stringBuilder.Append(",");
                    stringBuilder.Append(@"{@label@:@" + item + "@}");
                }
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString().Replace('@', '"');
        }
    }

}
