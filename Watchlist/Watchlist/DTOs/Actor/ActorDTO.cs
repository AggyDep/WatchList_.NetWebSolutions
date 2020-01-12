using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Actor
{
    public class ActorDTO
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Birthday { get; set; }
        public string Biography { get; set; }
        public string Website { get; set; }
        public string Image { get; set; }
        public ICollection<MovieActorDTO> MovieActorDTOs { get; set; }
    }
}
