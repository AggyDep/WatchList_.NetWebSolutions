using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UI.Models.Watchlist;

namespace UI.Models.User
{
    public class UserGetVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The username is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string Username { get; set; }

        [JsonPropertyName("image")]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessage = "This field must be a valid URL to an image.")]
        public string Image { get; set; }

        [JsonPropertyName("joined")]
        [Display(Name = "Joined")]
        [Required(ErrorMessage = "The joining date is required.")]
        [StringLength(10, ErrorMessage = "The length must be 10 characters.")]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "The joining date must be as DD/MM/YYYY.")]
        public string Joined { get; set; }

        [JsonPropertyName("watchListDTOs")]
        public ICollection<WatchlistVM> WatchListDTOs { get; set; }
    }
}
