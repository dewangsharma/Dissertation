using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           account_WCFLib.LoginClient acnt_proxy = new account_WCFLib.LoginClient();
                string[] res = acnt_proxy.userLogin(txtUsername.Text.Trim(),txtPassword.Text.Trim());

                if (res[0] != "NA")
                {
                    //Session or authentication logic

                    Session["userId"] = res[1];
                    Session["userName"] = txtUsername.Text.Trim();
                    if (res[0] == "Stu") //Go to the Student Index
                    {
                        Session["role"] = "Student";
                        Response.Redirect("~/Students/default.aspx");
                    }
                    else if (res[0] == "Prof")
                    {
                        Session["role1"] = "Professor";
                        Response.Redirect("~/Professors/default.aspx");
                    }
                    //Verfication reply has to redirect
                }
                else
                    lblError.Text = "Please enter correct password";
            
        }
    }
}