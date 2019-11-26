using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class WatchListPostDeleteVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int SerieMovieId { get; set; }
        public string SerieMovieName { get; set; }
    }
}
