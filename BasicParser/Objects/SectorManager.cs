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

        public void AdvanceItem(int sectorIdx, int itemIdx)
        {
            SectorItem item = sectors[sectorIdx].RemoveItem(itemIdx);
            if (sectors[sectorIdx].IsEmpty())
                sectors.RemoveAt(sectorIdx);
            bool end = item.GoToNextSector();
            if (end)
                return;
            AddItem(item);
        }

        public void MoveItemInSector(int sectorIdx, int itemIdx, int pos)
        {
            SectorItem item = sectors[sectorIdx].RemoveItem(itemIdx);
            if (pos >= sectors[sectorIdx].Count())
                pos = sectors[sectorIdx].Count();
            sectors[sectorIdx].AddItem(item, pos);
        }

        public List<string> GetSectors()
        {
            List<string> result = new List<string>();

            foreach(SectorList sector in sectors)
                result.Add(sector.GetSector().GetDescription());

            return result;
        }

        public void PrintSector(int idx)
        {
            sectors[idx].Print();
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
