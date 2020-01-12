using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Actor
{
    public class MovieActorVM
    {
        [JsonPropertyName("movieid")]
        [Display(Name = "Movie id")]
        public int MovieId { get; set; }

        [JsonPropertyName("movieName")]
        [Display(Name = "Movie name")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string MovieName { get; set; }

        [JsonPropertyName("actorId")]
        [Display(Name = "Actor id")]
        public int ActorId { get; set; }

        [JsonPropertyName("actorName")]
        [Display(Name = "Actor name")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string ActorName { get; set; }
    }
}
