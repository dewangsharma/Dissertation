using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Register_Click(object sender, EventArgs e)
        {
            if (Prof.Checked)
            {
                professor_WCFLib.Professor_Profile prfData = new professor_WCFLib.Professor_Profile();
                prfData.firstname = txtFName.Value;
                prfData.middlename = txtMName.Value;
                prfData.lastname = txtLName.Value;
                prfData.email = txtEmail.Value;
                prfData.pass_value = txtPassword.Value;
                professor_WCFLib.ProfessorProfileClient proxy_Professor = new professor_WCFLib.ProfessorProfileClient();
                string q = proxy_Professor.CreateProfessor(prfData);
                if (q == "1")
                {
                    btnVerification.Visible = true;
                    txtVerification.Visible = true;
                    lblVerification.Visible = true;
                }
            }
            else if (Stu.Checked)
            {
                student_WCFLib.StudentProfile stdntData = new student_WCFLib.StudentProfile();
                stdntData.firstname = txtFName.Value;
                stdntData.middlename = txtMName.Value;
                stdntData.lastname = txtLName.Value;
                stdntData.email = txtEmail.Value;
                stdntData.pass_value = txtPassword.Value;
                student_WCFLib.StudentProfileClient proxy_Student = new student_WCFLib.StudentProfileClient();
                string s = proxy_Student.CreateStudent(stdntData);
                if (s == "1")
                {

                    btnVerification.Visible = true;
                    txtVerification.Visible = true;
                    lblVerification.Visible = true;
                }


            }

        }

        protected void btnVerification_Click(object sender, EventArgs e)
        {
            bool role = false;
            if (Prof.Checked)
            {
                role = true;

            }
            else if (Stu.Checked)
            {
                role = false;
            }
            account_WCFLib.LoginClient acnt = new account_WCFLib.LoginClient();
            string s = acnt.accountVerification(txtEmail.Value, txtVerification.Text, role);
            if (s == "1")
            {
                //Login logic of cookie or session
            }
            else
            {
                lblError.Visible = true;

            }
        }
    }
}