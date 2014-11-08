//using Microsoft.Web.UI.WebControls;
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace RMTFRK.Web.Component
{
    /// <summary>
    /// WebGrid implements the IPageableItemContainer interface
    /// </summary>
    public class WebGrid : System.Web.UI.WebControls.GridView, IPageableItemContainer
    {
        public WebGrid()
        {

        }

        protected virtual void OnTotalRowCountAvailable(PageEventArgs e)
        {
            EventHandler<PageEventArgs> handler = (EventHandler<PageEventArgs>)Events[EventTotalRowCountAvailable];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Methods
        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int rows = base.CreateChildControls(dataSource, dataBinding);
            if (this.AllowPaging)
            {
                int totalRowCount = dataBinding ? rows : ((ICollection)dataSource).Count;
                IPageableItemContainer pageableItemContainer = this;
                this.OnTotalRowCountAvailable(new PageEventArgs(pageableItemContainer.StartRowIndex, pageableItemContainer.MaximumRows, totalRowCount));
                if (this.TopPagerRow != null)
                {
                    this.TopPagerRow.Visible = false;
                }
                if (this.BottomPagerRow != null)
                {
                    this.BottomPagerRow.Visible = false;
                }
            }
            return rows;
        }

        #region IPageableItemContainer Members

        public int MaximumRows
        {
            get
            {
                return this.PageSize;
            }
        }

        public void SetPageProperties(int startRowIndex, int maximumRows, bool databind)
        {
            int newPageIndex = startRowIndex / maximumRows;
            this.PageSize = maximumRows;
            if (this.PageIndex != newPageIndex)
            {
                bool isCanceled = false;
                if (databind)
                {
                    GridViewPageEventArgs args = new GridViewPageEventArgs(newPageIndex);
                    this.OnPageIndexChanging(args);
                    isCanceled = args.Cancel;
                    newPageIndex = args.NewPageIndex;
                }
                if (!isCanceled)
                {
                    this.PageIndex = newPageIndex;
                    if (databind)
                    {
                        this.OnPageIndexChanged(EventArgs.Empty);
                    }
                }
                if (databind)
                {
                    base.RequiresDataBinding = true;
                }
            }
        }

        public int StartRowIndex
        {
            get
            {
                return (this.PageSize * this.PageIndex);
            }
        }

        // Fields
        private static readonly object EventTotalRowCountAvailable = new object();

        // Events
        public event EventHandler<PageEventArgs> TotalRowCountAvailable
        {
            add
            {
                Events.AddHandler(EventTotalRowCountAvailable, value);
            }
            remove
            {
                Events.RemoveHandler(EventTotalRowCountAvailable, value);
            }
        }

        #endregion
    }
}
