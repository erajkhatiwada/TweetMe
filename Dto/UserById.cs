using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinterProject.Models;

namespace WinterProject.Dto
{
    public class UserById
    {
        public UserById()
        {
            Comment = new HashSet<Comment>();
            Picture = new HashSet<Picture>();
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Picture> Picture { get; set; }
    }
}
