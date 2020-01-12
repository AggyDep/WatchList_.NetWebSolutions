using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Movie
{
    public class MovieGetDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Synopsis { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Duration { get; set; }
        public float Score { get; set; }
        public string Image { get; set; }
    }
}
