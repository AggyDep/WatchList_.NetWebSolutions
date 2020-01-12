using API.Data;
using API.DTOs.User;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WatchlistContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(WatchlistContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserGetDTO>> GetUsers()
        {
            return await _context.Users.Include(u => u.WatchLists)
                .Select(u => new UserGetDTO()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Image = u.Image,
                    Joined = u.Joined,
                    WatchListDTOs = u.WatchLists.Select(w => new WatchListDTO()
                    {
                        UserId = u.Id,
                        UserName = u.UserName,
                        MovieId = w.MovieId,
                        MovieName = w.Movie.Name,
                        Status = w.Status,
                        Score = w.Score
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<UserDTO> GetUserDetails(string id)
        {
            return await _context.Users.Include(u => u.WatchLists)
                .Select(u => new UserDTO()
                {
                    Id = u.Id,
                    Role = u.Role,
                    Username = u.UserName,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    Birthday = u.Birthday,
                    About = u.About,
                    Image = u.Image,
                    Joined = u.Joined,
                    WatchListDTOs = u.WatchLists.Select(w => new WatchListDTO()
                    {
                        UserId = u.Id,
                        UserName = u.UserName,
                        MovieId = w.MovieId,
                        MovieName = w.Movie.Name,
                        Status = w.Status,
                        Score = w.Score
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<UserPostDTO> PostUser(UserPostDTO userPostDTO)
        {
            if (userPostDTO == null) { throw new ArgumentNullException(nameof(userPostDTO)); }

            User user = new User
            {
                Role = getRole(userPostDTO.Role),
                Name = userPostDTO.Name,
                LastName = userPostDTO.LastName,
                Email = userPostDTO.Email,
                UserName = userPostDTO.Username,
                Joined = DateTime.Now.ToString("dd\\/MM\\/yyyy")
            };

            await _userManager.CreateAsync(user, userPostDTO.Password).ConfigureAwait(false);

            userPostDTO.Id = user.Id;

            return userPostDTO;
        }

        public async Task<UserPutDTO> PutUser(string id, UserPutDTO userPutDTO)
        {
            if (userPutDTO == null) { throw new ArgumentNullException(nameof(userPutDTO)); }

            try
            {
                User user = await _context.Users.Include(u => u.WatchLists)
                    .FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
                user.Name = userPutDTO.Name;
                user.LastName = userPutDTO.LastName;
                user.Email = userPutDTO.Email;
                user.UserName = userPutDTO.Username;
                user.Birthday = userPutDTO.Birthday;
                user.About = userPutDTO.About;
                user.Image = userPutDTO.Image;

                _context.WatchLists.RemoveRange(user.WatchLists);

                foreach (WatchListDTO watchListDTO in userPutDTO.WatchListDTOs)
                {
                    Movie movie = _context.Movies.Find(watchListDTO.MovieId);
                    user.WatchLists.Add(new WatchList()
                    {
                        UserId = user.Id,
                        User = user,
                        MovieId = movie.Id,
                        Movie = movie,
                        Status = watchListDTO.Status,
                        Score = watchListDTO.Score
                    });
                }
                await _userManager.UpdateAsync(user).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await UserExists(id).ConfigureAwait(false) == false) return null;
                else throw;
            }
            return userPutDTO;
        }

        public async Task<UserPatchDTO> PatchUser(string id, UserPatchDTO userPatchDTO)
        {
            if (userPatchDTO == null) { throw new ArgumentNullException(nameof(userPatchDTO)); }

            try
            {
                User user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
                _ = await _userManager.ChangePasswordAsync(user, userPatchDTO.CurrentPassword, userPatchDTO.NewPassword).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await UserExists(id).ConfigureAwait(false) == false) return null;
                else throw;
            }

            return userPatchDTO;
        }

        public async Task<UserDeleteDTO> DeleteUser(string id)
        {
            var user = await _context.Users
                .Include(u => u.WatchLists)
                .FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);

            if (user == null) return null;

            _context.WatchLists.RemoveRange(user.WatchLists);
            _context.Users.Remove(user);

            await _userManager.DeleteAsync(user).ConfigureAwait(false);

            return new UserDeleteDTO()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName
            };
        }

        public async Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null) { throw new ArgumentNullException(nameof(userRegisterDTO)); }

            User user = new User
            {
                Role = getRole(userRegisterDTO.Role),
                UserName = userRegisterDTO.Username,
                Name = userRegisterDTO.Name,
                LastName = userRegisterDTO.LastName,
                Email = userRegisterDTO.Email,
                Joined = DateTime.Now.ToString("dd\\/MM\\/yyyy")
            };

            var registerResult = await _userManager.CreateAsync(user, userRegisterDTO.Password).ConfigureAwait(false);

            if (registerResult.Succeeded)
            {
                userRegisterDTO.Id = user.Id;

                // Assign default user role to user
                //await AssignRole(user).ConfigureAwait(false);

                return userRegisterDTO;
            }

            return null;
        }

        public async Task<UserDTO> GetUserWithWatchList(string userId)
        {
            return await _context.Users.Include(u => u.WatchLists)
                .Select(u => new UserDTO()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    WatchListDTOs = u.WatchLists.Select(w => new WatchListDTO()
                    {
                        UserId = u.Id,
                        UserName = u.UserName,
                        MovieId = w.MovieId,
                        MovieName = w.Movie.Name,
                        Status = w.Status,
                        Score = w.Score
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId)
                .ConfigureAwait(false);
        }

        public async Task<WatchListPostDeleteDTO> DeleteWatchlistItem(string id, WatchListPostDeleteDTO watchListDeleteDTO)
        {
            if (watchListDeleteDTO == null) { throw new ArgumentNullException(nameof(watchListDeleteDTO)); }

            try
            {
                WatchList watchlist = await _context.WatchLists
                    .FirstOrDefaultAsync(w => w.UserId == id && w.MovieId == watchListDeleteDTO.MovieId).ConfigureAwait(false); 
                _context.WatchLists.Remove(watchlist);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await UserExists(id).ConfigureAwait(false) == false) return null;
                else throw;
            }
            return new WatchListPostDeleteDTO()
            {
                UserId = id,
                MovieId = watchListDeleteDTO.MovieId
            };
        }

        private async Task<bool> UserExists(string id)
        {
            return await _userManager.FindByIdAsync(id).ConfigureAwait(false) != null ? true : false;
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
