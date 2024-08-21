<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Joe._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="login-section">
            <h1 id="aspnetTitle">Welcome to the Online Book Store</h1>

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" AssociatedControlID="TextUser" Text="Username:" CssClass="label"></asp:Label>
                <asp:TextBox ID="TextUser" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Label ID="Label2" runat="server" AssociatedControlID="TextPassword" Text="Password:" CssClass="label"></asp:Label>
                <asp:TextBox ID="TextPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>

            <asp:Label ID="IamMessage" runat="server" Text="Message" CssClass="message-label"></asp:Label>

            <div class="button-group">
                <asp:Button ID="Login" runat="server" OnClick="Login_Click" Text="Login" CssClass="btn btn-primary" />
                <asp:Button ID="Register" runat="server" OnClick="Register_Click" Text="Register" CssClass="btn btn-secondary" />
                
            </div>
        </section>
    </main>

</asp:Content>

