<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountPage.aspx.cs" Inherits="SITConnect.AccountPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Successfully Logged in! Welcome!</h1>
    
    <form id="form1" runat="server">
    <h2>Information of: 
        <asp:Label ID="userName" runat="server" Text="Name"></asp:Label>
     </h2>
     <p>Credit Card Number:
        <asp:Label ID="creditNum" runat="server" Text="Credit card num"></asp:Label>
     </p>
     <p>Credit Card Date:
        <asp:Label ID="creditDate" runat="server" Text="Credit card date"></asp:Label>
     </p>
     <p>Credit Card CVV:
        <asp:Label ID="creditCVV" runat="server" Text="Credit card cvv"></asp:Label>
     </p>
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
</body>
</html>
