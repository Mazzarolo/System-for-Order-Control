using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicParser.Objects
{
    [DataContract]
    class Sector
    {
        [DataMember] private string code, description;

        public Sector(string code, string description)
        {
            this.code = code;
            this.description = description;
        }

        public string GetCode()
        {
            return code;
        }

        public string GetDescription()
        {
            return description;
        }

        public void Print()
        {
            Console.WriteLine("Sector: {0} - {1}.", code, description);
        }
    }
}
