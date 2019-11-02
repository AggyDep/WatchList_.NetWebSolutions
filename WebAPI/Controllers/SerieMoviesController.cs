using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerieMoviesController : ControllerBase
    {
        private readonly WatchListContext _context;

        public SerieMoviesController(WatchListContext context)
        {
            _context = context;
        }

        // GET: api/SerieMovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SerieMovie>>> GetSerieMovies()
        {
            return await _context.SerieMovies.ToListAsync();
        }

        // GET: api/SerieMovies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SerieMovie>> GetSerieMovie(int id)
        {
            var serieMovie = await _context.SerieMovies.FindAsync(id);

            if (serieMovie == null)
            {
                return NotFound();
            }

            return serieMovie;
        }

        // PUT: api/SerieMovies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSerieMovie(int id, SerieMovie serieMovie)
        {
            if (id != serieMovie.Id)
            {
                return BadRequest();
            }

            _context.Entry(serieMovie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieMovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SerieMovies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SerieMoviePostDTO>> PostSerieMovie(SerieMoviePostDTO serieMoviePost)
        {
            var serieMovie = _context.SerieMovies.Add( new SerieMovie()
            {
                IsSerie = serieMoviePost.IsSerie,
                Name = serieMoviePost.Name,
                Director = serieMoviePost.Director,
                Status = serieMoviePost.Status,
                Aired = serieMoviePost.Aired,
                Duration = serieMoviePost.Duration,
                SerieMovieGenres = null,
                SerieMovieActors = null
            });

           
            await _context.SaveChangesAsync();


            foreach (SerieMovieActorDTO serieMovieActorDTO in serieMoviePost.SerieMovieActorDTOs)
            {
                Actor actor = _context.Actors.Find(serieMovieActorDTO.ActorId);
                _context.SerieMovieActors.Add(new SerieMovieActor() { SerieMovieId = serieMovie.Entity.Id, ActorId = actor.Id });
            }

            foreach (SerieMovieGenreDTO serieMovieGenreDTO in serieMoviePost.SerieMovieGenreDTOs)
            {
                Genre genre = _context.Genres.Find(serieMovieGenreDTO.GenreId);
                _context.SerieMovieGenres.Add(new SerieMovieGenre() { SerieMovieId = serieMovie.Entity.Id, GenreId = genre.Id });
            }

            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSerieMovie", new { id = serieMovie.Entity.Id }, serieMovie);
        }

        // DELETE: api/SerieMovies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SerieMovie>> DeleteSerieMovie(int id)
        {
            var serieMovie = await _context.SerieMovies.FindAsync(id);
            if (serieMovie == null)
            {
                return NotFound();
            }

            _context.SerieMovies.Remove(serieMovie);
            await _context.SaveChangesAsync();

            return serieMovie;
        }

        private bool SerieMovieExists(int id)
        {
            return _context.SerieMovies.Any(e => e.Id == id);
        }
    }
}
