using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.EventsArgs
{
    public class FileFindedEventArgs : EventArgs
    {
        public string Message { get; }

        public FileFindedEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
