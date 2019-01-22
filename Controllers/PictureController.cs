using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinterProject.Models;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly WinterBreakContext _context;

        public PictureController(WinterBreakContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPicture([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pictureTable = await _context.Picture.FindAsync(id);

            if (pictureTable == null)
            {
                return NotFound();
            }

            return Ok(pictureTable);
        }

        [HttpPost]
        public async Task<IActionResult> PostPicture([FromBody] Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Picture.AddAsync(picture);
            await _context.SaveChangesAsync();

            return Ok(picture);

        }

    }
}