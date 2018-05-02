using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2_Task_2;

namespace M2_Task_2_UnitTests
{
    [TestFixture]
    public class StringConverterTests
    {
        [TestCase("23423", ExpectedResult = 23423)]
        [TestCase("+23423", ExpectedResult = 23423)]
        [TestCase("-23423", ExpectedResult = -23423)]
        [TestCase("000000000000000", ExpectedResult = 0)]
        public int ToInt_TestCases(string str)
        {
            return str.ToInt();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("         ")]
        public void ToInt_ArgumentNullException(string str)
        {
            Assert.Throws<ArgumentNullException>(() => str.ToInt());
        }

        [TestCase("+++++12")]
        [TestCase("12314.123")]
        [TestCase("12,32")]
        [TestCase("123 213")]
        [TestCase("erwfewrf")]
        public void ToInt_FormatException(string str)
        {
            Assert.Throws<FormatException>(() => str.ToInt());
        }

        [TestCase("23423", ExpectedResult = 23423)]
        [TestCase("+23423", ExpectedResult = 23423)]
        [TestCase("-23423", ExpectedResult = -23423)]
        [TestCase("000000000000000", ExpectedResult = 0)]
        public int TryToInt_TestCases(string str)
        {
            int number;
            str.TryToInt(out number);
            return number;
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("         ", ExpectedResult = 0)]
        [TestCase("+++++12", ExpectedResult = 0)]
        [TestCase("12314.123", ExpectedResult = 0)]
        [TestCase("12,32", ExpectedResult = 0)]
        [TestCase("123 213", ExpectedResult = 0)]
        [TestCase("erwfewrf", ExpectedResult = 0)]
        public int TryToInt_null(string str)
        {
            int number;
            str.TryToInt(out number);
            return number;
        }
    }
}
