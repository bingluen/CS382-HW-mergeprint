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
        private static List<string> key = new List<string>();
        private static List<Dictionary<string, string>> inputData = new List<Dictionary<string, string>>();

        static void Main(string[] args)
        {

            parseArgs(args);

            
            if (data.Length == 0 || templete.Length == 0 || output.Length == 0)
            {
                Console.WriteLine("Arguments error! try again");
            }
            else
            {
                using (StreamReader inputFile = new StreamReader(data))
                {
                    readData(inputFile);
                }
                using (StreamReader templeteFile = new StreamReader(templete))
                using (StreamWriter outputFile = new StreamWriter(output))
                {
                    mergePrint(templeteFile, outputFile);
                }
            }

 
            Console.ReadLine();
        }

        private static void mergePrint(StreamReader templeteFile, StreamWriter outputFile)
        {
            string templeteText = templeteFile.ReadToEnd();
            string printStream;
            for (int i = 0; i < inputData.Count; i++)
            {
                printStream = templeteText;
                for (int j = 0; j < key.Count; j++)
                {
                    string replacement;
                    if (inputData[i].TryGetValue(key[j], out replacement))
                    {
                        printStream = printStream.Replace("${" + key[j] + "}", replacement);
                    }
                }
                Console.WriteLine(printStream);
                Console.WriteLine('\n');
                outputFile.WriteLine(printStream);
                outputFile.WriteLine('\n');
            }
        }

        private static void readData(StreamReader inputFile)
        {
            // Get column name - key
            key.AddRange(inputFile.ReadLine().Split('\t'));

            //Get Data
            string tempStream = "";
            string[] tempRow;
            Dictionary<string, string> tempDataRow;
            while ((tempStream = inputFile.ReadLine()) != null)
            {
                tempDataRow = new Dictionary<string, string>();
                tempRow = tempStream.Split('\t');
                for (int i = 0; i < tempRow.Length; i++)
                {
                    tempDataRow.Add(key[i], tempRow[i]);
                }
                inputData.Add(tempDataRow);
            }
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
            }
        }
    }
}
