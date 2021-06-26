using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Models;
using SmartAC.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DataContext _context;

        public DeviceRepository(DataContext context)
        {
            _context = context;
        }

        public Device CreateDevice(Device newDevice)
        {
            var entity = newDevice.Map();
            _context.Add(entity);
            _context.SaveChanges();
            return entity.Map();
        }

        public SensorReading CreateSensorReading(SensorReading reading)
        {
            var entity = reading.Map();
            _context.SensorReadings.Add(entity);
            _context.SaveChanges();
            return entity.Map();
        }

        public Device[] GetAllDevices(Page page)
        {
            var result = _context.Devices.Skip(page.Offset).Take(page.Limit).ToList();
            return result.Select(Mapper.Map).ToArray();
        }

        public Client GetClient(string serialNumber)
        {
            var client = _context.Clients.FirstOrDefault(c => c.SerialNumber == serialNumber);
            return client.Map();
        }

        public Device GetDevice(Guid deviceId)
        {
            var device = _context.Devices.FirstOrDefault(d => d.DeviceId == deviceId);
            return device.Map();
        }

        public Device GetDevice(string serialNumber)
        {
            var device = _context.Devices.FirstOrDefault(d => d.SerialNumber == serialNumber);
            return device.Map();
        }

        public SensorReading GetSensorReading(Guid deviceId, DateTimeOffset timeStamp)
        {
            var query = from readings in _context.SensorReadings
                        where readings.DeviceId == deviceId
                        where readings.TimeStamp == timeStamp
                        select readings;
            var reading = query.FirstOrDefault(r => r.TimeStamp == timeStamp);
            return reading.Map();
        }

        public SensorReading[] GetSensorReadings(string serialNumber, DateTimeOffset cutOffTimeStamp, Page pageOptions)
        {
            var query = from readings in _context.SensorReadings
                        where readings.Device.SerialNumber == serialNumber
                        where readings.TimeStamp > cutOffTimeStamp
                        select readings;
            var sensorReadings = query.Skip(pageOptions.Offset).Take(pageOptions.Limit).ToList();
            return sensorReadings.Select(Mapper.Map).ToArray();
        }
    }
}
