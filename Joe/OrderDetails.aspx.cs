using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Joe.MyBooks
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        string mycon = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the customer ID from the query string
                string customerId = Request.QueryString["customerId"];

                if (!string.IsNullOrEmpty(customerId))
                {
                    // Load and display itemsOrdered details for the selected customer
                    LoadOrderDetails(customerId);
                }
                else
                {
                    // Handle the case when the customer ID is not provided
                    // You may redirect the user or display an error message
                }
            }
        }

        private void LoadOrderDetails(string customerId)
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

                    GridViewOrderDetails.DataSource = dt;
                    GridViewOrderDetails.DataBind();
                }
            }
        }
        protected void gvCustomerOrderedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Your event handling logic for selected index change in GridViewOrderDetails
        }

        protected void RedirectToCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customer.aspx");
        }
    }
}
