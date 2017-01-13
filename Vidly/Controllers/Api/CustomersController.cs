using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            this._context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return this._context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
        }

        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = this._context.Customers.SingleOrDefault(i => i.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            this._context.Customers.Add(customer);
            this._context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = this._context.Customers.SingleOrDefault(i => i.Id == id);

            if (customerInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(customerDto, customerInDb);
            
            this._context.SaveChanges();

            return Ok(customerDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = this._context.Customers.SingleOrDefault(i => i.Id == id);

            if (customerInDb == null)
            {
                return NotFound();
            }

            this._context.Customers.Remove(customerInDb);
            this._context.SaveChanges();

            return Ok();
        }
    }
}
