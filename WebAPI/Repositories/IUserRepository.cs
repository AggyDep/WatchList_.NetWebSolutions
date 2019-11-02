using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetUser(int id);
        Task<UserPostDTO> PostUser(UserPostDTO userPostDTO);
        Task<UserPutDTO> PutUser(int id, UserPutDTO userPutDTO);
        Task<UserPatchDTO> PatchUser(int id, UserPatchDTO userPatchDTO);
        Task<UserDeleteDTO> DeleteUser(int id);
    }
}
