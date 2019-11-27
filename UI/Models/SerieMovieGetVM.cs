using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models
{
    public class SerieMovieGetVM
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("isSerie")]
        [Required]
        public Boolean IsSerie { get; set; }
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }
        [JsonPropertyName("episode")]
        public int Episode { get; set; }
        [JsonPropertyName("season")]
        public int Season { get; set; }
        [JsonPropertyName("synopsis")]
        public string Synopsis { get; set; }
        [JsonPropertyName("director")]
        [Required]
        public string Director { get; set; }
        [JsonPropertyName("duration")]
        [Required]
        public string Duration { get; set; }
        [JsonPropertyName("score")]
        public float Score { get; set; }
        [JsonPropertyName("ranking")]
        public int Ranking { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
