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
            <asp:GridView ID="GridViewCustomers" runat="server" Width="1270px" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewCustomers_SelectedIndexChanged" DataKeyNames="c_id" AutoGenerateColumns="False">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="c_id" HeaderText="c_id" SortExpression="c_id" />
                    <asp:BoundField DataField="c_name" HeaderText="c_name" SortExpression="c_name" />
                    <asp:BoundField DataField="c_email" HeaderText="c_email" SortExpression="c_email" />
                    <asp:BoundField DataField="c_address" HeaderText="c_address" SortExpression="c_address" />
                    <asp:BoundField DataField="c_username" HeaderText="c_username" SortExpression="c_username" />
                    <asp:BoundField DataField="c_password" HeaderText="c_password" SortExpression="c_password" />
                </Columns>
                <AlternatingRowStyle BackColor="White" />
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BookStoreDBConnectionString %>" 
                SelectCommand="SELECT [c_id], [c_name], [c_email], [c_address], [c_username], [c_password] FROM [Customer]"
                DeleteCommand="DELETE FROM [Customer] WHERE [c_id] = @c_id">
                <DeleteParameters>
                    <asp:Parameter Name="c_id" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>
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
&nbsp;&nbsp;&nbsp;                
                 <asp:Button ID="ViewOrder" runat="server" Text="View Order" OnClick="ViewOrder_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="RedirectToBooks" runat="server" Text="Go to Books Maintenance" OnClick="RedirectToBooks_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="RegisterAdmin" runat="server" OnClick="RegisterAdmin_Click" Text="Register Admin" CssClass="btn btn-secondary" />

            </div>
        </div>
    </div>
    </main>
</asp:Content>
