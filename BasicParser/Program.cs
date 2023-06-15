using BasicParser;
using System;

namespace Application
{
    class Application
    {
        static void Main(string[] args)
        {
            Reader reader = new Reader();
            reader.LoadFiles();
            reader.ParseFiles();
        }
    }
}