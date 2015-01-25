<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="myWebAspClient.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblError" runat="server" ></asp:Label>
    <asp:TextBox ID="txtUsername" runat ="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Login" ControlToValidate="txtUsername" runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPassword" ValidationGroup="Login" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" ValidationGroup="Login" />
    </div>
    </form>
</body>
</html>
