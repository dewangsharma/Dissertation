<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="myWebAspClient.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtUsername" runat ="server" ></asp:TextBox>
        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" ></asp:TextBox>
        <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" />
    </div>
    </form>
</body>
</html>
