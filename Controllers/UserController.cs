using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WinterProject.Data;
using WinterProject.Models;
using System.Security.Claims;
using Newtonsoft.Json;
using WinterProject.Dto;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WinterBreakContext _context;
        private readonly ICheck _check;

        public UserController(WinterBreakContext context, ICheck check)
        {
            _context = context;
            _check = check;
        }

     

        //[Authorize]
        //[HttpGet]
        //public async Task<IActionResult> GetUser([FromHeader (Name = "Authorization")] string token)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var bearer = token.Substring(0, 7).Trim();
        //    if (token == null || !bearer.Equals("Bearer"))
        //    {
        //        return Unauthorized();
        //    }

        //    var actual_token = token.Replace("Bearer","").Trim();

        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(actual_token);
        
        //    var x = jsonToken.ToString();

        //    var json = @"{""key1"":1,""key2"":""value2"", ""object1"":{""property1"":""value1"",""property2"":[2,3,4,5,6,7]}}";
        //    var parsedObject = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

        //    return Ok(parsedObject);
        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var x =  await _context.User.FindAsync(id);
            if (x == null)
            {
                return NotFound();
            }

            var newUser = new UserById()
            {
                Username = x.Username,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Picture = x.Picture,
                Comment = x.Comment
            };

            return Ok(newUser);
        }
        
    }

    
} 