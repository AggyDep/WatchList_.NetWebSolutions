using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models
{
    public class ActorDeleteVM
    {
        [JsonPropertyName("id")]

        public int Id { get; set; }
        [JsonPropertyName("fullName")]
        [Required]
        public string FullName { get; set; }
        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }
    }
}
