<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="Feed_Report.aspx.cs" Inherits="myWebAspClient.Professors.Feed_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div id="page-wrapper">

            <div class="container-fluid">
                <%--Header Menu --%>
                <div class="row">
                    <div class="col-lg-12">
                        <h1 class="page-header">Feed Reports<small>  Feedback report on all lectures.</small>
                        </h1>
                        <ol class="breadcrumb">
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Default.aspx">Dashboard</a>
                            </li>

                            <li class="active">
                                <i class="fa fa-fw fa-file"></i>Feed Report
                            </li>
                        </ol>

                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="col-lg-7">
                    <div class="form-group">
                        <div class="col-lg-7">
                            <label>Select Module</label>
                            <asp:DropDownList ID="ddlModule" runat="server" class="form-control" OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                            </div>
                        <div class="col-lg-10 table-responsive">
                            
                        <asp:Chart ID="FeedChart" runat="server" Height="300px" Width="400px" EnableViewState="true">
                                    <Titles>
                                        <asp:Title ShadowOffset="3" Name="Items" />
                                    </Titles>

                                    <Legends>
                                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                                    </Legends>
                                    <Series>


                                        <asp:Series Name="Default" XValueMember="Lecture" IsValueShownAsLabel="true" YValueMembers="Average" PostBackValue="#VALX" LegendPostBackValue="#VALX" />

                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />

                                    </ChartAreas>
                                </asp:Chart>
                                
                            </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Content>
