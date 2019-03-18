using System;
using System.Collections.Generic;

namespace WinterProject.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Like = new HashSet<Like>();
        }

        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string UserComment { get; set; }
        public string DateCreated { get; set; }
        public string CommentType { get; set; }

        public User User { get; set; }
        public ICollection<Like> Like { get; set; }
    }
}
