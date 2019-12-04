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
using WebAPI.DTOs.User;
using WebAPI.Models;
using WebAPI.Repositories;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WatchListContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public UsersController(WatchListContext context, IUserRepository userRepository, IUserService userService)
        {
            _context = context;
            _userRepository = userRepository;
            _userService = userService;
        }

        // GET: api/Users
        /// <summary>
        /// Get all users.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetUsers()
        {
            return Ok(await _userRepository.GetUsers().ConfigureAwait(false));
        }

        // GET: api/Users/5
        /// <summary>
        /// Get the details of a specified user.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(string id)
        {
            var user = await _userRepository.GetUser(id).ConfigureAwait(false);

            if (user == null) return NotFound();

            return user;
        }

        // POST: api/Users
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="userPostDTO"></param>
        [HttpPost]
        public async Task<ActionResult<UserPostDTO>> PostUser(UserPostDTO userPostDTO)
        {
            var userResult = await _userRepository.PostUser(userPostDTO).ConfigureAwait(false);

            return CreatedAtAction("GetUser", new { id = userResult.Id }, userResult);
        }

        // PUT: api/Users/5
        /// <summary>
        /// Update a specified user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userPutDTO"></param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserPutDTO userPutDTO)
        {
            if (userPutDTO == null) { throw new ArgumentNullException(nameof(userPutDTO));  }

            if (id != userPutDTO.Id) return BadRequest();

            var userResult = await _userRepository.PutUser(id, userPutDTO).ConfigureAwait(false);

            if (userResult == null) return NotFound();

            return NoContent();
        }

        // POST: api/Users/register
        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userPostDTO"></param>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserPostDTO>> RegisterUser(UserPostDTO userPostDTO)
        {
            return await this.PostUser(userPostDTO);
        }

        // POST: api/Users/authenticate
        /// <summary>
        /// Authenticate an existing user.
        /// </summary>
        /// <param name="userAuthenticateDTO"></param>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDTO>> AuthenticateUser(UserAuthenticateDTO userAuthenticateDTO)
        {
            var userResult = await _userService.Authenticate(userAuthenticateDTO.UserName, userAuthenticateDTO.Password).ConfigureAwait(false);

            if (userResult == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return CreatedAtAction("GetUser", new { id = userResult.Id }, userResult);
        }

        // PATCH: api/Users/5/ChangePassword
        /// <summary>
        /// Change the password of a specified user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userPatchDTO"></param>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(string id, UserPatchDTO userPatchDTO)
        {
            if(userPatchDTO == null) { throw new ArgumentNullException(nameof(userPatchDTO));  }

            if (id != userPatchDTO.Id) return BadRequest();

            var userResult = await _userRepository.PatchUser(id, userPatchDTO).ConfigureAwait(false);

            if (userResult == null) return NotFound();

            return NoContent();
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Delete a specified user.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDeleteDTO>> DeleteUser(string id)
        {
            var userResult = await _userRepository.DeleteUser(id).ConfigureAwait(false);

            if (userResult == null) return NotFound();

            return userResult;
        }
    }
}
