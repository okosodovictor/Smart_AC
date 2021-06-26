using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SmartAC.Domain.Models.Notification;

namespace SmartAC.Infrastructure.Entities
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public string Text { get; set; }
        public bool IsDismissed { get; set; }
        public string SubjectId { get; set; }
        public SubjectType Type { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
