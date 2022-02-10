<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SITConnect.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script src="https://www.google.com/recaptcha/api.js?render=6LdnEVYeAAAAAPJgTLcsMRElnpR34O8dCL8eg_9l"></script>
   <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>
   
</head>
<body>

    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" href="#">SIT Connect</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
            <li class="nav-item">
                <a class="nav-link" href="Registration.aspx">Registration</a>
            </li>
            <li class="nav-item active">
                <a class="nav-link" href="Login.aspx">Login<span class="sr-only">(current)</span></a>
            </li>
            <li class="nav-item">
                <a class="nav-link" href="AccountPage.aspx">Account Page</a>
            </li>
        </ul>
        </div>
    </nav>
    <form id="form1" runat="server">
        <h1>Login</h1>

         <table class="auto-style1">
            <tr>
                <td class="auto-style2">User Email</td>
                <td >
                    <asp:TextBox ID="tb_userEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="loginEmail" runat="server" ControlToValidate="tb_userEmail" ErrorMessage="Email Required" style="color:red;"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td class="auto-style2">Password</td>
                <td >
                    <asp:TextBox ID="tb_userPass" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="loginPass" runat="server" ControlToValidate="tb_userPass" ErrorMessage="Password Required" style="color:red;"></asp:RequiredFieldValidator>
                </td>
                </tr>
             <tr>
                 <td>
                     <asp:Label ID="errorText" runat="server" style="color: red;"/>
                 </td>
             </tr>
          </table>
        <asp:Label runat="server" ID="gScore"></asp:Label>
        
        
    
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
