using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(1000, ErrorMessage = "Synopsis length can't be more theb 1000")]
        public string Synopsis { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Aired { get; set; } //DateTime??
        [Required]
        public string Duration { get; set; }
        public float Score { get; set; }
        public string Image { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
        public ICollection<WatchList> WatchLists { get; set; }
    }
}
