using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Books.Data;

namespace BooksCategories.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public int? CurrentCategoryId { get; set; }
        public string Message { get; set; }
    }
}