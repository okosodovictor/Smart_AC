using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Interface.Repository
{
    public interface INotificationRepository
    {
        void Create(Notification notification);
        Notification GetNotification(Guid id);
        Notification UpdateIsDimissed(Guid id, bool isDismissed);
        Notification[] GetNotifications(Page page);
        Notification[] GetNotificationsByDismissedStatus(Page page, bool isDismissed);
    }
}
