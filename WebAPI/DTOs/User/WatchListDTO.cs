using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.DTOs
{
    public class WatchListDTO
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
