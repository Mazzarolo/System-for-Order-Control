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
    class SectorList
    {
        [DataMember] private Sector sector;
        [DataMember] private List<SectorItem> items;

        public SectorList(Sector sector)
        {
            this.sector = sector;
            items = new List<SectorItem>();
        }

        public void AddItem(SectorItem newItem)
        {
            int index = 0;
            foreach (SectorItem item in items)
            {
                if(item.GetEndDateNumber() >= newItem.GetEndDateNumber() && !item.GetModified())
                {
                    items.Insert(index, newItem);
                    return;
                }

                index++;
            }
            items.Add(newItem);
        }

        public Sector GetSector()
        {
            return sector;
        }

        public void Print()
        {
            Console.WriteLine("\nSetor: {0}\n", sector.GetDescription());

            foreach (SectorItem item in items)
            {
                Console.WriteLine("\n\t------------------------------------------------------------\n");
                item.Print();
                Console.WriteLine("\t------------------------------------------------------------");
            }

            Console.WriteLine("\n---------------------------------------------------------------------------------\n");
        }
    }
}
