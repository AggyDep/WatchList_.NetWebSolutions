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
    public class ActorsController : ControllerBase
    {
        private readonly WatchListContext _context;

        public ActorsController(WatchListContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetActors()
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
                .ToListAsync();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDTO>> GetActor(int id)
        {
            var actor = await _context.Actors.Include(a => a.SerieMovieActors)
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
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null) return NotFound();

            return actor;
        }

        // POST: api/Actors
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ActorPostDTO>> PostActor(ActorPostDTO actorPostDTO)
        {
            var actorResult = _context.Actors.Add(new Actor()
            {
                FullName = actorPostDTO.FullName,
                Birthday = actorPostDTO.Birthday,
                Age = (Int32.Parse(DateTime.Now.Year.ToString()) - Int32.Parse(actorPostDTO.Birthday.Substring(actorPostDTO.Birthday.Length - 4))),
                Image = actorPostDTO.Image
            });

            await _context.SaveChangesAsync();

            actorPostDTO.Id = actorResult.Entity.Id;

            return CreatedAtAction("GetActor", new { id = actorPostDTO.Id }, actorPostDTO);
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorPutDTO actorPutDTO)
        {
            if (id != actorPutDTO.Id) return BadRequest();
            //_context.Entry(actor).State = EntityState.Modified;

            try
            {
                Actor actor = await _context.Actors.Include(a => a.SerieMovieActors)
                    .FirstOrDefaultAsync(a => a.Id == id);
                actor.FullName = actorPutDTO.FullName;
                actor.Birthday = actorPutDTO.Birthday;
                actor.Age = actorPutDTO.Age;
                actor.Biography = actorPutDTO.Biography;
                actor.Website = actorPutDTO.Website;
                actor.Image = actorPutDTO.Image;

                _context.SerieMovieActors.RemoveRange(actor.SerieMovieActors);

                foreach(SerieMovieActorDTO serieMovieActorDTO in actorPutDTO.SerieMovieActorDTOs)
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

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }


        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActorDeleteDTO>> DeleteActor(int id)
        {
            var actor = await _context.Actors
                .Include(a => a.SerieMovieActors)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null) return NotFound();

            _context.SerieMovieActors.RemoveRange(actor.SerieMovieActors);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

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
