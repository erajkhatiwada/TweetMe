using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinterProject.Models;

namespace WinterProject.Dto
{
    public class DtoLike
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
