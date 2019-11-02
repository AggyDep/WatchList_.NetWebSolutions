using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.DTOs.User;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WatchListContext _context;

        public UserRepository(WatchListContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
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
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<UserDTO> GetUser(int id)
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
                .FirstOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<UserPostDTO> PostUser(UserPostDTO userPostDTO)
        {
            if (userPostDTO == null) { throw new ArgumentNullException(nameof(userPostDTO)); }

            var userResult = _context.Users.Add(new User()
            {
                Role = getRole(userPostDTO.Role),
                Name = userPostDTO.Name,
                LastName = userPostDTO.LastName,
                Email = userPostDTO.Email,
                Username = userPostDTO.Username,
                Password = userPostDTO.Password,
                Joined = DateTime.Now.ToString("dd\\/MM\\/yyyy")
            });

            await _context.SaveChangesAsync().ConfigureAwait(false);

            userPostDTO.Id = userResult.Entity.Id;

            return userPostDTO;
        }

        public async Task<UserPutDTO> PutUser(int id, UserPutDTO userPutDTO)
        {
            if (userPutDTO == null) { throw new ArgumentNullException(nameof(userPutDTO)); }

            try
            {
                User user = await _context.Users.Include(u => u.WatchLists).Include(u => u.UserFriends)
                    .FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
                user.Name = userPutDTO.Name;
                user.LastName = userPutDTO.LastName;
                user.Email = userPutDTO.Email;
                user.Username = userPutDTO.Username;
                user.Birthday = userPutDTO.Birthday;
                user.About = userPutDTO.About;
                user.Image = userPutDTO.Image;

                _context.WatchLists.RemoveRange(user.WatchLists);

                foreach (WatchListDTO watchListDTO in userPutDTO.WatchListDTOs)
                {
                    WatchList watchList = _context.WatchLists.Find(watchListDTO.SerieMovieId);
                    user.WatchLists.Add(new WatchList()
                    {
                        UserId = user.Id,
                        User = user,
                        SerieMovieId = watchList.SerieMovieId,
                        SerieMovie = watchList.SerieMovie,
                        Status = watchList.Status,
                        Score = watchList.Score,
                        Episode = watchList.Episode
                    });
                }

                _context.UserFriends.RemoveRange(user.UserFriends);

                foreach (UserFriendDTO userFriendDTO in userPutDTO.UserFriendsDTOs)
                {
                    User userFriend = _context.Users.Find(userFriendDTO.FriendId);
                    user.UserFriends.Add(new UserFriend()
                    {
                        UserId = user.Id,
                        User = user,
                        FriendId = userFriend.Id,
                        Friend = userFriend
                    });

                    //userFriend.UserFriends.Add(new UserFriend()
                    //{
                    //    UserId = userFriend.Id,
                    //    User = userFriend,
                    //    FriendId = user.Id,
                    //    Friend = user
                    //});
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id)) return null;
                else throw;
            }

            return userPutDTO;
        }

        public async Task<UserPatchDTO> PatchUser(int id, UserPatchDTO userPatchDTO)
        {
            if (userPatchDTO == null) { throw new ArgumentNullException(nameof(userPatchDTO)); }

            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
                user.Password = userPatchDTO.Password;

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id)) return null;
                else throw;
            }

            return userPatchDTO;
        }

        public async Task<UserDeleteDTO> DeleteUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.WatchLists)
                .Include(u => u.UserFriends)
                .FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);

            if (user == null) return null;

            _context.WatchLists.RemoveRange(user.WatchLists);
            _context.UserFriends.RemoveRange(user.UserFriends);
            _context.Users.Remove(user);

            await _context.SaveChangesAsync().ConfigureAwait(false);

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
