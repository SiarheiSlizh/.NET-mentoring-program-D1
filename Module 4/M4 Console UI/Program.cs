using M4_Console_UI.Configuration;
using M4_Console_UI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M4_Console_UI
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcher fsw = new FileSystemWatcher();

            var section = (FileSystemSettings)ConfigurationManager.GetSection("fileSystemSettings");

            FileSystemMonitoringService service = new FileSystemMonitoringService(section, new ConsoleLogger());
            service.StartWatch();

            do
            {
                do
                {
                     
                } while (!Console.KeyAvailable);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            service.EndWatch();
        }
    }
}
