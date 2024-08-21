<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Joe._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Welcome to Online Book Store</h1>
            <p>
                <asp:Label ID="Label1" runat="server" Text="Username :"></asp:Label>
&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextUser" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="Label2" runat="server" Text="Password :"></asp:Label>
&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextPassword" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:Label ID="IamMessage" runat="server" Text="Message"></asp:Label>
            </p>
            <p>
                <asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Login" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Register" runat="server" OnClick="Register_Click" Text="Register" />
            </p>
        </section>
    </main>

</asp:Content>
