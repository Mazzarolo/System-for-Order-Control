using System;
using System.IO;

namespace BasicParser
{
    class Reader
    {
        private List<String> files;

        public Reader() 
        {
            Console.WriteLine("New Parser created");
            files = new List<String>();
        }

        public void LoadFiles()
        {
            string folderPath = "Files/";
            Console.WriteLine("Loading files from: " + folderPath);
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.txt"))
            {
                files.Add(File.ReadAllText(file));
            }
        }

        public void PrintFiles()
        {
            foreach (string file in files)
            {
                Console.WriteLine(file);
            }
        }

        public void ParseFiles()
        {
            Parser parser = new Parser();
            foreach (string file in files)
            {
                parser.PrintFirstLine(file);
            }
        }
    }
}
