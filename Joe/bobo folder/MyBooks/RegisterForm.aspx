<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="Joe.MyBooks.RegisterForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="REGISTRATION FORM"></asp:Label>
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Already have an account? Click here to login</asp:HyperLink>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Name: "></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="RegisterName" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Email :"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="RegisterEmail" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Address : "></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="RegisterAddress" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Username :"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="RegisterUsername" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Password :"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="RegisterPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Confirm Password : "></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="RegisterConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="JustALabel" runat="server" Text="Hello, Register now"></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnCustomerRegister" runat="server" OnClick="BtnCustomerRegister_Click" Text="Register" />
        </div>
    </form>
</body>
</html>
