<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="Quiz_All.aspx.cs" Inherits="myWebAspClient.Professors.Quiz_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">

    

         <div class="container-fluid">
            <%--Header Menu --%>
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">All Quiz <small>List of all quiz.</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li>
                            <i class="fa fa-dashboard"></i><a href="Default.aspx">Dashboard</a>
                        </li>
                        <li class="active">
                            <i class="fa fa-fw fa-file"></i>All Quiz
                        </li>
                    </ol>

                </div>
            </div>

            <%--Error Message--%>
            <asp:Label ID="lblError" runat="server" Text="" Visible="false" CssClass="alert alert-danger"></asp:Label>

             <%--Display All the Quiz in the Grid--%>
             <div class="table-responsive" style="max-height:400px; overflow-y:auto">
             
             <asp:GridView ID="grdAllQuiz" runat="server"  CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false" >
                 <Columns>
                     <asp:TemplateField HeaderText="Quiz" ItemStyle-Width="900" >
                         <ItemTemplate>
                             <table>
                                 <tr>
                                     <td>
                                         <asp:Label ID="lblquizId" runat="server" Text='<%#Eval("PK_Quiz_Id") %>' visible="false"></asp:Label>
                                     </td>
                                     <td><asp:Label ID="lblQuestion" runat ="server" Font-Size="Medium" Text ='<%#Eval("quiz_title") %>' ></asp:Label> </td>
                                 </tr>
                             </table>

                         </ItemTemplate>
                        
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Action">
                         <ItemTemplate>

                             <asp:LinkButton ID="btnView" runat="server" Text="View" CommandArgument='<%# Eval("PK_Quiz_Id") %>' OnClick="btnView_Click" ></asp:LinkButton>
                             &nbsp;&nbsp;&nbsp;
                             <asp:LinkButton ID="btnAttend" runat="server" Text="Attend" CommandArgument='<%# Eval("PK_Quiz_Id") %>' OnClick="btnAttend_Click" ></asp:LinkButton>


                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
                         
             </div> 
             <br />
             <br />

</div>
        </div>

</asp:Content>
