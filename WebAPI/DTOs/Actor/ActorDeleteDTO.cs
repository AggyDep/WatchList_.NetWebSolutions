using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class ActorDeleteDTO
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Birthday { get; set; }
    }
}
