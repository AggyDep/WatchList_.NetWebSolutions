﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UI.Models.Watchlist;

namespace UI.Models.User
{
    public class UserVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("role")]
        [RegularExpression(@"^(?:user|User|U|admin|Admin|A)", ErrorMessage = "The role can only be u, User, user or a, Admin, admin.")]
        public int Role { get; set; }

        [JsonPropertyName("username")]
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The username is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string Username { get; set; }

        [JsonPropertyName("name")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string Name { get; set; }

        [JsonPropertyName("lastName")]
        [Display(Name = "Last name")]
        [Required(ErrorMessage = "The lastname is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 60 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "An email address is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "The email adress is invalid.")]
        public string Email { get; set; }

        [JsonPropertyName("birthday")]
        [Display(Name = "Birthday")]
        [StringLength(10, ErrorMessage = "The length must be 10 characters.")]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "The birthday must be as DD/MM/YYYY.")]
        public string Birthday { get; set; }

        [JsonPropertyName("about")]
        public string About { get; set; }

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

        [JsonPropertyName("token")]
        [Display(Name = "Token")]
        public string Token { get; set; }

        [JsonPropertyName("watchListDTOs")]
        public ICollection<WatchlistVM> WatchListDTOs { get; set; }
    }
}
