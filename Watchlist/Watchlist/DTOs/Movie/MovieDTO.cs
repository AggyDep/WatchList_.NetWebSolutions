using API.DTOs.Actor;
using API.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.DTOs.Movie
{
    public class MovieDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string Name { get; set; }

        [JsonPropertyName("synopsis")]
        public string Synopsis { get; set; }

        [JsonPropertyName("director")]
        [Required(ErrorMessage = "The director is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string Director { get; set; }

        [JsonPropertyName("aired")]
        [Required(ErrorMessage = "The airing date is required.")]
        [StringLength(10, ErrorMessage = "The length must be 10 characters.")]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "The airing date must be as DD/MM/YYYY.")]
        public string Aired { get; set; }

        [JsonPropertyName("duration")]
        [Required(ErrorMessage = "The duration is required.")]
        [StringLength(4, ErrorMessage = "The length must be 4 characters.")]
        [RegularExpression(@"([0-9]h[0-9][0-9])", ErrorMessage = "The duration must be as 0h00.")]
        public string Duration { get; set; }

        [JsonPropertyName("score")]
        [RegularExpression(@"^(?:[0-9]|0[1-9]|10)$", ErrorMessage = "The score must be between 0 and 10.")]
        public int Score { get; set; }

        [JsonPropertyName("image")]
        [DataType(DataType.ImageUrl, ErrorMessage = "This field must be a valid URL to an image.")]
        public string Image { get; set; }

        [JsonPropertyName("movieGenreDTOs")]
        public ICollection<MovieGenreDTO> MovieGenreDTOs { get; set; }
        [JsonPropertyName("movieActorDTOs")]
        public ICollection<MovieActorDTO> MovieActorDTOs { get; set; }
    }
}
