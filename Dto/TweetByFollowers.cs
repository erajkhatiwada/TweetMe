﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinterProject.Models;

namespace WinterProject.Dto
{
    public class TweetByFollowers
    {
        public TweetByFollowers()
        {
            Like = new HashSet<Like>();
        }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string UserComment { get; set; }
        public string DateCreated { get; set; }
        public string CommentType { get; set; }

        public string UserName { get; set; }

        public ICollection<Like> Like { get; set; }
    }
}
