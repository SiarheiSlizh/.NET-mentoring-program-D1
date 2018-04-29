using M5_Task_1;
using M5_Task_1.Exceptions;
using M5_UnitTests.Examples;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace M5_UnitTests
{
    [TestFixture]
    public class ContainerTests
    {
        private Container container;

        [SetUp]
        public void MefInit()
        {
            container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());
        }

        [Test]
        public void CreateInstance_Tests()
        {
            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(CustomerBLL2));

            var logger = container.CreateInstance<Logger>();
            var custBll = container.CreateInstance<CustomerBLL>();
            var custBll2 = container.CreateInstance<CustomerBLL2>();

            var logger2 = (Logger)container.CreateInstance(typeof(Logger));
            var custBl = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var custBl2 = (CustomerBLL2)container.CreateInstance(typeof(CustomerBLL2));

            Assert.IsNotNull(logger);
            Assert.IsNotNull(custBll);
            Assert.IsNotNull(custBll2);
            Assert.IsNotNull(logger2);
            Assert.IsNotNull(custBl);
            Assert.IsNotNull(custBl2);

            Assert.IsNotNull(custBll2.logger);
            Assert.IsNotNull(custBll2.CustomerDAL);
        }

        [Test]
        public void CreateInstance_IoCException()
        {
            Assert.Throws<IoCException>(() => container.CreateInstance<CustomerBLL>());
        }
    }
}
