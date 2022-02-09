<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="SITConnect.Verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>Enter verification code sent to your email below:</h4>
            <asp:TextBox ID="code" runat="server"></asp:TextBox>
            <asp:Button ID="submit" runat="server" Text="Verify" OnClick="verifyOTP" />
            <br />
            <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
