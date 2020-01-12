using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.User
{
    public class UserPatchDTO
    {
        public string Id { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
