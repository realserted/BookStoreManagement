using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Joe
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomers();
            } 
        }

        protected void SearchCustomer_Click(object sender, EventArgs e)
        {
            string searchTerm = TextSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                    {
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand("SearchCustomer", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@searchTerm", searchTerm);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);

                                GridViewCustomers.DataSource = dt;
                                GridViewCustomers.DataBind();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error searching for customers: {ex.Message}');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Enter a search term');</script>");
            }
        }

        protected void AddCustomer_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                con.Open();

                // Check if the username already exists
                using (SqlCommand cmdUsernameCheck = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE c_username = @username", con))
                {
                    cmdUsernameCheck.Parameters.AddWithValue("@username", TextUsername.Text);
                    int existingUsernameCount = Convert.ToInt32(cmdUsernameCheck.ExecuteScalar());

                    if (existingUsernameCount > 0)
                    {
                        Response.Write("<script>alert('Username already exists');</script>");
                        return;
                    }
                }

                if (!TextEmpty())
                {
                    using (SqlCommand cmd = new SqlCommand("InsertCustomer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@c_name", TextName.Text);
                        cmd.Parameters.AddWithValue("@c_email", TextEmail.Text);
                        cmd.Parameters.AddWithValue("@c_address", TextAddress.Text);
                        cmd.Parameters.AddWithValue("@c_username", TextUsername.Text);
                        cmd.Parameters.AddWithValue("@c_password", TextPassword.Text);

                        cmd.ExecuteNonQuery();
                    }

                    con.Close();

                    ClearForm();
                    LoadCustomers();
                    Response.Write("<script>alert('Customer added successfully');</script>");
                }
                else
                {
                        Response.Write("<script>alert('Fill all fields');</script>"); 
                }
            }
        }

        protected void UpdateCustomer_Click(object sender, EventArgs e)
        {
            int selectedIndex = GridViewCustomers.SelectedIndex;

            if (selectedIndex >= 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                    {
                        con.Open();

                        GridViewRow selectedRow = GridViewCustomers.SelectedRow;

                        // Assuming the customer ID is stored in the first column (index 0) of the GridView
                        if (int.TryParse(selectedRow.Cells[1].Text, out int customerIdToUpdate))
                        {
                            using (SqlCommand cmd = new SqlCommand("UpdateCustomer", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@c_id", customerIdToUpdate);
                                cmd.Parameters.AddWithValue("@c_name", TextName.Text);
                                cmd.Parameters.AddWithValue("@c_email", TextEmail.Text);
                                cmd.Parameters.AddWithValue("@c_address", TextAddress.Text);
                                cmd.Parameters.AddWithValue("@c_username", TextUsername.Text);
                                cmd.Parameters.AddWithValue("@c_password", TextPassword.Text);

                                cmd.ExecuteNonQuery();
                            }

                            con.Close();

                            ClearForm();  // Clear the form after update if needed
                            LoadCustomers();  // Reload the GridView after update if needed

                            Response.Write("<script>alert('Customer updated successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid customer ID');</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error updating customer: {ex.Message}');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a customer to update');</script>");
            }
        }

        protected void DeleteCustomer_Click(object sender, EventArgs e)
        {
            int selectedIndex = GridViewCustomers.SelectedIndex;

            if (selectedIndex >= 0)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
                    {
                        con.Open();

                        GridViewRow selectedRow = GridViewCustomers.SelectedRow;

                        // Assuming the customer ID is stored in the first column (index 0) of the GridView
                        if (int.TryParse(selectedRow.Cells[1].Text, out int customerIdToDelete))
                        {
                            using (SqlCommand cmd = new SqlCommand("DeleteCustomer", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@c_id", customerIdToDelete);

                                cmd.ExecuteNonQuery();
                            }

                            con.Close();

                            ClearForm();  // Clear the form after deletion if needed
                            LoadCustomers();  // Reload the GridView after deletion if needed

                            Response.Write("<script>alert('Customer deleted successfully');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid customer ID');</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error deleting customer: {ex.Message}');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a customer to delete');</script>");
            }
        }

        protected void CancelCustomer_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadCustomers();
            GridViewCustomers.SelectedIndex = -1;
        }

        private void ClearForm()
        {
            TextName.Text = string.Empty;
            TextEmail.Text = string.Empty;
            TextAddress.Text = string.Empty;
            TextUsername.Text = string.Empty;
            TextPassword.Text = string.Empty;
        }

        private void LoadCustomers()
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", con))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        GridViewCustomers.DataSource = dt;
                        GridViewCustomers.DataBind();
                    }
                }
            }
        }

        private bool TextEmpty()
        {
            return string.IsNullOrEmpty(TextName.Text) ||
                   string.IsNullOrEmpty(TextEmail.Text) ||
                   string.IsNullOrEmpty(TextAddress.Text) ||
                   string.IsNullOrEmpty(TextUsername.Text) ||
                   string.IsNullOrEmpty(TextPassword.Text);
        }

        protected void GridViewCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridViewCustomers.SelectedIndex >= 0)
            {
                GridViewRow selectedRow = GridViewCustomers.SelectedRow;

                // Assuming the column indices based on your GridView structure
                TextName.Text = selectedRow.Cells[2].Text;
                TextEmail.Text = selectedRow.Cells[3].Text;
                TextAddress.Text = selectedRow.Cells[4].Text;
                TextUsername.Text = selectedRow.Cells[5].Text;
                TextPassword.Text = selectedRow.Cells[6].Text;
                // Add more lines as needed for other columns

                // You can enable/disable or show/hide certain controls based on your requirements
                // For example, you might want to enable an "Update" button or show a message
            }
        }
    }
}