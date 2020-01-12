using API.Data;
using API.DTOs.Actor;
using API.DTOs.Genre;
using API.DTOs.Movie;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly WatchlistContext _context;

        public MovieRepository(WatchlistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieGetDTO>> GetMovies()
        {
            return await _context.Movies.Include(s => s.MovieActors)
               .Include(s => s.MovieGenres).Select(s => new MovieGetDTO()
               {
                   Id = s.Id,
                   Name = s.Name,
                   Synopsis = s.Synopsis,
                   Director = s.Director,
                   Duration = s.Duration,
                   Score = s.Score,
                   Image = s.Image
               })
               .AsNoTracking()
               .ToListAsync()
               .ConfigureAwait(false);
        }

        public async Task<MovieDTO> GetMovie(int id)
        {
            return await _context.Movies
                 .Include(s => s.MovieActors).Include(s => s.MovieGenres)
                 .Select(s => new MovieDTO()
                 {
                     Id = s.Id,
                     Name = s.Name,
                     Synopsis = s.Synopsis,
                     Director = s.Director,
                     Aired = s.Aired,
                     Duration = s.Duration,
                     Score = s.Score,
                     Image = s.Image,
                     MovieActorDTOs = s.MovieActors.Select(a => new MovieActorDTO()
                     {
                         MovieId = s.Id,
                         MovieName = s.Name,
                         ActorId = a.ActorId,
                         ActorName = a.Actor.FullName
                     }).ToList(),
                     MovieGenreDTOs = s.MovieGenres.Select(g => new MovieGenreDTO()
                     {
                         MovieId = s.Id,
                         MovieName = s.Name,
                         GenreId = g.GenreId,
                         GenreName = g.Genre.GenreName
                     }).ToList()
                 })
                 .AsNoTracking()
                 .FirstOrDefaultAsync(s => s.Id == id)
                 .ConfigureAwait(false);
        }

        public async Task<MoviePostDTO> PostMovie(MoviePostDTO moviePostDTO)
        {
            if (moviePostDTO == null) { throw new ArgumentNullException(nameof(moviePostDTO)); }

            var movie = _context.Movies.Add(new Movie()
            {
                Name = moviePostDTO.Name,
                Director = moviePostDTO.Director,
                Aired = moviePostDTO.Aired,
                Duration = moviePostDTO.Duration,
                MovieGenres = null,
                MovieActors = null
            });

            foreach (MovieActorDTO movieActorDTO in moviePostDTO.MovieActorDTOs)
            {
                Actor actor = _context.Actors.Find(movieActorDTO.ActorId);
                _context.MovieActors.Add(new MovieActor()
                {
                    MovieId = movie.Entity.Id,
                    Movie = movie.Entity,
                    ActorId = actor.Id,
                    Actor = actor
                });
            }

            foreach (MovieGenreDTO movieGenreDTO in moviePostDTO.MovieGenreDTOs)
            {
                Genre genre = _context.Genres.Find(movieGenreDTO.GenreId);
                _context.MovieGenres.Add(new MovieGenre()
                {
                    MovieId = movie.Entity.Id,
                    Movie = movie.Entity,
                    GenreId = genre.Id,
                    Genre = genre
                });
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);

            moviePostDTO.Id = movie.Entity.Id;

            return moviePostDTO;


        }

        public async Task<MovieDTO> PutMovie(int id, MovieDTO movieDTO)
        {
            if (movieDTO == null) { throw new ArgumentNullException(nameof(movieDTO)); }

            try
            {
                Movie movie = await _context.Movies.Include(s => s.MovieActors)
                    .Include(s => s.MovieGenres).FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
                movie.Name = movieDTO.Name;
                movie.Synopsis = movieDTO.Synopsis;
                movie.Director = movieDTO.Director;
                movie.Aired = movieDTO.Aired;
                movie.Duration = movieDTO.Duration;
                movie.Score = movieDTO.Score;
                movie.Image = movieDTO.Image;

                _context.MovieActors.RemoveRange(movie.MovieActors);

                foreach (MovieActorDTO movieActorDTO in movieDTO.MovieActorDTOs)
                {
                    Actor actor = _context.Actors.Find(movieActorDTO.ActorId);
                    movie.MovieActors.Add(new MovieActor()
                    {
                        MovieId = movie.Id,
                        Movie = movie,
                        ActorId = actor.Id,
                        Actor = actor
                    });
                }

                _context.MovieGenres.RemoveRange(movie.MovieGenres);

                foreach (MovieGenreDTO movieGenreDTO in movieDTO.MovieGenreDTOs)
                {
                    Genre genre = _context.Genres.Find(movieGenreDTO.GenreId);
                    movie.MovieGenres.Add(new MovieGenre()
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
                if (!MovieExists(id)) return null;
                else throw;
            }

            return movieDTO;
        }

        public async Task<MovieDeleteDTO> DeleteMovie(int id)
        {
            var movie = await _context.Movies
                .Include(s => s.MovieActors)
                .Include(s => s.MovieGenres)
                .Include(s => s.WatchLists)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (movie == null) return null;

            _context.MovieActors.RemoveRange(movie.MovieActors);
            _context.MovieGenres.RemoveRange(movie.MovieGenres);
            _context.WatchLists.RemoveRange(movie.WatchLists);
            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new MovieDeleteDTO()
            {
                Id = movie.Id,
                Name = movie.Name
            };
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
