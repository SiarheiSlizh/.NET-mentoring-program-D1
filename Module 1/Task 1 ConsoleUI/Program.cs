using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1;

namespace Task_1_ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemVisitor fsv = new FileSystemVisitor();
            fsv.StartNotification += (o, s) => Console.WriteLine(s.Message);
            fsv.EndNotification += (o, s) => Console.WriteLine(s.Message);
            fsv.FileFindedNotification += (o, s) => Console.WriteLine(s.Message);
            fsv.DirectoryFindedNotification += (o, s) => Console.WriteLine(s.Message);
            fsv.FilteredFileFindedNotification += (o, s) => Console.WriteLine(s.Message);
            fsv.FilteredDirectoryFindedNotification += (o, s) => Console.WriteLine(s.Message);

            Console.WriteLine("Part1");
            try
            {
                foreach (var element in fsv.GetFilesAndFoldersSequence(@"E:\other"))
                {
                    Console.WriteLine(element);
                }
                Console.WriteLine("------------------");
                Console.WriteLine("GOOOOD");
            }
            catch (DirectoryNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
    
            Console.WriteLine();

            Console.WriteLine("Part 2");
            try
            {
                foreach (var element in fsv.GetFilesAndFoldersSequence(@"E:\other", x => x.Length > 8))
                {
                    Console.WriteLine(element);
                }
                Console.WriteLine("------------------");
                Console.WriteLine("GOOOOD");
            }
            catch (DirectoryNotFoundException exc)
            {
                Console.WriteLine(exc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            Console.WriteLine();

            //Console.WriteLine("Part 3");
            //try
            //{
            //    foreach (var element in fsv.GetFilesAndFolders(@"E:\other"))
            //    {
            //        Console.WriteLine(element);
            //    }
            //    Console.WriteLine("------------------");
            //    Console.WriteLine("GOOOOD");
            //}
            //catch (DirectoryNotFoundException exc)
            //{
            //    Console.WriteLine(exc.Message);
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc.Message);
            //}
        }
    }
}
