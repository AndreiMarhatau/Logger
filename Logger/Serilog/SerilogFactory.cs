using Logger.Interfaces;
using Logger.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Logger.Serilog
{
    public class SerilogFactory : ILoggerFactory<ILogger>
    {
        private readonly LoggerConfiguration _loggerConfiguration;

        public SerilogFactory(LoggerConfiguration loggerConfiguration)
        {
            this._loggerConfiguration = loggerConfiguration;
        }

        public SerilogFactory()
        {
            this._loggerConfiguration = new LoggerConfiguration();
        }

        public ILogger CreateLogger(IConfiguration config)
        {
            return _loggerConfiguration
                .Enrich.WithThreadId()
                .Enrich.WithThreadName()
                .MinimumLevel.Is(ToLogEventLevel(config.MinimumLogLevel))
                .WriteTo.File(config.Path,
                    rollingInterval: config.RollbackByDays ? RollingInterval.Day : RollingInterval.Infinite,
                    fileSizeLimitBytes: config.FileSizeLimit,
                    retainedFileCountLimit: config.FilesCountLimit,
                    outputTemplate: config.OutputTemplate,
                    hooks: config.Compress ? new ZipArchivingHook(config.SaveCompressedFilesForDays) : null,
                    shared: config.SharedFile)
                .CreateLogger();
        }

        private LogEventLevel ToLogEventLevel(LogLevel logLevel)
        {
            return (LogEventLevel)((int)logLevel + 1);
        }

        private class ZipArchivingHook : FileLifecycleHooks
        {
            private int _saveCompressedFilesForDays;

            public ZipArchivingHook(int saveCompressedFilesForDays)
            {
                this._saveCompressedFilesForDays = saveCompressedFilesForDays;
            }

            public override void OnFileDeleting(string filePath)
            {
                var targetFileName = $"{Path.GetFileName(filePath)}.zip";
                var targetPath = Path.Combine(Path.GetDirectoryName(filePath), targetFileName);

                using (var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var targetStream =
                    new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                using (var compressStream = new ZipArchive(targetStream, ZipArchiveMode.Create))
                {
                    var entry = compressStream.CreateEntry(Path.GetFileName(filePath));

                    using (var zipStream = entry.Open())
                    {
                        sourceStream.CopyTo(zipStream);
                    }
                }

                RemoveOldArchives(Path.GetDirectoryName(filePath));
            }

            private void RemoveOldArchives(string directoryPath)
            {
                foreach (var file in Directory.GetFiles(directoryPath).Where(file =>
                    file.EndsWith(".zip") && File.GetCreationTime(file) < DateTime.Today - TimeSpan.FromDays(_saveCompressedFilesForDays)))
                {
                    File.Delete(file);
                }
            }
        }
    }
}
