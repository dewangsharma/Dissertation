<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tryAjax.aspx.cs" Inherits="myWebAspClient.tryAjax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function radioButtonItemOnClick(elementRef, itemValue) {
            if (elementRef.checked == true) {


                //alert("Welcome");
            }
        }
        function tryThis() {

        }
        function tryDelete() {

            var s = document.getElementByName('rdbList');
            var s1 = document.getElementsByTagName('input');
            for (var q = 0 ; q < s1.length; q++) {
                if (s1[q].checked)
                    alert(s1[q].value);
            }




        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="updtGrid" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdQuiz" runat="server" Width="550px" CssClass="table-responsive" PageSize="1" GridLines="Horizontal" OnRowDataBound="grdQuiz_RowDataBound"  Font-Names="Arial" Font-Size="11pt" AlternatingRowStyle-BackColor="#C2D69B" HeaderStyle-BackColor="green" AllowPaging="true" ShowFooter="true" AutoGenerateColumns="false" OnPageIndexChanging="grdQuiz_PageIndexChanging" OnRowEditing="grdQuiz_RowEditing" OnRowUpdating="grdQuiz_RowUpdating" OnRowCancelingEdit="grdQuiz_RowCancelingEdit">
                    <PagerSettings  Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
                     
                    <Columns>
                        
                        <%--<asp:BoundField HeaderText="Question" DataField="question" />--%>
                        <%--<asp:BoundField HeaderText="PK_QuestionId" DataField="PK_Question_Id" />--%>
                        <asp:TemplateField ControlStyle-Width="0" HeaderText="No">
                            <ItemTemplate>
                                <asp:Label ID="lblQuestionId" runat="server" Text='<%#Eval("PK_Question_Id")%>' Visible="false"  DataField="PK_Question_Id" />
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
                                
                                <asp:RadioButtonList ID="lblOption" runat="server" ></asp:RadioButtonList>
                                  
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
    </form>
</body>
</html>
