using BasicParser.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    [DataContract]
    class Item
    {
        [DataMember] private string code, description, quantity, unit;
        [DataMember] private Composition composition;
        [DataMember] private List<Sector> sectors;

        public Item(string code, string description, string quantity, string unit)
        {
            this.code = code;
            this.description = description;
            this.quantity = quantity;
            this.unit = unit;
            sectors = new List<Sector>();
        }

        public void AddSector(Sector sector)
        {
            sectors.Add(sector);
        }

        public void AddComposition(Composition composition)
        {
            this.composition = composition;
        }

        public string GetCode()
        {
            return code;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetQuantity()
        {
            return quantity;
        }

        public string GetUnit()
        {
            return unit;
        }

        public Composition GetComposition()
        {
            return composition;
        }

        public List<Sector> GetSectors()
        {
            return sectors;
        }

        public void PrintComposition()
        {
            Console.WriteLine("\t\t\tCódigo: " + composition.GetCode() + ".");
            Console.WriteLine("\t\t\tMaterial: " + composition.GetMaterial() + ".");
            Console.WriteLine("\t\t\tQuantidade: " + composition.GetQuantity() + ".");
            Console.WriteLine("\t\t\tUnidade: " + composition.GetUnit() + ".");
            Console.WriteLine();
        }

        public void PrintSectors()
        {
            foreach (Sector sector in sectors)
            {
                sector.Print();
            }
        }

        public void Print()
        {
            Console.WriteLine("\t\tCódigo: " + code + ".");
            Console.WriteLine("\t\tDescrição: " + description + ".");
            Console.WriteLine("\t\tQuantidade: " + quantity + ".");
            Console.WriteLine("\t\tUnidade: " + unit + ".");
            Console.WriteLine("\n\t\tComposição:");
            PrintComposition();
            Console.WriteLine("\t\tSetores:");
            PrintSectors();
            Console.WriteLine();
        }
    }
}
