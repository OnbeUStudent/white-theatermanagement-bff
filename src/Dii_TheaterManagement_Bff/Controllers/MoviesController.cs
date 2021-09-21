using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dii_TheaterManagement_Bff.Clients;
using Dii_TheaterManagement_Bff.Features.SyntheticBehavior;
using System;

namespace Dii_TheaterManagement_Bff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly OrderingSvcClient _orderingSvcClient;
        private readonly MovieCatalogSvcClient movieCatalogSvcClient;

        public MoviesController(OrderingSvcClient orderingSvcClient, MovieCatalogSvcClient movieCatalogSvcClient)
        {
            _orderingSvcClient = orderingSvcClient;
            this.movieCatalogSvcClient = movieCatalogSvcClient;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var str = await movieCatalogSvcClient.GetMovies();
            var movies = await _orderingSvcClient.ApiMoviesGetAsync();
            return movies.ToList();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(Guid id)
        {
            return await _orderingSvcClient.ApiMoviesGetAsync(id);
        }
    }
}
