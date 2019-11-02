﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public Enumerations.Role Role { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username  { get; set; }
        [Required]
        public string Password { get; set; }
        public int Age { get; set; }
        public string Birthday  { get; set; } //DateTime??
        public string About { get; set; }
        public  string Image { get; set; }
        [Required]
        public  string Joined { get; set; } //DateTime??

        public ICollection<WatchList> WatchLists { get; set; }

        public ICollection<UserFriend> UserFriends { get; set; }
        public ICollection<UserFriend> FriendUsers { get; set; }
    }
}
