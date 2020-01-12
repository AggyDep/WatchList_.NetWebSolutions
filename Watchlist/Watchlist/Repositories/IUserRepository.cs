using API.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserGetDTO>> GetUsers();
        Task<UserDTO> GetUserDetails(string id);
        Task<UserPostDTO> PostUser(UserPostDTO userPostDTO);
        Task<UserPutDTO> PutUser(string id, UserPutDTO userPutDTO);
        Task<UserPatchDTO> PatchUser(string id, UserPatchDTO userPatchDTO);
        Task<UserDeleteDTO> DeleteUser(string id);
        Task<UserRegisterDTO> RegisterUser(UserRegisterDTO userRegisterDTO);
        Task<UserDTO> GetUserWithWatchList(string userId);
        Task<WatchListPostDeleteDTO> DeleteWatchlistItem(string id, WatchListPostDeleteDTO watchListDeleteDTO);
    }
}
