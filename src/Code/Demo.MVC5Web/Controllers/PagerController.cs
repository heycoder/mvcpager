using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demo.MVC5Web.Models;

namespace Demo.MVC5Web.Controllers
{
    public class PagerController : Controller
    {
        public ActionResult Index(PagerModel model)
        {
            return View(model);
        }

        public ActionResult BootStrap(PagerModel model)
        {
            return View(model);
        }
    }
}