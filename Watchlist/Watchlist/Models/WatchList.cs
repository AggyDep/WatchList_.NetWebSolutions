using API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class WatchList
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public Enumerations.Status Status { get; set; }
        public int Score { get; set; }
    }
}
