using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        professor_Report_WCFLib.ProfessorReportClient r = new professor_Report_WCFLib.ProfessorReportClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            DataSet d = r.getAllModuleByTerm("Jan-2014");
            grdTest.DataSource = d.Tables[0];
            grdTest.DataBind();

            DataBindChart();
        }

         
        

        private void DataBindChart()
        {
            DataSet d = r.getAllModuleByTerm("Jan-2014");
          
            DataTable  ds = new DataTable();
            ds.Columns.Add("quiz");
            
            ds.Columns.Add("marks");
            //for (int i = 0; i < d.Tables[0].Rows.Count; i++)
            //{
            //    DataRow dr = ds.NewRow();
            //    dr["quiz"] = d.Tables[0].Rows[i][0].ToString();
            //    dr["marks"] = d.Tables[0].Rows[i][1].ToString();
            //    ds.Rows.Add(dr);
            //}

            //Chart1.DataSource = ds;
            //Chart1.DataBind();
            for (int j = 0; j < d.Tables.Count; j++)
            {

                Series sr = new Series();
                sr["q"] = d.Tables[j].ToString();
                
                
               // DataRow dr1= d.Tables[j].NewRow();
                //dr1["quiz1"] = d.Tables[j].Rows[j][0].ToString();
                //sr.Points.AddXY(dr1["quiz1"], j);
                for (int i = 0; i < d.Tables[j].Rows.Count; i++)
                {
                    DataRow dr = ds.NewRow();
                    dr["quiz"] = d.Tables[j].Rows[i][0].ToString();
                    dr["marks"] = d.Tables[j].Rows[i][1].ToString();
                    sr.Points.AddXY(dr["quiz"].ToString(),Convert.ToDouble(dr["marks"].ToString()));
                    ds.Rows.Add(dr);
                }
                Chart1.Series.Add(sr);
               
            }
            
            Chart1.DataSource = ds;
            Chart1.DataBind();
            Chart2.DataSource = ds;
            Chart2.DataBind();
                
            //Google Chart
            DataTable dsChartData = ds;
            StringBuilder strScript = new StringBuilder();
            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['quiz', 'marks'],");

            foreach (DataRow row in dsChartData.Rows)
            {
                strScript.Append("['" + row["quiz"] + "'," + row["marks"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
            strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");

            ltScripts.Text = strScript.ToString(); 

            //Pie Chart

            StringBuilder strScript1 = new StringBuilder();
            strScript1.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']}); </script>  
                      
                    <script type='text/javascript'>  
                     
                    function drawChart() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['quiz', 'marks'],");

            foreach (DataRow row in dsChartData.Rows)
            {
                strScript1.Append("['" + row["quiz"] + "'," + row["marks"] + "],");
            }
            strScript1.Remove(strScript1.Length - 1, 1);
            strScript1.Append("]);");

            strScript1.Append(@" var options = {     
                                    title: 'My Daily Schedule',            
                                    is3D: true,          
                                    };   ");

            strScript1.Append(@"var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));          
                                chart.draw(data, options);        
                                }    
                            google.setOnLoadCallback(drawChart);  
                            ");
            strScript1.Append(" </script>");

            ltScripts.Text = strScript1.ToString();  

        }

        protected void Chart2_Click(object sender, ImageMapEventArgs e)
        {
            string s = e.PostBackValue;
        }
        
    }
}