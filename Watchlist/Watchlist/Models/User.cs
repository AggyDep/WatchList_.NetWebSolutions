using API.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User : IdentityUser
    {
        public Enumerations.Role Role { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        [Required]
        public string Joined { get; set; }

        public ICollection<WatchList> WatchLists { get; set; }
    }
}
