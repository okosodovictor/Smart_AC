using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Managers
{
    public interface IAccountManager
    {
        User ValidateUser(string email, string password);
    }
}
