using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class SerieMovieGenreDTO
    {
        public int SerieMovieId { get; set; }
        public string SerieMovieName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
