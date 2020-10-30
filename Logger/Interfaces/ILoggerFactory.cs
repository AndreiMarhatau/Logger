using Logger.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Interfaces
{
    public interface ILoggerFactory<ILogger>
    {
        ILogger CreateLogger(IConfiguration config);
    }
}
