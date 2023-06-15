using System;
using System.IO;
using System.Collections;
using System.Text;

namespace Tools
{
    class FileReader
    {
        ArrayList lines;
        public void Read(string path)
        {
            lines = new ArrayList();
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
        }

        public void Print()
        {
            string ordem = lines[4].ToString().Substring(22, 8).Replace(" ", "");
            string pedido = lines[4].ToString().Substring(59, 8).Replace(" ", "");
            string emissao = lines[4].ToString().Substring(90, 10).Replace(" ", "");
            string saida = lines[4].ToString().Substring(122, 10).Replace(" ", "");
            foreach (string line in lines)
            {
                //Console.WriteLine(line);
            }
            Console.WriteLine("Ordem: " + ordem + ".");
            Console.WriteLine("Pedido: " + pedido + ".");
            Console.WriteLine("Emissao: " + emissao + ".");
            Console.WriteLine("Saida: " + saida + ".");
        }
    }
}