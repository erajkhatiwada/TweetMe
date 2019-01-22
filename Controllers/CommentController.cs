using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var x =  _context.Comment.ToList();
            return x;
        }

        [HttpGet("byComment={id}")]
        public async Task<IActionResult> GetCommentByCommentId([FromRoute] int id)
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

        [HttpGet("byUser={id}")]
        public async Task<IActionResult> GetAllCommentFromUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment =  _context.Comment.Where(x => x.UserId == id).Select(x => new DtoUserComments()
            {
                CommentId = x.CommentId,
                UserComment = x.UserComment,
                DateCreated = _iComment.convertedDate(x.DateCreated)
            }).ToList();

            //var theActualDays = _iComment.convertedDate(comment.DateCreated);

            //var newComment = new Comment
            //{
            //    CommentId = comment.CommentId,
            //    UserId = comment.UserId,
            //    UserComment = comment.UserComment,
            //    DateCreated = theActualDays,
            //    User = comment.User
            //};

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
    }
}