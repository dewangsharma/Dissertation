using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Feed_Report : System.Web.UI.Page
    {
        modules_WCFLib.ModulesClient moduleProxy = new modules_WCFLib.ModulesClient();
        professor_Report_WCFLib.ProfessorReportClient reportProxy = new professor_Report_WCFLib.ProfessorReportClient();
        
        static int profId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {

                string s = Session["userId"].ToString();
                profId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {

                modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);

                ddlModule.Items.Insert(0, new ListItem("---Please Select Module---"));
                for (int i = 0; i < mod.Length; i++)
                {

                    ddlModule.Items.Insert(i + 1, new ListItem(mod[i].module_name, mod[i].PK_Module_id.ToString()));
                }
            }

        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlModule.SelectedItem.Text != "---Please Select Module---")
            {
                int moduleId = Convert.ToInt32(ddlModule.SelectedItem.Value);
                DataSet ds = reportProxy.getAllFeedbacks(moduleId);
                FeedChart.DataSource = ds.Tables[0];
                FeedChart.DataBind();
            }
        }
    }
}