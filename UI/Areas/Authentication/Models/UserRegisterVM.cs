using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Authentication.Models
{
    public class UserRegisterVM
    {
        public string Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "The username is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 50 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string Username { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 50 characters.")]
        [RegularExpression(@"^([A-Z][a-z]+)\s([A-Z][a-zA-Z-]+)$", ErrorMessage = "Invalid characters are used.")]
        public string Name { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "The last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 50 characters.")]
        [RegularExpression(@"^([A-Z][a-z]+)\s([A-Z][a-zA-Z-]+)$", ErrorMessage = "Invalid characters are used.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "An email address is required.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$", ErrorMessage = "The email adress is invalid.")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password.")]
        public string Password { get; set; }
    }
}
