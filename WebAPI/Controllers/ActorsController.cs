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
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly WatchListContext _context;
        private readonly IActorRepository _actorRepository;

        public ActorsController(WatchListContext context, IActorRepository actorRepository)
        {
            _context = context;
            _actorRepository = actorRepository;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetActors()
        {
            return Ok(await _actorRepository.GetActors().ConfigureAwait(false));
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDTO>> GetActor(int id)
        {
            var actor = await _actorRepository.GetActor(id).ConfigureAwait(false);

            if (actor == null) return NotFound();

            return actor;
        }

        // POST: api/Actors
        [HttpPost]
        public async Task<ActionResult<ActorPostDTO>> PostActor(ActorPostDTO actorPostDTO)
        {
            var actorResult = await _actorRepository.PostActor(actorPostDTO).ConfigureAwait(false);

            return CreatedAtAction("GetActor", new { id = actorResult.Id }, actorResult);
        }

        // PUT: api/Actors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, ActorPutDTO actorPutDTO)
        {
            if(actorPutDTO == null) { throw new ArgumentNullException(nameof(actorPutDTO)); }

            if (id != actorPutDTO.Id) return BadRequest();

            var actorResult = await _actorRepository.PutActor(id, actorPutDTO).ConfigureAwait(false);

            if (actorResult == null) return NotFound();

            return NoContent();
        }


        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActorDeleteDTO>> DeleteActor(int id)
        {
            var actorResult = await _actorRepository.DeleteActor(id).ConfigureAwait(false);

            if (actorResult == null) return NotFound();

            return actorResult;
        }
    }
}
