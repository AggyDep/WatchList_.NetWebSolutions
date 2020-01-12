﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Genre
{
    public class GenrePostDTO
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }
    }
}