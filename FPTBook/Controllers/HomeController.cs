using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var books = _db.Books.ToList();
            return View(books);
        }
    }
}