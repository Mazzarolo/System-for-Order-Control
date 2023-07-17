using System;
using System.IO;
using Objects;

namespace Parser
{
    class Reader
    {
        private List<Note> files;

        public Reader() 
        {
            files = new List<Note>();
        }

        public void LoadFiles()
        {
            string folderPath = "Files/";

            foreach (string path in Directory.EnumerateFiles(folderPath, "*.txt"))
            {
                files.Add(new Note(path));
            }
        }

        public void PrintFiles()
        {
            foreach (Note file in files)
            {
                file.Print();
            }
        }

        public void PrintInfo()
        {
            int i = 1;
            foreach (Note file in files)
            {
                Console.WriteLine("Nota {0}:", i++);
                file.PrintInfo();
            }
        }

        public void ParseFiles()
        {
            foreach (Note file in files)
            {
                file.Parse();
            }
        }

        public List<Note> getFiles()
        {
            return files;
        }
    }
}
