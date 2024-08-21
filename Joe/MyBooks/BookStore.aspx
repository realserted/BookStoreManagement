<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookStore.aspx.cs" Inherits="Joe.MyBooks.BookStore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online Bookstore</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        #form-container {
            max-width: 1200px;
            margin: 20px auto;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1, h2 {
            color: #333;
            text-align: center;
        }

        #btnLogout, #Cart {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
        }

        #btnLogout:hover, #Cart:hover {
            background-color: #0056b3;
        }

        #lblConfirmation {
            color: #333;
            font-size: 18px;
            font-weight: bold;
        }

        #gvBooks {
            margin-top: 20px;
            border-collapse: collapse;
            width: 100%;
        }

        #gvBooks th, #gvBooks td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        #gvBooks th {
            background-color: #007bff;
            color: #fff;
        }

        #gvBooks tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        #gvBooks tr:hover {
            background-color: #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="form-container">
            <h1>WELCOME TO THE ONLINE BOOKSTORE</h1>
            <br />
            <h2><asp:Label ID="IamName" runat="server" Text="Label"></asp:Label></h2>
            <div style="text-align: right;">
                <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
                <asp:Button ID="Cart" runat="server" OnClick="Cart_Click" Text="Shopping Cart" />
            </div>
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
                    <asp:BoundField DataField="StockQuantity" HeaderText="Stock Quantity" SortExpression="StockQuantity" />
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
        </div>
    </form>
</body>
</html>
