using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Models
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public string Text { get; set; }
        public bool IsDismissed { get; set; }
        public string SubjectId { get; set; }
        public SubjectType Type { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public enum SubjectType { SensorReading, Device }
    }
}
