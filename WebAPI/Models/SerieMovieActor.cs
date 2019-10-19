using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class SerieMovieActor
    {
        public int SerieMovieId { get; set; }
        public SerieMovie SerieMovie { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
