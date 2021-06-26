using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Managers
{
    public interface INotificationManager
    {
        Notification[] GetNotifications(Page page);
        Notification GetNotification(Guid id);
        void Dismiss(Guid id);
        IEnumerable<Notification> GetPendingNotifications(Page page);
    }
}
