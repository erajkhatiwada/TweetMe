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
    }
}