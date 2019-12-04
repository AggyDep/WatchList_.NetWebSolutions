using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.DTOs.User;

namespace WebAPI.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public int Age { get; set; }
        public string Birthday { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public string Joined { get; set; }
        public string Token { get; set; }
        public ICollection<WatchListDTO> WatchListDTOs { get; set; }
        public ICollection<UserFriendDTO> UserFriendsDTOs { get; set; }
    }
}
