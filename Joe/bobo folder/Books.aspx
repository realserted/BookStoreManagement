<%@ Page Title="Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Joe.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <style type="text/css">
        .scrollableGrid {
            overflow-y: auto;
            max-height: 300px; /* Set the maximum height for the scrollable area */
        }
    </style>

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
            <asp:GridView ID="GridViewBooks" runat="server" Width="1100px" OnSelectedIndexChanged="GridViewBooks_SelectedIndexChanged" CssClass="scrollableGrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Selector" ShowHeader="True" ShowSelectButton="True" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
    </p>
    <div>
        <asp:Label ID="Label3" runat="server" Text="ISBN :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextISBN" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Price :"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextPrice" runat="server"></asp:TextBox>
            <br />
    </div>
    <div>
        <asp:Label ID="Label4" runat="server" Text="Title :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextTitle" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label8" runat="server" Text="Book Type :"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBookType" runat="server"></asp:TextBox>
            <br />
    </div>
    <div>
        <asp:Label ID="Label5" runat="server" Text="Author :"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextAuthor" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
    </div>
    <div>
        <asp:Label ID="Label6" runat="server" Text="Genre :"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextGenre" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <div>
&nbsp;&nbsp;&nbsp;
            <br />
            <div>
                <asp:Button ID="AddBook" runat="server" Text="Add" OnClick="AddBook_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="UpdateBook" runat="server" Text="Update" OnClick="UpdateBook_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="DeleteBook" runat="server" Text="Delete" OnClick="DeleteBook_Click" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="CancelBook" runat="server" Text="Cancel" OnClick="CancelBook_Click" />
            </div>
        </div>
    </div>
           </main>
</asp:Content>
