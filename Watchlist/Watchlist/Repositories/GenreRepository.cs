using API.Data;
using API.DTOs.Genre;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly WatchlistContext _context;

        public GenreRepository(WatchlistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreDTO>> GetGenres()
        {
            return await _context.Genres.Include(g => g.MovieGenres)
                .Select(g => new GenreDTO()
                {
                    Id = g.Id,
                    GenreName = g.GenreName,
                    MovieGenreDTOs = g.MovieGenres.Select(x => new MovieGenreDTO()
                    {
                        MovieId = x.MovieId,
                        MovieName = x.Movie.Name,
                        GenreId = g.Id,
                        GenreName = g.GenreName
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<GenreDTO> GetGenre(int id)
        {
            return await _context.Genres.Include(g => g.MovieGenres)
                 .Select(g => new GenreDTO()
                 {
                     Id = g.Id,
                     GenreName = g.GenreName,
                     MovieGenreDTOs = g.MovieGenres.Select(x => new MovieGenreDTO()
                     {
                         MovieId = x.MovieId,
                         MovieName = x.Movie.Name,
                         GenreId = g.Id,
                         GenreName = g.GenreName
                     }).ToList()
                 })
                 .AsNoTracking()
                 .FirstOrDefaultAsync(g => g.Id == id)
                 .ConfigureAwait(false);
        }

        public async Task<GenrePostDTO> PostGenre(GenrePostDTO genrePostDTO)
        {
            if (genrePostDTO == null) { throw new ArgumentNullException(nameof(genrePostDTO)); }

            var genreResult = _context.Genres.Add(new Genre()
            {
                GenreName = genrePostDTO.GenreName
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);

            genrePostDTO.Id = genreResult.Entity.Id;

            return genrePostDTO;
        }

        public async Task<GenreDTO> PutGenre(int id, GenreDTO genreDTO)
        {
            if (genreDTO == null) { throw new ArgumentNullException(nameof(genreDTO)); }

            try
            {
                Genre genre = await _context.Genres.Include(g => g.MovieGenres)
                    .FirstOrDefaultAsync(g => g.Id == id).ConfigureAwait(false);
                genre.GenreName = genreDTO.GenreName;

                _context.MovieGenres.RemoveRange(genre.MovieGenres);

                foreach (MovieGenreDTO movieGenreDTO in genreDTO.MovieGenreDTOs)
                {
                    Movie movie = _context.Movies.Find(movieGenreDTO.MovieId);
                    genre.MovieGenres.Add(new MovieGenre()
                    {
                        MovieId = movie.Id,
                        Movie = movie,
                        GenreId = genre.Id,
                        Genre = genre
                    });
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id)) return null;
                else throw;
            }

            return genreDTO;
        }

        public async Task<GenreDTO> DeleteGenre(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.MovieGenres)
                .FirstOrDefaultAsync(g => g.Id == id).ConfigureAwait(false);

            if (genre == null) return null;

            _context.MovieGenres.RemoveRange(genre.MovieGenres);
            _context.Genres.Remove(genre);

            await _context.SaveChangesAsync().ConfigureAwait(false);

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
