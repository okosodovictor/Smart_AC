using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Models
{
    public class SensorReading: Model
    {
        public string SensorReadingId { get; set; }
        [Required]
        public decimal Temperature { get; set; }
        [Required]
        public decimal AirHumidity { get; set; }
        [Required]
        public decimal CarbonMonoxide { get; set; }
        [Required]
        public string HealthStatus { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid DeviceId { get; set; }
    }
}
