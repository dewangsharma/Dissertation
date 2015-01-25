<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="Quiz_Report.aspx.cs" Inherits="myWebAspClient.Professors.Quiz_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">



        <div class="container-fluid">
            <%--Header Menu --%>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Quiz Reports<small>  Class performance for per question of the quiz.</small>
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
                            <asp:DropDownList ID="ddlModule" runat="server" class="form-control" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-7">
                            <label>Select Quiz</label>
                            <asp:DropDownList ID="ddlAllQuiz" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-7">
                            <br />
                            <asp:Button ID="btnResult" runat="server" CssClass="btn btn-primary" Text="Result" Enabled="false" OnClick="btnResult_Click" />
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-lg-12">
                    <br />
                    <div class="col-lg-7">
                        <div style="max-height: 500px; max-width: 600px; overflow-x: auto; overflow-y: auto;">
                            <div class="table-responsive">

                                <asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px" OnClick="Chart1_Click" EnableViewState="true">
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Items" />
                                    </Titles>

                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                    </Legends>
                                    <Series>


                                        <asp:Series Name="Default" XValueMember="qId" IsValueShownAsLabel="true" YValueMembers="marks" PostBackValue="#VALX" LegendPostBackValue="#VALX" />

                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />

                                    </ChartAreas>
                                </asp:Chart>
                                <asp:GridView ID="GridView1" runat="server">
                            </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5">
                        <%--PieChart--%>
                        <div>
                            <script type="text/javascript" src="https://www.google.com/jsapi"></script>
                            <asp:Label ID="lblQuestionTitle" runat="server" ></asp:Label>
                            <br />
                            <br />
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            <div id="piechart_3d">
                                
                            </div>
                        </div>


                    </div>

                </div>
            </div>


        </div>
    </div>

</asp:Content>
