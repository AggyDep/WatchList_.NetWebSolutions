using API.DTOs.Actor;
using API.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Movie
{
    public class MoviePostDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Aired { get; set; }
        [Required]
        public string Duration { get; set; }
        public ICollection<MovieGenreDTO> MovieGenreDTOs { get; set; }
        public ICollection<MovieActorDTO> MovieActorDTOs { get; set; }
    }
}
