using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinterProject.Models;
using WinterProject.Data;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowUserController : ControllerBase
    {
        private readonly WinterBreakContext _context;
        private readonly ICheck _check;

        public FollowUserController(WinterBreakContext context, ICheck check)
        {
            _context = context;
            _check = check;
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

    }
}