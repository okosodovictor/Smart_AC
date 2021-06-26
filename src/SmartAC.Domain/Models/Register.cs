using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Models
{
    public class Register: Model
    {
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        public string FirmwareVersion { get; set; }
    }
}
