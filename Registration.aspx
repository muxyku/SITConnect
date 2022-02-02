<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SITConnect.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body>
    <h1>SIT Registration</h1>
    <form id="form1" runat="server">
       
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">First Name</td>
                    <td >
                        <asp:TextBox id="tb_userFn" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Last Name</td>
                    <td>
                        <asp:TextBox id="tb_userLn" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Credit Card Number</td>
                    <td >
                        <asp:TextBox id="tb_userCreditNum" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Credit Card Date</td>
                    <td >
                        <asp:TextBox id="tb_userCreditDate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style2">Credit Card CVV</td>
                    <td >
                        <asp:TextBox id="tb_userCreditCVV" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">Email</td>
                    <td >
                        <asp:TextBox id="tb_userEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Password</td>
                    <td >
                        <asp:TextBox id="tb_userPass" onkeyup="javascript:validate()" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lbl_pwdchecker" runat="server" Text="Label"></asp:Label>
                    </td>
                        
                </tr>
                <tr>
                    <td class="auto-style2">Date of Birth</td>
                    <td >
                        <asp:TextBox id="tb_userDob" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Photo</td>
                    <td >
                        <asp:TextBox id="photo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2"><asp:Button Text="Submit" ID="register_btn" OnClick="btn_Submit_Click" runat="server"/></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
