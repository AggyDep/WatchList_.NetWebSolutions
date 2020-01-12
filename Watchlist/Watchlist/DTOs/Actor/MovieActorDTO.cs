using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Actor
{
    public class MovieActorDTO
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
    }
}
