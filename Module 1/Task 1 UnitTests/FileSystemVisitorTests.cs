using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1;

namespace Task_1_UnitTests
{
    [TestFixture]
    public class FileSystemVisitorTests
    {
        private FileSystemVisitor fsv;
        private Predicate<string>[] conditions;

        [SetUp]
        public void Initialize()
        {
            fsv = new FileSystemVisitor();
            conditions = new Predicate<string>[2];

            conditions[0] = null;
            conditions[1] = x => x.Length > 8;
        }

        [TestCase(@"E:\others")]
        public void GetAllFilesAndFolders_DirectoryNotFoundException(string rootPath)
        {
            Assert.Throws<DirectoryNotFoundException>(delegate () {
                foreach (var el in fsv.GetFilesAndFoldersSequence(rootPath, conditions[0]))
                { }
            });
        }

        [TestCase(@"E:\other\photos\home", ExpectedResult = "home cats.jpg dogs.jpg morecatsanddogs ura.jpg ")]
        [TestCase(@"E:\other\docs\univer", ExpectedResult = "univer Lab1.docx Lab2.doc Lab3.doc Lab4.docx ")]
        public string GetAllFilesAndFolders_TestCases(string rootPath)
        {
            string result = "";
            foreach (var el in fsv.GetFilesAndFoldersSequence(rootPath, conditions[0]))
            {
                result += el + " ";
            }
            return result;
        }

        [TestCase(@"E:\other\photos\home", ExpectedResult = "morecatsanddogs ")]
        [TestCase(@"E:\other\docs\univer", ExpectedResult = "Lab1.docx Lab4.docx ")]
        [TestCase(@"E:\other", ExpectedResult = "Lab1.docx Lab4.docx aspnet-web-api-poster.pdf webapi.pptx SOLID.pdf niochem.png prost.jpg morecatsanddogs friends.jpg sheep.jpg ")]
        public string GetAllFilesAndFoldersByPredicate_TestCases(string rootPath)
        {
            string result = "";
            foreach (var el in fsv.GetFilesAndFoldersSequence(rootPath, conditions[1]))
            {
                result += el + " ";
            }
            return result;
        }
    }
}