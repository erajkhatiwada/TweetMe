using System;
using System.Collections.Generic;

namespace WinterProject.Models
{
    public partial class Follow
    {
        public int FollowId { get; set; }
        public int UserId { get; set; }
        public int FollowedUserId { get; set; }

        public User User { get; set; }
    }
}
