using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class UserPatchVM
    {
        public string Id { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
