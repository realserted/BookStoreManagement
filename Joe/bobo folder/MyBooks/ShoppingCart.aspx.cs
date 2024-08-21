using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Joe.MyBooks.BookDetails;

namespace Joe.MyBooks
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        String mycon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerId"] == null)
            {
                // User is not authenticated, redirect to the login page
                Response.Redirect("/Default.aspx");
            }

            if (!IsPostBack)
            {
                if (Session["CustomerId"] != null)
                {
                    int customerId = Convert.ToInt32(Session["CustomerId"]);
                    LoadCustomerOrderedItems(customerId);
                }
                else
                {
                    // Handle the case when no customer ID is available
                    // You may redirect the user or display an error message
                }
     
              LoadShoppingCart();
            }
        }

        private void MoveCartItemsToItemsOrdered(int customerId, int orderId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                connection.Open();  

                // Insert items from CartItems to ItemsOrdered
                string insertItemsOrderedQuery = "INSERT INTO ItemsOrdered (o_id, i_id, qty) SELECT @OrderId, i_id, qty FROM CartItems WHERE c_id = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(insertItemsOrderedQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    cmd.ExecuteNonQuery();
                }

                // Clear the user's shopping cart
                ClearShoppingCart(customerId);
            }
        }

        private void ClearShoppingCart(int customerId)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
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

        protected void Order_Click(object sender, EventArgs e)
        {
            if (Session["CustomerId"] != null)
            {
                int customerId = Convert.ToInt32(Session["CustomerId"]);

                using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    connection.Open();

                    // Insert a new order record
                    string insertOrderQuery = "INSERT INTO Orders (o_date, ship_address, c_id) VALUES (GETDATE(), @ShipAddress, @CustomerId); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(insertOrderQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ShipAddress", shipaddress.Text); // You should get the shipping address from the user input or another source.
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);

                        int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Move items from CartItems to ItemsOrdered
                        MoveCartItemsToItemsOrdered(customerId, orderId);
                    }
                }

                // Optionally, you can display a confirmation message
                Response.Write("<script>alert('Order placed successfully');</script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookStore.aspx");
        }

        private void LoadCustomerOrderedItems(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                string selectQuery = "SELECT I.title, I.price, IO.qty, O.o_id, O.o_date " +
                                     "FROM ItemsOrdered IO " +
                                     "INNER JOIN Items I ON IO.i_id = I.i_id " +
                                     "INNER JOIN Orders O ON IO.o_id = O.o_id " +
                                     "WHERE O.c_id = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvCustomerOrderedItems.DataSource = dt;
                    gvCustomerOrderedItems.DataBind();
                }
            }
        }


        private void LoadShoppingCart()
        {
            if (Session["CustomerId"] != null)
            {
                int customerId = Convert.ToInt32(Session["CustomerId"]);

                using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    connection.Open();

                    string selectQuery = "SELECT C.cart_item_id, I.title, I.price, C.qty " +
                                         "FROM CartItems C " +
                                         "INNER JOIN Items I ON C.i_id = I.i_id " +
                                         "WHERE C.c_id = @CustomerId";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        gvCart.DataSource = dt;
                        gvCart.DataBind();
                    }
                }
            }
        }

        protected void ClearCart_Click(object sender, EventArgs e)
        {
            if (Session["CustomerId"] != null)
            {
                int customerId = Convert.ToInt32(Session["CustomerId"]);

                // Clear the user's shopping cart
                ClearShoppingCart(customerId);

                // Refresh the GridView to reflect the changes
                LoadShoppingCart();

                // Optionally, you can display a confirmation message
                Response.Write("<script>alert('Shopping cart cleared successfully');</script>");
            }
        }
    }

}
