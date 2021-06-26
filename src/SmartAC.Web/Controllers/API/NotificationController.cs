using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartAC.Web.Controllers.API
{
    [Route("api/notifications")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationManager _notification;

        public NotificationController(INotificationManager notification)
        {
            _notification = notification;
        }

        /// <summary>
        /// Returns the list of notifications in descending order
        /// </summary>
        /// <param name="page">Page to be returned must start from 1</param>
        /// <param name="size">Number of Items per Page. Default is 50</param>
        /// <returns>Notifications</returns>
        [HttpGet]
        public IEnumerable<Notification> Get([FromQuery]int page = 1, [FromQuery]int size = 50)
        {
            return _notification.GetNotifications(new Page(page, size));
        }

        [HttpGet("pending")]
        public IEnumerable<Notification> GetPending([FromQuery] int page = 1, [FromQuery] int size = 50)
        {
            return _notification.GetPendingNotifications(new Page(page, size));
        }

        /// <summary>
        /// Fetches the Details of a Single Notification
        /// </summary>
        /// <param name="id">NotificationId</param>
        /// <returns>Notification</returns>
        [HttpGet("{id}")]
        public Notification Get(Guid id)
        {
            return _notification.GetNotification(id);
        }

        /// <summary>
        /// Dismisses a Notification
        /// </summary>
        /// <param name="id">Id of Notification to be Dismissed</param>
        [HttpPost("{id}/dismiss")]
        public void Post(Guid id)
        {
            _notification.Dismiss(id);
        }
    }
}
