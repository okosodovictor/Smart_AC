using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAC.Web.Models
{
    public class DetailsModel: Model
    {
        public Device Device { get; set; }
        public SensorFilter Filter { get; set; }
        public SensorReading[] SensorReadings { get; set; } = Array.Empty<SensorReading>();
    }
}
