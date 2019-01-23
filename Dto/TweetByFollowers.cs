using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinterProject.Dto
{
    public class TweetByFollowers
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string UserComment { get; set; }
        public string DateCreated { get; set; }

        public string UserName { get; set; }
    }
}
