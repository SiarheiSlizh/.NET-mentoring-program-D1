using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5_Task_1.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ImportAttribute : Attribute
    {
        #region props
        public Type ContractType { get; }
        #endregion

        #region ctors
        public ImportAttribute()
            : this((Type)null)
        {
        }

        public ImportAttribute(Type contractType)
        {
            this.ContractType = contractType;
        }
        #endregion
    }
}
