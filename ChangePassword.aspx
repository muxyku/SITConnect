<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="SITConnect.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ChangePassword ID="ChangePassword1" runat="server" OnChangingPassword="OnChangingPassword"
                RenderOuterTable="false" 
                NewPasswordRegularExpression="^[\s\S]{12,}$"
                NewPasswordRegularExpressionErrorMessage="Password must be of minimum 12 characters."
                CancelDestinationPageUrl = "~/AccountPage.aspx">
</asp:ChangePassword>
<br />
<asp:Label ID="lblMessage" runat="server" />
        </div>
    </form>
</body>
</html>
