using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMTFRK.Web.Component
{
    public class PagerInfoField : DataPagerField
    {
        #region private fields

        int _currentPageIndex = 0;
        int _startRowIndex = 0;
        int _endRowIndex = 0;
        int _pagesRemain = 0;
        int _recordsRemain = 0;

        #endregion

        #region private methods

        int PageCount
        {
            get { return (DataPager.TotalRowCount / DataPager.PageSize) + ((DataPager.TotalRowCount % DataPager.PageSize)==0?0:1); }
        }

        private string GetCustomInfoHTML(string origText)
        {
            if (!string.IsNullOrEmpty(origText) && origText.IndexOf('%') >= 0)
            {
                string[] props = new string[] { "recordcount", "pagecount", "currentpageindex", "startrecordindex", "endrecordindex", "pagesize", "pagesremain", "recordsremain" };
                StringBuilder sb = new StringBuilder(origText);
                Regex reg = new Regex("(?<ph>%(?<pname>\\w{8,})%)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mts = reg.Matches(origText);
                foreach (Match m in mts)
                {
                    string p = m.Groups["pname"].Value.ToLower();
                    if (Array.IndexOf(props, p) >= 0)
                    {
                        string repValue = null;
                        switch (p)
                        {
                            case "recordcount":
                                repValue = DataPager.TotalRowCount.ToString(); break;
                            case "pagecount":
                                repValue = PageCount.ToString(); break;
                            case "currentpageindex":
                                repValue = _currentPageIndex.ToString(); break;
                            case "startrecordindex":
                                repValue = _startRowIndex.ToString(); break;
                            case "endrecordindex":
                                repValue = _endRowIndex.ToString(); break;
                            case "pagesize":
                                repValue = DataPager.PageSize.ToString(); break;
                            case "pagesremain":
                                repValue = _pagesRemain.ToString(); break;
                            case "recordsremain":
                                repValue = _recordsRemain.ToString(); break;
                        }
                        if (repValue != null)
                            sb.Replace(m.Groups["ph"].Value, repValue);
                    }
                }
                return sb.ToString();
            }
            return origText;
        }

        #endregion

        // Methods
        public override void CreateDataPagers(DataPagerFieldItem container, int startRowIndex, int maximumRows, int totalRowCount, int fieldIndex)
        {
            //init data
            _currentPageIndex = (startRowIndex / DataPager.PageSize) + (totalRowCount>0?1:0);//startRowIndex:0 based
            _startRowIndex = startRowIndex + 1;//to display, startRowIndex based 0
            _endRowIndex = startRowIndex + DataPager.PageSize - 1;
            _pagesRemain = PageCount - _currentPageIndex;
            _recordsRemain = DataPager.TotalRowCount - _currentPageIndex * DataPager.PageSize;
            //render control
            string html = string.Format("<span class='{0}'>{1}</span>", CssClass, GetCustomInfoHTML(CustomInfoHTML));
            container.Controls.Add(new LiteralControl(html));
        }   

        public string CustomInfoHTML
        {
            get
            {
                if (ViewState["CustomInfoHTML"]==null)
                {
                    return "%CurrentPageIndex% / %PageCount%";
                }
                return ViewState["CustomInfoHTML"].ToString();
            }
            set
            {
                ViewState["CustomInfoHTML"] = value;
            }
        }

        public string CssClass
        {
            get
            {
                if (ViewState["CssClass"] == null)
                {
                    return "pagerinfo";
                }
                return ViewState["CssClass"].ToString();
            }
            set
            {
                ViewState["CssClass"] = value;
            }
        }

        protected override DataPagerField CreateField()
        {
            return new PagerInfoField();
        }

        public override void HandleEvent(CommandEventArgs e)
        {
        }
    }
}
