using BasicParser.Objects;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [DataContract]
    class DataBase
    {
        [DataMember] private List<Note> notes;

        [DataMember] private SectorManager sectorManager;

        public DataBase()
        {
            notes = new List<Note>();

            sectorManager = new SectorManager();
            sectorManager.Print();
        }

        public void AddNotes(List<Note> newNotes)
        {
            foreach (Note newNote in newNotes)
            {
                bool add = true;
                foreach (Note note in notes)
                {
                    if(newNote.GetOrder() == note.GetOrder())
                    {
                        add = false;
                    }
                }
                if(add)
                {
                    notes.Add(newNote);
                    int i = 0;
                    foreach (Item item in newNote.GetItems())
                    {
                        string orderNumber = newNote.GetOrder() + "/" + i;
                        SectorItem sectorItem = new SectorItem(orderNumber, item.GetCode(), item.GetDescription(), newNote.GetEndDate(),
                                                                item.GetComposition(), item.GetSectors());
                        sectorManager.AddItem(sectorItem);
                    }
                }
            }
        }

        public void PrintItemsPerSector()
        {
            if (sectorManager != null)
                sectorManager.Print();
            else
                Console.WriteLine("empty");
        }

        public void PrintOrders()
        {
            int i = 1;
            foreach (Note note in notes)
            {
                Console.WriteLine("Ordem {0}:", i++);
                note.PrintInfo();
            }
        }

        public List<string> getSectors()
        {
            List<string> uniqueSectors = new List<string>();
            foreach (Note note in notes)
            {
                foreach (string sector in note.GetSectors())
                {
                    uniqueSectors.Add(sector);
                }
            }
            return uniqueSectors.Distinct().ToList();
        }

        public bool PrintOrders(List<string> startDates, List<string> endDates, List<string> sectors)
        {
            int i = 1;
            Console.WriteLine("\n\n\n\n--------------------------------------------------------------------------\n");
            foreach (Note note in notes)
            {
                bool startOk = false, endOk = false, sectorsOk = false;
                if(startDates.Count() == 0)
                    startOk = true;
                else
                    startOk = note.StartDateIsBetween(startDates[0], startDates[1]);
                if(endDates.Count() == 0)
                    endOk = true;
                else
                    endOk = note.EndDateIsBetween(endDates[0], endDates[1]);
                if(sectors.Count == 0)
                    sectorsOk = true;
                else
                {
                    foreach (string uniqueSector in note.GetSectors())
                    {
                        foreach (string sector in sectors)
                        {
                            if (uniqueSector.Equals(sector))
                            {
                                sectorsOk = true;
                            }
                        }
                    }
                }
                if(startOk && endOk && sectorsOk)
                {
                    Console.WriteLine("Ordem {0}:", i++);
                    note.PrintInfo();
                }
            }

            if (i == 1)
                return false;
            else
                return true;
        }
    }
}
