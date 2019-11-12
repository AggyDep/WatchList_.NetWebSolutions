using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Models
{
    public class WatchList
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int SerieMovieId { get; set; }
        public SerieMovie SerieMovie { get; set; }
        [Required]
        public Enumerations.Status Status { get; set; }
        public int Score { get; set; }
        public int Episode { get; set; }
    }
}
