using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        [Required]
        public string Joined { get; set; }
        public ICollection<WatchListDTO> WatchListDTOs { get; set; }
    }
}
