using API.DTOs.Actor;
using API.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Movie
{
    public class MovieDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Synopsis { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Aired { get; set; } //DateTime??
        [Required]
        public string Duration { get; set; }
        public float Score { get; set; }
        public string Image { get; set; }
        public ICollection<MovieGenreDTO> MovieGenreDTOs { get; set; }
        public ICollection<MovieActorDTO> MovieActorDTOs { get; set; }
    }
}
