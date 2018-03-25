using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2_Task_1_ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;
            do
            {
                Console.WriteLine("\nEnter string");
                str = Console.ReadLine();
                try
                {
                    Console.WriteLine(str.Trim()[0]);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The string is empty. Try again");
                }
                Console.WriteLine("Would you like to continue?\nYes - Press any key\nNo - press escape");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.WriteLine("Finish");
        }
    }
}
