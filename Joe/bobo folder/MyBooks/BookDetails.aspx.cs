using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joe.MyBooks
{
    public partial class BookDetails : System.Web.UI.Page
    {
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

            if (!int.TryParse(BookQtyText.Text, out int temp)) {

                Response.Write("<script>alert('dont input 0 or string');</script>");

                return;
            }
            else if (temp == 0) {
                Response.Write("<script>alert('dont input 0 or string');</script>");
                return;
            }

            int quantity = temp; // You can adjust this based on user input

            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                connection.Open();

                // Check if the item is already in the user's cart
                string checkCartItemQuery = "SELECT COUNT(*) FROM CartItems WHERE c_id = @CustomerId AND i_id = @BookId";
                using (SqlCommand checkCmd = new SqlCommand(checkCartItemQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@CustomerId", customerId);
                    checkCmd.Parameters.AddWithValue("@BookId", bookId);

                    int existingItemCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (existingItemCount > 0)
                    {
                        // If the item already exists, update the quantity
                        string updateQuantityQuery = "UPDATE CartItems SET qty = qty + @Quantity " +
                                                     "WHERE c_id = @CustomerId AND i_id = @BookId";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuantityQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@CustomerId", customerId);
                            updateCmd.Parameters.AddWithValue("@BookId", bookId);
                            updateCmd.Parameters.AddWithValue("@Quantity", quantity);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // If the item doesn't exist, insert a new row
                        string insertCartItemQuery = "INSERT INTO CartItems (c_id, i_id, qty) VALUES (@CustomerId, @BookId, @Quantity)";

                        using (SqlCommand insertCmd = new SqlCommand(insertCartItemQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@CustomerId", customerId);
                            insertCmd.Parameters.AddWithValue("@BookId", bookId);
                            insertCmd.Parameters.AddWithValue("@Quantity", quantity);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Optionally, you can display a confirmation message
            Response.Write("<script>alert('Item added to cart successfully');</script>");
        }



        private void LoadBookDetails(int bookId)
        {
            // Connect to the database (replace connection string with your actual connection string)
            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
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