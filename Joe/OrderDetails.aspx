<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="Joe.MyBooks.OrderDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Details</title>
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

        h2 {
            color: #333;
            text-align: center;
        }

        #GridViewOrderDetails {
            margin-top: 20px;
            border-collapse: collapse;
            width: 100%;
        }

        #GridViewOrderDetails th, #GridViewOrderDetails td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }

        #GridViewOrderDetails th {
            background-color: #007bff;
            color: #fff;
        }

        #GridViewOrderDetails tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        #GridViewOrderDetails tr:hover {
            background-color: #ddd;
        }

        .total-price {
            font-weight: bold;
            color: #555;
        }

        #RedirectToCustomer {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
            margin-top: 20px;
            transition: background-color 0.3s ease;
        }

        #RedirectToCustomer:hover {
            background-color: #218838;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="form-container">
            <h2>Order Details</h2>
            <asp:GridView ID="GridViewOrderDetails" runat="server" AutoGenerateColumns="False" EmptyDataText="No ordered items found" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gvCustomerOrderedItems_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="o_id" HeaderText="Order ID" />
                    <asp:BoundField DataField="o_date" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="title" HeaderText="Item Title" />
                    <asp:BoundField DataField="price" HeaderText="Item Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="qty" HeaderText="Quantity" />

                    <asp:TemplateField HeaderText="Total Price" SortExpression="TotalPrice">
                        <ItemTemplate>
                            <span class="total-price">
                                <%# (Convert.ToDecimal(Eval("price")) * Convert.ToInt32(Eval("qty"))).ToString("C") %>
                            </span>
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

&nbsp;&nbsp;&nbsp;
            <asp:Button ID="RedirectToCustomer" runat="server" Text="Back to Customer Maintenance" OnClick="RedirectToCustomer_Click" />
        </div>
    </form>
</body>
</html>
