using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Joe.MyBooks
{
    public partial class RegisterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCustomerRegister_Click(object sender, EventArgs e)
        {
            // Get user input from the registration form
            string name = RegisterName.Text.Trim();
            string email = RegisterEmail.Text.Trim();
            string address = RegisterAddress.Text.Trim();
            string username = RegisterUsername.Text.Trim();
            string password = RegisterPassword.Text;
            string confirmPassword = RegisterConfirmPassword.Text;

            // Validate user input (you can add more validation as needed)
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                // Display an error message if any required field is empty
                JustALabel.Text = "All fields are required.";
                return;
            }

            if (password != confirmPassword)
            {
                // Display an error message if passwords do not match
                JustALabel.Text = "Passwords do not match.";
                return;
            }

            if (IsUsernameExists(username))
            {
                // Display an error message if the username already exists
                JustALabel.Text = "Username already exists. Please choose a different username.";
                return;
            }

            try
            {
                // Insert user registration data into the database
                using (SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BookStore; Integrated Security = True; Connect Timeout = 30; Encrypt = False;"))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("InsertCustomer", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@c_name", name);
                        cmd.Parameters.AddWithValue("@c_email", email);
                        cmd.Parameters.AddWithValue("@c_address", address);
                        cmd.Parameters.AddWithValue("@c_username", username);
                        cmd.Parameters.AddWithValue("@c_password", password);

                        cmd.ExecuteNonQuery();
                    }

                    con.Close();

                    // Redirect to a success page or display a success message
                    Response.Redirect("/Default.aspx");
                }
            }
            catch (Exception ex)
            {
                // Display an error message if registration fails
                JustALabel.Text = $"Registration failed: {ex.Message}";
            }
        }

        private bool IsUsernameExists(string username)
        {
            // Check if the username already exists in the database
            using (SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BookStore; Integrated Security = True; Connect Timeout = 30; Encrypt = False; "))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE c_username = @username", con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    int existingUsernameCount = Convert.ToInt32(cmd.ExecuteScalar());
                    return existingUsernameCount > 0;
                }
            }
        }
    }
}