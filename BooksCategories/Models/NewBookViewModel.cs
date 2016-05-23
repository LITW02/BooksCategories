using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Books.Data;

namespace BooksCategories.Models
{
    public class NewBookViewModel
    {
        public IEnumerable<Category> Categories { get; set; } 
    }
}