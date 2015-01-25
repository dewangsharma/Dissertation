using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Students
{
    public partial class Report_Quiz_All : System.Web.UI.Page
    {
        student_Report_WCFLib.StudentReportClient stdntRProxy = new student_Report_WCFLib.StudentReportClient();

        static int studentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                string s = Session["userId"].ToString();
                studentId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");

            lineChart();
        }
        void lineChart()
        {
            DataSet ds = stdntRProxy.getAllQuizByStudentId(studentId);

            StringBuilder strngBuilder1 = new StringBuilder();
            strngBuilder1.Append(@"<script type=text/javascript> google.load( *visualization*, *1*, {packages:[*corechart*]});
                google.setOnLoadCallback(drawChart);
                function drawChart() {
               
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'qTitle');
                    data.addColumn('number', 'qMarks');
                    
                    data.addRows(" + ds.Tables[0].Rows.Count + ");");

            Int32 i;
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                strngBuilder1.Append("data.setValue( " + i + "," + 0 + "," + "'" + ds.Tables[0].Rows[i]["qTitle"].ToString() + "');");
                strngBuilder1.Append("data.setValue(" + i + "," + 1 + "," + ds.Tables[0].Rows[i]["qMarks"].ToString() + ") ;");

            }
            strngBuilder1.Append("var chart = new google.visualization.LineChart(document.getElementById('chart_div'));");
            strngBuilder1.Append("chart.draw(data, {width: 800, height: 350, legend: 'bottom',is3D: false,title: 'Performance in all quiz (Percentage)',");
            strngBuilder1.Append("vAxis: {title: 'Marks', titleTextStyle: {color: 'blue'}}");
            strngBuilder1.Append("}); }");
            strngBuilder1.Append("</script>");

            Literal1.Text = strngBuilder1.ToString().TrimEnd(',').Replace('*', '"');
            
        }
    }
}