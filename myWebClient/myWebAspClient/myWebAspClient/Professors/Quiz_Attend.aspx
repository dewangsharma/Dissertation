<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="Quiz_Attend.aspx.cs" Inherits="myWebAspClient.Professors.Attend_Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="page-wrapper">

        <div class="container-fluid">
            <asp:Label ID="lblQuizId" runat="server" Visible="false"></asp:Label>
            <%--Header Menu --%>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Attend the Quiz <small>Let's play with the quiz.</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-dashboard"></i><a href="Default.aspx">Dashboard</a>
                        </li>
                        <li>
                            <i class="fa fa-table"></i><a href="Quiz_All.aspx">All Quiz</a>
                        </li>
                        <li class="active">
                            <i class="fa fa-fw fa-file"></i>Attend Quiz
                        </li>
                    </ol>

                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-lg-12">
                            <h2>
                                <asp:Label ID="lblQuizTitle" runat="server"></asp:Label>
                            </h2>
                            <div class="alert alert-info alert-dismissable" id="alert" runat="server" visible="false">
                                <i class="fa fa-info-circle"></i><strong>Your score: </strong>
                                <asp:Label ID="lblFinalResult" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                                out of 
                                <asp:Label ID="lblTotal" runat="server" Visible="false" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">

                            <div style="max-height: 500px; overflow-y: auto; border-bottom: 5px solid #D8D8D8; border-top: 5px solid #D8D8D8">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdQuiz" runat="server" BorderStyle="None" GridLines="Horizontal" CssClass="table table-bordered table-hover" OnRowDataBound="grdQuiz_RowDataBound" Font-Names="Arial" Font-Size="11pt" HeaderStyle-BackColor="#A4A4A4" ShowFooter="true" AutoGenerateColumns="false" OnPageIndexChanging="grdQuiz_PageIndexChanging">
                                        <%--<PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />--%>
                                        <Columns>

                                            <asp:TemplateField ItemStyle-Width="5" HeaderText="No">
                                                <ItemTemplate>


                                                    <asp:Label ID="lblQuestionId" runat="server" Text='<%#Eval("PK_Question_Id")%>' Visible="false" DataField="PK_Question_Id" />
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Question">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("question")%>' DataField="question" />
                                                            <asp:Label ID="lblAnswer" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lblCorrect" runat="server" Visible="false" Text="" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblQuestionType" runat="server" Visible="false" Text='<%#Eval("question_type") %>'></asp:Label>
                                                            <asp:Label ID="lblQuestionMark" runat="server" Visible="false" Text='<%#Eval("marks") %>'></asp:Label>
                                                            <br />
                                                            <br />
                                                        </div>
                                                        <div class="col-lg-12">

                                                            <asp:RadioButtonList ID="lblOption" runat="server" RepeatDirection="Horizontal" Visible="false" CssClass="increaseSpace"></asp:RadioButtonList>
                                                            <asp:CheckBoxList ID="checkOption" runat="server" RepeatDirection="Horizontal" Visible="false" CssClass="increaseSpace"></asp:CheckBoxList>
                                                            <asp:TextBox ID="txtOption" runat="server" Width="95%" Visible="false" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </div>

                            </div>
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>



                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <style type="text/css">
                .increaseSpace input {
                    margin-left: 50px;
                }
            </style>
        </div>
    </div>

</asp:Content>
