using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            this._context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult NewRental(NewRentalDto newRental)
        {
            var customer = this._context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);            

            var movies = this._context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();            
                                    
            foreach (var movie in movies)
            {
                if (movie.NumberAvailable <= 0)
                {
                    return BadRequest("Movie is not available");
                }

                movie.NumberAvailable--;

                var rental = new Rental()
                {
                    Movie = movie,
                    Customer = customer,
                    DateRented = DateTime.UtcNow

                };               

                this._context.Rentals.Add(rental);                
            }           

            this._context.SaveChanges();

            return Ok();
        }
    }
}
