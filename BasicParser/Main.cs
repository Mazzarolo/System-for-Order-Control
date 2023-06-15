using System;
using Tools;

namespace Main
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            FileReader fr = new FileReader();
            fr.Read("Data/18691.txt");
            fr.Print();
        }
    }
}