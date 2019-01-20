using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.SqlServer.Scaffolding.Internal;
using WinterProject.Dto;
using WinterProject.Models;

namespace WinterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSearchController : ControllerBase
    {
        private readonly WinterBreakContext _context;

        public UserSearchController(WinterBreakContext context)
        {
            _context = context;
        }

        [HttpGet("query={q}")]
        public IActionResult GetUserFromSearch([FromRoute] string q)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var searchQuery = _context.User.Where(x => x.Username.StartsWith(q)).Select(y => new SearchResult() { Username = y.Username, FirstName = y.FirstName, LastName = y.LastName, userId = y.UserId}).ToList();

            return Ok(searchQuery);
        }

        //[HttpGet("query={q}")]
        //public IActionResult GetUserFromSearch([FromRoute] string q)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var original = q + "%";

        //    original = "'" + original + "'";

        //    var searchQuery = _context.User.FromSql("SELECT * FROM [dbo].[User] WHERE (Username LIKE "+original+" OR FirstName LIKE "+original+" OR LastName LIKE "+original+")").ToList();


        //    return Ok(searchQuery);
        //}


        //[HttpGet]
        //public IActionResult New()
        //{
        //    using (var command = _context.Database.GetDbConnection().CreateCommand())
        //    {
        //        command.CommandText = "eraj1";
        //        command.CommandType = CommandType.StoredProcedure;

        //        _context.Database.OpenConnection();



        //        using (var dataReader = command.ExecuteReader())
        //        {

        //        }



        //    }
        //}

    }
}