using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public interface IUserService
    {
        Task<UserDTO> Authenticate(string userName, string password);
    }
}
