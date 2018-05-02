using M5_Task_1.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M5_UnitTests.Examples
{
    public class CustomerBLL
    {
        private readonly ICustomerDAL dal;
        private readonly Logger logger;

        [ImportConstructor]
        public CustomerBLL(ICustomerDAL dal, Logger logger)
        {
            this.dal = dal;
            this.logger = logger;
        }
    }

    public class CustomerBLL2
    {
        [Import]
        public ICustomerDAL CustomerDAL { get; set; }
        [Import]
        public Logger logger { get; set; }
    }
}
