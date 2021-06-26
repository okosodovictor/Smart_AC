using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAC.Domain.Exceptions;
using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Models;
using SmartAC.Web.Extensions;
using SmartAC.Web.Filters;
using SmartAC.Web.Interface.Services;
using SmartAC.Web.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartAC.Web.Controllers.API
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly IDeviceManager _deviceManager;
        private readonly ITokenService _tokenService;

        public DeviceController(IDeviceManager deviceManager, ILogger<DeviceController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _deviceManager = deviceManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Authenticate/Registers Devices
        /// </summary>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public ActionResult<TokenModel> Authenticate([FromBody] Register registration)
        {
            try
            {
                var device = _deviceManager.Register(registration);
                var token = _tokenService.GenerateToken(device);
                return token;
            }
            catch (ForbiddenException fbex)
            {
                _logger.LogError("Operation is Forbidden: {message}", fbex.Message);
                return Forbid(fbex.Message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError("Could not Authenticate: {message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Accepts and Processes Sensor Readings
        /// </summary>
        /// <param name="sensorReadings">List of Sensor Readings to Capture</param>
        /// <returns>List of Sensor Readings that were not processed</returns>
        [Authorize(Constants.AuthScheme.JWT)]
        [HttpPost("upload")]
        public ActionResult<SensorReading[]> Upload([FromBody] SensorReading[] sensorReadings)
        {
            try
            {
                var serialNumber = User.Identity.GetSerialNumber();
                return _deviceManager.ProcessReadings(serialNumber, sensorReadings);
            }
            catch (ApplicationException apex)
            {
                return BadRequest(apex.Message);
            }
        }
    }
}
