using M4_Console_UI.Configuration;
using M4_Console_UI.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace M4_Console_UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ru-RU culture");
            FileSystemWatcher fsw = new FileSystemWatcher();

            var section = (FileSystemSettings)ConfigurationManager.GetSection("fileSystemSettings");

            FileSystemMonitoringService service = new FileSystemMonitoringService(section, new ConsoleLogger());
            service.StartWatch();

            PauseWhileEscNotEntered();

            Console.WriteLine("Change culture to en-US");
            var currCulture = new System.Globalization.CultureInfo(section.CultureInfo.Name);

            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = currCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = currCulture;

            PauseWhileEscNotEntered();

            service.EndWatch();

            Console.WriteLine("END");
        }

        private static void PauseWhileEscNotEntered()
        {
            do
            {
                do
                {

                } while (!Console.KeyAvailable);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
