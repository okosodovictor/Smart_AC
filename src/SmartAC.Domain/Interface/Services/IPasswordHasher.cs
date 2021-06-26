using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);

        bool CompareHash(string password, string passwordHash);
    }
}
