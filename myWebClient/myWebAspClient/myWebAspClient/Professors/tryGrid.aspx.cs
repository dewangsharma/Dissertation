using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class tryGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            DateTime d = Convert.ToDateTime(txtDate.Value);
            DateTime dt =d.Add(TimeSpan.Parse(txtTime.Value));
            lblAnswer.Text = dt.ToString();
        }

        protected void btnForm1_Click(object sender, EventArgs e)
        {
            txtForm1.Value = "Dewang";
        }

        protected void btnForm2_Click(object sender, EventArgs e)
        {
            txtForm2.Value = "Success";
        }
    }
}