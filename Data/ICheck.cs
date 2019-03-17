using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinterProject.Data
{
    public interface ICheck
    {
        Task<bool> UsernameExist(string username);
        Task<bool> EmailExist(string email);
        Task<bool> UserFollowed(int userId, int followUserId);
    }
}
