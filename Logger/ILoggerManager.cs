using Logger.Interfaces;
using Logger.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public interface ILoggerManager<TLogger>
    {
        ILoggerAdapter<TLogger> CreateLogger(IConfiguration configuration);
    }
}
