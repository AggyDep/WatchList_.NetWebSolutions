using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class WatchListPostDeleteDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int SerieMovieId { get; set; }
        public string SerieMovieName { get; set; }
    }
}
