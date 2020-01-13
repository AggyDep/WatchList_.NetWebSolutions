using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Watchlist
{
    public class WatchlistPostDeleteVM
    {
        [JsonPropertyName("userId")]
        [Display(Name = "User id")]
        public string UserId { get; set; }

        [JsonPropertyName("movieId")]
        [Display(Name = "Movie id")]
        public int MovieId { get; set; }
    }
}
