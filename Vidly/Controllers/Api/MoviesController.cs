﻿using System;
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
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            this._context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = this._context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }            

            var movieDtos = moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = this._context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            movie.DateAdded = DateTime.UtcNow;

            this._context.Movies.Add(movie);
            this._context.SaveChanges();

            movieDto.Id = movie.Id;
            movieDto.DateAdded = movie.DateAdded;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }
        
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = this._context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            Mapper.Map(movieDto, movie);

            this._context.SaveChanges();

            return Ok(movieDto);
        }
                
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            var movie = this._context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            this._context.Movies.Remove(movie);
            this._context.SaveChanges();

            return Ok();
        }
    }
}
