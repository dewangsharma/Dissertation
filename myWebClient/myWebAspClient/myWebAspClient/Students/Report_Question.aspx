<%@ Page Title="" Language="C#" MasterPageFile="~/Students/student.Master" AutoEventWireup="true" CodeBehind="Report_Question.aspx.cs" Inherits="myWebAspClient.Students.Report_Question" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h3><small class="btn-navbar"><i class="fa fa-angle-right"></i>Reports</small> <i class="fa fa-angle-right"></i>Performance on All Question of Quiz </h3>


    <div class="row mt mb">

        <div class="form-panel">

            <div class="row">
                <div class="col-lg-12">
                    <div class="col-lg-10">
                       <label>Select Quiz from the list.</label>
                    </div>
                    <div class="col-lg-5">
                        <asp:DropDownList ID="ddlAllQuiz" runat="server" class="form-control" OnSelectedIndexChanged="ddlAllQuiz_SelectedIndexChanged" AutoPostBack="true" >

                        </asp:DropDownList>
                    </div>
                    <br />
                    <div class="col-lg-10">
                        
                            <div class="table-responsive">

                                <script type="text/javascript" src="https://www.google.com/jsapi"></script>
                                <asp:Label ID="lblQuestionTitle" runat="server"></asp:Label>
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
</asp:Content>
