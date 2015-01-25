<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="Quiz_Report2.aspx.cs" Inherits="myWebAspClient.Professors.Quiz_Report2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">



        <div class="container-fluid">
            <%--Header Menu --%>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Quiz Reports<small> Class performance on Quiz (average percantage).</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-dashboard"></i><a href="Default.aspx">Dashboard</a>
                        </li>

                        <li class="active">
                            <i class="fa fa-fw fa-file"></i>Quiz Report
                        </li>
                    </ol>

                </div>
            </div>

            <div class="row">
                <div class="col-lg-7">
                    <div class="form-group">
                        <div class="col-lg-7">
                            <label>Select Module</label>
                            <i>Average performance of the class on all quiz.</i>
                            <asp:DropDownList ID="ddlModule" runat="server" class="form-control" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                 
                </div>

            </div>
            
            <div class="row">
                <div class="col-lg-12">
                    <br />
                    <div class="col-lg-10">
                        
                            <div class="table-responsive">

                                <asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px" EnableViewState="true"  >
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Items" />
                                    </Titles>

                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                    </Legends>
                                    <Series>


                                        <asp:Series Name="Default" XValueMember="quiz" ChartType="Bar"  IsValueShownAsLabel="true" YValueMembers="marks" PostBackValue="#VALX" LegendPostBackValue="#VALX" />

                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                                        
                                    </ChartAreas>
                                </asp:Chart>
                            </div>
                        
                    </div>
                </div>
            </div>

           

            <div class="row">
                <div class="col-lg-12">
                    <br />
                    <div class="col-lg-10">
                        <div style="max-height: 500px; max-width: 900px; overflow-x: auto; overflow-y: auto;">
                            <div class="table-responsive">

                            <script type="text/javascript" src="https://www.google.com/jsapi"></script>
                            <asp:Label ID="lblQuestionTitle" runat="server" ></asp:Label>
                            <br />
                            <br />
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            <div id="chart_div">
                                
                            </div>
                        </div>
</div> 
                        </div> 
                    </div> 
                </div> 
            
             
        </div>
    </div>
</asp:Content>
