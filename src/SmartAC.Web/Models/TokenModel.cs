using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAC.Web.Models
{
    public class TokenModel
    {
        public string Jwt { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}
