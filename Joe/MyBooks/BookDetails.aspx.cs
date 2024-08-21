using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joe.MyBooks
{
    [Serializable]
    public class CartItem
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }

    public partial class BookDetails : System.Web.UI.Page
    {
        string mycon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the book ID is provided as a query parameter
                if (Request.QueryString["id"] != null)
                {
                    // Retrieve and display book details
                    int bookId = Convert.ToInt32(Request.QueryString["id"]);
                    LoadBookDetails(bookId);
                }
                else
                {
                    // Handle the case when no book ID is provided
                    // You may redirect the user to the book listing page or display an error message
                    Response.Redirect("BookStore.aspx");
                }
            }
        }

        private void ClearShoppingCart(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                // Delete items from CartItems
                string clearCartQuery = "DELETE FROM CartItems WHERE c_id = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(clearCartQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["CustomerId"] == null)
            {
                // Redirect to the login page or display a message indicating the user needs to be logged in
                Response.Redirect("~/Default.aspx");
                return;
            }

            int customerId = Convert.ToInt32(Session["CustomerId"]);
            int bookId = Convert.ToInt32(Request.QueryString["id"]);

            if (!int.TryParse(BookQtyText.Text, out int temp))
            {
                Response.Write("<script>alert('Please enter a valid quantity.');</script>");
                return;
            }
            else if (temp <= 0)
            {
                Response.Write("<script>alert('Please enter a valid quantity greater than zero.');</script>");
                return;
            }

            int requestedQuantity = temp; // User input quantity

            // Validate stock availability
            if (!IsStockAvailable(bookId, requestedQuantity))
            {
                Response.Write("<script>alert('Insufficient stock. Please enter a lower quantity.');</script>");
                return;
            }

            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                // If the item is not in the cart, insert a new row
                string insertCartItemQuery = "INSERT INTO CartItems (c_id, i_id, qty) VALUES (@CustomerId, @BookId, @Quantity)";

                using (SqlCommand insertCmd = new SqlCommand(insertCartItemQuery, connection))
                {
                    insertCmd.Parameters.AddWithValue("@CustomerId", customerId);
                    insertCmd.Parameters.AddWithValue("@BookId", bookId);
                    insertCmd.Parameters.AddWithValue("@Quantity", requestedQuantity);

                    insertCmd.ExecuteNonQuery();
                }
            }

            // Optionally, you can display a confirmation message
            Response.Write("<script>alert('Item added to cart successfully');</script>");
        }



        // Add this function to get or create the order ID
        private int GetOrCreateOrderId(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                // Check if there is an open order for the customer
                string checkOrderQuery = "SELECT o_id FROM Orders WHERE c_id = @CustomerId";
                using (SqlCommand checkCmd = new SqlCommand(checkOrderQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@CustomerId", customerId);

                    object result = checkCmd.ExecuteScalar();
                    if (result != null)
                    {
                        // If there is an open order, return its ID
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        // If there is no open order, create a new order and return its ID
                        string createOrderQuery = "INSERT INTO Orders (c_id) VALUES (@CustomerId); SELECT SCOPE_IDENTITY();";
                        using (SqlCommand createCmd = new SqlCommand(createOrderQuery, connection))
                        {
                            createCmd.Parameters.AddWithValue("@CustomerId", customerId);

                            // Execute the query and return the generated order ID
                            return Convert.ToInt32(createCmd.ExecuteScalar());
                        }
                    }
                }
            }
        }

        // When the user decides to place an order:
        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(Session["CustomerId"]);

            if (customerId != 0)
            {
                int orderId = GetOrCreateOrderId(customerId);

                using (SqlConnection connection = new SqlConnection(mycon))
                {
                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        List<CartItem> cartItems = GetCartItems(customerId);

                        foreach (var cartItem in cartItems)
                        {
                            int bookId = cartItem.BookId;
                            int quantity = cartItem.Quantity;

                            // Insert into ItemsOrdered table using the bookId and quantity
                            string insertOrderItemsQuery = "INSERT INTO ItemsOrdered (o_id, i_id, qty) VALUES (@OrderId, @BookId, @Quantity)";
                            using (SqlCommand cmd = new SqlCommand(insertOrderItemsQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@OrderId", orderId);
                                cmd.Parameters.AddWithValue("@BookId", bookId);
                                cmd.Parameters.AddWithValue("@Quantity", quantity);

                                cmd.ExecuteNonQuery();
                            }

                           
                            // Add this SELECT statement to retrieve and log the current stock quantity
                            string selectStockQuery = "SELECT quantity FROM ItemStock WHERE i_id = @BookId";
                            using (SqlCommand stockSelectCmd = new SqlCommand(selectStockQuery, connection, transaction))
                            {
                                stockSelectCmd.Parameters.AddWithValue("@BookId", bookId);
                                int currentStock = Convert.ToInt32(stockSelectCmd.ExecuteScalar());
                                Response.Write($"<script>alert('Current stock for Book ID {bookId}: {currentStock}');</script>");
                            }
                        }

                        // Clear the user's shopping cart
                        ClearShoppingCart(customerId);

                        // Commit the transaction if everything is successful
                        transaction.Commit();

                        // Optionally, you can display a confirmation message
                        Response.Write("<script>alert('Order placed successfully');</script>");
                    }
                    catch (Exception ex)
                    {
                        // Log the exception or handle it appropriately
                        // Roll back the transaction
                        transaction.Rollback();

                        Response.Write($"<script>alert('An error occurred while placing the order. {ex.Message}');</script>");
                    }
                }
            }
            else
            {
                // Handle the case when no customer is logged in
                Response.Redirect("~/Default.aspx");
            }
        }


        private List<CartItem> GetCartItems(int customerId)
        {
            List<CartItem> cartItems = new List<CartItem>();

            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                string selectQuery = "SELECT i_id, qty FROM CartItems WHERE c_id = @CustomerId";
                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int bookId = Convert.ToInt32(reader["i_id"]);
                            int quantity = Convert.ToInt32(reader["qty"]);

                            cartItems.Add(new CartItem { BookId = bookId, Quantity = quantity });
                        }
                    }
                }
            }

            return cartItems;
        }


        private int GetAvailableStock(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                // Retrieve available stock from the ItemStock table
                string selectStockQuery = "SELECT quantity FROM ItemStock WHERE i_id = @BookId";
                using (SqlCommand stockCmd = new SqlCommand(selectStockQuery, connection))
                {
                    stockCmd.Parameters.AddWithValue("@BookId", bookId);

                    int availableStock = Convert.ToInt32(stockCmd.ExecuteScalar());

                    return availableStock;
                }
            }
        }


        private bool IsStockAvailable(int bookId, int requestedQuantity)
        {
            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                // Rest of the code remains unchanged
                string selectStockQuery = "SELECT quantity FROM ItemStock WHERE i_id = @BookId";
                using (SqlCommand stockCmd = new SqlCommand(selectStockQuery, connection))
                {
                    stockCmd.Parameters.AddWithValue("@BookId", bookId);

                    int availableStock = Convert.ToInt32(stockCmd.ExecuteScalar());

                    // Check if requested quantity is greater than available stock
                    return requestedQuantity <= availableStock;
                }
            }
        }



        private void LoadBookDetails(int bookId)
        {
            // Connect to the database (replace connection string with your actual connection string)
            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                // Retrieve book details from the Items table
                string selectQuery = "SELECT title, author, genre, price, i_Type FROM Items WHERE i_id = @BookId";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@BookId", bookId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Display book details on the page
                            lblTitle.Text = "Title: " + reader["title"].ToString();
                            lblAuthor.Text = "Author: " + reader["author"].ToString();
                            lblGenre.Text = "Genre: " + reader["genre"].ToString();
                            lblPrice.Text = "Price: " + Convert.ToDecimal(reader["price"]).ToString("C");
                            lbli_Type.Text = "Book Type: " + reader["i_Type"].ToString();
                        }
                        else
                        {
                            // Handle the case when no book with the specified ID is found
                            // You may redirect the user to the book listing page or display an error message
                            Response.Redirect("BookStore.aspx");
                        }
                    }
                }
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookStore.aspx");
        }
    }
}
