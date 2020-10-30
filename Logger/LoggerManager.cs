using Logger.Interfaces;
using Logger.Models;
using Logger.Serilog;
using Serilog;
using System;
using System.Collections.Generic;

namespace Logger
{
    public class LoggerManager<TLogger, TLoggerAdapter>: ILoggerManager<TLogger> where TLoggerAdapter: ILoggerAdapter<TLogger>, new()
    {
        private readonly IDictionary<IConfiguration, TLoggerAdapter> _cache;
        private readonly ILoggerFactory<TLogger> _factory;

        public LoggerManager(ILoggerFactory<TLogger> factory)
        {
            this._factory = factory;
            _cache = new Dictionary<IConfiguration, TLoggerAdapter>();
        }
        public ILoggerAdapter<TLogger> CreateLogger(IConfiguration configuration)
        {
            if(!_cache.TryGetValue(configuration, out TLoggerAdapter loggerAdapter))
            {
                var logger = new TLoggerAdapter() { Logger = _factory.CreateLogger(configuration) };
                return PutAdapterToCache(configuration, logger);
            }
            return loggerAdapter;
        }

        private TLoggerAdapter PutAdapterToCache(IConfiguration configuration, TLoggerAdapter loggerAdapter)
        {
            lock (_cache)
            {
                if (!_cache.TryGetValue(configuration, out TLoggerAdapter outAdapter))
                {
                    _cache.Add(configuration, loggerAdapter);
                    return loggerAdapter;
                }
                return outAdapter;
            }
        }
    }
}
