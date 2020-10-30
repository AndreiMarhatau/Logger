using Logger.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Serilog
{
    public class SerilogAdapter : ILoggerAdapter<ILogger>
    {
        private ILogger _logger;
        public ILogger Logger { get => _logger; set { if (_logger == null) _logger = value; } }

        public void Debug(string text)
        {
            _logger.Debug(text);
        }

        public void Debug(string text, Exception exception)
        {
            _logger.Debug(exception, text);
        }

        public void Error(string text)
        {
            _logger.Error(text);
        }

        public void Error(string text, Exception exception)
        {
            _logger.Error(exception, text);
        }

        public void Fatal(string text)
        {
            _logger.Fatal(text);
        }

        public void Fatal(string text, Exception exception)
        {
            _logger.Fatal(exception, text);
        }

        public void Info(string text)
        {
            _logger.Information(text);
        }

        public void Info(string text, Exception exception)
        {
            _logger.Information(exception, text);
        }

        public void Warn(string text)
        {
            _logger.Warning(text);
        }

        public void Warn(string text, Exception exception)
        {
            _logger.Warning(exception, text);
        }
    }
}
