using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UI.Data;

namespace UI.Models.Watchlist
{
    public class WatchlistVM
    {
        [JsonPropertyName("userId")]
        [Display(Name = "User id")]
        public string UserId { get; set; }

        [JsonPropertyName("movieId")]
        [Display(Name = "Movie id")]
        public int MovieId { get; set; }

        [JsonPropertyName("status")]
        [Display(Name = "Status")]
        [RegularExpression(@"^(?:Watching|Watched|PlanToWatch)", ErrorMessage = "The status must be PlanToWatch, Watching or Watched.")]
        public string Status { get; set; }

        [JsonPropertyName("score")]
        [Display(Name = "Score")]
        [RegularExpression(@"^(?:[0-9]|0[1-9]|10)$", ErrorMessage = "The score must be between 0 and 10.")]
        public int Score { get; set; }
    }
}
