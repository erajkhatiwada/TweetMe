using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinterProject.Data
{
    public interface IAuth
    {
        string HashPassword(string password);
        Task<bool> Login(string username, string password);
    }
}
