using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Interfaces
{
    public interface ILoggerAdapter<TLogger>
    {
        TLogger Logger { get; set; }

        public void Debug(string text);
        public void Debug(string text, Exception exception);
        public void Error(string text);
        public void Error(string text, Exception exception);
        public void Fatal(string text);
        public void Fatal(string text, Exception exception);
        public void Info(string text);
        public void Info(string text, Exception exception);
        public void Warn(string text);
        public void Warn(string text, Exception exception);
    }
}
