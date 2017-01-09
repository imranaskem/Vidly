using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "John Williams", Id = 1 },
                new Customer { Name = "Hans Zimmer", Id = 2 }
            };

            var viewModel = new CustomerList { Customers = customers };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "John Williams", Id = 1 },
                new Customer { Name = "Hans Zimmer", Id = 2 }
            };

            Customer displayedCustomer = null;

            foreach (var customer in customers)
            {
                if (customer.Id == id)
                {
                    displayedCustomer = customer;
                }
            }

            if (displayedCustomer == null)
            {
                return HttpNotFound("Customer with that Id doesn't exist!");
            }
            else
            {
                return View(displayedCustomer);
            } 

        }
    }
}