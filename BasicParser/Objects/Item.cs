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
            Console.WriteLine("Código: " + composition.GetCode() + ".");
            Console.WriteLine("Material: " + composition.GetMaterial() + ".");
            Console.WriteLine("Quantidade: " + composition.GetQuantity() + ".");
            Console.WriteLine("Unidade: " + composition.GetUnit() + ".");
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
            Console.WriteLine("Código: " + code + ".");
            Console.WriteLine("Descrição: " + description + ".");
            Console.WriteLine("Quantidade: " + quantity + ".");
            Console.WriteLine("Unidade: " + unit + ".");
            Console.WriteLine("Composição:");
            PrintComposition();
            Console.WriteLine("Setores:");
            PrintSectors();
            Console.WriteLine();
        }
    }
}
