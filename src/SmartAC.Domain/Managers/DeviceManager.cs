using SmartAC.Domain.Exceptions;
using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Interface.Services;
using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Managers
{
    public class DeviceManager : IDeviceManager
    {
        private readonly IAlertingService _alertingService;
        private readonly IDeviceRepository _deviceRepo;

        public DeviceManager(IDeviceRepository deviceRepo, IAlertingService alertingService)
        {
            _alertingService = alertingService;
            _deviceRepo = deviceRepo;
        }

        public SensorReading[] ProcessReadings(string serialNumber, SensorReading[] sensorReadings)
        {
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            if (sensorReadings is null) throw new ArgumentNullException(nameof(sensorReadings));

            var device = _deviceRepo.GetDevice(serialNumber);
            if (device == null) throw new ForbiddenException("Invalid Serial Number");

            var errorList = new List<SensorReading>();
            foreach(var reading in sensorReadings)
            {
                reading.DeviceId = device.DeviceId;
                try
                {
                    reading.Validate();

                    //Check to see if Sensor Reading has been captured
                    var existing = _deviceRepo.GetSensorReading(reading.DeviceId, reading.TimeStamp);
                    if(existing == null)
                    {
                        existing = _deviceRepo.CreateSensorReading(reading);
                        _alertingService.Notify(device, reading);
                    }
                }
                catch
                {
                    errorList.Add(reading);
                }
            }
            return errorList.ToArray();
        }

        public Device[] GetAllDevices(Page page) => _deviceRepo.GetAllDevices(page);

        public Device Register(Register model)
        {
            if (model is null)throw new ArgumentNullException(nameof(model));

            //Get Client Credentials if they exist
            var client = _deviceRepo.GetClient(model.SerialNumber);
            if(client.Secret.ToString() != model.Secret)
            {
                throw new ForbiddenException("Invalid Client Secret");
            }

            //Check to see if the Device is already registered
            var device = _deviceRepo.GetDevice(model.SerialNumber);
            if (device != null) return device;

            //Validate Device
            model.Validate();
            device = new Device
            {
                FirmwareVersion = model.FirmwareVersion,
                SerialNumber = model.SerialNumber,
                RegistrationDate = DateTimeOffset.Now,
            };

            return _deviceRepo.CreateDevice(device);
        }

        public Device GetDevice(Guid deviceId) => _deviceRepo.GetDevice(deviceId);
        public SensorReading[] GetSensorReadings(string serialNumber, SensorFilter filter, Page page)
        {
            var cutOffDate = GetCutOff(filter);
            return _deviceRepo.GetSensorReadings(serialNumber, cutOffDate, new Page(1));
        }

        private static DateTimeOffset GetCutOff(SensorFilter filter)
        {
            var now = DateTimeOffset.UtcNow;
            switch (filter)
            {
                case SensorFilter.Today:
                    return new DateTimeOffset(now.Year, now.Month, now.Day, 0, 0, 0, now.Offset);
                case SensorFilter.ThisWeek:
                    int diff = (7 + (now.DayOfWeek - DayOfWeek.Sunday)) % 7;
                    var weekDate = now.AddDays(-1 * diff).Date;
                    return new DateTimeOffset(weekDate.Year, weekDate.Month, weekDate.Day, 0, 0, 0, now.Offset);
                case SensorFilter.ThisMonth:
                    return new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, now.Offset);
                case SensorFilter.ThisYear:
                    return new DateTimeOffset(now.Year, 1, 1, 0, 0, 0, now.Offset);
            }
            return now;
        }
    }
}
