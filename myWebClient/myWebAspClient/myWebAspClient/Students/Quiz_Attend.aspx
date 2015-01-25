<%@ Page Title="" Language="C#" MasterPageFile="~/Students/student.Master" AutoEventWireup="true" CodeBehind="Quiz_Attend.aspx.cs" Inherits="myWebAspClient.Students.Quiz_Attend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h3><small><i class="fa fa-angle-right"></i>Attend Quiz </small><i class="fa fa-angle-right"></i>
                <asp:Label ID="lblQuizTitle" runat="server"></asp:Label></h3>
            <div class="row mt mb">

                <div class="form-panel">


                    <div class="row">
                        <div class="col-lg-12">

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
                            <div class="form-group">
                                <h3>
                                    <asp:Label ID="lblAlready" runat="server" Visible="false" Text="You have already attended the quiz. "></asp:Label>
                                </h3>

                            </div>
                        </div>
                    </div>
                    <div class="row" id="divQuiz" runat="server">
                        <div class="col-lg-12">
                            <asp:Label ID="lblQuizId" runat="server" Visible="false"></asp:Label>
                            <div style="border-bottom: 5px solid #D8D8D8; border-top: 5px solid #D8D8D8">
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
                                                            <div class="col-lg-11">
                                                                <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("question")%>' DataField="question" />
                                                                <asp:Label ID="lblAnswer" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCorrect" runat="server" Visible="false" Text="" Font-Bold="true"></asp:Label>
                                                                <asp:Label ID="lblQuestionType" runat="server" Visible="false" Text='<%#Eval("question_type") %>'></asp:Label>
                                                                <br />
                                                                <span>
                                                                    <b>Hint: </b>
                                                                </span>
                                                                <asp:Label ID="lblHint" runat="server" Text='<%#Eval("hint") %>' Font-Italic="true" Font-Size="Small"></asp:Label>
                                                            </div>

                                                            <div class="col-lg-1">
                                                                <span style="text-align: right">
                                                                    <asp:Label ID="lblQuestionMark" runat="server" Text='<%#Eval("marks") %>'> </asp:Label>
                                                                </span>
                                                                <br />

                                                                <br />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-12">
                                                            <asp:PlaceHolder ID="place1" runat="server" Visible="false"></asp:PlaceHolder>
                                                            <asp:RadioButtonList ID="lblOption" runat="server" RepeatDirection="Horizontal" Visible="false" CssClass="increaseSpace"></asp:RadioButtonList>
                                                            <asp:CheckBoxList ID="checkOption" runat="server" RepeatDirection="Horizontal" Visible="false" CssClass="increaseSpace"></asp:CheckBoxList>
                                                            <asp:TextBox ID="txtOption" runat="server" Width="95%" Visible="false" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:TextBox ID="txtBlank" runat="server" Width="35%" Visible="false"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                </ItemTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                    </div>

                                </div>
                            </div>
                        </div>
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

</asp:Content>
