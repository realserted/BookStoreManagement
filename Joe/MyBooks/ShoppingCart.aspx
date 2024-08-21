<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="Joe.MyBooks.ShoppingCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Shopping Cart</title>
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

        #gvCart, #gvCustomerOrderedItems {
            margin-top: 20px;
            border-collapse: collapse;
            width: 100%;
        }

        #gvCart th, #gvCart td, #gvCustomerOrderedItems th, #gvCustomerOrderedItems td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        #gvCart th, #gvCustomerOrderedItems th {
            background-color: #007bff;
            color: #fff;
        }

        #gvCart tr:nth-child(even), #gvCustomerOrderedItems tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        #gvCart tr:hover, #gvCustomerOrderedItems tr:hover {
            background-color: #ddd;
        }

        .label {
            font-weight: bold;
            color: #555;
        }

        #shipaddress {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
            border: 1px solid #ddd;
            border-radius: 4px;
        }

        #ClearCart, #Order, #Button2 {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
        }

        #ClearCart:hover, #Order:hover, #Button2:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="form-container">
            <h1>Your Shopping Cart</h1>
           <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvCart_SelectedIndexChanged">
           <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Qty" HeaderText="Quantity" SortExpression="Quantity" />
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
            <asp:Button ID="ClearCart" runat="server" OnClick="ClearCart_Click" Text="Clear Cart" />
            <br />
            <br />
            <div>
                <span class="label">Shipping Address:</span>
                <asp:TextBox ID="shipaddress" runat="server"></asp:TextBox>
            </div>
            <br />
            <br />
            <asp:Button ID="Order" runat="server" OnClick="Order_Click" Text="Order" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Back" Width="89px" />
            <br />
            <h2>Items Ordered</h2>
            <asp:GridView ID="gvCustomerOrderedItems" runat="server" AutoGenerateColumns="False" EmptyDataText="No ordered items found" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1081px" OnSelectedIndexChanged="gvCustomerOrderedItems_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="o_id" HeaderText="Order ID" />
                    <asp:BoundField DataField="o_date" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="title" HeaderText="Item Title" />
                    <asp:BoundField DataField="price" HeaderText="Item Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="qty" HeaderText="Quantity" />

                    <asp:TemplateField HeaderText="Total Price" SortExpression="TotalPrice">
                        <ItemTemplate>
                            <%# (Convert.ToDecimal(Eval("price")) * Convert.ToInt32(Eval("qty"))).ToString("C") %>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <br />
        </div>
    </form>
</body>
</html>
