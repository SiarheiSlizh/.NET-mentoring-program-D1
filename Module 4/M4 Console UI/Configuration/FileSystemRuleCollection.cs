using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI.Configuration
{
    public class FileSystemRuleCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileSystemRule();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileSystemRule)element).Filter;
        }
    }
}
