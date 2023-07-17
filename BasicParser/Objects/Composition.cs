using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicParser.Objects
{
    [DataContract]
    class Composition
    {
        [DataMember] private string code, material, quantity, unit;
        public Composition(string code, string material, string quantity, string unit)
        {
            this.code = code;
            this.material = material;
            this.quantity = quantity;
            this.unit = unit;
        }

        public string GetCode()
        {
            return code;
        }

        public string GetMaterial()
        {
            return material;
        }

        public string GetQuantity()
        {
            return quantity;
        }

        public string GetUnit()
        {
            return unit;
        }
    }
}
