using SmartAC.Domain.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Services
{
    public class MD5PasswordHasher : IPasswordHasher
    {
        public bool CompareHash(string password, string passwordHash)
        {
            return Hash(password) == passwordHash;
        }

        public string HashPassword(string password)
        {
            return Hash(password);
        }

        private static string Hash(string plainText)
        {
            var bytes =
                System.Security
                    .Cryptography.MD5.Create()
                    .ComputeHash(Encoding.UTF8.GetBytes(plainText));
            return Convert.ToBase64String(bytes);
        }
    }
}
