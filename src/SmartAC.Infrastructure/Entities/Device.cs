using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Infrastructure.Entities
{
    public class Device
    {
        public Guid DeviceId { get; set; }
        public string SerialNumber { get; set; }
        public string FirmwareVersion { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }

        public ICollection<SensorReading> SensorReadings { get; set; } = new HashSet<SensorReading>();
    }
}
