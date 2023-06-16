using BasicParser.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    class Item
    {
        private string code, description, quantity, unit;
        private Composition composition;
        private List<Sector> sectors;

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
    }
}
