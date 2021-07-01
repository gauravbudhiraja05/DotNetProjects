using BooksAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace BooksAppAPI.Controllers
{
    
    public class BooksController : ApiController
    {
        const string sConnString = @"Data Source=DESKTOP-V32N4KG\SQLEXPRESS;Persist Security Info=False;" +
            "Initial Catalog=BooksDb;User Id=sa;Password=1234;Connect Timeout=30;";

        // LIST OBJECT WILL HOLD AND RETURN A LIST OF BOOKS.
        List<Books> MyBooks = new List<Books>();

        [HttpPost()]
        public IEnumerable<Books> Perform_CRUD(Books list)
        {
            bool bDone = false;

            using (SqlConnection con = new SqlConnection(sConnString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *FROM dbo.Books"))
                {
                    cmd.Connection = con;
                    con.Open();

                    switch (list.Operation)
                    {
                        case "READ":
                            bDone = true;
                            break;

                        case "SAVE":

                            if (list.BookName != "" & list.Category != "" & list.Price > 0)
                            {
                                cmd.CommandText = "INSERT INTO dbo.Books (BookName, Category, Price) " +
                                    "VALUES (@BookName, @Category, @Price)";

                                cmd.Parameters.AddWithValue("@BookName", list.BookName.Trim());
                                cmd.Parameters.AddWithValue("@Category", list.Category.Trim());
                                cmd.Parameters.AddWithValue("@Price", list.Price);

                                bDone = true;
                            }

                            break;
                        case "UPDATE":

                            if (list.BookName != "" & list.Category != "" & list.Price > 0)
                            {
                                cmd.CommandText = "UPDATE dbo.Books SET BookName = @BookName, Category = @Category, " + "Price = @Price WHERE BookID = @BookID";

                                cmd.Parameters.AddWithValue("@BookName", list.BookName.Trim());
                                cmd.Parameters.AddWithValue("@Category", list.Category.Trim());
                                cmd.Parameters.AddWithValue("@Price", list.Price);
                                cmd.Parameters.AddWithValue("@BookID", list.BookID);

                                bDone = true;
                            }

                            break;
                        case "DELETE":
                            cmd.CommandText = "DELETE FROM dbo.Books WHERE BookID = @BookID";
                            cmd.Parameters.AddWithValue("@BookID", list.BookID);

                            bDone = true;
                            break;
                    }

                    if (bDone)
                    {
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }

            if (bDone)
            {
                GetData();
            }
            return MyBooks;             // RETURN THE LIST TO THE CLIENT APP.
        }

        private void GetData()
        {
            using (SqlConnection con = new SqlConnection(sConnString))
            {
                SqlCommand objComm = new SqlCommand("SELECT *FROM dbo.Books", con);
                con.Open();

                SqlDataReader reader = objComm.ExecuteReader();

                // POPULATE THE LIST WITH DATA.
                while (reader.Read())
                {
                    MyBooks.Add(new Books
                    {
                        BookID = Convert.ToInt32(reader["BookID"]),
                        BookName = reader["BookName"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    });
                }

                con.Close();
            }
        }
    }
}