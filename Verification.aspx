<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="SITConnect.Verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>

</head>
<body>
     <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand nav-link disabled" href="#" >SIT Connect</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
            <li class="nav-item">
            <a class="nav-link disabled" href="#">Registration</a>
            </li>
            <li class="nav-item">
            <a class="nav-link disabled" href="#">Login</a>
            </li>
            <li class="nav-item">
            <a class="nav-link disabled" href="#">Account Page</a>
            </li>
        </ul>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div>
            <h4>Enter verification code sent to your email below:</h4>
            <asp:TextBox ID="code" runat="server"></asp:TextBox>
            <asp:Button ID="submit" runat="server" Text="Verify" OnClick="verifyOTP" />
            <asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancelBtn" />
            <br />
            <asp:Label ID="errorMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
