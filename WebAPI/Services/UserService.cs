using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<AppSettings> appSettings, SignInManager<User> signInManager, UserManager<User> userManager, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Authenticate(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false).ConfigureAwait(false);

            // return null if user failed to authenticate
            if (!result.Succeeded)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserDTO userDTO = await _userRepository.GetUser(user.Id).ConfigureAwait(false);

            userDTO.Token = tokenHandler.WriteToken(token);

            return userDTO;
        }
    }
}
