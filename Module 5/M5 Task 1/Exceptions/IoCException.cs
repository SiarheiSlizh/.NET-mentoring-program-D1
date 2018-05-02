using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5_Task_1.Exceptions
{
    public class IoCException : Exception
    {
        public string Message { get; }

        public IoCException(string message) : base(message)
        {
            this.Message = message;
        }
    }
}
