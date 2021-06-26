using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Services
{
    public interface IAlertingService
    {
        void Notify(Device device,SensorReading reading);
    }
}
