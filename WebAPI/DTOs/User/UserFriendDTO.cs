using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.User
{
    public class UserFriendDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FriendId { get; set; }
        public string FriendName { get; set; }
    }
}
