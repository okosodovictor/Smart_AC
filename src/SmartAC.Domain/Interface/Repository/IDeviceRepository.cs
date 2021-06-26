using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Repository
{
    public interface IDeviceRepository
    {
        Client GetClient(string serialNumber);
        Device GetDevice(Guid deviceId);
        Device GetDevice(string serialNumber);
        Device CreateDevice(Device newDevice);
        SensorReading CreateSensorReading(SensorReading reading);
        SensorReading GetSensorReading(Guid deviceId, DateTimeOffset timeStamp);
        Device[] GetAllDevices(Page page);
        SensorReading[] GetSensorReadings(string serialNumber, DateTimeOffset cutOffTimeStamp, Page page);
    }
}
