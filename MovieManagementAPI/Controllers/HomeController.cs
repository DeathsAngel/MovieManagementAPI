using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementAPI.Models;
using System.Linq;
using System;

namespace MovieManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        readonly MovieDataContext _movieDataContext;

        public HomeController(MovieDataContext movieDataContext)
        {
            _movieDataContext = movieDataContext;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(_movieDataContext.Movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            try
            {
                return Ok(_movieDataContext.Movies.Find(id));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost]
        public IActionResult Create(Movies movie)
        {
            try
            {
                _movieDataContext.Add(movie);

                _movieDataContext.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        public IActionResult Edit(Movies movie)
        {
            try
            {
                _movieDataContext.Movies.Update(movie);

                _movieDataContext.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete]
        public IActionResult Delete(Movies movie)
        {
            try
            {
                _movieDataContext.Movies.Remove(movie);

                _movieDataContext.SaveChanges();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
