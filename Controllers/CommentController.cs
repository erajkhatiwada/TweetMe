﻿using System;
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

            if (comment == null)
            {
                return NotFound();
            }

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

            var comment = _context.Comment.Where(x => x.UserId == id).Select(x => new DtoUserComments()
            {
                CommentId = x.CommentId,
                UserComment = x.UserComment,
                DateCreated = _iComment.convertedDate(x.DateCreated),
                CommentType = x.CommentType
            }).ToList();

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

            var comment = _context.Comment.Where(x => x.UserId == id).Where(x => x.CommentType == "public").Select(x => new DtoUserComments()
            {
                CommentId = x.CommentId,
                UserComment = x.UserComment,
                DateCreated = _iComment.convertedDate(x.DateCreated),
                CommentType = x.CommentType
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

            var comment = _context.Comment.Where(x => x.UserId == id).Where(x => x.CommentType != "private").Select(x => new DtoUserComments()
            {
                CommentId = x.CommentId,
                UserComment = x.UserComment,
                DateCreated = _iComment.convertedDate(x.DateCreated),
                CommentType = x.CommentType
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
        public async Task<IActionResult> DeleteComment([FromRoute] int id, [FromBody] Comment comments)
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
    }
}