using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinterProject.Data
{
    public interface IComment
    {
        string convertedDate(string dateCreated);
    }
}
