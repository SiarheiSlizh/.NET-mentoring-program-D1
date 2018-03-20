using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.EventsArgs
{
    public class EndEventArgs : EventArgs
    {
        public string Message { get; }

        public EndEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
