using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAC.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private string _scheme;

        public AuthorizeAttribute(string Scheme = Constants.AuthScheme.JWT)
        {
            _scheme = Scheme;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasIdentity = context.HttpContext.User.Identities.Where(i => i.AuthenticationType == _scheme).Any();
            if (!hasIdentity)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
