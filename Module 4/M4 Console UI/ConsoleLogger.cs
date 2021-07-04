using M4_Console_UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string message) => Console.WriteLine(message);

        public void Error(string message) => Console.WriteLine(message);

        public void Fatal(string message) => Console.WriteLine(message);

        public void Info(string message) => Console.WriteLine(message);

        public void Trace(string message) => Console.WriteLine(message);

        public void Warn(string message) => Console.WriteLine(message);
    }
}
