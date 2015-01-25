<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="myWebAspClient.Registration" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-md-4">
            
            <input type="radio" id="Prof" runat="server" name="profile" value="Professor" checked/>Professor
            <input type="radio" id="Stu" runat="server"  name="profile" value="Student" />Student

            <input type="text"  id="txtFName" runat="server" placeholder="First Name" required="required" class="form-control"/>
            <input type="text" id="txtMName" runat="server" placeholder="Middle Name" class="form-control"/>
            <input type="text" id="txtLName" runat="server" placeholder="Last Name" required="required" class="form-control"/>
            <input type="email" id="txtEmail" runat="server" placeholder="Email" required="required" class="form-control"/>
            <input type="text" id="txtPassword" runat="server" placeholder="Passeord" required="required" class="form-control"/>
            <asp:Button id="Register" runat="server" Text="Register" OnClick="Register_Click" CssClass="btn btn-default" />
             
            <asp:Label ID="lblVerification" runat="server" Visible="false" Text="Please check your email and enter verification code." ></asp:Label>
            <asp:TextBox ID="txtVerification" runat="server" Visible="false" CssClass="form-control" ></asp:TextBox>
            <asp:Button ID="btnVerification" runat="server" Text="Verify" Visible="false" CssClass="btn btn-default" OnClick="btnVerification_Click" />
            <asp:Label ID="lblError" runat="server" Visible="false" Text="Please check your verification code." ForeColor="Red" ></asp:Label>
            </div>
         <//div>
    

     
    
</asp:Content>
