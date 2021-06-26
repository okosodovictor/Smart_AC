using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartAC.Domain.Exceptions;
using SmartAC.Domain.Interface.Managers;
using SmartAC.Domain.Models;
using SmartAC.Web.Controllers.API;
using SmartAC.Web.Interface.Services;
using SmartAC.Web.Models;
using System;

namespace SmartAC.Web.Tests.Controllers
{
    [TestClass]
    public class DeviceControllerTests
    {
        [TestMethod]
        public void AuthenticateReturnsForbiddenForInvalidRegistration()
        {
            //Arrange
            var mockDeviceManager = new Mock<IDeviceManager>();
            mockDeviceManager.Setup(d => d.Register(It.IsAny<Register>())).Throws(new ForbiddenException("Could not find Serial Number"));

            var mockLogger = new Mock<ILogger<DeviceController>>();
            var mockTokenService = new Mock<ITokenService>();
            var controller = new DeviceController(mockDeviceManager.Object, mockLogger.Object, mockTokenService.Object);
            var fakeRegister = new Register
            {
                FirmwareVersion = "test-firmware",
                Secret = "test-secret",
                SerialNumber = "Fake-Device-01"
            };
            //Act
            var result = controller.Authenticate(fakeRegister);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(ForbidResult));
        }


        [TestMethod]
        public void AuthenticateReturnsTokenForValidRegistration()
        {
            //Arrange
            var fakeRegister = new Register
            {
                FirmwareVersion = "test-firmware",
                Secret = "test-secret",
                SerialNumber = "Fake-Device-01"
            };

            var fakeDevice = new Device
            {
                DeviceId = Guid.NewGuid(),
                FirmwareVersion = "test-firmware",
                SerialNumber = "Fake-Device-01",
                RegistrationDate = DateTimeOffset.Now,
            };

            var fakeToken = new TokenModel
            {
                Jwt = "fake-jwt",
                DateCreated = DateTime.Now
            };

            var mockLogger = new Mock<ILogger<DeviceController>>();

            var mockDeviceManager = new Mock<IDeviceManager>();
            mockDeviceManager.Setup(d => d.Register(fakeRegister)).Returns(fakeDevice);

            var mockTokenService = new Mock<ITokenService>();
            mockTokenService.Setup(t => t.GenerateToken(fakeDevice)).Returns(fakeToken);

            var controller = new DeviceController(mockDeviceManager.Object, mockLogger.Object, mockTokenService.Object);

            //Act
            var result = controller.Authenticate(fakeRegister);

            //Assert
            Assert.AreEqual(result.Value, fakeToken);
        }
    }
}
