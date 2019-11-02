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

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly WatchListContext _context;

        public UsersController(WatchListContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _context.Users.Include(u => u.WatchLists).Include(u => u.UserFriends)
                .Select(u => new UserDTO() 
                { 
                    Id = u.Id,
                    Username = u.Username,
                    About = u.About,
                    Image = u.Image,
                    Joined = u.Joined, 
                    WatchListDTOs = u.WatchLists.Select(w => new WatchListDTO() 
                    {
                        UserId = u.Id,
                        UserName = u.Username,
                        SerieMovieId = w.SerieMovieId,
                        SerieMovieName = w.SerieMovie.Name,
                        Status = w.Status,
                        Score = w.Score,
                        Episode = w.Episode
                    }).ToList(), 
                    UserFriendsDTOs = u.UserFriends.Select(x => new UserFriendDTO() 
                    {
                        UserId = u.Id,
                        UserName = u.Username, 
                        FriendId = x.FriendId,
                        FriendName = x.Friend.Username
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.WatchLists).Include(u => u.UserFriends)
                .Select(u => new UserDTO()
                {
                    Id = u.Id,
                    Username = u.Username,
                    About = u.About,
                    Image = u.Image,
                    Joined = u.Joined,
                    WatchListDTOs = u.WatchLists.Select(w => new WatchListDTO()
                    {
                        UserId = u.Id,
                        UserName = u.Username,
                        SerieMovieId = w.SerieMovieId,
                        SerieMovieName = w.SerieMovie.Name,
                        Status = w.Status,
                        Score = w.Score,
                        Episode = w.Episode
                    }).ToList(),
                    UserFriendsDTOs = u.UserFriends.Select(x => new UserFriendDTO()
                    {
                        UserId = u.Id,
                        UserName = u.Username,
                        FriendId = x.FriendId,
                        FriendName = x.Friend.Username
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound();

            return user;
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserPostDTO>> PostUser(UserPostDTO userPostDTO)
        {
            var userResult = _context.Users.Add(new User()
            {
                Role = getRole(userPostDTO.Role),
                Name = userPostDTO.Name,
                LastName = userPostDTO.LastName,
                Email = userPostDTO.Email,
                Username = userPostDTO.Username,
                Password = userPostDTO.Password,
                Joined = DateTime.Now.ToString("MM\\/dd\\/yyyy")
            });

            await _context.SaveChangesAsync();

            userPostDTO.Id = userResult.Entity.Id;

            return CreatedAtAction("GetUser", new { id = userPostDTO.Id }, userPostDTO);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserPutDTO userPutDTO)
        {
            if (id != userPutDTO.Id) return BadRequest();
            //_context.Entry(user).State = EntityState.Modified;

            try
            {
                User user = await _context.Users.Include(u => u.WatchLists).Include(u => u.UserFriends)
                    .FirstOrDefaultAsync(u => u.Id == id);
                user.Name = userPutDTO.Name;
                user.LastName = userPutDTO.LastName;
                user.Email = userPutDTO.Email;
                user.Username = userPutDTO.Username;
                user.Birthday = userPutDTO.Birthday;
                user.About = userPutDTO.About;
                user.Image = userPutDTO.Image;

                _context.WatchLists.RemoveRange(user.WatchLists);

                foreach(WatchListDTO watchListDTO in userPutDTO.WatchListDTOs)
                {
                    WatchList watchList = _context.WatchLists.Find(watchListDTO.SerieMovieId);
                    user.WatchLists.Add(new WatchList() 
                    {
                        
                    });
                }

                _context.UserFriends.RemoveRange(user.UserFriends);

                foreach(UserFriendDTO userFriendDTO in userPutDTO.UserFriendsDTOs)
                {
                    User userFriend = _context.Users.Find(userFriendDTO.UserId);
                    user.UserFriends.Add(new UserFriend()
                    {
                        UserId = user.Id,
                        User = user,
                        FriendId = userFriend.Id,
                        Friend = userFriend
                    });
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // PATCH: api/Users/5/ChangePassword
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(int id, UserPatchDTO userPatchDTO)
        {
            if (id != userPatchDTO.Id) return BadRequest();

            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                user.Password = userPatchDTO.Password;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDeleteDTO>> DeleteUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.WatchLists)
                .Include(u => u.UserFriends)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return NotFound();

            _context.WatchLists.RemoveRange(user.WatchLists);
            _context.UserFriends.RemoveRange(user.UserFriends);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new UserDeleteDTO() 
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username
            };
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private Enumerations.Role getRole(string givenRole)
        {
            Enumerations.Role roleValue;
            switch (givenRole)
            {
                
                case "user":
                case "User":
                case "U":
                    roleValue = Enumerations.Role.User;
                    break;
                case "admin":
                case "Admin":
                case "A":
                    roleValue = Enumerations.Role.Admin;
                    break;
                default:
                    roleValue = Enumerations.Role.User;
                    break;
            }
            return roleValue;
        } 
    }
}
