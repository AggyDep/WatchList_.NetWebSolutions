using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Actor
    {
        public int Id  { get; set; }
        [Required]
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Birthday { get; set; } //DateTime??
        public string Biography { get; set; }
        public string Website { get; set; }
        public string Image { get; set; }

        public ICollection<SerieMovieActor> SerieMovieActors { get; set; }
    }
}
