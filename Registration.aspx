<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="SITConnect.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
   
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
    
    <!-- Client side password validation -->
    <script>
        function validate() {
            var str = document.getElementById('<%=tb_userPass.ClientID %>').value;

            //Check for minimum 12 characters
            if (str.length < 12) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password length must be at least 12 Characters";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("too short");
            }
            //Check of contains numbers
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 number";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_number");
            }
            //Check if contains uppercase
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 upper case character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }
            //Check if contains lowercase
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 lower case character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_lowercase");
            }
            //Check if contains special characters
            else if (str.search(/[!@#$%^&*]/) == -1) {
                document.getElementById("lbl_pwdchecker").innerHTML = "Password requires at least 1 special character";
                document.getElementById("lbl_pwdchecker").style.color = "Red";
                return ("no_special");
            }
            document.getElementById("lbl_pwdchecker").innerHTML = "Excellent!"
            document.getElementById("lbl_pwdchecker").style.color = "Green";
        }
    </script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>
   
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
  <a class="navbar-brand" href="Registration.aspx">SIT Connect</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>

  <div class="collapse navbar-collapse" id="navbarSupportedContent">
    <ul class="navbar-nav mr-auto">
      <li class="nav-item active">
        <a class="nav-link" href="Registration.aspx">Registration<span class="sr-only">(current)</span></a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="Login.aspx">Login</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="AccountPage.aspx">Account Page</a>
      </li>
    </ul>
  </div>
</nav>

    <h1>Registration</h1>
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
                        <asp:TextBox id="tb_userCreditNum" runat="server" required="true" MaxLength="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_userCreditNum" ErrorMessage="Credit card number is required" style="color:red;"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="(Please enter a valid credit number)"  ValidationExpression="^4[0-9]{12}(?:[0-9]{3})?$" ControlToValidate="tb_userCreditNum" style="color:red;"></asp:RegularExpressionValidator>

                    </td>
                    <td>
                        <asp:Label ID="numMsg" runat="server" Text="" style="color:red;"></asp:Label>
                    </td>
                    
                </tr>
                <tr>
                    <td class="auto-style2">Credit Card Date</td>
                    <td >
                        <asp:TextBox id="tb_userCreditDate" runat="server" required="true" MaxLength="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tb_userCreditDate" ErrorMessage="Credit card date is required" style="color:red;"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="(Please enter a valid expiry date)"  ValidationExpression="\d{4}" ControlToValidate="tb_userCreditDate" style="color:red;"></asp:RegularExpressionValidator>

                    </td>

                </tr>
                 <tr>
                    <td class="auto-style2">Credit Card CVV</td>
                    <td >
                        <asp:TextBox id="tb_userCreditCVV" runat="server" required="true" TextMode="Number" MaxLength="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_userCreditCVV" ErrorMessage="Credit card CVV is required" ValidationExpression="^[0-9]{0,3}$" style="color:red;"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="(Please enter a valid CVV)"  ValidationExpression="\d{3}" ControlToValidate="tb_userCreditCVV" style="color:red;"></asp:RegularExpressionValidator>

                    </td>
                     <td>
                        <asp:Label ID="cvvMsg" runat="server" Text="" style="color:red;"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">Email</td>
                    <td >
                        <asp:TextBox id="tb_userEmail" runat="server" required="true" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_userEmail" ErrorMessage="Email is required" style="color:red;"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Enter a valid email address"  ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ControlToValidate="tb_userEmail" style="color:red;"></asp:RegularExpressionValidator>
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
                    <td class="auto-style1">File upload</td>
                    <td class="auto-style1" >
                        <asp:TextBox id="TextBox1" runat="server" type="file"></asp:TextBox>
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
