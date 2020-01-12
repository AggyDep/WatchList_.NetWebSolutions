using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.User
{
    public class WatchListPostDeleteDTO
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
    }
}
