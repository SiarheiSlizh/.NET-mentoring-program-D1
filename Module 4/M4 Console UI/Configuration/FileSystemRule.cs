using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M4_Console_UI.Configuration
{
    public class FileSystemRule : ConfigurationElement
    {
        [ConfigurationProperty("filter", IsKey = true)]
        public string Filter
        {
            get
            {
                return (string)base["filter"];
            }
        }
        
        [ConfigurationProperty("path")]
        public string Path
        {
            get
            {
                return (string)base["path"];
            }
        }

        [ConfigurationProperty("addSerial")]
        public bool AddSerial
        {
            get
            {
                return (bool)base["addSerial"];
            }
        }

        [ConfigurationProperty("addDateTime")]
        public bool AddDateTime
        {
            get
            {
                return (bool)base["addDateTime"];
            }
        }
    }
}
