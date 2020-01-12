using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.User
{
    public class WatchListPostDeleteDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
    }
}
