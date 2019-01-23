using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinterProject.Models;
using WinterProject.Data;
using System.Collections;
using WinterProject.Dto;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUserController : ControllerBase
    {
        private readonly WinterBreakContext _context;
        private readonly ICheck _check;
        private readonly IComment _comment;

        public FollowUserController(WinterBreakContext context, ICheck check, IComment comment)
        {
            _context = context;
            _check = check;
            _comment = comment;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllResults([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var allData = _context.Follow.Where(x => x.UserId == id).ToList();

            return Ok(allData);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody] Follow follow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = follow.UserId;
            var followId = follow.FollowedUserId;

            if (await _check.UserFollowed(userId, followId))
            {
                return BadRequest("Already Followed");
            }


            await _context.Follow.AddAsync(follow);
            await _context.SaveChangesAsync();

            return Ok(follow);
        }

        [HttpPost("check")]
        public async Task<IActionResult> Check([FromBody] Follow follow)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = follow.UserId;
            var followId = follow.FollowedUserId;

            if (await _check.UserFollowed(userId, followId))
            {
                return Ok("true");
            }


            return Ok("false");
        }

        [HttpGet("allTweets={id}")]
        public async Task<IActionResult> GetAllTweets([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ArrayList list = new ArrayList();

            ArrayList allTweets = new ArrayList();

            var allResult = _context.Follow.Where(x => x.UserId == id).Select(x => list.Add(x.FollowedUserId)).ToList();

            int[] temp = new int[list.Count];

            for(int i = 0; i < temp.Length; i++)
            {
                temp[i] = (int)list[i];
            }

            for(int i = 0; i < temp.Length; i++)
            {
                var x = _context.Comment.Where(m => m.UserId == temp[i]).Select(m => allTweets.Add(new TweetByFollowers() {
                    CommentId = m.CommentId,
                    UserId = m.UserId,
                    UserComment = m.UserComment,
                    DateCreated = _comment.convertedDate(m.DateCreated),
                    UserName = m.User.Username
                })).ToList();

            }

            return Ok(allTweets);
        }

    }
}