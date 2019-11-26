using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models
{
    public class GenreVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("genreName")]
        [Required]
        public string GenreName { get; set; }
        [JsonPropertyName("serieMovieGenreDTOs")]
        public ICollection<SerieMovieGenreVM> SerieMovieGenreDTOs { get; set; }
    }
}
