﻿using FPTBook.Models;
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
                books = _db.Books.ToList();
                return View(books);
            }

            books = _db.Books.Where(b => b.Name.Contains(search)).ToList();
            return View(books);
        }
    }
}