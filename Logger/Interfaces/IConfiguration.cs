using Logger.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Interfaces
{
    public interface IConfiguration
    {
        string Path { get; set; }
        bool RollbackByDays { get; set; }
        long FileSizeLimit { get; set; }
        int FilesCountLimit { get; set; }
        string OutputTemplate { get; set; }
        bool Compress { get; set; }
        int SaveCompressedFilesForDays { get; set; }
        LogLevel MinimumLogLevel { get; set; }
        bool SharedFile { get; set; }
    }
}
