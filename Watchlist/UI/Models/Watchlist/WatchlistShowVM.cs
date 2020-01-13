using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Watchlist
{
    public class WatchlistShowVM
    {
        [JsonPropertyName("userId")]
        [Display(Name = "User id")]
        public string UserId { get; set; }

        [JsonPropertyName("movieId")]
        [Display(Name = "Movie id")]
        public int MovieId { get; set; }

        [JsonPropertyName("movieName")]
        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "The movie name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string MovieName { get; set; }

        [JsonPropertyName("image")]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessage = "This field must be a valid URL to an image.")]
        public string Image { get; set; }

        [JsonPropertyName("status")]
        [Display(Name = "Status")]
        [RegularExpression(@"^(?:Watching|Watched|PlanToWatch)", ErrorMessage = "The status must be Watching, Finished or PlanToWatch.")]
        public string Status { get; set; }

        [JsonPropertyName("score")]
        [Display(Name = "Score")]
        [RegularExpression(@"^(?:[0-9]|0[1-9]|10)$", ErrorMessage = "The score must be between 0 and 10.")]
        public int Score { get; set; }
    }
}
