using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class SerieMovieActorDTO
    {
        public int SerieMovieId { get; set; }
        public string SerieMovieName { get; set; }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
    }
}
