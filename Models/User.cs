using System;
using System.Collections.Generic;

namespace WinterProject.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Follow = new HashSet<Follow>();
            Like = new HashSet<Like>();
            Picture = new HashSet<Picture>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Follow> Follow { get; set; }
        public ICollection<Like> Like { get; set; }
        public ICollection<Picture> Picture { get; set; }
    }
}
