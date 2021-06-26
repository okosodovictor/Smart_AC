using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Models
{
    public class User: Model
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
