using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WinterProject.Models;

namespace WinterProject.Data
{
    public class Check : ICheck
    {
        private readonly WinterBreakContext _context;

        public Check(WinterBreakContext context)
        {
            _context = context;
        }

        public async Task<bool> UsernameExist(string username)
        {
            if (await _context.User.AnyAsync(x => x.Username == username))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> EmailExist(string email)
        {
            if (await _context.User.AnyAsync(x => x.Email == email))
            {
                return true;
            }

            return false;
        }



    }
}
