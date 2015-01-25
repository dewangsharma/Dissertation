using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class professor : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string role = Session["role1"].ToString();
            if(Session["userName"] != null || Session["userId"] != null && role != "Professor")
            {
                lblUserName.Text = Session["userName"].ToString();
                lblUserId.Text = Session["userId"].ToString();
            }
            else 
                Response.Redirect("~/Login.aspx");
        }
    }
}