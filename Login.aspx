<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SITConnect.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:Label ID="errorText" runat="server" style="color: red;"/>
        <div>
            User Email<asp:TextBox ID="tb_userEmail" runat="server"></asp:TextBox>
        </div>
        <p>
            Password<asp:TextBox ID="tb_userPass" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" />
    </form>
</body>
</html>
