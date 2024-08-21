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

namespace Joe
{
    public partial class Contact : Page
    {
        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                getItems();
            }
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
                int SelectedIndex = GridViewBooks.SelectedRow.RowIndex;
                if (SelectedIndex >= 0)
                {
                    con.Open();
                    cmd = new SqlCommand("DeleteItem", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@i_id", int.Parse(GridViewBooks.SelectedRow.Cells[1].Text));

                    cmd.ExecuteNonQuery();
                    con.Close();

                    getItems();

                    Response.Write("<script>alert('Successfully Deleted');</script>");

                }
                else
                {
                    Response.Write("<script>alert('Please select.');</script>");

                }
            }
            else {
                Response.Write("<script>alert('Please select.');</script>");
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

        void Clear() {
            TextISBN.Text = String.Empty;
            TextTitle.Text = String.Empty;
            TextAuthor.Text = String.Empty;
            TextGenre.Text = String.Empty;
            TextPrice.Text = String.Empty;
            TextBookType.Text = String.Empty;
        }

        void getItems() {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT * FROM Items", con);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GridViewBooks.DataSource = dt;
                GridViewBooks.DataBind();
            }
            catch (Exception ex)
            {
                // Handle the exception (log, display, etc.)
                // For now, we'll just print the exception message to the console
                Console.WriteLine("Error in getItems(): " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public Boolean TextEmpty() {
            if (TextISBN.Text == String.Empty || TextTitle.Text == String.Empty || TextAuthor.Text == String.Empty || TextGenre.Text == String.Empty || TextPrice.Text == String.Empty || TextBookType.Text == String.Empty) { return true; }
            else { return false; }
        }

        protected void GridViewBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextISBN.Text = GridViewBooks.SelectedRow.Cells[2].Text;
            TextTitle.Text = GridViewBooks.SelectedRow.Cells[3].Text;
            TextAuthor.Text = GridViewBooks.SelectedRow.Cells[4].Text;
            TextGenre.Text = GridViewBooks.SelectedRow.Cells[5].Text;
            TextPrice.Text = GridViewBooks.SelectedRow.Cells[6].Text;
            TextBookType.Text = GridViewBooks.SelectedRow.Cells[7].Text;
        }
    }
}