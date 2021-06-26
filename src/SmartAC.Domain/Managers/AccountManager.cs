using SmartAC.Domain.Exceptions;
using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Interface.Services;
using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IPasswordHasher _hasher;
        private readonly IUserRepository _userRepo;

        public AccountManager(IUserRepository userRepo, IPasswordHasher hasher)
        {
            _hasher = hasher;
            _userRepo = userRepo;
        }
        public User ValidateUser(string email, string password)
        {
            var user = _userRepo.GetUser(email);
            if (user == null) throw new ForbiddenException("Invalid Email or Password");

            var hashedPassword = _hasher.HashPassword(password);
            var validUser = _userRepo.GetUser(email, hashedPassword);
            if (validUser == null) throw new ForbiddenException("Invalid Email or Password");

            return validUser;
        }
    }
}
