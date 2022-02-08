<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SITConnect.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script src="https://www.google.com/recaptcha/api.js?render=6LdnEVYeAAAAAPJgTLcsMRElnpR34O8dCL8eg_9l"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h1>SIT Connect Login</h1>
        
        <div>
            User Email<asp:TextBox ID="tb_userEmail" runat="server"></asp:TextBox>
        </div>
        <p>
            Password<asp:TextBox ID="tb_userPass" runat="server"></asp:TextBox>
        </p>
        <asp:Label runat="server" ID="gScore"></asp:Label>
        <asp:Label ID="errorText" runat="server" style="color: red;"/>
        <p>Login attempts made:
        <asp:Label ID="LA" runat="server" Text="Email"></asp:Label>

            <p>Login attempts local:
        <asp:Label ID="addLA" runat="server" Text="Email"></asp:Label>
     </p>
        <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" />
        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
    </form>
</body>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LdnEVYeAAAAAPJgTLcsMRElnpR34O8dCL8eg_9l', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });

        document.getElementById("gScore").style.display = 'none';
    </script>
</html>
