<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="Joe.MyBooks.ShoppingCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Your Shopping Cart</h1>
       </div>
       <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="850px">
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
&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text=" Shipping Address :"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="shipaddress" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="Order" runat="server" OnClick="Order_Click" Text="Order" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" style="width: 55px" Text="Back" />
        <br />
            <h1>Items Ordered</h1>
        <asp:GridView ID="gvCustomerOrderedItems" runat="server" AutoGenerateColumns="False" EmptyDataText="No ordered items found" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1081px">
            <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="o_id" HeaderText="Order ID" />
        <asp:BoundField DataField="o_date" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
        <asp:BoundField DataField="title" HeaderText="Item Title" />
        <asp:BoundField DataField="price" HeaderText="Item Price" DataFormatString="{0:C}" />
        <asp:BoundField DataField="qty" HeaderText="Quantity" />
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
    </form>
</body>
</html>
