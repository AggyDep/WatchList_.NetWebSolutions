using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
