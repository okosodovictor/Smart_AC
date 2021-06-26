using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Infrastructure.Entities
{
    public class SensorReading
    {
        public Guid SensorReadingId { get; set; }
        public decimal Temperature { get; set; }
        public decimal AirHumidity { get; set; }
        public decimal CarbonMonoxide { get; set; }
        [MaxLength(150)]
        public string HealthStatus { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
