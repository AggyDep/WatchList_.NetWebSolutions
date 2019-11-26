using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models
{
    public class UserVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("username")]
        [Required]
        public string Username { get; set; }
        [JsonPropertyName("about")]
        public string About { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("joined")]
        [Required]
        public string Joined { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("watchListDTOs")]
        public ICollection<WatchListVM> WatchLists { get; set; }
        [JsonPropertyName("userFriendsDTOs")]
        public ICollection<UserFriendVM> UserFriends { get; set; }
    }
}
