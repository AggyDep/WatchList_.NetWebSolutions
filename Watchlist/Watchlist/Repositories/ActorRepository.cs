using API.Data;
using API.DTOs.Actor;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly WatchlistContext _context;

        public ActorRepository(WatchlistContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActorDTO>> GetActors()
        {
            return await _context.Actors.Include(a => a.MovieActors)
                .Select(a => new ActorDTO()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Birthday = a.Birthday,
                    Biography = a.Biography,
                    Website = a.Website,
                    Image = a.Image,
                    MovieActorDTOs = a.MovieActors.Select(x => new MovieActorDTO()
                    {
                        MovieId = x.MovieId,
                        MovieName = x.Movie.Name,
                        ActorId = a.Id,
                        ActorName = a.FullName
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<ActorDTO> GetActor(int id)
        {
            return await _context.Actors.Include(a => a.MovieActors)
                .Select(a => new ActorDTO()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Birthday = a.Birthday,
                    Biography = a.Biography,
                    Website = a.Website,
                    Image = a.Image,
                    MovieActorDTOs = a.MovieActors.Select(x => new MovieActorDTO()
                    {
                        MovieId = x.MovieId,
                        MovieName = x.Movie.Name,
                        ActorId = a.Id,
                        ActorName = a.FullName
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<ActorPostDTO> PostActor(ActorPostDTO actorPostDTO)
        {
            if (actorPostDTO == null) { throw new ArgumentNullException(nameof(actorPostDTO)); }

            var actorResult = _context.Actors.Add(new Actor()
            {
                FullName = actorPostDTO.FullName,
                Birthday = actorPostDTO.Birthday,
                Image = actorPostDTO.Image
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);

            actorPostDTO.Id = actorResult.Entity.Id;

            return actorPostDTO;
        }

        public async Task<ActorPutDTO> PutActor(int id, ActorPutDTO actorPutDTO)
        {
            if (actorPutDTO == null) { throw new ArgumentNullException(nameof(actorPutDTO)); }

            try
            {
                Actor actor = await _context.Actors.Include(a => a.MovieActors)
                    .FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);
                actor.FullName = actorPutDTO.FullName;
                actor.Birthday = actorPutDTO.Birthday;
                actor.Biography = actorPutDTO.Biography;
                actor.Website = actorPutDTO.Website;
                actor.Image = actorPutDTO.Image;

                _context.MovieActors.RemoveRange(actor.MovieActors);

                foreach (MovieActorDTO movieActorDTO in actorPutDTO.MovieActorDTOs)
                {
                    Movie movie = _context.Movies.Find(movieActorDTO.MovieId);
                    actor.MovieActors.Add(new MovieActor()
                    {
                        MovieId = movie.Id,
                        Movie = movie,
                        ActorId = actor.Id,
                        Actor = actor
                    });
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id)) return null;
                else throw;
            }
            return actorPutDTO;
        }

        public async Task<ActorDeleteDTO> DeleteActor(int id)
        {
            var actor = await _context.Actors
                .Include(a => a.MovieActors)
                .FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);

            if (actor == null) return null;

            _context.MovieActors.RemoveRange(actor.MovieActors);
            _context.Actors.Remove(actor);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new ActorDeleteDTO()
            {
                Id = actor.Id,
                FullName = actor.FullName
            };
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
