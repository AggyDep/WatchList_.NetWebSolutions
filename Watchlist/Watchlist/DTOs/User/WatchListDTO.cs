using API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.User
{
    public class WatchListDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public Enumerations.Status Status { get; set; }
        public int Score { get; set; }
    }
}
