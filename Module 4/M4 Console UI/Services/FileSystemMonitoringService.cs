using M4_Console_UI.Configuration;
using M4_Console_UI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI.Services
{
    public class FileSystemMonitoringService
    {
        private readonly FileSystemSettings settings;
        private readonly ILogger logger;
        private readonly FileSystemWatcher fsw = new FileSystemWatcher();

        public FileSystemMonitoringService(FileSystemSettings settings, ILogger logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        private void ValidateSettings(FileSystemSettings settings)
        {

        }
    }
}
