<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="Lectures_Create.aspx.cs" Inherits="myWebAspClient.Professors.Lecture_Create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="page-wrapper">
        <div class="container-fluid">

            <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">New Lecture <small>To create new lecture.</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li class="active">
                            <i class="fa fa-fw fa-file"></i>Create Lecture
                        </li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="alert alert-success alert-dismissable" id="alert" runat="server" visible="false">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                        <i class="fa fa-info-circle"></i><strong>You have successfully added new lecture. </strong>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <%--Create Lecture--%>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <label>Lecture Title</label>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtTitle" ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="Please enter lecture title" ValidationGroup="create"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-12">
                                <div class="col-lg-5">
                                    <input type="date" runat="server" id="txtDate" class="form-control" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtDate" ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ErrorMessage="Please enter lecture title" ValidationGroup="create"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-3">
                                    <input type="time" runat="server" id="txtTime" class="form-control" />
                                </div>
                            </div>

                        </div>
                        <div class="form-group">
                            <label>Select Module</label>
                            <asp:DropDownList ID="ddlModule" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">

                            <asp:CheckBox ID="checkFeed" runat="server" />
                            <label>Create Feedback for your lecture </label>

                            <input type="date" runat="server" id="endDate" class="form-control" />

                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnSave" runat="server" Text="Create" OnClick="btnSave_Click" CssClass="btn btn-primary" ValidationGroup="create" />
                        </div>
                    </div>

                    <%--View Created Lecture--%>
                    <asp:GridView ID="grdLectures" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Lectures">
                                <ItemTemplate>
                                    <asp:Label ID="lblLectureId" runat="server" Text='<%#Eval("PK_Lecture_id") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblLectureTitle" runat="server" Text='<%#Eval("lecture_title") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLectureTitle" runat="server" Text='<%#Eval("lecture_title") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
