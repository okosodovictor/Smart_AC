using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Models;
using SmartAC.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public User GetUser(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user.Map();
        }

        public User GetUser(string email, string hashedPassword)
        {
            var query = from user in _context.Users
                        where user.Email == email
                        where user.PasswordHash == hashedPassword
                        select user;
            return query.FirstOrDefault().Map();
        }
    }
}
