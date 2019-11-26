using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.DTOs.SerieMovie;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SerieMoviesController : ControllerBase
    {
        private readonly WatchListContext _context;
        private readonly ISerieMovieRepository _serieMovieRepository;

        public SerieMoviesController(WatchListContext context, ISerieMovieRepository serieMovieRepository)
        {
            _context = context;
            _serieMovieRepository = serieMovieRepository;
        }

        // GET: api/SerieMovies
        /// <summary>
        /// Get all series and/or movies.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SerieMovieDTO>>> GetSerieMovies()
        {
            return Ok(await _serieMovieRepository.GetSerieMovies().ConfigureAwait(false));
        }

        // GET: api/SerieMovies/5
        /// <summary>
        /// Get a specified serie of movie.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<SerieMovieDTO>> GetSerieMovie(int id)
        {
            var serieMovie = await _serieMovieRepository.GetSerieMovie(id).ConfigureAwait(false);

            if (serieMovie == null) return NotFound();

            return serieMovie;
        }

        // POST: api/SerieMovies
        /// <summary>
        /// Create a new serie or movie.
        /// </summary>
        /// <param name="serieMoviePostDTO"></param>
        [HttpPost]
        public async Task<ActionResult<SerieMoviePostDTO>> PostSerieMovie(SerieMoviePostDTO serieMoviePostDTO)
        {
            var serieMovieResult = await _serieMovieRepository.PostSerieMovie(serieMoviePostDTO).ConfigureAwait(false);

            return CreatedAtAction("GetSerieMovie", new { id = serieMovieResult.Id }, serieMovieResult);
        }

        // PUT: api/SerieMovies/5
        /// <summary>
        /// Update a specified serie or movie.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serieMovieDTO"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSerieMovie(int id, SerieMovieDTO serieMovieDTO)
        {
            if(serieMovieDTO == null) { throw new ArgumentNullException(nameof(serieMovieDTO));  }

            if (id != serieMovieDTO.Id) return BadRequest();

            var serieMovieResult = await _serieMovieRepository.PutSerieMovie(id, serieMovieDTO).ConfigureAwait(false);

            if (serieMovieResult == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/SerieMovies/5
        /// <summary>
        /// Delete a specified serie or movie.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SerieMovieDeleteDTO>> DeleteSerieMovie(int id)
        {
            var serieMovieResult = await _serieMovieRepository.DeleteSerieMovie(id).ConfigureAwait(false);

            if (serieMovieResult == null) return NotFound();

            return serieMovieResult;
        }
    }
}
