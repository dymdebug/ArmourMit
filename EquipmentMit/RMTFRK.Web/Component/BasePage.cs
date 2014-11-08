using System;
using RMTFRK.Web.ViewModel;

namespace RMTFRK.Web.Component
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            if (LoginUser == null)
            {
               // this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>top.location.href='../login.aspx'</script> ");
                Response.Redirect("~/login.html");
            }
        }

        protected Sys_User LoginUser
        {
            get
            {
                return (Sys_User)Session["User"];
            }
        }
        
    }
}
