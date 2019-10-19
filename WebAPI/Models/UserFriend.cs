﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class UserFriend
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int FriendId { get; set; }
        public User Friend { get; set; }
    }
}
