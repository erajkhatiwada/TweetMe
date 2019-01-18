using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WinterProject.Data;
using WinterProject.Dto;
using WinterProject.Models;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WinterBreakContext _context;
        private readonly IAuth _auth;
        private readonly ICheck _check;
        private readonly IConfiguration _config;


        public AuthController(WinterBreakContext context, IAuth auth, ICheck check, IConfiguration config)
        {
            _context = context;
            _auth = auth;
            _check = check;
            _config = config;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _check.UsernameExist(user.Username))
            {
                if (await _check.EmailExist(user.Email))
                {
                    return BadRequest("Username and Email is already taken");
                }
                return BadRequest("Username already exists");
            }

            else if (await _check.EmailExist(user.Email))

            {
                if (await _check.UsernameExist(user.Username))
                {
                    return BadRequest("Username and Email is already taken");
                }
                return BadRequest("Email already exists");

            }

            var hashedPassword = _auth.HashPassword(user.Password);

            var newUser = new User
            {
                Username = user.Username.ToLower(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = hashedPassword
            };

            await _context.User.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var newPostedUser = await _context.User.FirstOrDefaultAsync(x => x.Username == user.Username);
            return Ok(newPostedUser);
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] DtoLogin dtoUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginUsernameOrEmail = dtoUser.UsernameOrEmail.ToLower();
            var loginPassword = _auth.HashPassword(dtoUser.Password);

            if ( ! await _auth.Login(loginUsernameOrEmail, loginPassword))
            {
                return Unauthorized();
            }

            var loggedInUser = await _context.User.FirstAsync(x => (x.Username == loginUsernameOrEmail) || (x.Email == loginUsernameOrEmail));


            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,  loggedInUser.UserId.ToString()),
                new Claim(ClaimTypes.Name, loggedInUser.Username)
            };

            //secret key generator
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.
                GetSection("AppSettings:Token").Value));

            //generate signing credentials
            //use algorithm to hash the above secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //security (Create token) descriptor(contains: 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                Id = loggedInUser.UserId,
                Username = loggedInUser.Username,
                Token = tokenHandler.WriteToken(token)
            });
        }

    }
}