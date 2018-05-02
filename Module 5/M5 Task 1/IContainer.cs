using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5_Task_1
{
    public interface IContainer
    {
        T CreateInstance<T>();
    }
}
