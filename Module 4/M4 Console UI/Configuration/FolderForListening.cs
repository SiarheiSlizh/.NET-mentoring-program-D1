using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI.Configuration
{
    public class FolderForListening : ConfigurationElement
    {
        [ConfigurationProperty("path", IsKey = true)]
        public string Path
        {
            get
            {
                return (string)base["path"];
            }
        }
    }
}
