<%@ Page Title="Customer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="Joe.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %> Maintenance</h2>
        <div>
            <asp:Label ID="Label2" runat="server" Text="Search :"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextSearch" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="SearchCustomer" runat="server" Text="Search" OnClick="SearchCustomer_Click" />
    </div>
        <p>
            <asp:GridView ID="GridViewCustomers" runat="server" Width="1270px" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewCustomers_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField HeaderText="Selector" ShowHeader="True" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
    </p>
    <div>
        <asp:Label ID="Label3" runat="server" Text="Name :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextName" runat="server"></asp:TextBox>
        <br />
        <br />
    </div>
    <div>
        <asp:Label ID="Label4" runat="server" Text="Email :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextEmail" runat="server"></asp:TextBox>
        <br />
        <br />
    </div>
    <div>
        <asp:Label ID="Label5" runat="server" Text="Address :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextAddress" runat="server"></asp:TextBox>
        <br />
        <br />
    </div>
    <div>
        <asp:Label ID="Label6" runat="server" Text="Username :"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextUsername" runat="server"></asp:TextBox>
        <br />
        <br />
        <div>
            <asp:Label ID="Label7" runat="server" Text="Password :"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <div>
                <asp:Button ID="AddCustomer" runat="server" Text="Add" OnClick="AddCustomer_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="UpdateCustomer" runat="server" Text="Update" OnClick="UpdateCustomer_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="DeleteCustomer" runat="server" Text="Delete" OnClick="DeleteCustomer_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="CancelCustomer" runat="server" Text="Cancel" OnClick="CancelCustomer_Click" />
            </div>
        </div>
    </div>
    </main>
</asp:Content>
