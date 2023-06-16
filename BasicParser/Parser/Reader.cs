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
            foreach (Note file in files)
            {
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
    }
}
