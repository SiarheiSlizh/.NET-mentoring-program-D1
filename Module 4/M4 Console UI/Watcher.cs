using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI
{
    public class Watcher
    {
        private readonly string path;
        private readonly string filter;

        public Watcher(string path, string filter)
        {
            this.path = path;
            this.filter = filter;
        }
    }
}
