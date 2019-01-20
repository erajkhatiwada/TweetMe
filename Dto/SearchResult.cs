using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinterProject.Models;

namespace WinterProject.Dto
{
    public class SearchResult
    {
        public int userId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
