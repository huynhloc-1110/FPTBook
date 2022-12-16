using FPTBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBook.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext _db = new ApplicationDbContext();
    }
}