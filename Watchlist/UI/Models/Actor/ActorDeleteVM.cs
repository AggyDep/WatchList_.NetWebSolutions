using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Actor
{
    public class ActorDeleteVM
    {
        [JsonPropertyName("id")]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "The id is required.")]
        public int Id { get; set; }

        [JsonPropertyName("fullName")]
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "The full name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string FullName { get; set; }
    }
}
