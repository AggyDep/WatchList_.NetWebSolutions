using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class SerieMovieGenre
    {
        public int SerieMovieId { get; set; }
        public SerieMovie SerieMovie { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
