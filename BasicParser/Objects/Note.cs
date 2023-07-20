using BasicParser.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Objects
{
    [DataContract]
    class Note
    {
        [DataMember] private List<String> lines;
        [DataMember] private List<Item> items;
        [DataMember] private string order, id, startDate, endDate, client;

        public Note(string path)
        {
            lines = new List<string>();
            items = new List<Item>();

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
                
                while (i < lines.Count - 2)
                {
                    string sectorCode = lines[i].ToString().Substring(84, 4).Replace(" ", "");
                    if(sectorCode.Length == 4 && !sectorCode.Equals("----"))
                    {
                        ((Item)items[items.Count - 1]).AddSector(new Sector(sectorCode, Regex.Split(lines[i].ToString().Substring(89), @" {2,8}")[0].Replace(" _", "").Replace("_", "")));
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

        private int TurnDateToNumber(string date)
        {
            return Int32.Parse(date.Substring(0, 2)) + Int32.Parse(date.Substring(3, 2)) * 100
                    + Int32.Parse(date.Substring(6, 4)) * 10000;
        }

        public bool StartDateIsBetween(string start, string end)
        {
            return TurnDateToNumber(startDate) >= TurnDateToNumber(start) && TurnDateToNumber(startDate) <= TurnDateToNumber(end);
        }

        public bool EndDateIsBetween(string start, string end)
        {
            return TurnDateToNumber(endDate) >= TurnDateToNumber(start) && TurnDateToNumber(endDate) <= TurnDateToNumber(end);
        }

        public List<string> GetSectors()
        {
            List<string> list = new List<string>();
            foreach (Item item in items)
            {
                foreach (Sector sector in item.GetSectors())
                {
                    list.Add(sector.GetDescription());
                }
            }
            return list;
        }

        public List<Item> GetItems()
        {
            return items;
        }

        public string GetEndDate()
        {
            return endDate;
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
                Console.WriteLine("\tItem " + i++ + ":");
                item.Print();
            }
        }

        public string GetOrder()
        {
            return order;
        }
    }
}
