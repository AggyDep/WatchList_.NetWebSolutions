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

        [JsonPropertyName("userName")]
        [Display(Name = "Username")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string Username { get; set; }

        [JsonPropertyName("movieId")]
        [Display(Name = "Movie id")]
        public int MovieId { get; set; }

        [JsonPropertyName("movieName")]
        [Display(Name = "Movie name")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string MovieName { get; set; }
    }
}
