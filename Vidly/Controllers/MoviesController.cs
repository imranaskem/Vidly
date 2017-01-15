using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            this._context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies        
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }
            else
            {
                return View("ReadOnlyList");
            }
        }

        public ActionResult Details(int id)
        {
            var displayedMovie = this._context.Movies.Include(g => g.Genre).SingleOrDefault(i => i.Id == id);            

            if (displayedMovie == null)
            {
                return HttpNotFound("No movie with that Id");
            }
            else
            {
                return View(displayedMovie);
            }
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Movie = new Movie(),
                Genres = this._context.Genres.ToList()
            };
            
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = new Movie(),
                    Genres = this._context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.UtcNow;
                this._context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = this._context.Movies.Single(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            this._context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = this._context.Movies.Include(g => g.Genre).SingleOrDefault(i => i.Id == id);

            if (movie == null)
            {
                return HttpNotFound("Movie with that Id does not exist");
            }

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = this._context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }        
    }
}