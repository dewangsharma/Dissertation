using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Quiz_Report2 : System.Web.UI.Page
    {
        //Create proxy to initialise the service reference
        professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient();
        modules_WCFLib.ModulesClient moduleProxy = new modules_WCFLib.ModulesClient();
        professor_Report_WCFLib.ProfessorReportClient reportProxy = new professor_Report_WCFLib.ProfessorReportClient();

        static int profId;


        protected void Page_Load(object sender, EventArgs e)
        {
            //Authenticate user
            if (Session["userId"] != null)
            {

                string s = Session["userId"].ToString();
                profId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                //Load all the modules to the drop down list
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
                //Get all the quiz for module
                int moduleId = Convert.ToInt32(ddlModule.SelectedItem.Value);
               
                DataSet ds = reportProxy.getAllQuizByModule(moduleId);
              
                Chart1.DataSource = ds.Tables[0];
                Chart1.DataBind();
                Chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                Chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                //Call linechart to visualise data
                lineChart(moduleId);
            }
        }

        //load line chart
        void lineChart(int moduleId)
        {
            
              DataSet ds = reportProxy.getAllQuizByModule(moduleId);
            StringBuilder strngBuilder1 = new StringBuilder();
             strngBuilder1.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
                google.setOnLoadCallback(drawChart);
                function drawChart() {
               
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'quiz');
                    data.addColumn('number', 'marks');
                    
                    data.addRows(" + ds.Tables[0].Rows.Count + ");");
           
            Int32 i;
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                strngBuilder1.Append("data.setValue( " + i + "," + 0 + "," + "'" + ds.Tables[0].Rows[i]["quiz"].ToString() + "');");
                strngBuilder1.Append("data.setValue(" + i + "," + 1 + "," + ds.Tables[0].Rows[i]["marks"].ToString() + ") ;");
                
            }
            strngBuilder1.Append("var chart = new google.visualization.LineChart(document.getElementById('chart_div'));");
            strngBuilder1.Append("chart.draw(data, {width: 800, height: 350, legend: 'bottom',is3D: false,title: 'Average Performance of class based on quiz',");
            strngBuilder1.Append("vAxis: {title: 'Marks', titleTextStyle: {color: 'blue'}}");
            strngBuilder1.Append("}); }");
            strngBuilder1.Append("</script>");

            Literal1.Text = strngBuilder1.ToString().TrimEnd(',').Replace('*', '"');
            
        }
       
        
    }
}