using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.DTOs.User;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WatchListContext _context;
        private readonly IUserRepository _userRepository;

        public UsersController(WatchListContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsers().ConfigureAwait(false));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id).ConfigureAwait(false);

            if (user == null) return NotFound();

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserPostDTO>> PostUser(UserPostDTO userPostDTO)
        {
            var userResult = await _userRepository.PostUser(userPostDTO).ConfigureAwait(false);

            return CreatedAtAction("GetUser", new { id = userResult.Id }, userResult);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserPutDTO userPutDTO)
        {
            if (userPutDTO == null) { throw new ArgumentNullException(nameof(userPutDTO));  }

            if (id != userPutDTO.Id) return BadRequest();

            var userResult = await _userRepository.PutUser(id, userPutDTO).ConfigureAwait(false);

            if (userResult == null) return NotFound();

            return NoContent();
        }

        // PATCH: api/Users/5/ChangePassword
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, UserPatchDTO userPatchDTO)
        {
            if(userPatchDTO == null) { throw new ArgumentNullException(nameof(userPatchDTO));  }

            if (id != userPatchDTO.Id) return BadRequest();

            var userResult = await _userRepository.PatchUser(id, userPatchDTO).ConfigureAwait(false);

            if (userResult == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDeleteDTO>> DeleteUser(int id)
        {
            var userResult = await _userRepository.DeleteUser(id).ConfigureAwait(false);

            if (userResult == null) return NotFound();

            return userResult;
        }
    }
}
