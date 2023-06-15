using System;

namespace BasicParser
{
    class Parser
    {
        public void Parse()
        {
            Console.WriteLine("Parsing...");
        }

        public void PrintFirstLine(String file)
        {
            Console.WriteLine(file.Substring(0, 10));
        }
    }
}
