﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Genre
{
    public class GenreVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("genreName")]
        [Display(Name = "Genre name")]
        [Required(ErrorMessage = "The genre name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string GenreName { get; set; }

        [JsonPropertyName("movieGenreDTOs")]
        public ICollection<MovieGenreVM> MovieGenreDTOs { get; set; }
    }
}
