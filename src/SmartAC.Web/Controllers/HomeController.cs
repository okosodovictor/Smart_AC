using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Models;
using SmartAC.Web.Models;

namespace SmartAC.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDeviceManager _device;

        public HomeController(ILogger<HomeController> logger, IDeviceManager device)
        {
            _logger = logger;
            _device = device;
        }

        public IActionResult Index()
        {
            var devices = _device.GetAllDevices(new Page(1));
            return View(devices);
        }

        public IActionResult Details(Guid id, [FromQuery] SensorFilter filter)
        {
            var model = new DetailsModel();
            var device = _device.GetDevice(id);
            if (device != null)
            {
                model.Device = device;
                model.Filter = filter;
                model.SensorReadings = _device.GetSensorReadings(device.SerialNumber, model.Filter, new Page(1));
            }

            return View(model);
        }

        public IActionResult Notifications()
        {
            var model = new List<Notification>();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
