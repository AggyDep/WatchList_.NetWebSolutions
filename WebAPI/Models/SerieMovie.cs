using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class SerieMovie
    {
        public int Id { get; set; }
        [Required]
        public Boolean IsSerie { get; set; }
        [Required]
        public string Name { get; set; }
        public int Episode { get; set; }
        public int Season { get; set; }
        [StringLength(1000, ErrorMessage = "Synopsis length can't be more theb 1000")]
        public string Synopsis { get; set; }
        public string Producer { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Aired { get; set; } //DateTime??
        [Required]
        public string Duration { get; set; }
        public float Score { get; set; }
        public int Ranking { get; set; }
        public int Member { get; set; }
        public string Image { get; set; }

        public ICollection<SerieMovieGenre> SerieMovieGenres { get; set; }
        public ICollection<SerieMovieActor> SerieMovieActors { get; set; }
        public ICollection<WatchList> WatchLists { get; set; }
    }
}
