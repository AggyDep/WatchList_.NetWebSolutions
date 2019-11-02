using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.User
{
    public class UserFriendDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int FriendId { get; set; }
        public string FriendName { get; set; }
    }
}
