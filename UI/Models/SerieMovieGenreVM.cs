using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class SerieMovieGenreVM
    {
        public int SerieMovieId { get; set; }
        public string SerieMovieName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
