using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Default : System.Web.UI.Page
    {
        professor_WCFLib.ProfessorProfileClient proxy = new professor_WCFLib.ProfessorProfileClient();
        static int profId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                string s = Session["userId"].ToString();
                profId = Convert.ToInt32(s);
            }

            else Response.Redirect("~/Login.aspx");
            int[] reply = proxy.Dashboard(profId);
            lblStudents.Text = reply[0].ToString();
            lblQuiz.Text = reply[1].ToString();
            lblFeedBack.Text = reply[2].ToString();
            lblToday.Text = reply[3].ToString();

        }
    }
}