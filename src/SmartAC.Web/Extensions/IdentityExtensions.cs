using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SmartAC.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetSerialNumber(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var subject = claimsIdentity?.FindFirst(JwtClaimTypes.Subject);
            return subject?.Value;
        }
    }
}
