using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Joe
{
    public partial class Contact : Page
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getItems();
            }
        }

        private void ResetStock(int resetValue)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;"))
            {
                connection.Open();

                // Update the stock quantity in the ItemStock table for all items
                string resetStockQuery = "UPDATE ItemStock SET quantity = @ResetValue";

                using (SqlCommand cmd = new SqlCommand(resetStockQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@ResetValue", resetValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        protected void btnResetStock_Click(object sender, EventArgs e)
        {
            int resetValue = 100; // Set the desired reset value

            // Call the ResetStock method to update the stock in the database
            ResetStock(resetValue);

            // Optionally, you can rebind the GridView or perform other actions after resetting the stock
            getItems();

            // Display a confirmation message
            Response.Write("<script>alert('Stock reset successfully');</script>");
        }
        protected void AddBook_Click(object sender, EventArgs e)
        {
            
                con.Open();
            if (int.TryParse(TextISBN.Text, out int temp))
            {
                cmd = new SqlCommand("Select count(*) from items where isbn = @isbnC", con);
                cmd.Parameters.AddWithValue("@isbnC", temp);
                temp = Convert.ToInt32(cmd.ExecuteScalar());

            }
            else if (TextISBN.Text.Trim() == "") {
                Response.Write("<script>alert('Fill all fields');</script>");
                return;
            }
            else
            {
                Response.Write("<script>alert('ISBN can only be number');</script>");
                return;
            }
                if (!TextEmpty() && (temp == 0))
                {
                    if (decimal.TryParse(TextPrice.Text, out decimal price))
                    {
                        cmd = new SqlCommand("AddItem", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@isbn", int.Parse(TextISBN.Text));
                        cmd.Parameters.AddWithValue("@title", TextTitle.Text);
                        cmd.Parameters.AddWithValue("@author", TextAuthor.Text);
                        cmd.Parameters.AddWithValue("@genre", TextGenre.Text);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@i_Type", TextBookType.Text);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        Clear();
                        getItems();
                        Response.Write("<script>alert('Successfully');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Price Can Only be number');</script>");
                    }
                }
                else
                {
                    if (temp == 1)
                    {
                        Response.Write("<script>alert('ISBN already exist');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Fill all fields');</script>");

                    }
                }
   
        }

        protected void UpdateBook_Click(object sender, EventArgs e)
        {
            int selectedIndex = GridViewBooks.SelectedIndex;

            if (selectedIndex >= 0)
            {
                con.Open();

                if (int.TryParse(TextISBN.Text, out int temp))
                {
                    // Check if the entered ISBN is already in use by other records
                    cmd = new SqlCommand("SELECT COUNT(*) FROM items WHERE isbn = @isbnC AND i_id != @selectedItemId", con);
                    cmd.Parameters.AddWithValue("@isbnC", temp);
                    cmd.Parameters.AddWithValue("@selectedItemId", int.Parse(GridViewBooks.SelectedRow.Cells[1].Text));
                    temp = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    Response.Write("<script>alert('ISBN can only be a number');</script>");
                    return;
                }

                if (!TextEmpty() && (temp == 0))
                {
                    // Proceed with the update since the ISBN is either unique or unchanged
                    if (decimal.TryParse(TextPrice.Text, out decimal price))
                    {
                        cmd = new SqlCommand("UpdateItem", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@i_id", int.Parse(GridViewBooks.SelectedRow.Cells[1].Text));
                        cmd.Parameters.AddWithValue("@isbn", int.Parse(TextISBN.Text));
                        cmd.Parameters.AddWithValue("@title", TextTitle.Text);
                        cmd.Parameters.AddWithValue("@author", TextAuthor.Text);
                        cmd.Parameters.AddWithValue("@genre", TextGenre.Text);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@i_Type", TextBookType.Text);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        Clear();
                        getItems();
                        Response.Write("<script>alert('Updated Successfully');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Price can only be a number');</script>");
                    }
                }
                else
                {
                    if (temp == 1)
                    {
                        Response.Write("<script>alert('ISBN already exists');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Fill all fields');</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a customer to update');</script>");
            }
        }

        protected void DeleteBook_Click(object sender, EventArgs e)
        {
            int selectedIndex = GridViewBooks.SelectedIndex;

            if (selectedIndex >= 0)
            {
                try
                {
                    int bookIdToDelete;

                    // Retrieve the book ID from the DataKeys collection
                    if (int.TryParse(GridViewBooks.DataKeys[selectedIndex].Value.ToString(), out bookIdToDelete))
                    {
                        using (SqlConnection deleteCon = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;"))
                        {
                            deleteCon.Open();

                            using (SqlCommand cmd = new SqlCommand("DeleteItem", deleteCon))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@i_id", bookIdToDelete);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        Clear();       // Clear the form after deletion if needed
                        getItems();    // Reload the GridView after deletion if needed

                        Response.Write("<script>alert('Book deleted successfully');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid book ID');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('Error deleting book: {ex.Message}');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a book to delete');</script>");
            }
        }



        protected void CancelBook_Click(object sender, EventArgs e)
        {
            Clear();
            GridViewBooks.SelectedIndex = -1;
        }

        protected void SearchCustomer_Click(object sender, EventArgs e)
        {
            if (TextSearch.Text != String.Empty)
            {

                con.Open();
                cmd = new SqlCommand("SearchItem",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@searchTerm",TextSearch.Text);
                cmd.ExecuteNonQuery();

                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                GridViewBooks.DataSource = dt;
                GridViewBooks.DataBind();

                TextSearch.Text = String.Empty;
            }
            else
            {

                Response.Write("<script>alert('Fill Search');</script>");

            }
        }

       private void Clear() {
            TextISBN.Text = String.Empty;
            TextTitle.Text = String.Empty;
            TextAuthor.Text = String.Empty;
            TextGenre.Text = String.Empty;
            TextPrice.Text = String.Empty;
            TextBookType.Text = String.Empty;
        }

        private void getItems()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;Connect Timeout=30;"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Items", connection))
                    {
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            dataAdapter.Fill(dt);
                            GridViewBooks.DataSource = dt;
                            GridViewBooks.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display, etc.)
                // For now, we'll just print the exception message to the console
                Console.WriteLine("Error in getItems(): " + ex.Message);
            }
        }


        public Boolean TextEmpty() {
            if (TextISBN.Text == String.Empty || TextTitle.Text == String.Empty || TextAuthor.Text == String.Empty || TextGenre.Text == String.Empty || TextPrice.Text == String.Empty || TextBookType.Text == String.Empty) { return true; }
            else { return false; }
        }

        protected void GridViewBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GridViewBooks.SelectedIndex >= 0 && GridViewBooks.SelectedIndex < GridViewBooks.Rows.Count)
            {
                GridViewRow selectedRow = GridViewBooks.Rows[GridViewBooks.SelectedIndex];
                if (selectedRow != null && selectedRow.Cells.Count >= 7)
                {
                    TextISBN.Text = selectedRow.Cells[2].Text;
                    TextTitle.Text = selectedRow.Cells[3].Text;
                    TextAuthor.Text = selectedRow.Cells[4].Text;
                    TextGenre.Text = selectedRow.Cells[5].Text;
                    TextPrice.Text = selectedRow.Cells[6].Text;
                    TextBookType.Text = selectedRow.Cells[7].Text;
                }
                else
                {
                    ClearTextBoxes();
                }
            }
            else
            {
                ClearTextBoxes();
            }
        }

        private void ClearTextBoxes()
        {
            TextISBN.Text = string.Empty;
            TextTitle.Text = string.Empty;
            TextAuthor.Text = string.Empty;
            TextGenre.Text = string.Empty;
            TextPrice.Text = string.Empty;
            TextBookType.Text = string.Empty;
        }



        protected void RedirectToCustomer_Click(object sender, EventArgs e)
        {  
                Response.Redirect("Customer.aspx");
        }
    }
}