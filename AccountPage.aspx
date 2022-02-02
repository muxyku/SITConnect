<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPage.aspx.cs" Inherits="SITConnect.AccountPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Successfully Logged in! Welcome!</h1>
    <form id="form1" runat="server">
        <div>
             <asp:Button id="btnLogout" runat="Server" OnClick="Logout" Text="Logout"/>
        </div>
    </form>
</body>
</html>
