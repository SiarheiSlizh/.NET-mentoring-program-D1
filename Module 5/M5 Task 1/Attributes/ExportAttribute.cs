using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5_Task_1.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
    public class ExportAttribute : Attribute
    {
        #region props
        public Type ContractType { get; }
        #endregion

        #region ctors
        public ExportAttribute()
            : this((Type)null)
        {
        }

        public ExportAttribute(Type contractType)
        {
            this.ContractType = contractType;
        }
        #endregion
    }
}
