using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Infrastructure.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public string SerialNumber { get; set; }
        public string Secret { get; set; }
    }
}
