using API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.DTOs.User
{
    public class WatchListDTO
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("movieId")]
        public int MovieId { get; set; }

        [JsonPropertyName("status")]
        [RegularExpression(@"^(?:Watching|Watched|PlanToWatch)", ErrorMessage = "The status must be PlanToWatch, Watching or Watched.")]
        public string Status { get; set; }

        [JsonPropertyName("score")]
        [RegularExpression(@"^(?:[0-9]|0[1-9]|10)$", ErrorMessage = "The score must be between 0 and 10.")]
        public int Score { get; set; }
    }
}
