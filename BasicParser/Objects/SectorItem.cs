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
    class SectorItem
    {
        [DataMember] private string orderNumber, code, description, endDate;
        [DataMember] private Composition composition;
        [DataMember] private List<Sector> sectors;
        [DataMember] private int currentSectorIdx;
        [DataMember] private bool positionModified;

        public SectorItem(string orderNumber, string code, string description, string endDate,
                            Composition composition, List<Sector> sectors)
        {
            this.orderNumber = orderNumber;
            this.code = code;
            this.description = description;
            this.endDate = endDate;
            this.composition = composition;
            this.sectors = sectors;
            currentSectorIdx = 0;
            positionModified = false;
        }

        public void ChangeCurrentSector(Sector newSector)
        {
            sectors[currentSectorIdx] = newSector;
        }

        public bool GoToNextSector()
        {
            currentSectorIdx++;
            if (currentSectorIdx < sectors.Count)
                return false;
            else
                return true;
        }

        private int TurnDateToNumber(string date)
        {
            return Int32.Parse(date.Substring(0, 2)) + Int32.Parse(date.Substring(3, 2)) * 100
                    + Int32.Parse(date.Substring(6, 4)) * 10000;
        }

        public int GetEndDateNumber()
        {
            return Int32.Parse(endDate.Substring(0, 2)) + Int32.Parse(endDate.Substring(3, 2)) * 100
                    + Int32.Parse(endDate.Substring(6, 4)) * 10000;
        }

        public bool GetModified()
        {
            return positionModified;
        }

        public Sector GetSector()
        {
            return sectors[currentSectorIdx];
        }

        private void PrintComposition()
        {
            Console.WriteLine("\t\tMaterial: " + composition.GetMaterial() + ".");
            Console.WriteLine("\t\tQuantidade: " + composition.GetQuantity() + ".");
            Console.WriteLine("\t\tUnidade: " + composition.GetUnit() + ".");
            Console.WriteLine();
        }

        public void Print()
        {
            Console.WriteLine("\tItem: " + orderNumber + ".");
            Console.WriteLine("\tCódigo: " + code + ".");
            Console.WriteLine("\tDescrição: " + description + ".");
            Console.WriteLine("\tSaída: " + endDate + ".");
            Console.WriteLine("\n\tComposição:");
            PrintComposition();
            Console.WriteLine();
        }
    }
}
