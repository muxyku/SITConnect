<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPage.aspx.cs" Inherits="SITConnect.AccountPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
          <li class="nav-item active">
            <a class="nav-link" href="Registration.aspx">Registration</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="Login.aspx">Login</a>
          </li>
          <li class="nav-item active">
            <a class="nav-link" href="AccountPage.aspx">Account Page<span class="sr-only">(current)</span></a>
          </li>
        </ul>
      </div>
    </nav>
    <h1>Successfully Logged in! Welcome!</h1>
    
    <form id="form1" runat="server">
    <h2>Information of: 
        <asp:Label ID="userName" runat="server" Text="Name"></asp:Label>
    </h2>
     <p>Email:
        <asp:Label ID="email" runat="server" Text="Email"></asp:Label>
     </p>
     
     <p>Date of birth:
        <asp:Label ID="dob" runat="server" Text="Date of birth"></asp:Label>
     </p>
    <div>
            <asp:Button id="btnLogout" runat="Server" OnClick="Logout" Text="Logout"/>
    </div>
    </form>
    <a href="ChangePassword.aspx">Change password</a>

</body>
</html>
