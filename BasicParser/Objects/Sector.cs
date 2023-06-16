using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicParser.Objects
{
    internal class Sector
    {
        private string code, description;

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
    }
}
