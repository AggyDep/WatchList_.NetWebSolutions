using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UI.Data;

namespace UI.Models
{
    public class WatchListVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int SerieMovieId { get; set; }
        public string SerieMovieName { get; set; }
        public Enumerations.Status Status { get; set; }
        public int Score { get; set; }
        public int Episode { get; set; }
    }
}
