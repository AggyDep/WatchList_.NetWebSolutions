using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class UserPatchDTO
    {
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
