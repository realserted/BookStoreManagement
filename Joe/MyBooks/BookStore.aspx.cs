using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Joe.MyBooks
{
    public partial class BookStore : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        SqlCommand cmd; 
        SqlDataAdapter adapter;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CustomerId"] == null)
            {
                // User is not authenticated, redirect to the login page
                Response.Redirect("/Default.aspx");
            }
            else
            {
                // User is authenticated, display user information
                int customerId = Convert.ToInt32(Session["CustomerId"]);
                string username = Session["Username"].ToString();
                string name = Session["customerName"].ToString();

                // Display user information on the page
                IamName.Text = "Welcome, " + name;
            }

            if (!IsPostBack)
            {
                DisplayBooks();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Sign out the user
            FormsAuthentication.SignOut();
            // Clear session data
            Session.Clear();

            // Abandon the session
            Session.Abandon();

            // Redirect to the login page
            Response.Redirect("~/Default.aspx");
        }

        void DisplayBooks()
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT i.*, ISNULL(s.quantity, 0) AS StockQuantity FROM Items i LEFT JOIN ItemStock s ON i.i_id = s.i_id", con);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                gvBooks.DataSource = dt;
                gvBooks.DataBind();
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display, etc.)
                // For now, we'll just print the exception message to the console
                Console.WriteLine("Error in DisplayBooks(): " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }


        protected void Cart_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShoppingCart.aspx");
        }

        protected void gvBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                // Extract the book ID from the clicked row
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int bookId = Convert.ToInt32(gvBooks.DataKeys[rowIndex].Value);

                // Redirect to BookDetails.aspx with the book ID as a query parameter
                Response.Redirect($"BookDetails.aspx?id={bookId}");
            }
        }

        protected void gvBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBooks.PageIndex = e.NewPageIndex;
            DisplayBooks();
        }
    }

}