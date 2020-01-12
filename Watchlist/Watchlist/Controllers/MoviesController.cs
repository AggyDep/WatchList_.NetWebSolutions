using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs.Movie;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly WatchlistContext _context;
        private readonly IMovieRepository _movieRepository;

        public MoviesController(WatchlistContext context, IMovieRepository movieRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
        }

        // GET: api/Movies
        /// <summary>
        /// Get all series and/or movies.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieGetDTO>>> GetMovies()
        {
            return Ok(await _movieRepository.GetMovies().ConfigureAwait(false));
        }

        // GET: api/Movies/5
        /// <summary>
        /// Get a specified serie of movie.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            var Movie = await _movieRepository.GetMovie(id).ConfigureAwait(false);

            if (Movie == null) return NotFound();

            return Movie;
        }

        // POST: api/Movies
        /// <summary>
        /// Create a new serie or movie.
        /// </summary>
        /// <param name="MoviePostDTO"></param>
        [HttpPost]
        public async Task<ActionResult<MoviePostDTO>> PostMovie(MoviePostDTO MoviePostDTO)
        {
            var MovieResult = await _movieRepository.PostMovie(MoviePostDTO).ConfigureAwait(false);

            return CreatedAtAction("GetMovie", new { id = MovieResult.Id }, MovieResult);
        }

        // PUT: api/Movies/5
        /// <summary>
        /// Update a specified serie or movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MovieDTO"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieDTO MovieDTO)
        {
            if (MovieDTO == null) { throw new ArgumentNullException(nameof(MovieDTO)); }

            if (id != MovieDTO.Id) return BadRequest();

            var MovieResult = await _movieRepository.PutMovie(id, MovieDTO).ConfigureAwait(false);

            if (MovieResult == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/Movies/5
        /// <summary>
        /// Delete a specified serie or movie.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieDeleteDTO>> DeleteMovie(int id)
        {
            var MovieResult = await _movieRepository.DeleteMovie(id).ConfigureAwait(false);

            if (MovieResult == null) return NotFound();

            return MovieResult;
        }
    }
}
