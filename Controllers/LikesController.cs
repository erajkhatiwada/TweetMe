using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WinterProject.Models;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly WinterBreakContext _context;

        public LikesController(WinterBreakContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Like> GetLikes()
        {
            return _context.Like.Include(x => x.Comment).Include(y => y.User).ToList();
        }

        [HttpPost("likeTweet")]
        public async Task<IActionResult> PostLikes([FromBody] Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wait = await checkIfLiked(like.UserId, like.CommentId);
            if (wait == true)
            {
                return BadRequest("Already in the database");
            }
            else
            {
                _context.Like.Add(like);
                await _context.SaveChangesAsync();
                return Ok(like);
            }

        }

        [HttpPost("removelike")]
        public async Task<IActionResult> RemoveLike([FromBody] Like like )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var findComment = await _context.Like.FirstOrDefaultAsync(x =>
                x.UserId == like.UserId && x.CommentId == like.CommentId);

            if (findComment == null)
            {
                return NotFound("Cannot remove like");
            }

            _context.Like.Remove(findComment);
            await _context.SaveChangesAsync();
            return Ok(findComment);
        }

        public async Task<bool> checkIfLiked(int userId, int commentId)
        {
            if (await _context.Like.AnyAsync(x => x.UserId == userId && x.CommentId == commentId)) 
            {
                return true;
            }

            return false;
        }
    }
}