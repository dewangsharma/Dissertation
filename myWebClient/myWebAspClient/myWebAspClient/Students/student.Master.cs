using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Students
{
    public partial class student : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string role = Session["role"].ToString();
                if (Session["userName"] != null || Session["userId"] != null && role != "Student")
                {
                    lblUserName.Text = Session["userName"].ToString();
                    lblUserId.Text = Session["userId"].ToString();
                }
                else
                    Response.Redirect("~/Login.aspx");
            }
            catch(Exception ex){Response.Redirect("~/Login.aspx");}
        }

        
    }
}