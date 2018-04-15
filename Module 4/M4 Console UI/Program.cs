using M4_Console_UI.Configuration;
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

            Console.WriteLine(section.CultureInfo.Name);

            foreach(FileSystemRule el in section.Rules)
            {
                Console.WriteLine(el.Filter + "   " + el.Path + "   " + el.AddSerial + "   " + el.AddDateTime);
            }

            foreach(FolderForListening el in section.Folders)
            {
                Console.Write(el.Path);
            }
        }
    }
}
