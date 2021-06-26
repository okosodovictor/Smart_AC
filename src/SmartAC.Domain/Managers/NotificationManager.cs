using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAC.Domain.Managers
{
    public class NotificationManager : INotificationManager
    {
        private INotificationRepository _notificationRepository;

        public NotificationManager(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void Dismiss(Guid id)
        {
            _notificationRepository.UpdateIsDimissed(id, true);
        }

        public Notification GetNotification(Guid id)
        {
            return _notificationRepository.GetNotification(id);
        }

        public Notification[] GetNotifications(Page page)
        {
            return _notificationRepository.GetNotifications(page);
        }

        public IEnumerable<Notification> GetPendingNotifications(Page page)
        {
            return _notificationRepository.GetNotificationsByDismissedStatus(page, false);
        }
    }
}
