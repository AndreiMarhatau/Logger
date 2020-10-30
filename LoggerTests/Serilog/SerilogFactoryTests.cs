using Logger;
using Logger.Interfaces;
using Logger.Models;
using Logger.Serilog;
using Moq;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerTests.Serilog
{
    public class SerilogFactoryTests
    {
        Mock<LoggerConfiguration> loggerConfiguration;
        IConfiguration config;
        ILoggerFactory<ILogger> factory;

        [SetUp]
        public void Setup()
        {
            loggerConfiguration = new Mock<LoggerConfiguration>();
            config = new SerilogConfiguration("Logs/log.txt");
            factory = new SerilogFactory(loggerConfiguration.Object);
        }

        [Test]
        public void CreateLogger_TryToCreateLoggerAndGetResult_ShouldReturnCreatedInstanceOfLogger()
        {
            ILogger logger = factory.CreateLogger(config);
        }
    }
}
