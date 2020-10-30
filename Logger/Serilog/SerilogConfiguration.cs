using Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.Models
{
    public class SerilogConfiguration: IConfiguration
    {
        public string Path { get; set; }
        public bool RollbackByDays { get; set; }
        public long FileSizeLimit { get; set; }
        public int FilesCountLimit { get; set; }
        public string OutputTemplate { get; set; }
        public bool Compress { get; set; }
        public int SaveCompressedFilesForDays { get; set; }
        public LogLevel MinimumLogLevel { get; set; }
        public bool SharedFile { get; set; }

        public SerilogConfiguration(
            string path = "Logs/log.txt",
            bool rollbackByDays = false,
            long fileSizeLimit = 104857600,
            int filesCountLimit = 1,
            string outputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] [{ThreadId}] {Message:lj}{NewLine}{Exception}",
            bool compress = false,
            int saveCompressedFilesForDays = 180,
            LogLevel minimumLogLevel = LogLevel.Info,
            bool sharedFile = false)
        {
            this.Path = path;
            this.RollbackByDays = rollbackByDays;
            this.FileSizeLimit = fileSizeLimit;
            this.FilesCountLimit = filesCountLimit;
            this.OutputTemplate = outputTemplate;
            this.Compress = compress;
            this.SaveCompressedFilesForDays = saveCompressedFilesForDays;
            this.MinimumLogLevel = minimumLogLevel;
            this.SharedFile = sharedFile;
        }

        public override bool Equals(object obj)
        {
            return obj is SerilogConfiguration configuration &&
                   Path == configuration.Path &&
                   RollbackByDays == configuration.RollbackByDays &&
                   FileSizeLimit == configuration.FileSizeLimit &&
                   FilesCountLimit == configuration.FilesCountLimit &&
                   OutputTemplate == configuration.OutputTemplate &&
                   Compress == configuration.Compress &&
                   SaveCompressedFilesForDays == configuration.SaveCompressedFilesForDays &&
                   MinimumLogLevel == configuration.MinimumLogLevel &&
                   SharedFile == configuration.SharedFile;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Path);
            hash.Add(RollbackByDays);
            hash.Add(FileSizeLimit);
            hash.Add(FilesCountLimit);
            hash.Add(OutputTemplate);
            hash.Add(Compress);
            hash.Add(SaveCompressedFilesForDays);
            hash.Add(MinimumLogLevel);
            hash.Add(SharedFile);
            return hash.ToHashCode();
        }
    }
}
