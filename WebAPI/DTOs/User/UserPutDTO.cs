using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs.User;

namespace WebAPI.DTOs
{
    public class UserPutDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        public string Birthday { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public ICollection<WatchListDTO> WatchListDTOs { get; set; }
        public ICollection<UserFriendDTO> UserFriendsDTOs { get; set; }
    }
}
