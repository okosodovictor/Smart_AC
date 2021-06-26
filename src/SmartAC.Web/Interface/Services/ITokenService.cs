using Microsoft.IdentityModel.Tokens;
using SmartAC.Domain.Models;
using SmartAC.Web.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAC.Web.Interface.Services
{
    public interface ITokenService
    {
        TokenModel GenerateToken(Device device);
        bool ValidateCurrentToken(string token, out JwtSecurityToken validatedToken);
    }
}
