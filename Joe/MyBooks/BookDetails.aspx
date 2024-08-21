<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetails.aspx.cs" Inherits="Joe.MyBooks.BookDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Details</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }

        #form-container {
            max-width: 600px;
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

        .book-info {
            margin-bottom: 10px;
        }

        .label {
            font-weight: bold;
            color: #555;
        }

        #BookQtyText {
            width: 100%;
            padding: 8px;
            box-sizing: border-box;
            border: 1px solid #ddd;
            border-radius: 4px;
        }

        #btnAddToCart, #Back {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
        }

        #btnAddToCart:hover, #Back:hover {
            background-color: #0056b3;
        }

        .btn-group {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="form-container">
            <h2>Book Details</h2>
            
            <div class="book-info">
                <span class="label">Title:</span>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
            </div>
            
            <div class="book-info">
                <span class="label">Author:</span>
                <asp:Label ID="lblAuthor" runat="server" Text=""></asp:Label>
            </div>
            
            <div class="book-info">
                <span class="label">Genre:</span>
                <asp:Label ID="lblGenre" runat="server" Text=""></asp:Label>
            </div>
            
            <div class="book-info">
                <span class="label">Price:</span>
                <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="lbli_Type" runat="server"></asp:Label>
            </div>
            
            <div class="book-info">
                <asp:TextBox ID="BookQtyText" runat="server" Text="Enter Quantity"></asp:TextBox>
            </div>

            <div class="btn-group">
                <asp:Button ID="btnAddToCart" Text="Add to Cart" OnClick="btnAddToCart_Click" runat="server" />
                <br />
                <br />
                <asp:Button ID="Back" Text="Back" OnClick="Back_Click" runat="server" style="margin-bottom: 0px" />
            </div>
        </div>
    </form>
</body>
</html>
