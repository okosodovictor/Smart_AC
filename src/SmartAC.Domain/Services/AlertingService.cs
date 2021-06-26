using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Interface.Services;
using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Services
{
    public class AlertingService : IAlertingService
    {
        private INotificationRepository _notification;

        public AlertingService(INotificationRepository notification)
        {
            _notification = notification;
        }

        public void Notify(Device device, SensorReading reading)
        {
            var healthMsg = HealthStatusCheck(device, reading);
            if(healthMsg != null)
            {
                var notification = new Notification
                {
                    IsDismissed = false,
                    SubjectId = reading.DeviceId.ToString(),
                    TimeStamp = DateTimeOffset.Now,
                    Type = Notification.SubjectType.Device,
                    Text = healthMsg
                };
                _notification.Create(notification);
            }

            var carbonMsg = CarbonStatusCheck(reading);
            if(carbonMsg != null)
            {
                var notification = new Notification
                {
                    IsDismissed = false,
                    SubjectId = reading.DeviceId.ToString(),
                    TimeStamp = DateTimeOffset.Now,
                    Text = carbonMsg,
                    Type = Notification.SubjectType.Device
                };
                _notification.Create(notification);
            }
        }

        private static string CarbonStatusCheck(SensorReading reading)
        {
            if(reading.CarbonMonoxide > 9)
            {
                return $"Carbon Monoxide Build up Detected";
            }
            return null;
        }

        private static string HealthStatusCheck(Device device, SensorReading reading)
        {
            return (reading.HealthStatus?.ToLower()) switch
            {
                "needs_service" => $"Device {device.SerialNumber} Needs Servicing",
                "needs_new_filter" => $"Device {device.SerialNumber} Needs a New Filter",
                "gas_leak" => $"A Gas leak has been detected on Device {device.SerialNumber}",
                _ => null,
            };
        }
    }
}
