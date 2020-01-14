using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.DTOs.User
{
    public class WatchListPostDeleteDTO
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("movieId")]
        public int MovieId { get; set; }
    }
}
