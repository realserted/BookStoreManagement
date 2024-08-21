using System;
using System.Data.SqlClient;

namespace Joe.MyBooks
{
    public partial class RegisterAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAdminRegister_Click(object sender, EventArgs e)
        {
            // Get admin input from the registration form
            string adminUsername = NewAdminUsername.Text.Trim();
            string adminPassword = NewAdminPassword.Text;

            // Validate admin input (you can add more validation as needed)
            if (string.IsNullOrEmpty(adminUsername) || string.IsNullOrEmpty(adminPassword))
            {
                // Display an error message if any required field is empty
                JustALabel.Text = "All fields are required.";
                return;
            }

            if (IsAdminUsernameExists(adminUsername))
            {
                // Display an error message if the admin username already exists
                JustALabel.Text = "Admin username already exists. Please choose a different username.";
                return;
            }

            try
            {
                // Insert admin registration data into the database
                using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Admin (admin_username, admin_password) VALUES (@adminUsername, @adminPassword)", con))
                    {
                        cmd.Parameters.AddWithValue("@adminUsername", adminUsername);
                        cmd.Parameters.AddWithValue("@adminPassword", adminPassword);

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
                JustALabel.Text = $"Admin registration failed: {ex.Message}";
            }
        }

        private bool IsAdminUsernameExists(string adminUsername)
        {
            // Check if the admin username already exists in the database
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Admin WHERE admin_username = @adminUsername", con))
                {
                    cmd.Parameters.AddWithValue("@adminUsername", adminUsername);
                    int existingAdminUsernameCount = (int)cmd.ExecuteScalar();
                    return existingAdminUsernameCount > 0;
                }
            }
        }
    }
}
