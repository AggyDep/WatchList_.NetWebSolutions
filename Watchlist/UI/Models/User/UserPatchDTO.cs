using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models.User
{
    public class UserPatchDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("currentPassword")]
        [Display(Name = "Current password")]
        [Required(ErrorMessage = "The current password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})",
            ErrorMessage = "The current password must have a minimum lenght of 6 characters, have at least one upper case and one lower case character, " +
            "one digit and one special character.")]
        public string CurrentPassword { get; set; }

        [JsonPropertyName("newPassword")]
        [Display(Name = "New password")]
        [Required(ErrorMessage = "The new password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{6,})",
            ErrorMessage = "The new password must have a minimum lenght of 6 characters, have at least one upper case and one lower case character, " +
            "one digit and one special character.")]
        public string NewPassword { get; set; }
    }
}
