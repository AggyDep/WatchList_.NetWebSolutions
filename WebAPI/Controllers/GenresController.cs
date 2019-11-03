using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.DTOs.Genre;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly WatchListContext _context;
        private readonly IGenreRepository _genreRepository;

        public GenresController(WatchListContext context, IGenreRepository genreRepository)
        {
            _context = context;
            _genreRepository = genreRepository;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            return Ok(await _genreRepository.GetGenres().ConfigureAwait(false));
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            var genre = await _genreRepository.GetGenre(id).ConfigureAwait(false);

            if (genre == null) return NotFound();

            return genre;
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<GenrePostDTO>> PostGenre(GenrePostDTO genrePostDTO)
        {
            var genreResult = await _genreRepository.PostGenre(genrePostDTO).ConfigureAwait(false);

            return CreatedAtAction("GetGenre", new { id = genreResult.Id }, genreResult);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, GenreDTO genreDTO)
        {
            if(genreDTO == null) { throw new ArgumentNullException(nameof(genreDTO)); }

            if (id != genreDTO.Id) return BadRequest();

            var genreResult = await _genreRepository.PutGenre(id, genreDTO).ConfigureAwait(false);

            if (genreResult == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDTO>> DeleteGenre(int id)
        {
            var genreResult = await _genreRepository.DeleteGenre(id).ConfigureAwait(false);

            if (genreResult == null) return NotFound();

            return genreResult;
        }
    }
}
