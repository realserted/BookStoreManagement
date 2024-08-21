<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookStore.aspx.cs" Inherits="Joe.MyBooks.BookStore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1><asp:Label ID="Label1" runat="server" Text="WELCOME TO THE ONLINE BOOKSTORE"></asp:Label></h1>
            <br />
           <h2 style="width: 183px"> <asp:Label ID="IamName" runat="server" Text="Label"></asp:Label> </h2>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" Height="29px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Cart" runat="server" OnClick="Cart_Click" Text="Shopping Cart" />
            <br />
            <br />
            <asp:Label ID="lblConfirmation" runat="server" Text="Available Books"></asp:Label>
            <br />
            <br />
            <h2>Book Listing</h2>
            <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="10" OnRowCommand="gvBooks_RowCommand" DataKeyNames="i_id" Width="1200px" OnPageIndexChanging="gvBooks_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" />
                 <Columns>
                    <asp:ButtonField ButtonType="Button" CommandName="ViewDetails" Text="View Details" />
                    <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
                    <asp:BoundField DataField="author" HeaderText="Author" SortExpression="author" />
                    <asp:BoundField DataField="genre" HeaderText="Genre" SortExpression="genre" />
                    <asp:BoundField DataField="price" HeaderText="Price" SortExpression="price" DataFormatString="{0:C}" />
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
            <br />
            <br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
        </div>


    </form>
</body>
</html>
