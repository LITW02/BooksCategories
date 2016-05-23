using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Books.Data;
using BooksCategories.Models;

namespace BooksCategories.Controllers
{
    public class BooksController : Controller
    {
        public ActionResult Index(int? categoryId)
        {
            BooksManager manager = new BooksManager(Properties.Settings.Default.ConStr);
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.Books = manager.GetBooks(categoryId);
            viewModel.Categories = manager.GetCategories();
            viewModel.CurrentCategoryId = categoryId;
            if (TempData["message"] != null)
            {
                viewModel.Message = (string) TempData["message"];
            }
            return View(viewModel);
        }

        public ActionResult NewBook()
        {
            BooksManager manager = new BooksManager(Properties.Settings.Default.ConStr);
            NewBookViewModel viewModel = new NewBookViewModel();
            viewModel.Categories = manager.GetCategories();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddBook(string title, string author, int yearPublished, int pageCount,
            int categoryId)
        {
            BooksManager manager = new BooksManager(Properties.Settings.Default.ConStr);
            int newId = manager.AddBook(new Book
             {
                 Title = title,
                 Author = author,
                 YearPublished = yearPublished,
                 PageCount = pageCount
             }, categoryId);
            TempData["message"] = "Book added successfully, new book id: " + newId;
            return Redirect("/books/index");
        }

        public ActionResult NewCategory()
        {
            BooksManager manager = new BooksManager(Properties.Settings.Default.ConStr);
            NewCategoryViewModel viewModel = new NewCategoryViewModel();
            viewModel.CategoriesString = String.Join(",", manager.GetCategories().Select(c => c.Name));

            //NewBookViewModel viewModel = new NewBookViewModel();
            //viewModel.Categories = manager.GetCategories();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCategory(string name)
        {
            BooksManager manager = new BooksManager(Properties.Settings.Default.ConStr);
            manager.AddCategory(name);
            return Redirect("/books/index");
        }
    }
}
