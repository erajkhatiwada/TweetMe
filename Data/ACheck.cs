using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinterProject.Data
{
    public abstract class ACheck
    {
        public abstract Task<bool> UsernameExist(string username);
        public abstract Task<bool> EmailExist(string email);
        public abstract Task<bool> UserFollowed(int userId, int followUserId);

        public void print()
        {
            Console.WriteLine("Hey Eraj");
        }
    }
}
