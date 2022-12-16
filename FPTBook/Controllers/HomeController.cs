using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string search)
        {
            List<Book> books;
            if (string.IsNullOrEmpty(search))
            {
                books = db.Books.OrderBy(b => b.CreatedDateTime).ToList();
                return View(books);
            }

            books = db.Books
                .Where(b => b.Name.Contains(search))
                .OrderBy(b => b.CreatedDateTime)
                .ToList();
            return View(books);
        }
    }
}