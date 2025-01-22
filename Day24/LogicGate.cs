using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{
    public class LogicGate : IWireValue
    {
        public string resultID = "";

        public IWireValue? left;
        public IWireValue? right;

        public LogicType mode;

        public LogicGate(string resultID, IWireValue? left = null, 
            IWireValue? right = null, LogicType mode = LogicType.OR)
        {
            // able to make empty and fill later
            this.resultID = resultID;
            this.left = left;
            this.right = right;
            this.mode = mode;
        }

        public int GetOutput()
        {
            if (left == null || right == null)
            {
                Console.WriteLine("ONE CONDITION NOT INITIALIZED " + resultID);
                return -1;
            }

            switch (mode)
            {
                case LogicType.OR:
                    if (left.GetOutput() == 1 || right.GetOutput() == 1) return 1;
                    else return 0;
                case LogicType.AND:
                    if (left.GetOutput() == 1 && right.GetOutput() == 1) return 1;
                    else return 0;
                case LogicType.XOR:
                    if ((left.GetOutput() == 1 && right.GetOutput() == 0) ||
                        (left.GetOutput() == 0 && right.GetOutput() == 1))
                        return 1;
                    else return 0;
            }

            Console.WriteLine("SOMETHING WENT WRONG IN LOGIC GATE " + resultID);
            return -1; // something's wrong
        }
    }
}
