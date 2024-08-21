<%@ Page Title="Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Joe.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .scrollableGrid {
            overflow-y: auto;
            max-height: 300px;
        }

        #main-container {
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

        #search-container {
            margin-bottom: 20px;
        }

        #search-container label, #search-container input, #search-container button {
            margin: 8px;
        }

        #details-container {
            margin-top: 20px;
            margin-bottom: 20px;
        }

        #details-container label, #details-container input {
            margin: 8px;
        }

        #button-container button {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
            margin: 8px;
        }

        #button-container button:hover {
            background-color: #0056b3;
        }

        #GridViewBooks {
            width: 100%;
            margin-top: 20px;
            border-collapse: collapse;
        }

        #GridViewBooks th, #GridViewBooks td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        #GridViewBooks th {
            background-color: #5D7B9D;
            color: #fff;
        }

        #GridViewBooks tr:nth-child(even) {
            background-color: #F7F6F3;
        }

        #GridViewBooks tr:hover {
            background-color: #E2DED6;
        }
    </style>

    <main id="main-container" aria-labelledby="title">
        <h2 id="title"><%: Title %> Maintenance</h2>

        <div id="search-container">
            <label for="TextSearch">Search :</label>
            <asp:TextBox ID="TextSearch" runat="server"></asp:TextBox>
            <asp:Button ID="SearchCustomer" runat="server" Text="Search" OnClick="SearchCustomer_Click" />
        </div>

       
                <asp:GridView ID="GridViewBooks" runat="server" AutoPostBack="false" OnSelectedIndexChanged="GridViewBooks_SelectedIndexChanged" Width="1100px" CssClass="scrollableGrid" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" DataKeyNames="i_id"> 
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="i_id" HeaderText="i_id" InsertVisible="False" ReadOnly="True" SortExpression="i_id" />
                    <asp:BoundField DataField="isbn" HeaderText="isbn" SortExpression="isbn" />
                    <asp:BoundField DataField="title" HeaderText="title" SortExpression="title" />
                    <asp:BoundField DataField="author" HeaderText="author" SortExpression="author" />
                    <asp:BoundField DataField="genre" HeaderText="genre" SortExpression="genre" />
                    <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
                    <asp:BoundField DataField="i_Type" HeaderText="i_Type" SortExpression="i_Type" />
                </Columns>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BookStoreDBConnectionString %>" 
                SelectCommand="SELECT [isbn], [title], [author], [genre], [price], [i_Type] FROM [Items]"
                DeleteCommand="DELETE FROM [Items] WHERE [i_id] = @i_id">
                <DeleteParameters>
                    <asp:Parameter Name="i_id" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>


        <div id="details-container">
            <label for="TextISBN">ISBN :</label>
            <asp:TextBox ID="TextISBN" runat="server"></asp:TextBox>

            <label for="TextTitle">Title :</label>
            <asp:TextBox ID="TextTitle" runat="server"></asp:TextBox>

            <label for="TextAuthor">Author :</label>
            <asp:TextBox ID="TextAuthor" runat="server"></asp:TextBox>

            <label for="TextGenre">Genre :</label>
            <asp:TextBox ID="TextGenre" runat="server"></asp:TextBox>

            <label for="TextPrice">Price :</label>
            <asp:TextBox ID="TextPrice" runat="server"></asp:TextBox>

            <label for="TextBookType">Book Type :</label>
            <asp:TextBox ID="TextBookType" runat="server"></asp:TextBox>
        </div>

        <div id="button-container">
            <asp:Button ID="btnResetStock" runat="server" Text="Restock Books" OnClick="btnResetStock_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="AddBook" runat="server" Text="Add" OnClick="AddBook_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="UpdateBook" runat="server" Text="Update" OnClick="UpdateBook_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="DeleteBook" runat="server" Text="Delete" OnClick="DeleteBook_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="CancelBook" runat="server" Text="Cancel" OnClick="CancelBook_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="RedirectToCustomer" runat="server" Text="Back to Customer Maintenance" OnClick="RedirectToCustomer_Click" />
        </div>
    </main>
</asp:Content>
