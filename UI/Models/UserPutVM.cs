using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models
{
    public class UserPutVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }
        [JsonPropertyName("lastName")]
        [Required]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; }
        [JsonPropertyName("username")]
        [Required]
        public string Username { get; set; }
        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }
        [JsonPropertyName("about")]
        public string About { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("watchListDTOs")]
        public ICollection<WatchListVM> WatchListDTOs { get; set; }
        [JsonPropertyName("userFriendsDTOs")]
        public ICollection<UserFriendVM> UserFriendsDTOs { get; set; }
    }
}
