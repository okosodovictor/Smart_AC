using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Infrastructure
{
    public static class Mapper
    {
        public static Entities.Client Map(this Client model)
        {
            if (model == null) return null;
            return new Entities.Client
            {
                ClientId = model.ClientId,
                Secret = model.Secret,
                SerialNumber = model.SerialNumber
            };
        }

        public static Client Map(this Entities.Client entity)
        {
            if (entity == null) return null;
            return new Client
            {
                ClientId = entity.ClientId,
                Secret = entity.Secret,
                SerialNumber = entity.SerialNumber
            };
        }

        public static Device Map(this Entities.Device entity)
        {
            if (entity == null) return null;
            return new Device
            {
                DeviceId = entity.DeviceId,
                FirmwareVersion = entity.FirmwareVersion,
                RegistrationDate = entity.RegistrationDate,
                SerialNumber = entity.SerialNumber
            };
        }

        public static Entities.Device Map(this Device model)
        {
            if (model == null) return null;
            return new Entities.Device
            {
                DeviceId = model.DeviceId,
                FirmwareVersion = model.FirmwareVersion,
                RegistrationDate = model.RegistrationDate,
                SerialNumber = model.SerialNumber,
            };
        }

        public static Entities.SensorReading Map(this SensorReading model)
        {
            if (model == null) return null;
            return new Entities.SensorReading
            {
                AirHumidity = model.AirHumidity,
                CarbonMonoxide = model.CarbonMonoxide,
                DeviceId = model.DeviceId,
                HealthStatus = model.HealthStatus,
                Temperature = model.Temperature,
                TimeStamp = model.TimeStamp,
            };
        }

        public static SensorReading Map(this Entities.SensorReading entity)
        {
            if (entity == null) return null;
            return new SensorReading
            {
                AirHumidity = entity.AirHumidity,
                CarbonMonoxide = entity.CarbonMonoxide,
                DeviceId = entity.DeviceId,
                HealthStatus = entity.HealthStatus,
                Temperature = entity.Temperature,
                TimeStamp = entity.TimeStamp
            };
        }

        public static User Map(this Entities.User entity)
        {
            if (entity == null) return null;
            return new User
            {
                Email = entity.Email,
                UserId = entity.UserId
            };
        }

        public static Entities.User Map(this User model)
        {
            if (model == null) return null;
            return new Entities.User
            {
                Email = model.Email,
                UserId = model.UserId
            };
        }

        public static Entities.Notification Map(this Notification model)
        {
            if (model == null) return null;
            return new Entities.Notification
            {
                IsDismissed = model.IsDismissed,
                NotificationId = model.NotificationId,
                SubjectId = model.SubjectId,
                Text = model.Text,
                TimeStamp = model.TimeStamp,
                Type = model.Type,
            };
        }

        public static Notification Map(this Entities.Notification entity)
        {
            if (entity == null) return null;
            return new Notification
            {
                IsDismissed = entity.IsDismissed,
                NotificationId = entity.NotificationId,
                SubjectId = entity.SubjectId,
                Text = entity.Text,
                TimeStamp = entity.TimeStamp,
                Type = entity.Type,
            };
        }
    }
}
