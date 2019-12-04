using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UI.Models
{
    public class UserGetVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("username")]
        [Required]
        public string Username { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("joined")]
        [Required]
        public string Joined { get; set; }
        [JsonPropertyName("watchListDTOs")]
        public ICollection<WatchListVM> WatchListDTOs { get; set; }
        [JsonPropertyName("userFriendsDTOs")]
        public ICollection<UserFriendVM> UserFriendsDTOs { get; set; }
    }
}
