<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="Quiz_Create.aspx.cs" Inherits="myWebAspClient.Professors.Quiz_Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="page-wrapper">

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
            
            <asp:Label ID="lblQuizId" runat="server" Text="0" Visible="false"></asp:Label>
            <%--Create Quiz--%>
            <div id="Quiz" runat="server">
                <div class="row">
                    <%--Enter Quiz fields here--%>

                    <div class="col-lg-4">

                        <div class="form-group">
                            <label>Enter quiz detail here-</label>
                            <input type="text" id="txtTitle" runat="server" placeholder="Title" class="form-control" required="required" />
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
                        <%--<div class="form-group">
                            <input type="text" id="txtWeightage" runat="server" placeholder="Weightage of Quiz" required="required" class="form-control" />
                        </div>--%>
                        <div class="form-group">
                            <asp:Button ID="btnCreateQuiz" runat="server" Text="Create" ValidationGroup="CreateQuiz" class="btn btn-primary" OnClick="btnCreateQuiz_Click" />
                        </div>
                    </div>

                </div>

            </div>
            <div class="breadcrumb">
                <h2>
                    <asp:Label ID="lblQuizName" runat="server" Text=""></asp:Label></h2>
            </div>

            <%--Create Questions--%>
            <div id="Question" runat="server" visible="false">
                
                <div class="col-lg-12">
                    <%--Create Question--%>
                    <div class="col-lg-5">
                        <div class="form-group">

                            <label>Question type</label>

                            <asp:DropDownList ID="ddlQuestionType" runat="server" CssClass="form-control btn btn-default">
                                <asp:ListItem>Optional</asp:ListItem>
                                <asp:ListItem>Multiple Choice</asp:ListItem>
                                <asp:ListItem>Paragraph</asp:ListItem>
                                <asp:ListItem>Fill in the blanks</asp:ListItem>
                            </asp:DropDownList>

                        </div>

                        <div class="form-group">

                            <%--<input type="text" id="txtQuestion" runat="server" placeholder="What is your Question ?" class="form-control" />--%>
                            <textarea id="txtQuestion" runat="server" row="150" cols="3" class="form-control" placeholder="Write is your question... "></textarea>
                        </div>
                        <div class="form-group">

                            <textarea id="txtHint" runat="server" placeholder="Hint for the question..." class="form-control" row="150" cols="2"></textarea>

                        </div>

                        <%--Optional --%>
                        <div class="opt div">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="col-lg-12 form-group">
                                        <div class="col-lg-6">
                                            <input type="text" id="txtOptOption" runat="server" placeholder="Option" class="form-control" />
                                            <br />
                                            <asp:Button ID="btnAddOption" runat="server" Text="Add Option" class="btn btn-active" OnClick="btnAddOption_Click" />
                                            <asp:ImageButton ID="btnOptionDelete" runat="server" AlternateText="Remove Option" Height="20" OnClick="btnOptionDelete_Click" ImageUrl="../Content/delete_icon.png" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <%--Multiple Choice--%>
                        <div class="mult div">
                            <div class="col-lg-12 form-group">
                                <div class="col-lg-6">
                                    <input type="text" id="txtMultOption" runat="server" placeholder="Option" class="form-control" />


                                    <br />
                                    <asp:Button ID="btnMultAdd" runat="server" Text="Add Option" class="btn btn-active" OnClick="btnMultAdd_Click" />
                                    <asp:ImageButton ID="btnMultDelete" runat="server" AlternateText="Remove Option" Height="20" OnClick="btnMultDelete_Click" ImageUrl="../Content/delete_icon.png" />
                                </div>
                                <div class="col-lg-6">
                                    <div style="width: 150px; height: 150px; overflow: auto">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">Option List</h3>
                                            </div>
                                            <div class="panel-body">
                                                <asp:CheckBoxList ID="checkList" runat="server"></asp:CheckBoxList>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <%--Paraphraph Question--%>
                        <div class="par div">
                        </div>

                        <%--Fill in the blanks--%>
                        <div class="blank div">
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-lg-12 form-group">
                                        <div class="col-lg-6">

                                            <asp:Button ID="btnAddBlank" runat="server" Text="Add Blanks" class="btn btn-active" OnClick="btnAddBlank_Click" />
                                            <asp:ImageButton ID="btnRemove" runat="server" AlternateText="Remove" Height="20" OnClick="btnRemove_Click" ImageUrl="../Content/delete_icon.png" />
                                        </div>

                                        <div class="col-lg-6">
                                            <div style="width: 150px; height: 150px; overflow: auto">

                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h3 class="panel-title">Option List</h3>
                                                    </div>
                                                    <div class="panel-body">

                                                        <asp:CheckBoxList ID="lstBlank" runat="server">
                                                        </asp:CheckBoxList>

                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel> --%>
                        </div>

                        <div class="form-group">
                            <div class="col-lg-12">
                                <div class="col-lg-4">
                                    <asp:TextBox ID="txtMark" runat="server" class="form-control" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtMark" ValidationGroup="question" ForeColor="Red"  ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-7">
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="question" Text="Create Question" CssClass="btn btn-primary" OnClick="btnSave_Click"  />
                                </div>



                            </div>
                        </div>
                    </div>

                    <%--Display Questions--%>
                    <div class="col-lg-7">
                        <div class="row">

                            <asp:UpdatePanel ID="updtGrid" runat="server">
                                <ContentTemplate>
                                <div style="max-height: 500px; max-width:700px;overflow-x:auto; overflow-y: auto; border-bottom: 5px solid #D8D8D8; border-top: 5px solid #D8D8D8">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdQuiz" runat="server"  BorderStyle="None" GridLines="Horizontal" CssClass="table table-bordered table-hover" OnRowCommand="grdQuiz_RowCommand"  OnRowDataBound="grdQuiz_RowDataBound" Font-Names="Arial" Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B" AllowPaging="true" ShowFooter="true" AutoGenerateColumns="false" OnPageIndexChanging="grdQuiz_PageIndexChanging" OnRowEditing="grdQuiz_RowEditing" OnRowUpdating="grdQuiz_RowUpdating" OnRowCancelingEdit="grdQuiz_RowCancelingEdit">
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
                                                    <asp:TextBox ID="txtQuestion" runat="server" Text='<%#Eval("question")%>' TextMode="MultiLine"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Question Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestionType" runat="server" Text='<%#Eval("question_type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Marks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuestionMark" runat="server" Text='<%#Eval("marks") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtQuestionMark" Width="25" runat="server" Text='<%#Eval("marks") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ControlStyle-Width="100" HeaderText="Options">

                                                <ItemTemplate>

                                                    <asp:RadioButtonList ID="lblOption" runat="server" Visible="false"></asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="lblCheckList" runat="server" Visible="false"></asp:CheckBoxList>
                                                </ItemTemplate>
                                                <EditItemTemplate>

                                                    <asp:RadioButtonList ID="gridRdbList" runat="server" Visible="false"></asp:RadioButtonList>
                                                    <asp:CheckBoxList ID="grdCheckList" runat="server" Visible="false"></asp:CheckBoxList>
                                                    <asp:TextBox ID="txtGridOptionAdd" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:CheckBox ID="chkgridIsAnswer" runat="server" Text="Is answer" Visible="false" />
                                                    <asp:LinkButton ID="btnGridAddOption" runat="server" Visible="false" Text="Add Option" CommandName="AddOpt" CausesValidation="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="btnGridRemoveOpt" runat="server" Visible="false" Text="Remove Option" CommandName="RemoveOpt" CausesValidation="false" CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                                                </EditItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" ControlStyle-Width="35">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("PK_Question_Id")%>' OnClientClick="return confirm('Do you want to delete?')" Text="Delete" OnClick="btnDelete_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                   Total Weightage= <asp:Label ID="lblGridWeightage" runat="server" Text="" ></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                         
                                            <asp:CommandField ShowEditButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                    </div> 
                                                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </div>
                    </div>
                </div>
            </div>
                



        </div>


    </div>
    <style>
        .div {
            display: none;
        }
    </style>

    <script type="text/javascript" src="http://code.jquery.com/jquery.js"></script>
    <script type="text/javascript">


        $(document).ready(function ($) {
            $("#ContentPlaceHolder1_ddlQuestionType").change(function () {
                $("#ContentPlaceHolder1_ddlQuestionType option:selected").each(function () {
                    if ($(this).attr("value") == "Optional") {

                        //$(".opt").show(50);
                        //$(".mult").hide(500);
                        //$(".par").hide(500);
                        //$(".blank").hide(500);
                        $(".opt").fadeIn(1000);
                        $(".mult").fadeOut(50);
                        $(".par").fadeOut(50);
                        $(".blank").fadeOut(50);

                    }
                    if ($(this).attr("value") == "Multiple Choice") {
                        $(".opt").fadeOut(50);
                        $(".mult").fadeIn(1000);
                        $(".par").fadeOut(50);
                        $(".blank").fadeOut(50);
                    }
                    if ($(this).attr("value") == "Paragraph") {
                        $(".opt").fadeOut(50);
                        $(".mult").fadeOut(50);
                        $(".par").fadeIn(1000);
                        $(".blank").fadeOut(50);

                    }
                    if ($(this).attr("value") == "Fill in the blanks") {
                        $(".opt").fadeOut(50);
                        $(".mult").fadeOut(50);
                        $(".par").fadeOut(50);
                        $(".blank").fadeIn(1000);
                    }
                });
            }).change();


        });

        //Validate Question
        function quesCheck() {
            alert("1");
            var searchEles = document.getElementById("ContentPlaceHolder1_qqq").children;
            var q = 1;
            for (var i = 0; i < searchEles.length; i++) {

                if (searchEles[i].tagName == 'INPUT' && searchEles[i].getAttribute("type") == 'text') {
                    if (searchEles[i].value.toString().trim() == "") {
                        searchEles[i].className = "alert alert-danger";
                        searchEles[i].setAttribute("placeholder", "Required");
                        q = 1;

                    }

                }
            }
            if (q) return false;

        }

    </script>
</asp:Content>
