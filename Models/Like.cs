using System;
using System.Collections.Generic;

namespace WinterProject.Models
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }

        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
