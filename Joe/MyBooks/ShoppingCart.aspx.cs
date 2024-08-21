using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joe.MyBooks
{
    public partial class ShoppingCart : Page
    {
        // Your connection string
        string mycon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
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

        private void MoveCartItemsToOrderItems(int customerId, int orderId)
        {
            using (SqlConnection connection = new SqlConnection(mycon))
            {
                connection.Open();

                // Insert items from CartItems to OrderItems
                string insertOrderItemsQuery = "INSERT INTO ItemsOrdered (o_id, i_id, qty) " +
                                               "SELECT @OrderId, i_id, qty " +
                                               "FROM CartItems " +
                                               "WHERE c_id = @CustomerId";

                using (SqlCommand cmd = new SqlCommand(insertOrderItemsQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    cmd.ExecuteNonQuery();
                }

                // Optionally, you can update the quantity of items in the ItemsOrdered table
                UpdateItemsOrderedQuantity(connection, orderId);

                // Update the stock quantity in the Items table
                UpdateItemsStockQuantity(connection);

                // Clear the user's shopping cart
                ClearShoppingCart(customerId);

                // Refresh the GridView to reflect the changes
                LoadShoppingCart();
            }
        }


        private void UpdateItemsStockQuantity(SqlConnection connection)
        {
            // Update the stock quantity in the ItemStock table
            string updateStockQuantityQuery = "UPDATE ItemStock " +
                                              "SET quantity = ItemStock.quantity - CartItems.qty " +
                                              "FROM ItemStock " +
                                              "INNER JOIN CartItems ON ItemStock.i_id = CartItems.i_id";

            using (SqlCommand cmd = new SqlCommand(updateStockQuantityQuery, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }




        private void UpdateItemsOrderedQuantity(SqlConnection connection, int orderId)
        {
            // Update the quantity of items in the ItemsOrdered table
            string updateItemsOrderedQuery = "UPDATE ItemsOrdered SET qty = C.qty " +
                                             "FROM ItemsOrdered IO " +
                                             "INNER JOIN CartItems C ON IO.i_id = C.i_id AND IO.o_id = @OrderId";

            using (SqlCommand cmd = new SqlCommand(updateItemsOrderedQuery, connection))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                cmd.ExecuteNonQuery();
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

        protected void Order_Click(object sender, EventArgs e)
        {
            if (Session["CustomerId"] != null)
            {
                int customerId = Convert.ToInt32(Session["CustomerId"]);

                using (SqlConnection connection = new SqlConnection(mycon))
                {
                    connection.Open();

                    // Insert a new order record
                    string insertOrderQuery = "INSERT INTO Orders (o_date, ship_address, c_id) VALUES (GETDATE(), @ShipAddress, @CustomerId); SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(insertOrderQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ShipAddress", shipaddress.Text);
                        cmd.Parameters.AddWithValue("@CustomerId", customerId);

                        int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Move items from CartItems to OrderItems
                        MoveCartItemsToOrderItems(customerId, orderId);

                        // Optionally, you can display a confirmation message
                        Response.Write("<script>alert('Order placed successfully');</script>");

                        // Refresh the ordered items GridView
                        LoadCustomerOrderedItems(customerId);
                    }
                }
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

                using (SqlConnection connection = new SqlConnection(mycon))
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
        


        protected void gvCart_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (gvCart.SelectedIndex >= 0)
            {
                // Get the selected row data
                GridViewRow selectedRow = gvCart.Rows[gvCart.SelectedIndex];
                string title = selectedRow.Cells[0].Text; // Assuming the title is in the first column

                // Perform custom logic based on the selected row data
                // For example, you can display a message with the selected title
                string message = $"You selected the item: {title}";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{message}');", true);
            }
            else
            {
                // No row is selected, you can handle this case as needed
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No item selected.');", true);
            }
        }
        protected void gvCustomerOrderedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Your event handling logic
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