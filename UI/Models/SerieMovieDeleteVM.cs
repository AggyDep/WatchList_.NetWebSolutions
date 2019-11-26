using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models
{
    public class SerieMovieDeleteVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("isSerie")]
        [Required]
        public Boolean IsSerie { get; set; }
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }
    }
}
