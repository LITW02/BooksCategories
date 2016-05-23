using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Books.Data
{
    public class BooksManager
    {
        private string _connectionString;

        public BooksManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Book> GetBooks(int? categoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT b.*, c.Name FROM Books B JOIN Categories c ON c.Id = b.CategoryId";
                if (categoryId != null)
                {
                    command.CommandText += " WHERE c.Id = @categoryId";
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                }

                List<Book> books = new List<Book>();
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = (int)reader["Id"];
                    book.Title = (string)reader["Title"];
                    book.PageCount = (int)reader["PageCount"];
                    book.Author = (string)reader["Author"];
                    book.YearPublished = (int)reader["YearPublished"];
                    book.CategoryName = (string)reader["Name"];
                    books.Add(book);
                }

                return books;
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Categories";
                connection.Open();
                List<Category> categories = new List<Category>();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"]
                    });
                }

                return categories;
            }
        }

        public int AddBook(Book book, int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Books (Title, Author, PageCount, YearPublished, CategoryId) " +
                                      "VALUES (@title, @author, @pageCount, @yearPublished, @categoryId); SELECT @@Identity";
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@pageCount", book.PageCount);
                command.Parameters.AddWithValue("@yearPublished", book.YearPublished);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                connection.Open();
                return (int)(decimal)command.ExecuteScalar();
            }
        }

        public void AddCategory(string name)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Categories (Name) " +
                                      "VALUES (@name)";
                command.Parameters.AddWithValue("@name", name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
