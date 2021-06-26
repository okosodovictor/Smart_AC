using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartAC.Domain.Exceptions;
using SmartAC.Domain.Interface.Repository;
using SmartAC.Domain.Interface.Services;
using SmartAC.Domain.Managers;
using SmartAC.Domain.Models;
using System;
using System.Linq;

namespace SmartAC.Domain.Tests
{
    [TestClass]
    public class DeviceManagerTests
    {
        [TestMethod]
        public void RegisterSavesNewDeviceToDatabase()
        {
            //Arrange
            var fakeSerialNumber = "Fake-Device-01";
            var fakeRegister = new Register
            {
                FirmwareVersion = "test-firmware",
                Secret = "test-secret",
                SerialNumber = fakeSerialNumber
            };

            var fakeDevice = new Device
            {
                DeviceId = Guid.NewGuid(),
                FirmwareVersion = "test-firmware",
                SerialNumber = fakeSerialNumber,
                RegistrationDate = DateTimeOffset.Now,
            };

            var fakeClient = new Client
            {
                ClientId = Guid.NewGuid(),
                Secret = "test-secret",
                SerialNumber = fakeSerialNumber
            };

            var deviceRepo = new Mock<IDeviceRepository>();
            deviceRepo.Setup(d => d.GetClient(fakeSerialNumber)).Returns(fakeClient);
            deviceRepo.Setup(d => d.GetDevice(It.IsAny<string>())).Returns((Device)null);
            deviceRepo.Setup(d => d.CreateDevice(It.IsAny<Device>())).Returns(fakeDevice);

            var alertingService = new Mock<IAlertingService>();

            var manager = new DeviceManager(deviceRepo.Object, alertingService.Object);
            //Act
            var result = manager.Register(fakeRegister);

            //Assert
            deviceRepo.Verify(d => d.CreateDevice(It.IsAny<Device>()), Times.Once);
            Assert.AreEqual(result.SerialNumber, fakeSerialNumber);
        }

        [TestMethod]
        public void RegisterReturnsExistingDevice()
        {
            //Arrange
            var fakeSerialNumber = "Fake-Device-01";
            var fakeRegister = new Register
            {
                FirmwareVersion = "test-firmware",
                Secret = "test-secret",
                SerialNumber = fakeSerialNumber
            };

            var fakeDevice = new Device
            {
                DeviceId = Guid.NewGuid(),
                FirmwareVersion = "test-firmware",
                SerialNumber = fakeSerialNumber,
                RegistrationDate = DateTimeOffset.Now
            };

            var fakeClient = new Client
            {
                ClientId = Guid.NewGuid(),
                Secret = "test-secret",
                SerialNumber = fakeSerialNumber
            };

            var deviceRepo = new Mock<IDeviceRepository>();
            deviceRepo.Setup(d => d.GetClient(fakeSerialNumber)).Returns(fakeClient);
            deviceRepo.Setup(d => d.GetDevice(It.IsAny<string>())).Returns(fakeDevice);
            deviceRepo.Setup(d => d.CreateDevice(It.IsAny<Device>())).Returns(fakeDevice);

            var alertingService = new Mock<IAlertingService>();

            var manager = new DeviceManager(deviceRepo.Object, alertingService.Object);
            //Act
            var result = manager.Register(fakeRegister);

            //Assert
            deviceRepo.Verify(d => d.CreateDevice(It.IsAny<Device>()), Times.Never);
            Assert.AreEqual(result.SerialNumber, fakeSerialNumber);
        }

        [TestMethod]
        public void RegisterThrowsForbiddenOnInvalidClientSecret()
        {
            //Arrange
            var fakeSerialNumber = "Fake-Device-01";
            var fakeRegister = new Register
            {
                FirmwareVersion = "test-firmware",
                Secret = "wrong-secret",
                SerialNumber = fakeSerialNumber
            };

            var fakeDevice = new Device
            {
                DeviceId = Guid.NewGuid(),
                FirmwareVersion = "test-firmware",
                SerialNumber = fakeSerialNumber,
                RegistrationDate = DateTimeOffset.Now
            };

            var fakeClient = new Client
            {
                ClientId = Guid.NewGuid(),
                Secret = "test-secret",
                SerialNumber = fakeSerialNumber
            };

            var deviceRepo = new Mock<IDeviceRepository>();
            deviceRepo.Setup(d => d.GetClient(fakeSerialNumber)).Returns(fakeClient);
            deviceRepo.Setup(d => d.GetDevice(It.IsAny<string>())).Returns(fakeDevice);
            deviceRepo.Setup(d => d.CreateDevice(It.IsAny<Device>())).Returns(fakeDevice);

            var alertingService = new Mock<IAlertingService>();

            var manager = new DeviceManager(deviceRepo.Object, alertingService.Object);
            //Act
            try
            {
                var result = manager.Register(fakeRegister);
                //Assert
                Assert.Fail("No Exception was thrown");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ForbiddenException), "An unexpected Exception was thrown");
            }
        }

        [TestMethod]
        public void GetAllDevicesReturnsDevices()
        {
            //Arrange 
            var page = new Page(2, 50);
            var fakeDevices = new[]
            {
                new Device
                {
                    DeviceId = Guid.NewGuid(),
                    FirmwareVersion = "test-firmware",
                    SerialNumber = "fake-serial-number",
                    RegistrationDate = DateTimeOffset.Now
                }
            };
            var mockDeviceRepo = new Mock<IDeviceRepository>();
            mockDeviceRepo.Setup(m => m.GetAllDevices(page)).Returns(fakeDevices);
            var manager = new DeviceManager(mockDeviceRepo.Object, null);

            //Act
            var actual = manager.GetAllDevices(page);

            //Assert
            Assert.AreEqual(fakeDevices, actual);
        }

        [TestMethod]
        public void ProcessReadingsShouldSkipExistingReadings()
        {
            //Arrange 
            var serialNumber = "fake-serial";
            var deviceId = Guid.NewGuid();
            var readings = new[]
            {
                new SensorReading
                {
                    DeviceId = deviceId,
                    AirHumidity = 0.3M,
                    CarbonMonoxide = 0.7M,
                    HealthStatus = "normal",
                    Temperature = 30,
                    TimeStamp = DateTimeOffset.Now
                },
                new SensorReading
                {
                    DeviceId = deviceId,
                    AirHumidity = 0.3M,
                    CarbonMonoxide = 0.7M,
                    HealthStatus = "normal",
                    Temperature = 30,
                    TimeStamp = DateTimeOffset.Now
                }
            };
            var page = new Page(2, 50);
            var device = new Device
            {
                DeviceId = deviceId,
                FirmwareVersion = "test-firmware",
                SerialNumber = serialNumber,
                RegistrationDate = DateTimeOffset.Now
            };
            var mockDeviceRepo = new Mock<IDeviceRepository>();
            mockDeviceRepo.Setup(m => m.GetDevice(serialNumber)).Returns(device);
            foreach(var reading in readings)
            {
                mockDeviceRepo.Setup(m => m.GetSensorReading(device.DeviceId, reading.TimeStamp)).Returns(reading);
            }
            

            var mockAlertingService = new Mock<IAlertingService>();
            var manager = new DeviceManager(mockDeviceRepo.Object, mockAlertingService.Object);

            //Act
            var actual = manager.ProcessReadings(serialNumber, readings);

            //Assert
            Assert.AreEqual(0, actual.Length);
            mockDeviceRepo.Verify(d => d.CreateSensorReading(It.IsAny<SensorReading>()), Times.Never, "Sensor Reading was Created");
        }

        [TestMethod]
        public void ProcessReadingsShouldCreateReadingsAndSendNotifications()
        {
            //Arrange 
            var serialNumber = "fake-serial";
            var deviceId = Guid.NewGuid();
            var readings = new[]
            {
                new SensorReading
                {
                    DeviceId = deviceId,
                    AirHumidity = 0.3M,
                    CarbonMonoxide = 0.7M,
                    HealthStatus = "normal",
                    Temperature = 30,
                    TimeStamp = DateTimeOffset.Now
                },
                new SensorReading
                {
                    DeviceId = deviceId,
                    AirHumidity = 0.3M,
                    CarbonMonoxide = 0.7M,
                    HealthStatus = "normal",
                    Temperature = 30,
                    TimeStamp = DateTimeOffset.Now
                }
            };
            var page = new Page(2, 50);
            var device = new Device
            {
                DeviceId = deviceId,
                FirmwareVersion = "test-firmware",
                SerialNumber = serialNumber,
                RegistrationDate = DateTimeOffset.Now
            };
            var mockDeviceRepo = new Mock<IDeviceRepository>();
            mockDeviceRepo.Setup(m => m.GetDevice(serialNumber)).Returns(device);


            var mockAlertingService = new Mock<IAlertingService>();
            var manager = new DeviceManager(mockDeviceRepo.Object, mockAlertingService.Object);

            //Act
            var actual = manager.ProcessReadings(serialNumber, readings);

            //Assert
            Assert.AreEqual(0, actual.Length);
            mockDeviceRepo.Verify(d => d.CreateSensorReading(It.IsAny<SensorReading>()), Times.AtLeastOnce, "Sensor Reading was Created");
            mockAlertingService.Verify(a => a.Notify(device, It.IsAny<SensorReading>()), Times.AtLeastOnce, "Alert was not Sent");
        }

        [TestMethod]
        public void ProcessReadingsShouldReturnInvalidReadings()
        {
            //Arrange 
            var serialNumber = "fake-serial";
            var deviceId = Guid.NewGuid();
            var readings = new[]
            {
                new SensorReading
                {
                    DeviceId = deviceId,
                }
            };
            var page = new Page(2, 50);
            var device = new Device
            {
                DeviceId = deviceId,
                FirmwareVersion = "test-firmware",
                SerialNumber = serialNumber,
                RegistrationDate = DateTimeOffset.Now
            };
            var mockDeviceRepo = new Mock<IDeviceRepository>();
            mockDeviceRepo.Setup(m => m.GetDevice(serialNumber)).Returns(device);


            var mockAlertingService = new Mock<IAlertingService>();
            var manager = new DeviceManager(mockDeviceRepo.Object, mockAlertingService.Object);

            //Act
            var actual = manager.ProcessReadings(serialNumber, readings);

            //Assert
            Assert.AreEqual(1, actual.Length);
            mockDeviceRepo.Verify(d => d.CreateSensorReading(It.IsAny<SensorReading>()), Times.Never, "Sensor Reading was Created");
        }
    }
}
