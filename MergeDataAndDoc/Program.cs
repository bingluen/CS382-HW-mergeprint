using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace MergeDataAndDoc
{
    class Program
    {
        private static string data = "", templete = "", output = "";
        static void Main(string[] args)
        {

            parseArgs(args);

            if (data.Length == 0 || templete.Length == 0 || output.Length == 0)
            {
                Console.WriteLine("Arguments error! try ");
            }

            using (StreamReader inputFile = new StreamReader(data))
            using (StreamReader templeteFile = new StreamReader(templete))
            using(StreamWriter outputFile = new StreamWriter(output))
            {
                string line; //test
                while((line = inputFile.ReadLine()) != null)
                {
                    string outputLine = "***" + line;
                    Console.WriteLine("Write line: " + outputLine);
                    outputFile.WriteLine(outputLine);
                }
            }
            Console.ReadLine();
        }
        static void parseArgs(string[] args)
        {
            Regex fileName = new Regex("[a-zA-Z]+[.]txt");
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-i" && fileName.IsMatch(args[i + 1]))
                {
                    data = args[i + 1];
                }
                else if (args[i] == "-t" && fileName.IsMatch(args[i + 1]))
                {
                    templete = args[i + 1];
                }
                else if (args[i] == "-r" && fileName.IsMatch(args[i + 1]))
                {
                    output = args[i + 1];
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
