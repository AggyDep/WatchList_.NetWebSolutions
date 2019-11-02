using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class GenrePutDeleteDTO
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }
        public ICollection<SerieMovieGenreDTO> SerieMovieGenreDTOs { get; set; }
    }
}
