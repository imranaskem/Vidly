using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Controllers
{
    public class RentalsController : Controller
    {
        public ActionResult Index()
        {
            return View("List");
        }

        public ActionResult New()
        {
            return View("NewRentalForm");
        }
    }
}