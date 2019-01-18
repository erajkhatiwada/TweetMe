using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WinterProject.Models;

namespace WinterProject.Data
{
    public class Auth : IAuth
    {
        private readonly WinterBreakContext _context;

        public Auth(WinterBreakContext context)
        {
            _context = context;
        }

        public string HashPassword(string password)
        {
   
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = Encoding.Unicode.GetBytes("pseudopseudohypoparathyroidism"); 
            
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(salt);
            //}
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");

            return hashed;
        }

        public async Task<bool> Login(string username, string password)
        {
            if ( ((await _context.User.AnyAsync(x => x.Username == username)) || (await _context.User.AnyAsync(x => x.Email == username))) && (await _context.User.AnyAsync(x => x.Password == password)))
            {
                return true;
            }
            return false;
        }
    
    }
}
