﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Actor
{
    public class ActorVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "The full name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string FullName { get; set; }

        [JsonPropertyName("birthday")]
        [Display(Name = "Birthday")]
        [StringLength(10, ErrorMessage = "The length must be 10 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "The birthday must be as DD/MM/YYYY.")]
        public string Birthday { get; set; }

        [JsonPropertyName("biography")]
        [Display(Name = "Biography")]
        public string Biography { get; set; }

        [JsonPropertyName("website")]
        [Display(Name = "Website")]
        [DataType(DataType.Url, ErrorMessage = "The website must be a valid URL.")]
        public string Website { get; set; }

        [JsonPropertyName("image")]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessage = "This field must be a valid URL to an image.")]
        public string Image { get; set; }

        [JsonPropertyName("movieActorDTOs")]
        public ICollection<MovieActorVM> MovieActorDTOs { get; set; }
    }
}
