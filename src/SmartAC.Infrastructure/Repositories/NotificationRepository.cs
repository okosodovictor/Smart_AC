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
    public class NotificationRepository: INotificationRepository
    {
        private DataContext _context;

        public NotificationRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Notification notification)
        {
            var entity = notification.Map();
            _context.Notifications.Add(entity);
            _context.SaveChanges();
        }

        public Notification GetNotification(Guid id)
        {
            return _context.Notifications.Find(id).Map();
        }

        public Notification[] GetNotifications(Page page)
        {
            var notifications = _context.Notifications.OrderByDescending(n => n.TimeStamp).Skip(page.Offset).Take(page.Limit).ToList();
            return notifications.Select(Mapper.Map).ToArray();
        }

        public Notification[] GetNotificationsByDismissedStatus(Page page, bool isDismissed)
        {
            var query = from notification in _context.Notifications
                        where notification.IsDismissed == isDismissed
                        orderby notification.TimeStamp descending
                        select notification;

            var notifications = query.Skip(page.Offset).Take(page.Limit).ToList();
            return notifications.Select(Mapper.Map).ToArray();
        }

        public Notification UpdateIsDimissed(Guid id, bool isDismissed)
        {
            var notification = _context.Notifications.Find(id);
            if(notification != null)
            {
                notification.IsDismissed = isDismissed;
                _context.SaveChanges();
            }
            return notification.Map();
        }
    }
}
