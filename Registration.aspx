<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SITConnect.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
   
</head>
<body>
    <h1>SIT Registration</h1>
    <form id="form1" runat="server">
       
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">First Name</td>
                    <td >
                        <asp:TextBox id="tb_userFn" runat="server" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_userFn" ErrorMessage="First name is required" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Last Name</td>
                    <td>
                        <asp:TextBox id="tb_userLn" runat="server" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_userLn" ErrorMessage="Last name is required" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Credit Card Number</td>
                    <td >
                        <asp:TextBox id="tb_userCreditNum" runat="server" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_userCreditNum" ErrorMessage="Credit card number is required" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Credit Card Date</td>
                    <td >
                        <asp:TextBox id="tb_userCreditDate" runat="server" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tb_userCreditDate" ErrorMessage="Credit card date is required" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td class="auto-style2">Credit Card CVV</td>
                    <td >
                        <asp:TextBox id="tb_userCreditCVV" runat="server" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_userCreditCVV" ErrorMessage="Credit card CVV is required" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">Email</td>
                    <td >
                        <asp:TextBox id="tb_userEmail" runat="server" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_userEmail" ErrorMessage="Email is required" style="color:red;"></asp:RequiredFieldValidator>
                        
                    </td>
                        
                    
                    
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lbl_email" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Password</td>
                    <td >
                        <asp:TextBox id="tb_userPass" onkeyup="javascript:validate()" runat="server" TextMode="Password"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tb_userPass" ErrorMessage="Password is required" style="color:red;"></asp:RequiredFieldValidator>
                        <asp:regularexpressionvalidator id="RegularExpressionValidator3" display="Dynamic" errormessage="Password must be at least 12 characters long</br> with at least one numeric character, one lowercase letter, one uppercase letter and one special character." forecolor="Red" validationexpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$" controltovalidate="tb_userPass" runat="server"></asp:regularexpressionvalidator>

                    </td>
                        
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="lbl_pwdchecker" runat="server" Text="Password strength" style="color:red;"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">Date of Birth</td>
                    <td class="auto-style1" >
                        <asp:TextBox id="tb_userDob" runat="server" type="date" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tb_userDob" ErrorMessage="Date of birth is required" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Photo</td>
                    <td >
                        <asp:TextBox id="photo" runat="server" required="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="photo" ErrorMessage="Photo is required" style="color:red;"></asp:RequiredFieldValidator>
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
