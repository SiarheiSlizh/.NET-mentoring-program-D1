using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5_Task_1.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class ImportConstructor : Attribute
    {
        #region ctors
        public ImportConstructor()
        {
        }
        #endregion
    }
}
