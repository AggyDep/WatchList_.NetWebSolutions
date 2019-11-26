using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
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
        /// <summary>
        /// Get all actors.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetActors()
        {
            return Ok(await _actorRepository.GetActors().ConfigureAwait(false));
        }

        // GET: api/Actors/5
        /// <summary>
        /// Get a specified actor.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDTO>> GetActor(int id)
        {
            var actor = await _actorRepository.GetActor(id).ConfigureAwait(false);

            if (actor == null) return NotFound();

            return actor;
        }

        // POST: api/Actors
        /// <summary>
        /// Create a new actor.
        /// </summary>
        /// <param name="actorPostDTO"></param>
        [HttpPost]
        public async Task<ActionResult<ActorPostDTO>> PostActor(ActorPostDTO actorPostDTO)
        {
            var actorResult = await _actorRepository.PostActor(actorPostDTO).ConfigureAwait(false);

            return CreatedAtAction("GetActor", new { id = actorResult.Id }, actorResult);
        }

        // PUT: api/Actors/5
        /// <summary>
        /// Update a specified actor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actorPutDTO"></param>
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
        /// <summary>
        /// Delete a specified actor.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActorDeleteDTO>> DeleteActor(int id)
        {
            var actorResult = await _actorRepository.DeleteActor(id).ConfigureAwait(false);

            if (actorResult == null) return NotFound();

            return actorResult;
        }
    }
}
