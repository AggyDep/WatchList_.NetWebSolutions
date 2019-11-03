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
        [StringLength(250)]
        public string FullName { get; set; }
        public int Age { get; set; }
        [StringLength(20)]
        public string Birthday { get; set; } //DateTime??
        [StringLength(3000, ErrorMessage ="Biography length can't be more theb 3000")]
        public string Biography { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        public string Image { get; set; }

        public ICollection<SerieMovieActor> SerieMovieActors { get; set; }
    }
}
