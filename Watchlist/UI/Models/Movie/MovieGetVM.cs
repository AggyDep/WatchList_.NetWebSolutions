using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.Movie
{
    public class MovieGetVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string Name { get; set; }

        [JsonPropertyName("synopsis")]
        [Display(Name = "Synopsis")]
        public string Synopsis { get; set; }

        [JsonPropertyName("director")]
        [Display(Name = "Director")]
        [Required(ErrorMessage = "The director is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string Director { get; set; }

        [JsonPropertyName("duration")]
        [Display(Name = "Duration")]
        [Required(ErrorMessage = "The duration is required.")]
        [StringLength(4, ErrorMessage = "The length must be 4 characters.")]
        [RegularExpression(@"([0-9]h[0-9][0-9])", ErrorMessage = "The duration must be as 0h00.")]
        public string Duration { get; set; }

        [JsonPropertyName("score")]
        [Display(Name = "Score")]
        [RegularExpression(@"^(?:[0-9]|0[1-9]|10)$", ErrorMessage = "The score must be between 0 and 10.")]
        public int Score { get; set; }

        [JsonPropertyName("image")]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessage = "This field must be a valid URL to an image.")]
        public string Image { get; set; }
    }
}
