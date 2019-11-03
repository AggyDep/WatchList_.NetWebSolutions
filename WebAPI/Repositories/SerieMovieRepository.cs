using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.DTOs.SerieMovie;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class SerieMovieRepository : ISerieMovieRepository
    {
        private readonly WatchListContext _context;

        public SerieMovieRepository(WatchListContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SerieMovieDTO>> GetSerieMovies()
        {
            return await _context.SerieMovies.Include(s => s.SerieMovieActors)
               .Include(s => s.SerieMovieGenres).Select(s => new SerieMovieDTO()
               {
                   Id = s.Id,
                   IsSerie = s.IsSerie,
                   Name = s.Name,
                   Episode = s.Episode,
                   Season = s.Season,
                   Synopsis = s.Synopsis,
                   Duration = s.Duration,
                   Score = s.Score,
                   Member = s.Member,
                   Image = s.Image,
                   SerieMovieActorDTOs = s.SerieMovieActors.Select(a => new SerieMovieActorDTO()
                   {
                       SerieMovieId = s.Id,
                       SerieMovieName = s.Name,
                       ActorId = a.ActorId,
                       ActorName = a.Actor.FullName
                   }).ToList(),
                   SerieMovieGenreDTOs = s.SerieMovieGenres.Select(g => new SerieMovieGenreDTO()
                   {
                       SerieMovieId = s.Id,
                       SerieMovieName = s.Name,
                       GenreId = g.GenreId,
                       GenreName = g.Genre.GenreName
                   }).ToList()
               })
               .AsNoTracking()
               .ToListAsync()
               .ConfigureAwait(false);
        }

        public async Task<SerieMovieDTO> GetActorGetSerieMovie(int id)
        {
           return await _context.SerieMovies
                .Include(s => s.SerieMovieActors).Include(s => s.SerieMovieGenres)
                .Select(s => new SerieMovieDTO()
                {
                    Id = s.Id,
                    IsSerie = s.IsSerie,
                    Name = s.Name,
                    Episode = s.Episode,
                    Season = s.Season,
                    Synopsis = s.Synopsis,
                    Background = s.Background,
                    Producer = s.Producer,
                    Director = s.Director,
                    Status = s.Status,
                    Aired = s.Aired,
                    Duration = s.Duration,
                    Score = s.Score,
                    Ranking = s.Ranking,
                    Member = s.Member,
                    Image = s.Image,
                    SerieMovieActorDTOs = s.SerieMovieActors.Select(a => new SerieMovieActorDTO()
                    {
                        SerieMovieId = s.Id,
                        SerieMovieName = s.Name,
                        ActorId = a.ActorId,
                        ActorName = a.Actor.FullName
                    }).ToList(),
                    SerieMovieGenreDTOs = s.SerieMovieGenres.Select(g => new SerieMovieGenreDTO()
                    {
                        SerieMovieId = s.Id,
                        SerieMovieName = s.Name,
                        GenreId = g.GenreId,
                        GenreName = g.Genre.GenreName
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<SerieMoviePostDTO> PostSerieMovie(SerieMoviePostDTO serieMoviePostDTO)
        {
            if (serieMoviePostDTO == null) { throw new ArgumentNullException(nameof(serieMoviePostDTO));  }

            var serieMovie = _context.SerieMovies.Add(new SerieMovie()
            {
                IsSerie = serieMoviePostDTO.IsSerie,
                Name = serieMoviePostDTO.Name,
                Director = serieMoviePostDTO.Director,
                Status = serieMoviePostDTO.Status,
                Aired = serieMoviePostDTO.Aired,
                Duration = serieMoviePostDTO.Duration,
                SerieMovieGenres = null,
                SerieMovieActors = null
            });

            foreach (SerieMovieActorDTO serieMovieActorDTO in serieMoviePostDTO.SerieMovieActorDTOs)
            {
                Actor actor = _context.Actors.Find(serieMovieActorDTO.ActorId);
                _context.SerieMovieActors.Add(new SerieMovieActor()
                {
                    SerieMovieId = serieMovie.Entity.Id,
                    SerieMovie = serieMovie.Entity,
                    ActorId = actor.Id,
                    Actor = actor
                });
            }

            foreach (SerieMovieGenreDTO serieMovieGenreDTO in serieMoviePostDTO.SerieMovieGenreDTOs)
            {
                Genre genre = _context.Genres.Find(serieMovieGenreDTO.GenreId);
                _context.SerieMovieGenres.Add(new SerieMovieGenre()
                {
                    SerieMovieId = serieMovie.Entity.Id,
                    SerieMovie = serieMovie.Entity,
                    GenreId = genre.Id,
                    Genre = genre
                });
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);

            serieMoviePostDTO.Id = serieMovie.Entity.Id;

            return serieMoviePostDTO;


        }

        public async Task<SerieMovieDTO> PutSerieMovie(int id, SerieMovieDTO serieMovieDTO)
        {
            if (serieMovieDTO == null) { throw new ArgumentNullException(nameof(serieMovieDTO)); }

            try
            {
                SerieMovie serieMovie = await _context.SerieMovies.Include(s => s.SerieMovieActors)
                    .Include(s => s.SerieMovieGenres).FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
                serieMovie.IsSerie = serieMovieDTO.IsSerie;
                serieMovie.Name = serieMovieDTO.Name;
                serieMovie.Episode = serieMovieDTO.Episode;
                serieMovie.Season = serieMovieDTO.Season;
                serieMovie.Synopsis = serieMovieDTO.Synopsis;
                serieMovie.Background = serieMovieDTO.Background;
                serieMovie.Producer = serieMovieDTO.Producer;
                serieMovie.Director = serieMovieDTO.Director;
                serieMovie.Status = serieMovieDTO.Status;
                serieMovie.Aired = serieMovieDTO.Aired;
                serieMovie.Duration = serieMovieDTO.Duration;
                serieMovie.Score = serieMovieDTO.Score;
                serieMovie.Ranking = serieMovieDTO.Ranking;
                serieMovie.Member = serieMovieDTO.Member;
                serieMovie.Image = serieMovieDTO.Image;

                _context.SerieMovieActors.RemoveRange(serieMovie.SerieMovieActors);

                foreach (SerieMovieActorDTO serieMovieActorDTO in serieMovieDTO.SerieMovieActorDTOs)
                {
                    Actor actor = _context.Actors.Find(serieMovieActorDTO.ActorId);
                    serieMovie.SerieMovieActors.Add(new SerieMovieActor()
                    {
                        SerieMovieId = serieMovie.Id,
                        SerieMovie = serieMovie,
                        ActorId = actor.Id,
                        Actor = actor
                    });
                }

                _context.SerieMovieGenres.RemoveRange(serieMovie.SerieMovieGenres);

                foreach (SerieMovieGenreDTO serieMovieGenreDTO in serieMovieDTO.SerieMovieGenreDTOs)
                {
                    Genre genre = _context.Genres.Find(serieMovieGenreDTO.GenreId);
                    serieMovie.SerieMovieGenres.Add(new SerieMovieGenre()
                    {
                        SerieMovieId = serieMovie.Id,
                        SerieMovie = serieMovie,
                        GenreId = genre.Id,
                        Genre = genre
                    });
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieMovieExists(id)) return null;
                else throw;
            }

            return serieMovieDTO;
        }

        public async Task<SerieMovieDeleteDTO> DeleteSerieMovie(int id)
        {
            var serieMovie = await _context.SerieMovies
                .Include(s => s.SerieMovieActors)
                .Include(s => s.SerieMovieGenres)
                .Include(s => s.WatchLists)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (serieMovie == null) return null;

            _context.SerieMovieActors.RemoveRange(serieMovie.SerieMovieActors);
            _context.SerieMovieGenres.RemoveRange(serieMovie.SerieMovieGenres);
            _context.WatchLists.RemoveRange(serieMovie.WatchLists);
            _context.SerieMovies.Remove(serieMovie);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new SerieMovieDeleteDTO()
            {
                Id = serieMovie.Id,
                IsSerie = serieMovie.IsSerie,
                Name = serieMovie.Name
            };
        }

        private bool SerieMovieExists(int id)
        {
            return _context.SerieMovies.Any(e => e.Id == id);
        }
    }
}
