using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinterProject.Models;

namespace WinterProject.Dto
{
    public class DtoUserCommentsWithUsername
    {
        public DtoUserCommentsWithUsername()
        {
            Like = new HashSet<DtoLike>();
        }

        public int CommentId { get; set; }
        public string UserComment { get; set; }
        public string DateCreated { get; set; }
        public string CommentType { get; set; }
        public string LikedByCurrentUser { get; set; }

        public ICollection<DtoLike> Like { get; set; }
    }
}
