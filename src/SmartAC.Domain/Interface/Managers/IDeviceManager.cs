using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Managers
{
    public interface IDeviceManager
    {
        Device Register(Register client);
        SensorReading[] ProcessReadings(string serialNumber, SensorReading[] sensorReadings);
        Device[] GetAllDevices(Page page);
        Device GetDevice(Guid deviceId);
        SensorReading[] GetSensorReadings(string serialNumber, SensorFilter filter ,Page page);
    }
}
