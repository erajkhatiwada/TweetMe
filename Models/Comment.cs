using System;
using System.Collections.Generic;

namespace WinterProject.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string UserComment { get; set; }
        public string DateCreated { get; set; }

        public User User { get; set; }
    }
}
