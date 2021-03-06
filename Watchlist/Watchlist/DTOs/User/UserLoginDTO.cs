﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.User
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "The username is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9ÀàáÂâçÉéÈèÊêëïîÔô'-\.\s]+$", ErrorMessage = "Invalid characters used.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$",
            ErrorMessage = "The password must have a minimum lenght of 6 characters, have at least one upper case and one lower case character, " +
            "one digit and one special character.")]
        public string Password { get; set; }
    }
}
