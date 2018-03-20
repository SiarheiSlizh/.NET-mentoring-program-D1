using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.EventsArgs
{
    public class FilteredFileFindedEventArgs : EventArgs
    {
        public string Message { get; }

        public FilteredFileFindedEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
