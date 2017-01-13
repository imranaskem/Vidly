using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            this._context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = this._context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = this._context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                this._context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = this._context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.DateOfBirth = customer.DateOfBirth;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            this._context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var displayedCustomer = this._context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);                       

            if (displayedCustomer == null)
            {
                return HttpNotFound("Customer with that Id doesn't exist!");
            }
            else
            {
                return View(displayedCustomer);
            } 

        }

        public ActionResult Edit(int id)
        {
            var customer = this._context.Customers.SingleOrDefault(i => i.Id == id);

            if (customer == null)
            {
                return HttpNotFound("No customer matching that id");
            }
            else
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = this._context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
        }
    }
}