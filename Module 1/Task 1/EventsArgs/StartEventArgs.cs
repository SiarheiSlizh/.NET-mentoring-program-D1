using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.EventsArgs
{
    public class StartEventArgs : EventArgs
    {
        public string Message { get; }

        public StartEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
