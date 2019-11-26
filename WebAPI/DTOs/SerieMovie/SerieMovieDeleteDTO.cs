﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class SerieMovieDeleteDTO
    {
        public int Id { get; set; }
        [Required]
        public Boolean IsSerie { get; set; }
        [Required]
        public string Name { get; set; }
    }
}