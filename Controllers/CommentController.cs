using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WinterProject.Data;
using WinterProject.Dto;
using WinterProject.Models;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly WinterBreakContext _context;
        private readonly IComment _iComment;


        public CommentController(WinterBreakContext context, IComment icomment)
        {
            _context = context;
            _iComment = icomment;
        }

        [HttpGet]
        public IEnumerable<Comment> GetComment()
        {
            var x =  _context.Comment.Include(y => y.Like).ToList();
            return x;
        }

        [HttpPost("byComment={id}")]
        public async Task<IActionResult> GetCommentByCommentId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment =  _context.Comment.Include(o => o.Like).FirstOrDefault(y => y.CommentId == id);


            if (comment == null)
            {
                return NotFound();
            }

            var theActualDays = _iComment.convertedDate(comment.DateCreated);

            var newComment = new DtoUserCommentsWithUsername()
            {
                CommentId = comment.CommentId,
                UserComment = comment.UserComment,
                DateCreated = theActualDays,
                Like = await usernameFinder(comment.Like),
                CommentType = comment.CommentType
            };

            return Ok(newComment);
        }

        [HttpGet("byUser={id}")]
        public async Task<IActionResult> GetAllCommentFromUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = _context.Comment.Where(x => x.UserId == id).Include(y => y.Like).ToList().Select(x => new DtoUserComments()
            {
                CommentId = x.CommentId,
                UserComment = x.UserComment,
                DateCreated = _iComment.convertedDate(x.DateCreated),
                CommentType = x.CommentType,
                Like = x.Like,
                LikedByCurrentUser = LikedByUser(id,x.CommentId,x.Like)
            }).ToList().OrderByDescending(x => x.CommentId);
            return Ok(comment);
        }

        /* when not followed */
        [HttpGet("byUserSearch={id}")]
        public async Task<IActionResult> GetAllCommentFromUserSearch([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = _context.Comment.Where(x => x.UserId == id).Include(y => y.Like).Where(x => x.CommentType == "public").Select(x => new DtoUserComments()
            {
                CommentId = x.CommentId,
                UserComment = x.UserComment,
                DateCreated = _iComment.convertedDate(x.DateCreated),
                CommentType = x.CommentType,
                Like = x.Like
            }).ToList();

            return Ok(comment);
        }

        /* when followed */
        [HttpGet("byUserSearchFollowed={id}")]
        public async Task<IActionResult> GetAllCommentFromUserSearchFollowed([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = _context.Comment.Where(x => x.UserId == id).Include(y => y.Like).Where(x => x.CommentType != "private").Select(x => new DtoUserComments()
            {
                CommentId = x.CommentId,
                UserComment = x.UserComment,
                DateCreated = _iComment.convertedDate(x.DateCreated),
                CommentType = x.CommentType,
                Like = x.Like

            }).ToList();

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var x = await _context.Comment.FindAsync(id);
            if (x == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(x);
            await _context.SaveChangesAsync();
            return Ok(x);

        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditComment([FromRoute] int id, [FromBody] Comment comments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comments.CommentId)
            {
                return BadRequest();
            }

            try
            {
                _context.Comment.Update(comments);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(comments);

        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.CommentId == id);
        }

        private string LikedByUser(int userId, int commentId, ICollection<Like> arr)
        {
            foreach (var x in arr)
            {
                if (x.UserId == userId && x.CommentId == commentId)
                {
                    return "true";
                }
            }

            return "false";
        }

        private async Task<ICollection<DtoLike>> usernameFinder(ICollection<Like> like)
        {
            ICollection<DtoLike> list = new List<DtoLike>();
            foreach (var likes in like)
            {
                var m = new DtoLike()
                {
                    CommentId = likes.CommentId,
                    LikeId = likes.LikeId,
                    Comment = likes.Comment,
                    UserId = likes.UserId,
                    User = likes.User,
                    UserName = await userNameFindingMethod(likes.UserId)
                };
                list.Add(m);
            }

            return list;
        }

        private async Task<string> userNameFindingMethod(int id)
        {
            var x = await _context.User.FirstOrDefaultAsync(y => y.UserId == id);
           
            return x.Username;
        }
    }
}