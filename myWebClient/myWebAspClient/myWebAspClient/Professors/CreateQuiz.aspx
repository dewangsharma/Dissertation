<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="CreateQuiz.aspx.cs" Inherits="myWebAspClient.Professors.Create_Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:Label ID="lblQuizId" runat="server" Text="0" Visible="false"></asp:Label>
        <div class="container-fluid">
            <%--Header Menu --%>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">New Quiz <small>To create new quiz.</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-dashboard"></i><a href="Default.aspx">Dashboard</a>
                        </li>
                        <li class="active">
                            <i class="fa fa-fw fa-file"></i>Create Quiz
                        </li>
                    </ol>

                </div>
            </div>

            <%--Error Message--%>
            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="alert alert-danger"></asp:Label>

            <%--Create Quiz--%>
            <div id="Quiz">
                <div class="row">
                    <%--Enter Quiz fields here--%>

                    <div class="col-lg-4">

                        <div class="form-group">
                            <label>Enter quiz detail here-</label>
                            <input type="text" id="txtTitle" runat="server" placeholder="Title" required="required" class="form-control" />
                        </div>
                        <div class="form-group">
                            <p>Select Module</p>
                            <asp:DropDownList ID="ddlModule" runat="server" class="form-control">
                                
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <p>Start Date & Time.</p>
                            <div class="col-lg-8">
                                <input type="date" id="txtDate" runat="server" placeholder="Start Date" required="required" class="form-control" />
                            </div>
                            <div class="col-lg-4">
                                <input type="time" id="txtTime" runat="server" placeholder="Start Time" required="required" class="form-control" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="form-group">
                            <p>Expiry Date & Time.</p>
                            <div class="col-lg-8">

                                <input type="date" id="txtEndDate" runat="server" placeholder="End Date" required="required" class="form-control" />
                            </div>
                            <div class="col-lg-4">
                                <input type="time" id="txtEndTime" runat="server" placeholder="End Time" required="required" class="form-control" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="form-group">

                            <p>Active this quiz with timer.</p>
                            <input type="radio" name="timeup" id="rdbTimeUp" runat="server" value="Yes" checked />Yes
                        <input type="radio" name="timeup" id="rdbTimeUp1" runat="server" value="No" />No
                        </div>
                        <div class="form-group">
                            <input type="text" id="txtLength" runat="server" placeholder="Length of Quiz" required="required" class="form-control" />
                        </div>
                        <div class="form-group">
                            <input type="text" id="txtWeightage" runat="server" placeholder="Weightage of Quiz" required="required" class="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnCreateQuiz" runat="server" Text="Create" class="btn btn-primary" OnClick="btnCreateQuiz_Click" />
                        </div>
                    </div>

                </div>
            </div>

            <div class="row">

                <%--Quiz Title--%>
                <div class="col-lg-12">

                    <div class="breadcrumb">
                        <h2>
                            <asp:Label ID="lblQuizName" runat="server" Text=""></asp:Label></h2>
                    </div>


                    <%--Create Question--%>
                    <div id="Question">
                        <%--Enter Question Fields--%>

                        <div class="col-lg-4">
                            <div class="form-group">
                                <label>Enter Questions details for the Quiz-</label>
                                <br />
                                <asp:Label ID="lblShowQuestion" runat="server" Text="1"></asp:Label>
                                <input type="text" id="txtQuestion" runat="server" placeholder="Question" class="form-control" />
                            </div>

                            <%--Question Type--%>
                            <%--<div class="form-group">
                                <input type="text" id="txtQuestionType" runat="server" placeholder="Question Type" class="form-control" />
                            </div>--%>

                            <div class="col-lg-12 form-group">
                                <div class="col-lg-6">
                                    <input type="text" id="txtOption" runat="server" placeholder="Option" class="form-control" />
                                </div>
                                <div class="col-lg-6">
                                    <div style="width: 150px; height: 150px; overflow: auto">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Option List</h3>
                                            </div>
                                            <div class="panel-body">
                                                <asp:RadioButtonList ID="rdbList" runat="server"></asp:RadioButtonList>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Button ID="btnAddOption" runat="server" Text="Add Option" class="btn btn-default" OnClick="btnAddOption_Click" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Create Question" Enabled="false" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </div>

                        </div>
                    </div>

                    <%--Get Quiz Data--%>
                    <div id="GridQuiz">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:UpdatePanel ID="updtGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grdQuiz" runat="server" Width="550px" CssClass="table-responsive" PageSize="1" GridLines="Horizontal" OnRowDataBound="grdQuiz_RowDataBound" Font-Names="Arial" Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B" HeaderStyle-BackColor="green" AllowPaging="true" ShowFooter="true" AutoGenerateColumns="false" OnPageIndexChanging="grdQuiz_PageIndexChanging" OnRowEditing="grdQuiz_RowEditing" OnRowUpdating="grdQuiz_RowUpdating" OnRowCancelingEdit="grdQuiz_RowCancelingEdit">
                                            <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />

                                            <Columns>

                                                <%--<asp:BoundField HeaderText="Question" DataField="question" />--%>
                                                <%--<asp:BoundField HeaderText="PK_QuestionId" DataField="PK_Question_Id" />--%>
                                                <asp:TemplateField ControlStyle-Width="0" HeaderText="No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuestionId" runat="server" Text='<%#Eval("PK_Question_Id")%>' Visible="false" DataField="PK_Question_Id" />
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="200" HeaderText="Question">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("question")%>' DataField="question" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQuestion" runat="server" Text='<%#Eval("question")%>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ControlStyle-Width="100" HeaderText="Options">

                                                    <ItemTemplate>

                                                        <asp:RadioButtonList ID="lblOption" runat="server"></asp:RadioButtonList>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <%-- Nested GridView
                           <asp:TemplateField>
                            <ItemTemplate>
                                <asp:GridView ID="grdOption" runat="server" DataSource='<%#Eval("optionModel")%>' AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label CssClass="myRdb" ID="rdbCheck" runat="server" Text='<%#Eval("option_value")%>' DataField="PK_option_value" />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                                                --%>


                                                <asp:TemplateField HeaderText="Action" ControlStyle-Width="35">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("PK_Question_Id")%>' OnClientClick="return confirm('Do you want to delete?')" Text="Delete" OnClick="btnDelete_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" />
                                            </Columns>
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>

                </div>
            </div>



        </div>
    </div>
    <script type="text/javascript">

        function showQuestions() {
            //var s = "Dewang";
            var s1 = document.getElementById("<%=lblQuizName.ClientID %>").innerText;
            if (s1 != "") {
                //alert(s);
                document.getElementById('Quiz').style.display = "none";
                document.getElementById('Question').style.display = "block";
                //document.getElementById('Question').style.visibility = "visible";
            }
            else {
                document.getElementById('Quiz').style.display = "block";
                document.getElementById('Question').style.display = "none";
                document.getElementById('GridQuiz').style.display = "block";

            }
        }

        function showGridQuiz() {
            var s1 = document.getElementById("<%=lblShowQuestion.ClientID%>").innerText;
            if (s1 != "1") {
                document.getElementById('GridQuiz').style.display = "block";
            }
            else {
                document.getElementById('GridQuiz').style.display = "block";
            }
        }

    </script>
    <style type="text/css">
        #Question {
        }

        #GridQuiz {
        }
    </style>
</asp:Content>
