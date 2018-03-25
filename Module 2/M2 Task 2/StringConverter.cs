using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace M2_Task_2
{
    public static class StringConverter
    {
        #region props
        public static Regex intRegex = new Regex(@"^([\d]|\+[\d]|-[\d])\d+$");
        #endregion

        #region API
        public static int ToInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException("String is empty");
            }
            
            if (!intRegex.IsMatch(str))
            {
                throw new FormatException("Incorrect format");
            }
            
            int sign = str[0] == '-' ? -1 : 1;
            int start = sign == 1 && str[0] != '+' ? 0 : 1;

            try
            {
                return GetResult(start, str) * sign;
            }
            catch (OverflowException)
            {
                throw;
            }
        }

        public static bool TryToInt(this string str, out int number)
        {
            number = 0;

            if (string.IsNullOrWhiteSpace(str) || !intRegex.IsMatch(str))
            {
                return false;
            }

            int sign = str[0] == '-' ? -1 : 1;
            int start = sign == 1 && str[0] != '+' ? 0 : 1;

            try
            {
                number = GetResult(start, str) * sign;
            }
            catch (OverflowException)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region private methods
        static private int GetResult(int start, string str)
        {
            int result = 0;

            for (int i = start; i < str.Length; i++)
            {
                result = result * 10 + (str[i] - '0');
            }
            return result;
        }
        #endregion
    }
}
