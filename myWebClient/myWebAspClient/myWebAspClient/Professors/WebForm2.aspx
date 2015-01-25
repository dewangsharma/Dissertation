<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="myWebAspClient.Professors.WebForm2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
     <div id="page-wrapper">
         <div class="row">
            <div class="col-lg-12">
                <asp:GridView ID="grdTest" runat="server"></asp:GridView>
            </div>
            <div class="col-lg-12">
                <asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px" Visible="true">
                    <Titles>
                        <asp:Title ShadowOffset="3" Name="Items" />
                    </Titles>
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                    </Legends>
                    <Series>
                        <asp:Series Name="Default" XValueMember="quiz" YValueMembers="marks" />
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                        
                    </ChartAreas>
                </asp:Chart>
            </div>
             <div class="col-lg-12">
                  <script type="text/javascript" src="https://www.google.com/jsapi"></script>  
        <asp:GridView ID="gvData" runat="server">  
        </asp:GridView>  
        <br />  
        <br />  
        <asp:Literal ID="ltScripts" runat="server"></asp:Literal>  
        <div id="chart_div" style="width: 660px; height: 400px;">  
        </div>  
             </div>

             <%--PieChart--%>
             <div class="col-lg-12">
                  <script type="text/javascript" src="https://www.google.com/jsapi"></script>  
        <asp:GridView ID="GridView1" runat="server">  
        </asp:GridView>  
        <br />  
        <br />  
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>  
        <div id="piechart_3d" style="width: 900px; height: 500px;">  
        </div>  
             </div>

             <%--Click Event--%>
             <div class="col-lg-1"> 
                       <asp:Chart ID="Chart2" runat="server" Height="300px" Width="400px" Visible="true" OnClick="Chart2_Click">
                    <Titles>
                        <asp:Title ShadowOffset="3" Name="Items" />
                    </Titles>
                    <Legends>
                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                    </Legends>
                    <Series>
                        <asp:Series Name="Default" XValueMember="quiz" YValueMembers="marks" PostBackValue="#VALX" LegendPostBackValue="#VALX" />
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                        
                    </ChartAreas>
                </asp:Chart>
             </div>

        </div>
    </div>
   
</asp:Content>
