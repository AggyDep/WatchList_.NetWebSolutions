using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UI.Areas.Authentication.Models
{
    public class UserLoginVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "The username is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 50 characters.")]
        [RegularExpression(@"^([a-zA-Z]+)[0-9]*\.*[a-zA-Z0-9]+$|^[a-zA-Z]+[0-9]*$", ErrorMessage = "Invalid characters used.")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password.")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
