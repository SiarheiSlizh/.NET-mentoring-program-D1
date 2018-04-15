using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4_Console_UI.Configuration
{
    public class FileSystemSettings : ConfigurationSection
    {
        [ConfigurationProperty("cultureInfo")]
        public CultureInfo CultureInfo
        {
            get
            {
                return (CultureInfo)base["cultureInfo"];
            }
        }

        [ConfigurationCollection(typeof(FolderForListening), AddItemName = "folder")]
        [ConfigurationProperty("foldersForListening")]
        public FolderForListeningCollection Folders
        {
            get
            {
                return (FolderForListeningCollection)this["foldersForListening"];
            }
        }

        [ConfigurationCollection(typeof(FileSystemRule), AddItemName = "rule")]
        [ConfigurationProperty("rules")]
        public FileSystemRuleCollection Rules
        {
            get
            {
                return (FileSystemRuleCollection)this["rules"];
            }
        }
    }
}
