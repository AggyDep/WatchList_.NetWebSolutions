using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Genre
{
    public class MovieGenreVM
    {
        [JsonPropertyName("movieId")]
        [Display(Name = "Movie id")]
        public int MovieId { get; set; }

        [JsonPropertyName("movieName")]
        [Display(Name = "Movie name")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string MovieName { get; set; }

        [JsonPropertyName("genreId")]
        [Display(Name = "Genre id")]
        public int GenreId { get; set; }

        [JsonPropertyName("genreName")]
        [Display(Name = "Genre name")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string GenreName { get; set; }
    }
}
