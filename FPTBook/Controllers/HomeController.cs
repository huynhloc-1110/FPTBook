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
            var books = from b in db.Books
                        select b;
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                books = books.Where(b => b.Name.Contains(search));
            }

            return View(books.OrderByDescending(b => b.CreatedDateTime).ToList());
        }
    }
}