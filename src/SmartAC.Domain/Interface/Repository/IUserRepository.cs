using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Repository
{
    public interface IUserRepository
    {
        User GetUser(string email);
        User GetUser(string email, string hashedPassword);
    }
}
