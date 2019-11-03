using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly WatchListContext _context;

        public ActorRepository(WatchListContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActorDTO>> GetActors()
        {
            return await _context.Actors.Include(a => a.SerieMovieActors)
                .Select(a => new ActorDTO()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Birthday = a.Birthday,
                    Biography = a.Biography,
                    Age = a.Age,
                    Website = a.Website,
                    Image = a.Image,
                    SerieMovieActorDTOs = a.SerieMovieActors.Select(x => new SerieMovieActorDTO()
                    {
                        SerieMovieId = x.SerieMovieId,
                        SerieMovieName = x.SerieMovie.Name,
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
            return await _context.Actors.Include(a => a.SerieMovieActors)
                .Select(a => new ActorDTO()
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    Birthday = a.Birthday,
                    Age = a.Age,
                    Biography = a.Biography,
                    Website = a.Website,
                    Image = a.Image,
                    SerieMovieActorDTOs = a.SerieMovieActors.Select(x => new SerieMovieActorDTO()
                    {
                        SerieMovieId = x.SerieMovieId,
                        SerieMovieName = x.SerieMovie.Name,
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
            if(actorPostDTO == null) { throw new ArgumentNullException(nameof(actorPostDTO)); }

            var actorResult = _context.Actors.Add(new Actor()
            {
                FullName = actorPostDTO.FullName,
                Birthday = actorPostDTO.Birthday,
                Age = (Int32.Parse(DateTime.Now.Year.ToString()) - Int32.Parse(actorPostDTO.Birthday.Substring(actorPostDTO.Birthday.Length - 4))),
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
                Actor actor = await _context.Actors.Include(a => a.SerieMovieActors)
                    .FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);
                actor.FullName = actorPutDTO.FullName;
                actor.Birthday = actorPutDTO.Birthday;
                actor.Age = actorPutDTO.Age;
                actor.Biography = actorPutDTO.Biography;
                actor.Website = actorPutDTO.Website;
                actor.Image = actorPutDTO.Image;

                _context.SerieMovieActors.RemoveRange(actor.SerieMovieActors);

                foreach (SerieMovieActorDTO serieMovieActorDTO in actorPutDTO.SerieMovieActorDTOs)
                {
                    SerieMovie serieMovie = _context.SerieMovies.Find(serieMovieActorDTO.SerieMovieId);
                    actor.SerieMovieActors.Add(new SerieMovieActor()
                    {
                        SerieMovieId = serieMovie.Id,
                        SerieMovie = serieMovie,
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
                .Include(a => a.SerieMovieActors)
                .FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);

            if (actor == null) return null;

            _context.SerieMovieActors.RemoveRange(actor.SerieMovieActors);
            _context.Actors.Remove(actor);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new ActorDeleteDTO()
            {
                Id = actor.Id,
                FullName = actor.FullName,
                Birthday = actor.Birthday
            };
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
