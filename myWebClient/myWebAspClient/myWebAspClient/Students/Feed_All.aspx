<%@ Page Title="" Language="C#" MasterPageFile="~/Students/student.Master" AutoEventWireup="true" CodeBehind="Feed_All.aspx.cs" Inherits="myWebAspClient.Students.Feed_All" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <h3><small class="btn-navbar"><i class="fa fa-angle-right"></i><a href="Module_View.aspx">All Module</a></small> <i class="fa fa-angle-right"></i>View All Lectures </h3>
    <asp:Label ID="lblModuleId" runat="server" Visible="false"></asp:Label>

    <div class="row mt mb">

        <div class="form-panel">
            <div class="table-responsive">
                <asp:GridView ID="grdFeedAll" runat="server" class="table table-bordered table-hover"  DataKeyNames="PK_Lecture_id" AutoGenerateColumns="false" GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Font-Size="15" ItemStyle-CssClass="col-lg-12" HeaderStyle-CssClass="site-footer">
                            <HeaderTemplate>
                                <i class="fa fa-bullhorn"></i>List of all Lectures for Feedback  
                            </HeaderTemplate>
                            <ItemTemplate>

                                <div class="col-lg-12">
                                    

                                        <div class="col-lg-8">
                                            <asp:Label ID="lblLectureId" runat="server" Text='<%#Eval("PK_Lecture_id") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("lecture_title") %>'></asp:Label>
                                            <asp:Label ID="lblDateTime" runat="server" Text='<%# "[ "+ Eval("lecture_date")+" "+Eval("lecture_time") + " ]"%> '></asp:Label>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:Rating ID="Rating1" runat="server" AutoPostBack="true" StarCssClass="StarB" WaitingStarCssClass="StarW" EmptyStarCssClass="StarB"
                                                FilledStarCssClass="StarF" OnChanged="Rating1_Changed" >
                                            </asp:Rating>

                                        </div>
    

                                        
                                        <%--      <asp:LinkButton ID="lnkView" runat="server" Font-Bold="true" Font-Size="11pt" Text='<%#Eval("quiz_title") %>' CommandArgument='<%#Eval("PK_Quiz_Id") %>' OnClick="lnkView_Click"></asp:LinkButton>
                                        --%>
                                    
                                </div>


                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>
        </div>
        <style type="text/css">
            .StarB {
                background-image: url(assets/img/StarB.gif);
                height: 17px;
                width: 17px;
            }

            .StarW {
                background-image: url(assets/img/StarW.gif);
                height: 17px;
                width: 17px;
            }

            .StarF {
                background-image: url(assets/img/StarF.gif);
                height: 17px;
                width: 17px;
            }
        </style>
    </div>
    
</asp:Content>
