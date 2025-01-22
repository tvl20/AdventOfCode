using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    internal class WireConnection : IWireValue
    {
        public string id;
        public int value;

        public WireConnection(string id, int value)
        {
            this.id = id;
            this.value = value;
        }

        public int GetOutput()
        {
            return value;
        }
    }
}
