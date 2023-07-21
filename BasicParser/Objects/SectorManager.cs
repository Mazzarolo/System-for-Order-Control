using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    [DataContract]
    class SectorManager
    {
        [DataMember] private List<SectorList> sectors;

        public SectorManager()
        {
            sectors = new List<SectorList>();
        }

        public void AddItem(SectorItem item)
        {
            foreach (SectorList sector in sectors)
            {
                if(sector.GetSector().GetDescription() == item.GetSector().GetDescription())
                {
                    sector.AddItem(item);
                    return;
                }
            }
            sectors.Add(new SectorList(item.GetSector()));
            sectors[^1].AddItem(item);
        }

        public void Print()
        {
            Console.WriteLine("\n\n\n\n\n\n---------------------------------------------------------------------------------\n");
            Console.WriteLine("\t\t\tCONSULTA DE ITENS POR SETOR");
            Console.WriteLine("\n---------------------------------------------------------------------------------\n");
            foreach (SectorList sector in sectors)
            {
                if(!sector.IsEmpty())
                    sector.Print();
            }
        }
    }
}
