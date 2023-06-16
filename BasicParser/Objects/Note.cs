using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    class Note
    {
        private ArrayList lines = new ArrayList();
        private ArrayList items = new ArrayList();
        private string order, id, startDate, endDate, client;

        public Note(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
        }

        public void Parse()
        {
            order = lines[4].ToString().Substring(22, 8).Replace(" ", "");
            id = lines[4].ToString().Substring(59, 8).Replace(" ", "");
            startDate = lines[4].ToString().Substring(90, 10).Replace(" ", "");
            endDate = lines[4].ToString().Substring(122, 10).Replace(" ", "");
            client = lines[6].ToString().Substring(13);

            for (int i = 12; i < lines.Count; i++)
            {
                Console.WriteLine("oi");
                string itemCode = lines[i].ToString().Substring(3, 14).Replace(" ", "");
                string itemDescription = lines[i].ToString().Substring(22, 50).Replace(" ", "");
                for (int offset = 1; lines[i + offset].ToString().Replace(" ", "").Length > 0; offset++)
                {
                    itemDescription += lines[i + offset].ToString().Substring(22).Replace(" ", "");
                }
                string itemUnit = lines[i].ToString().Substring(91, 3).Replace(" ", "");
                Console.WriteLine("Code: " + itemCode + "Description: " + itemDescription + "Unity: " + itemUnit);
                string itemQuantity = lines[i].ToString().Substring(105, 11).Replace(" ", "");
                Console.WriteLine(itemCode + " " + itemDescription + " " + itemUnit + " " + itemQuantity);
                items.Add(new Item(itemCode, itemDescription, itemQuantity, itemUnit));
                while (lines[i].ToString().Replace(" ", "").Length > 0)
                    i++;
                i++;
            }
        }

        public void Print()
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public void PrintInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Ordem: " + order + ".");
            Console.WriteLine("Pedido: " + id + ".");
            Console.WriteLine("Emissao: " + startDate + ".");
            Console.WriteLine("Saida: " + endDate + ".");
            Console.WriteLine("Cliente: " + client + ".");
            Console.WriteLine("Tam: " + lines[15].ToString().Replace(" ", "").Length + ".");
        }
    }
}
