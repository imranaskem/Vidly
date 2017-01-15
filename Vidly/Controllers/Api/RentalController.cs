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
    public class RentalController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalController()
        {
            this._context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetRentals()
        {
            var rentals = this._context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .Where(r => r.DateReturned == null);

            var rentalDtos = rentals.ToList().Select(Mapper.Map<Rental, RentalDto>);

            return Ok(rentalDtos);
        }

        [HttpPost]
        public IHttpActionResult ReturnRental(int id)
        {
            var rental = this._context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .SingleOrDefault(r => r.Id == id);

            if (rental == null)
            {
                return BadRequest("Rental not found");
            }

            rental.DateReturned = DateTime.UtcNow.Date;

            this._context.SaveChanges();

            return Ok();
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
                    DateRented = DateTime.UtcNow.Date

                };               

                this._context.Rentals.Add(rental);                
            }           

            this._context.SaveChanges();

            return Ok();
        }
    }
}
