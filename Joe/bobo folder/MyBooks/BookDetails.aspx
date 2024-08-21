<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="Joe.MyBooks.BookDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Book Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </h2>
            <div>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                <br />
            </div>
            <div>
                <asp:Label ID="lblAuthor" runat="server" Text=""></asp:Label>
                <br />
            </div>
            <div>
                <asp:Label ID="lblGenre" runat="server" Text=""></asp:Label>
                <br />
            </div>
            <div>
                <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lbli_Type" runat="server"></asp:Label>
                <br />
                <br />
            </div>
            <!-- Add more labels for other book details as needed -->

            <div>
                <asp:TextBox ID="BookQtyText" runat="server">Enter Quantity </asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnAddToCart" Text="Add to Cart" OnClick="btnAddToCart_Click" runat="server" />
                <br />
                <br />
                <asp:Button ID="Back" Text="Back" OnClick="Back_Click" runat="server" style="margin-bottom: 0px" />
                <br />
            </div>
        </div>
    </form>
</body>
</html>
