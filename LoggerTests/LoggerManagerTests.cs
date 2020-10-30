using Logger;
using Logger.Interfaces;
using Logger.Models;
using Logger.Serilog;
using Moq;
using NUnit.Framework;
using Serilog;
using System;
using System.Linq;

namespace LoggerTests
{
    public class LoggerManagerTests
    {
        ILoggerManager<ILogger> loggerManager;
        Mock<ILogger> loggerMock;
        Mock<ILoggerFactory<ILogger>> factoryMock;
        IConfiguration config;

        [SetUp]
        public void Setup()
        {
            factoryMock = new Mock<ILoggerFactory<ILogger>>();
            loggerMock = new Mock<ILogger>();
            loggerManager = new LoggerManager<ILogger, SerilogAdapter>(factoryMock.Object);
            config = new SerilogConfiguration("Logs/log.txt");
        }

        [Test]
        public void CreateLogger_CallMethodWithConfigureParameters_ShouldCallILoggerFactoryWithTheSameParameters()
        {
            
            loggerManager.CreateLogger(config);
            factoryMock.Verify(obj => obj.CreateLogger(config), Times.Once);
        }

        [Test]
        public void CreateLogger_CallMethodWithConfigureParameters_ShouldReturnAdapterWithLoggerThatReturnedFromFactory()
        {
            factoryMock.Setup(obj => obj.CreateLogger(config)).Returns(loggerMock.Object);
            var logger = loggerManager.CreateLogger(config);
            Assert.That(logger.Logger, Is.EqualTo(loggerMock.Object));
        }

        [Test]
        public void CreateLogger_CallSeveralTimes_ShouldWithTheSameParametersReturnTheSameObject()
        {
            var otherConfig = new SerilogConfiguration("Log2/log.txt");
            var logger1 = loggerManager.CreateLogger(config);
            var logger2 = loggerManager.CreateLogger(otherConfig);
            var logger3 = loggerManager.CreateLogger(config);
            Assert.AreNotSame(logger1, logger2);
            Assert.AreNotSame(logger2, logger3);
            Assert.AreSame(logger1, logger3);
        }

        [Test]
        public void CreateLogger_CallSeveralTimes_ShouldWithEqualParametersReturnTheSameObject()
        {
            var config1 = new Mock<IConfiguration>();
            config1.Setup(obj => obj.GetHashCode()).Returns(1);
            var config2 = new Mock<IConfiguration>();
            config2.Setup(obj => obj.GetHashCode()).Returns(1);
            config1.Setup(obj => obj.Equals(config2.Object)).Returns(true);
            config2.Setup(obj => obj.Equals(config1.Object)).Returns(true);
            var otherConfig = new Mock<IConfiguration>();
            otherConfig.Setup(obj => obj.GetHashCode()).Returns(2);
            otherConfig.Setup(obj => obj.Equals(config1.Object)).Returns(false);
            otherConfig.Setup(obj => obj.Equals(config2.Object)).Returns(false);
            config1.Setup(obj => obj.Equals(otherConfig.Object)).Returns(false);
            config2.Setup(obj => obj.Equals(otherConfig.Object)).Returns(false);

            var logger1 = loggerManager.CreateLogger(config1.Object);
            var logger2 = loggerManager.CreateLogger(config2.Object);
            var logger3 = loggerManager.CreateLogger(otherConfig.Object);

            Assert.AreSame(logger1, logger2);
            Assert.AreNotSame(logger1, logger3);
            Assert.AreNotSame(logger2, logger3);
        }
    }
}