using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
                        
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        public ActionResult Index()
        {
            var movies = new List<Movie>
            {
                new Movie { Name = "Wall-E", Id = 1 },
                new Movie { Name = "Shrek", Id = 2 }
            };

            var viewModel = new MovieList
            {
                Movies = movies
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var movies = new List<Movie>
            {
                new Movie { Name = "Wall-E", Id = 1 },
                new Movie { Name = "Shrek", Id = 2 }
            };

            Movie displayedMovie = null;

            foreach (var movie in movies)
            {
                if (movie.Id == id)
                {
                    displayedMovie = movie;
                }
            }

            if (displayedMovie == null)
            {
                return HttpNotFound("No movie with that Id");
            }
            else
            {
                return View(displayedMovie);
            }

        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}