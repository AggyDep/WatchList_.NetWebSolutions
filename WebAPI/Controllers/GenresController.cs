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

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly WatchListContext _context;

        public GenresController(WatchListContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetGenres()
        {
            return await _context.Genres.Include(g => g.SerieMovieGenres)
                .Select(g => new GenreDTO()
                {
                    Id = g.Id,
                    GenreName = g.GenreName,
                    SerieMovieGenreDTOs = g.SerieMovieGenres.Select(x => new SerieMovieGenreDTO()
                    {
                        SerieMovieId = x.SerieMovieId,
                        SerieMovieName = x.SerieMovie.Name,
                        GenreId = g.Id,
                        GenreName = g.GenreName
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenre(int id)
        {
            var genre = await _context.Genres.Include(g => g.SerieMovieGenres)
                .Select( g => new GenreDTO()
                {
                    Id = g.Id,
                    GenreName = g.GenreName,
                    SerieMovieGenreDTOs = g.SerieMovieGenres.Select(x => new SerieMovieGenreDTO()
                    {
                        SerieMovieId = x.SerieMovieId,
                        SerieMovieName = x.SerieMovie.Name,
                        GenreId = g.Id,
                        GenreName = g.GenreName
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null) return NotFound();

            return genre;
        }

        // POST: api/Genres
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GenrePostDTO>> PostGenre(GenrePostDTO genrePostDTO)
        {
            var genreResult = _context.Genres.Add(new Genre() 
            {
                GenreName = genrePostDTO.GenreName
            });

            await _context.SaveChangesAsync();

            genrePostDTO.Id = genreResult.Entity.Id;

            return CreatedAtAction("GetGenre", new { id = genrePostDTO.Id }, genrePostDTO);
        }

        // PUT: api/Genres/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, GenreDTO genreDTO)
        {
            if (id != genreDTO.Id) return BadRequest();
            //_context.Entry(genre).State = EntityState.Modified;

            try
            {
                Genre genre = await _context.Genres.Include(g => g.SerieMovieGenres)
                    .FirstOrDefaultAsync(g => g.Id == id);
                genre.GenreName = genreDTO.GenreName;

                _context.SerieMovieGenres.RemoveRange(genre.SerieMovieGenres);

                foreach(SerieMovieGenreDTO serieMovieGenreDTO in genreDTO.SerieMovieGenreDTOs)
                {
                    SerieMovie serieMovie = _context.SerieMovies.Find(serieMovieGenreDTO.SerieMovieId);
                    genre.SerieMovieGenres.Add(new SerieMovieGenre()
                    {
                        SerieMovieId = serieMovie.Id,
                        SerieMovie = serieMovie,
                        GenreId = genre.Id,
                        Genre = genre
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GenreDTO>> DeleteGenre(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.SerieMovieGenres)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null) return NotFound();

            _context.SerieMovieGenres.RemoveRange(genre.SerieMovieGenres);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return new GenreDTO() 
            {
                Id = genre.Id,
                GenreName = genre.GenreName
            };
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
