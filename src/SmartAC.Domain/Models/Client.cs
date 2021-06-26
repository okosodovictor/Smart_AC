using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Models
{
    public class Client : Model
    {
        public Guid ClientId { get; set; }
        public string SerialNumber { get; set; }
        public string Secret { get; set; }
    }
}
