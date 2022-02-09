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

        function validateEmail() {
            var str = document.getElementById('<%=tb_userEmail.ClientID %>/*').value;
            if (str.search(/^w+([.-]?w+)*@w+([.-]?w+)*(.w{2,3})+$/) == -1) {
                document.getElementById("emailMsg").innerHTML = "Not a valid email. Please enter a valid one.";
                document.getElementById("emailMsg").style.color = "Red";
                return ('no_email')

            }
            document.getElementById("emailMsg").innerHTML = "Valid email format!"
            document.getElementById("emailMsg").style.color = "Green";
        }

        function validateCreditNum() {
            var str = document.getElementById('<%=tb_userCreditNum.ClientID %>').value;
            if (str.search(/\d{16}/) == -1) {
                document.getElementById("numMsg").innerHTML = "Credit card number needs to be 16 digits";
                document.getElementById("numMsg").style.color = "Red";
                return ('no_digits')
            }
            document.getElementById("numMsg").innerHTML = "Valid credit card number format!"
            document.getElementById("numMsg").style.color = "Green";
        }
        function validateCreditCVV() {
            var str = document.getElementById('<%=tb_userCreditCVV.ClientID %>').value;
                if (str.search(/\d{3}/) == -1) {
                    document.getElementById("cvvMsg").innerHTML = "Credit card cvv needs to be 3 digits";
                    document.getElementById("cvvMsg").style.color = "Red";
                    return('no_digits')
                }
                document.getElementById("cvvMsg").innerHTML = "Valid credit card cvv format!"
                document.getElementById("cvvMsg").style.color = "Green";
         }
    </script>
   
</head>
<body>
    <h1>SIT Connect Registration</h1>
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
                        <asp:TextBox id="tb_userCreditNum" onkeyup="javascript:validateCreditNum()" runat="server" required="true"  Textmode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_userCreditNum" ErrorMessage="Credit card number is required" ValidationExpression="^[0-9]{0,16}$" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="numMsg" runat="server" Text="" style="color:red;"></asp:Label>
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
                        <asp:TextBox id="tb_userCreditCVV" onkeyup="javascript:validateCreditCVV()" runat="server" required="true"  Textmode="Number"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_userCreditCVV" ErrorMessage="Credit card CVV is required" ValidationExpression="^[0-9]{0,3}$" style="color:red;"></asp:RequiredFieldValidator>
                    </td>
                     <td>
                        <asp:Label ID="cvvMsg" runat="server" Text="" style="color:red;"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">Email</td>
                    <td >
                        <asp:TextBox id="tb_userEmail" runat="server" required="true" onkeyup="javascript:validateEmail()"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_userEmail" ErrorMessage="Email is required" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" style="color:red;"></asp:RequiredFieldValidator>
                        
                    </td>
                    <td>
                        <asp:Label ID="emailMsg" runat="server" Text="" style="color:red;"></asp:Label>
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
