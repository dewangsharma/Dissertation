<%@ Page Title="" Language="C#" MasterPageFile="~/Students/student.Master" AutoEventWireup="true" CodeBehind="Quiz_All.aspx.cs" Inherits="myWebAspClient.Students.Quiz_All" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><small class="btn-navbar"><i class="fa fa-angle-right"></i> <a href="Module_View.aspx">All Module</a></small> <i class="fa fa-angle-right"></i>  View All Quiz </h3>
    <asp:Label ID="lblModuleId" runat="server" Visible="false"></asp:Label>
    
    <div class="row mt mb">

        <div class="form-panel">
            <div class="table-responsive">
                <asp:GridView ID="grdQuizAll" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="false" GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Font-Size="15" ItemStyle-CssClass="col-lg-12" HeaderStyle-CssClass="site-footer">
                            <HeaderTemplate>
                                <i class="fa fa-bullhorn"></i>  List of all Quiz of the Module 
                            </HeaderTemplate>
                            <ItemTemplate>
                                
                                    <div class="col-lg-12">
                                        <div class="form-panel" style ="background:#E6E6E6">
                                            
                                                        <asp:Label ID="lblQuizId" runat="server" Text='<%#Eval("PK_Quiz_id") %>' Visible="false"></asp:Label>
                                                   
                                                         <asp:LinkButton ID="lnkView" runat="server" Font-Bold="true" Font-Size="11pt" Text='<%#Eval("quiz_title") %>' CommandArgument='<%#Eval("PK_Quiz_Id") %>' OnClick="lnkView_Click"></asp:LinkButton>
                                                                                            </div>
                                    </div>
                                

                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>



</asp:Content>
