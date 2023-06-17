using BasicParser.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            for (int i = 12; i < lines.Count - 6; i++)
            {
                string itemCode = lines[i].ToString().Substring(3, 14).Replace(" ", "");
                string itemDescription = Regex.Split(lines[i].ToString().Substring(22, 50), @" {2,8}")[0];
                for (int offset = 1; lines[i + offset].ToString().Replace(" ", "").Length > 0; offset++)
                {
                    itemDescription += " " + lines[i + offset].ToString().Substring(22).Replace(" ", "");
                }
                string itemUnit = lines[i].ToString().Substring(90, 3).Replace(" ", "");
                string itemQuantity = lines[i].ToString().Substring(105, 11).Replace(" ", "");
                items.Add(new Item(itemCode, itemDescription, itemQuantity, itemUnit));
                while (lines[i].ToString().Replace(" ", "").Length > 0)
                    i++;
                i += 3;
                ((Item)items[items.Count - 1]).AddComposition(new Composition(lines[i].ToString().Substring(28, 14).Replace(" ", ""),
                                                                              Regex.Split(lines[i].ToString().Substring(43, 40), @" {2,8}")[0],
                                                                              lines[i].ToString().Substring(0, 10).Replace(" ", ""),
                                                                              lines[i].ToString().Substring(13, 2).Replace(" ", "")));
                
                while (i < lines.Count - 6)
                {
                    string sectorCode = lines[i].ToString().Substring(84, 4).Replace(" ", "");
                    if(sectorCode.Length == 4 && !sectorCode.Equals("----"))
                    {
                        ((Item)items[items.Count - 1]).AddSector(new Sector(sectorCode, Regex.Split(lines[i].ToString().Substring(89), @" {2,8}")[0]));
                    }
                    if (lines[i + 1].ToString().Replace(" ", "").Length == 0)
                    {
                        i += 2;
                        if(lines[i].ToString().Replace(" ", "").Length == 0)
                            i++;
                    }
                    else
                    {
                        i++;
                    }
                    if (lines[i].ToString().Substring(89, 4).Replace(" ", "").Length == 2)
                    {
                        i--;
                        break;
                    }
                }
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
            Console.WriteLine();
            int i = 1;
            foreach (Item item in items)
            {
                Console.WriteLine("Item " + i++ + ":");
                item.Print();
            }
        }
    }
}
