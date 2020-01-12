using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Genre
{
    public class MovieGenreDTO
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
