using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI.Configuration
{
    public class FolderForListeningCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderForListening();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderForListening)element).Path;
        }
    }
}
