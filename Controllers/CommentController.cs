using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinterProject.Data;
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
            var x =  _context.Comment.ToList();
            return x;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.Comment.FindAsync(id);

            var theActualDays = _iComment.convertedDate(comment.DateCreated);

            var newComment = new Comment
            {
                CommentId = comment.CommentId,
                UserId = comment.UserId,
                UserComment = comment.UserComment,
                DateCreated = theActualDays,
                User = comment.User
            };

            return Ok(newComment);
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
    }
}