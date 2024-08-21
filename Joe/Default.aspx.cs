using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joe
{
    public partial class _Default : Page
    {
       string AdminUsername = "admin";
       string AdminPassword = "admin123";

        protected void Page_Load(object sender, EventArgs e)
        {      
            // Your existing Page_Load logic here
        }



        protected void Login_Click(object sender, EventArgs e)
        {
            string enteredUsername = TextUser.Text.Trim();
            string enteredPassword = TextPassword.Text;

            if (ValidateUser(enteredUsername, enteredPassword))
            {
                // Valid user, proceed with user login
                int customerId = GetCustomerId(enteredUsername);
                string customerName = GetCustomerName(enteredUsername);

                Session["CustomerId"] = customerId;
                Session["CustomerName"] = customerName;
                Session["Username"] = enteredUsername;

                // Redirect to the homepage with the username in the query string
                Response.Redirect("~/MyBooks/BookStore.aspx");
            }
            else if (ValidateAdmin(enteredUsername, enteredPassword))
            {
                // Admin login
                // Redirect to admin page or perform admin-specific actions
                Response.Redirect("~/Customer.aspx");
            }
            else
            {
                // Display an error message if the login fails
                IamMessage.Text = "Invalid username or password.";
            }
        }
        private bool ValidateAdmin(string username, string password)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE admin_username = @username AND admin_password = @password", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int adminCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return adminCount > 0;
                }
            }
        }
        protected void Register_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MyBooks/RegisterForm.aspx");
        }

        private bool ValidateUser(string username, string password)
        {
            // Validate the username and password against the database
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE c_username = @username AND c_password = @password", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());

                    return userCount > 0;
                }
            }
        }
        



        private int GetCustomerId(string username)
        {
            // Retrieve the CustomerId based on the entered username
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT c_id FROM Customer WHERE c_username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    object customerId = cmd.ExecuteScalar();

                    // Check if customerId is not null before converting
                    return customerId != null ? Convert.ToInt32(customerId) : 0;
                }
            }
        }

        private string GetCustomerName(string username)
        {
            // Retrieve the CustomerName based on the entered username
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT c_name FROM Customer WHERE c_username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    object customerName = cmd.ExecuteScalar();

                    return customerName != null ? customerName.ToString() : string.Empty;
                }
            }
        }

        protected void RegisterAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MyBooks/RegisterAdmin.aspx");
        }
    }
}