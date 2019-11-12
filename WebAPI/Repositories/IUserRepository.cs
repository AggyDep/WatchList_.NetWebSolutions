using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> GetUser(string id);
        Task<UserPostDTO> PostUser(UserPostDTO userPostDTO);
        Task<UserPutDTO> PutUser(string id, UserPutDTO userPutDTO);
        Task<UserPatchDTO> PatchUser(string id, UserPatchDTO userPatchDTO);
        Task<UserDeleteDTO> DeleteUser(string id);
    }
}
