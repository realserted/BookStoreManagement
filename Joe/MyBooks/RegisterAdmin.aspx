<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAdmin.aspx.cs" Inherits="Joe.MyBooks.RegisterAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Registration Form</title>
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f4;
            text-align: center;
            padding: 20px;
        }

        form {
            max-width: 400px;
            margin: 0 auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        label {
            display: inline-block;
            text-align: left;
            margin-bottom: 5px;
        }

        input[type="text"],
        input[type="password"] {
            width: 100%;
            padding: 8px;
            margin-bottom: 15px;
            box-sizing: border-box;
        }

        a {
            color: #007bff;
            text-decoration: none;
        }

        .button {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
        }

        .button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="ADMIN REGISTRATION FORM"></asp:Label>
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Already have an account? Click here to login</asp:HyperLink>
            <br />
            <br />
            <label for="NewAdminUsername">New Admin Username:</label>
            <asp:TextBox ID="NewAdminUsername" runat="server"></asp:TextBox>
            <br />
            <br />
            <label for="NewAdminPassword">New Admin Password:</label>
            <asp:TextBox ID="NewAdminPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="JustALabel" runat="server" Text="Hello, Register now"></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnAdminRegister" runat="server" OnClick="BtnAdminRegister_Click" Text="Register Admin" CssClass="button" />
        </div>
    </form>
</body>
</html>
