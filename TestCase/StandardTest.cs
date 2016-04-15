using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MergeDataAndDoc
{
    [TestFixture]
    public class StandardTest
    {
        [Test]
        public void standardTest()
        {
            string testString = "Test Column1\tColumn2\n1\t2";
            string testTemplete = "{Test Column1} is {Test Column2}";
            string outputResult = "1 is 2";
            StringReader testReader = new StringReader(testString);
            StringReader testTempleteReader = new StringReader(testTemplete);
            StringWriter testWriter = new StringWriter();
            Program.readData(testReader);
            Program.mergePrint(testTempleteReader, testWriter);
            Assert.That(outputResult.ToString(), Is.EqualTo(outputResult));
        }
    }
}
