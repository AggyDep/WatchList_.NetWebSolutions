using API.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IUserService
    {
        Task<UserDTO> Authenticate(string userName, string password);
    }
}
