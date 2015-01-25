using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Quiz_Report : System.Web.UI.Page
    {
        //Create proxy to initialise the service reference
        professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient();
        modules_WCFLib.ModulesClient moduleProxy = new modules_WCFLib.ModulesClient();
        professor_Report_WCFLib.ProfessorReportClient reportProxy = new professor_Report_WCFLib.ProfessorReportClient();
                
        static int profId;
                           
        protected void Page_Load(object sender, EventArgs e)
        {
            //Authentication
            if (Session["userId"] != null)
            {

                string s = Session["userId"].ToString();
                profId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                //Add modules to the drop down list
                modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);

                ddlModule.Items.Insert(0, new ListItem ("---Please Select Module---"));
                for (int i = 0; i < mod.Length; i++)
                {
                    ddlModule.Items.Insert(i+1, new ListItem(mod[i].module_name, mod[i].PK_Module_id.ToString()));
                }
            }

        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlAllQuiz.Items.Clear();
            if (ddlModule.SelectedItem.Text != "---Please Select Module---")
            {
                //Get all the quiz by using module Id
                int moduleId = Convert.ToInt32(ddlModule.SelectedItem.Value);
                professor_Quiz_WCFLib.quizModel[] d = proxy.GetAllQuizByModule(moduleId, profId);
                if (d.Length > 0)
                {
                    for (int i = 0; i < d.Length; i++)
                    {
                        //Add Quiz to the drop down list
                        ddlAllQuiz.Items.Insert(i, new ListItem(d[i].quiz_title, d[i].PK_Quiz_id.ToString()));
                    }
                    btnResult.Enabled = true;
                }
                else
                {
                    ddlAllQuiz.Items.Insert(0, new ListItem("No quiz for the module"));
                    btnResult.Enabled = false;
                }
            }
            else
                btnResult.Enabled = false;
            

            
        }

        protected void btnResult_Click(object sender, EventArgs e)
        {
            if (ddlModule.SelectedItem.Text != "---Please Select Module---" || ddlAllQuiz.SelectedItem.Text != "No quiz for the module")
            {
                //Get all the questions of the quiz by using quiz id

                DataSet ds = reportProxy.getAllQuestionsByQuiz(Convert.ToInt32(ddlAllQuiz.SelectedItem.Value));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Chart1.DataSource = ds.Tables[0];
                    Chart1.DataBind();
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    

                }
            }
        }

        //Bind data to pie chart 
        void pieChart(int q)
        {
            string[] qResult = reportProxy.getStatusByQuestionId(q);
            
            StringBuilder strScript1 = new StringBuilder();
            strScript1.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']}); </script>  
                      
                    <script type='text/javascript'>  
                     
                    function drawChart() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['quiz', 'marks'],");

            strScript1.Append("['" + "Fail" + "'," + qResult[1] + "],");
            strScript1.Append("['" + "Pass" + "'," + qResult[2] + "],");
            strScript1.Append("['" + "Merit" + "'," + qResult[3] + "],");
            strScript1.Append("['" + "Distinction" + "'," + qResult[4] + "],");
            strScript1.Remove(strScript1.Length - 1, 1);
            strScript1.Append("]);");

            strScript1.Append(@" var options = {     
                                    title: 'Class performance on Question',            
                                    is3D: true,          
                                    };   ");

            strScript1.Append(@"var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));          
                                chart.draw(data, options);        
                                }    
                            google.setOnLoadCallback(drawChart);  
                            ");
            strScript1.Append(" </script>");

            Literal1.Text = strScript1.ToString();
            lblQuestionTitle.Text = qResult[5];

 
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            string s = e.PostBackValue;
            
            Response.Write(s);
            //Call piechar visualisation
            pieChart(Convert.ToInt32(s));
        }
    }
}