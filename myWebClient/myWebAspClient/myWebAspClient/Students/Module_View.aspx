<%@ Page Title="" Language="C#" MasterPageFile="~/Students/student.Master" AutoEventWireup="true" CodeBehind="Module_View.aspx.cs" Inherits="myWebAspClient.Students.Module_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><i class="fa fa-angle-right"></i> All active Modules</h3>

        <div class="row mt mb">
        <div class="col-lg-12">
            <div class="form-panel">
    <div class="table-responsive" style="max-height: 400px; overflow-y: auto">
        <asp:GridView ID="grdModules" GridLines="None" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="List of Active Modules" HeaderStyle-Font-Size="15" ItemStyle-CssClass="col-lg-12" HeaderStyle-CssClass="task-panel tasks-widget" >
                    <HeaderTemplate>
                        <i class="fa fa-bullhorn"></i>List of all attending Modules
                    </HeaderTemplate>
                    <ItemTemplate>

                        <div class="row-mt">
                            <div class="col-lg-12">
                                <div class="form-panel">
                                    <asp:Label ID="lblModuleId" runat="server" Text='<%#Eval("PK_Module_id") %>' Visible="false"></asp:Label>

                                    <asp:Label ID="lblModuleTitle" runat="server" Text='<%#Eval("module_name") %>' Font-Bold="true"></asp:Label>

                                    <asp:LinkButton ID="lnkView" Text="Quiz" CommandArgument='<%#Eval("PK_Module_id") %>' runat="server" OnClick="lnkView_Click"  ></asp:LinkButton>
                                    <asp:LinkButton ID="lnkFeed" Text="Feedback" CommandArgument='<%#Eval("PK_Module_id") %>' runat="server" OnClick="lnkFeed_Click"  ></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>

</div>
            </div></div>

</asp:Content>
