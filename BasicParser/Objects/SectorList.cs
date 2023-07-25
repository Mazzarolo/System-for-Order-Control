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

        public void AddItem(SectorItem newItem, int idx)
        {
            newItem.SetModified(true);
            items.Insert(idx, newItem);
        }

        public SectorItem RemoveItem(int idx)
        {
            SectorItem item = items[idx];
            items.RemoveAt(idx);
            item.SetModified(false);
            return item;
        }

        public Sector GetSector()
        {
            return sector;
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public int Count()
        {
            return items.Count();
        }

        public void Print()
        {
            Console.WriteLine("\nSetor: {0}\n", sector.GetDescription());

            int i = 0;
            foreach (SectorItem item in items)
            {
                Console.WriteLine("\t------------------------------------------------------------");
                Console.WriteLine("\t|                                                          |");
                item.Print(i);
                i++;
            }
            Console.WriteLine("\t------------------------------------------------------------\n");

            Console.WriteLine("\n---------------------------------------------------------------------------------\n");
        }
    }
}
