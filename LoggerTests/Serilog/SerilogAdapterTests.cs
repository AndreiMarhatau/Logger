using Logger.Interfaces;
using Logger.Serilog;
using Moq;
using NUnit.Framework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoggerTests.Serilog
{
    public class SerilogAdapterTests
    {
        ILoggerAdapter<ILogger> adapter;
        Mock<ILogger> loggerMock;

        [SetUp]
        public void Setup()
        {
            loggerMock = new Mock<ILogger>();
            adapter = new SerilogAdapter() { Logger = loggerMock.Object };
        }

        [Test]
        public void Messages_CallEveryMethodToLog_EveryMethodInAdapterShouldRedirectCallToSerilogLogger()
        {
            string text = "text";
            Exception ex = new Exception();
            adapter.Info(text);
            adapter.Info(text, ex);
            adapter.Warn(text);
            adapter.Warn(text, ex);
            adapter.Debug(text);
            adapter.Debug(text, ex);
            adapter.Error(text);
            adapter.Error(text, ex);
            adapter.Fatal(text);
            adapter.Fatal(text, ex);

            loggerMock.Verify(obj => obj.Information(text), Times.Once);
            loggerMock.Verify(obj => obj.Information(ex, text), Times.Once);
            loggerMock.Verify(obj => obj.Warning(text), Times.Once);
            loggerMock.Verify(obj => obj.Warning(ex, text), Times.Once);
            loggerMock.Verify(obj => obj.Debug(text), Times.Once);
            loggerMock.Verify(obj => obj.Debug(ex, text), Times.Once);
            loggerMock.Verify(obj => obj.Error(text), Times.Once);
            loggerMock.Verify(obj => obj.Error(ex, text), Times.Once);
            loggerMock.Verify(obj => obj.Fatal(text), Times.Once);
            loggerMock.Verify(obj => obj.Fatal(ex, text), Times.Once);
        }
    }
}
